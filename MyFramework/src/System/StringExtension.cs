using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ComponentModel;

namespace System
{
    public static class StringExtension
    {
        public static string AlignRight(this string value, int length)
        {
            string result = value + new string(' ', length - Encoding.Default.GetBytes(value).Length);
            return result;
        }

        public static byte[] ToBytes(this string value, char separator, bool isHex)
        {
            List<byte> bytes = new List<byte>();
            string[] items = value.Trim().Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++) {
                bytes.Add(isHex ? byte.Parse(items[i], NumberStyles.HexNumber) : byte.Parse(items[i]));
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// 返回字符串, 其中的不可打印字符使用转义字符表示, "\"符号用"\\"表示.
        /// </summary>
        public static string ToVisualString(this string s)
        {
            string result = "";
            for (int i = 0; i < s.Length; i++) {
                if (char.IsControl(s[i])) {
                    switch (s[i]) {
                        case '\n':
                            result += @"\n";
                            break;
                        case '\r':
                            result += @"\r";
                            break;
                        case '\t':
                            result += @"\t";
                            break;
                        default:
                            result += @"\" + ((ushort)s[i]).ToString("X4");
                            break;
                    }
                } else {
                    result += (s[i] == '\\' ? "\\\\" : s[i].ToString());
                }
            }

            return result;
        }

        public static T ConvertTo<T>(this string strValue)
        {
            return (T)(TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(strValue));
        }
    }
}
