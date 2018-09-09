using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace My.Media
{
    public class FormatConverter
    {
        // 构造函数
        public FormatConverter()
        {
        }

        //FFmpeg配置信息
        private string ffmpegpath = "/FFmpeg/ffmpeg.exe";//FFmpeg的服务器路径
        private string imgsize = "400*300";     //视频截图大小
        private string videosize = "480*360"; //视频大小

        #region 也可将信息添加到配置文件中
        //public static string ffmpegpath = ConfigurationManager.AppSettings["ffmpeg"];
        //public static string imgsize = ConfigurationManager.AppSettings["imgsize"];
        //public static string videosize = ConfigurationManager.AppSettings["videoize"];
        #endregion


        /// <summary>
        /// 视频路径
        /// </summary>
        public string DestVideo { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string DestImage { get; set; }

        /// <summary>
        /// 视频长度
        /// </summary>
        public int VideoLength { get; set; }

        /// <summary>
        /// 返回枚举类型的描述信息
        /// </summary>
        private string GetDiscription(System.Enum myEnum)
        {

            System.Reflection.FieldInfo fieldInfo = myEnum.GetType().GetField(myEnum.ToString());
            object[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0) {
                DescriptionAttribute desc = attrs[0] as DescriptionAttribute;
                if (desc != null) {
                    return desc.Description.ToLower();
                }
            }
            return myEnum.ToString();
        }

        //将GetDesCription定义为扩展方法,需.net3.5
        //public static string Description(this Enum myEnum)
        //{
        //    return GetDiscription(myEnum);
        //}

        #region 使用FFmpeg进行格式转换

        /// <summary>
        /// 运行格式转换
        /// </summary>
        /// <param name="sourceFile">要转换文件绝对路径</param>
        /// <param name="destPath">转换结果存储的相对路径</param>
        /// <param name="videoType">要转换成的文件类型</param>
        /// <param name="createImage">是否生成截图</param>
        /// <returns>
        /// 执行成功返回空，否则返回错误信息
        /// </returns>
        public string ConvertVideo(string sourceFile, string destPath, string uniqueName, VideoType videoType, bool createImage, bool getDuration)
        {
            //取得ffmpeg.exe的物理路径
            string ffmpeg = HttpContext.Current.Server.MapPath(ffmpegpath);
            if (!File.Exists(ffmpeg)) {
                return "找不到格式转换程序！";
            }
            if (!File.Exists(sourceFile)) {
                return "找不到源文件！";
            }
            //string uniquename = FileHelper.GetUniqueFileName();
            string filename = uniqueName + GetDiscription(videoType);
            string destFile = HttpContext.Current.Server.MapPath(destPath + filename);
            //if (Path.GetExtension(sourceFile).ToLower() != GetDiscription(videotype).ToLower())
            //{
            System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            /*ffmpeg参数说明
             * -i 1.avi   输入文件
             * -ab/-ac <比特率> 设定声音比特率，前面-ac设为立体声时要以一半比特率来设置，比如192kbps的就设成96，转换 
                均默认比特率都较小，要听到较高品质声音的话建议设到160kbps（80）以上
             * -ar <采样率> 设定声音采样率，PSP只认24000
             * -b <比特率> 指定压缩比特率，似乎ffmpeg是自动VBR的，指定了就大概是平均比特率，比如768，1500这样的   --加了以后转换不正常
             * -r 29.97 桢速率（可以改，确认非标准桢率会导致音画不同步，所以只能设定为15或者29.97）
             * s 320x240 指定分辨率
             * 最后的路径为目标文件
             */
            FilestartInfo.Arguments = " -i " + sourceFile + " -ab 80 -ar 22050 -r 29.97 -s " + videosize + " " + destFile;
            //FilestartInfo.Arguments = "-y -i " + sourceFile + " -s 320x240 -vcodec h264 -qscale 4  -ar 24000 -f psp -muxvb 768 " + destFile;
            try {
                //转换
                System.Diagnostics.Process.Start(FilestartInfo);
                DestVideo = destPath + filename;
            } catch {
                return "格式转换失败！";
            }
            //}
            //格式不需要转换则直接复制文件到目录
            //else
            //{
            //    File.Copy(sourceFile, destFile,true);
            //    destVideo = destPath + filename;
            //}
            //提取视频长度
            if (getDuration) {
                VideoLength = Media.GetVideoLength(ffmpeg, sourceFile);
            }
            //提取图片
            if (createImage) {
                //定义进程
                ProcessStartInfo ImgstartInfo = new ProcessStartInfo(ffmpeg);

                //截大图
                string imgpath = destPath + uniqueName + ".jpg";// FileHelper.GetUniqueFileName(".jpg");
                ConvertImage(sourceFile, imgpath, imgsize, ImgstartInfo);

                //截小图
                imgpath = destPath + uniqueName + "_thumb.jpg";
                DestImage = ConvertImage(sourceFile, imgpath, "80*80", ImgstartInfo);

            }
            return "";
        }

        /// <summary>
        /// convert image
        /// </summary>
        private string ConvertImage(string sourceFile, string imgpath, string imgsize, ProcessStartInfo ImgstartInfo)
        {
            ImgstartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            /*参数设置
             * -y（覆盖输出文件，即如果生成的文件（flv_img）已经存在的话，不经提示就覆盖掉了）
             * -i 1.avi 输入文件
             * -f image2 指定输出格式
             * -ss 8 后跟的单位为秒，从指定时间点开始转换任务
             * -vframes
             * -s 指定分辨率
             */
            //duration: 00:00:00.00
            int seconds = VideoLength;
            int ss = seconds > 5 ? 5 : seconds - 1;
            ImgstartInfo.Arguments = " -i " + sourceFile + " -y -f image2 -ss " + ss.ToString() + " -vframes 1 -s " + imgsize + " "
                + HttpContext.Current.Server.MapPath(imgpath);
            try {
                System.Diagnostics.Process.Start(ImgstartInfo);
                return imgpath;
            } catch {
                return "";
            }
        }

        #endregion
    }

    //文件类型
    public enum VideoType
    {
        [Description(".avi")]
        AVI,
        [Description(".mov")]
        MOV,
        [Description(".mpg")]
        MPG,
        [Description(".mp4")]
        MP4,
        [Description(".flv")]
        FLV
    }

}