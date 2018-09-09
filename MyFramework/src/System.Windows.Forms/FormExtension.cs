using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace System.Windows.Forms
{
    public static class FormExtension
    {
        public static void SetLocationToBottomRight(this Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - form.Size.Width,
                Screen.PrimaryScreen.WorkingArea.Height - form.Size.Height);
        }

        public static void AppendVersion(this Form form)
        {
            form.Text += " v" + Assembly.GetEntryAssembly().GetName().Version;
        }

        public static void AppendText(this Form form, string text)
        {
            form.Text += text;
        }
    }
}
