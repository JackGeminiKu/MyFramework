using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.Threading;
using My.Log;

namespace My
{
    public partial class ExceptionBox : Form
    {
        public ExceptionBox()
        {
            InitializeComponent();
        }

        public ExceptionBox(Exception ex, bool mustTerminate)
        {
            ExceptionThrown = ex;
            MustTerminate = mustTerminate;
        }

        private void ExceptionBox_Shown(object sender, EventArgs e)
        {
            buttonReport.Visible = ReportException != null;
            buttonContinue.Visible = !MustTerminate;
            textBoxExceptionMessage.Text = GetExceptionMessage();
            pictureBoxLogo.Image = Logo;
        }

        public Exception ExceptionThrown { get; set; }

        public bool MustTerminate { get; set; }

        public Image Logo { get; set; }

        public string StatementMessage
        {
            get { return labelStatementMessage.Text; }
            set { labelStatementMessage.Text = value; }
        }

        public string ThanksMessage
        {
            get { return labelThanksMessage.Text; }
            set { labelThanksMessage.Text = value; }
        }

        public string ExceptionMessage { get; private set; }

        public Action<string> ReportException { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Ignore;
            Close();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            ReportException(textBoxExceptionMessage.Text);
            MessageBox.Show("已将此异常报告给开发小组, 感谢您的配合!", "报告异常", MessageBoxButtons.OK);
        }

        string GetExceptionMessage()
        {
            string message = "";
            Version v = Assembly.GetEntryAssembly().GetName().Version;
            message += "App Version : " + v.ToString() + Environment.NewLine;
            message += ".NET Version : " + Environment.Version.ToString() + Environment.NewLine;
            message += "OS Version : " + Environment.OSVersion.ToString() +
                 string.Format(", {0}bit", IntPtr.Size == 4 ? 32 : 64) + Environment.NewLine;

            string cultureName = null;
            try {
                cultureName = CultureInfo.CurrentCulture.Name;
                message += "Current culture : " + CultureInfo.CurrentCulture.EnglishName +
                    " (" + cultureName + ")" + Environment.NewLine;
            } catch { }

            try {
                if (SystemInformation.TerminalServerSession) {
                    message += "Terminal Server Session" + Environment.NewLine;
                }
                if (SystemInformation.BootMode != BootMode.Normal) {
                    message += "Boot Mode            : " + SystemInformation.BootMode + Environment.NewLine;
                }
            } catch { }

            message += "Working Set Memory : " + (Environment.WorkingSet / 1024) + "kb" + Environment.NewLine;
            message += "GC Heap Memory : " + (GC.GetTotalMemory(false) / 1024) + "kb" + Environment.NewLine;
            message += Environment.NewLine;

            message += "Exception thrown : " + Environment.NewLine;
            message += ExceptionThrown.ToString();

            return message;
        }

        public static void RegisterExceptionBoxForUnhandledExceptions()
        {
            Application.ThreadException += ShowErrorBox;
            AppDomain.CurrentDomain.UnhandledException += ShowErrorBox;
        }

        static void ShowErrorBox(object sender, ThreadExceptionEventArgs e)
        {
            LogService.Error("ThreadException caught", e.Exception);
            ShowErrorBox(e.Exception, null, false);
        }

        static void ShowErrorBox(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            LogService.Fatal("UnhandledException caught", ex);
            if (e.IsTerminating)
                LogService.Fatal("Runtime is terminating because of unhandled exception.");
            ShowErrorBox(ex, "Unhandled exception", e.IsTerminating);
        }

        static void ShowErrorBox(Exception exception, string message, bool mustTerminate)
        {
            try {
                using (ExceptionBox box = new ExceptionBox(exception, mustTerminate)) {
                    try {
                        box.ShowDialog();
                    } catch (InvalidOperationException) {
                        box.ShowDialog();
                    }
                }
            } catch (Exception ex) {
                LogService.Warn("Error showing ExceptionBox", ex);
                MessageBox.Show(exception.ToString(), message, MessageBoxButtons.OK,
                                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
