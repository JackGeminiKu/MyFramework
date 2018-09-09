using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    public static class TextBoxBaseExtension
    {
        public static void AppendLineCrossThread(this TextBoxBase textBox, string text)
        {
            if (textBox.InvokeRequired) {
                Action<TextBoxBase, string> action = new Action<TextBoxBase, string>(AppendLineCrossThread);
                textBox.Invoke(action, new object[] { textBox, text });
            } else {
                textBox.AppendText(text + "\n");
            }
        }

        public static void AppendLineCrossThread(this TextBoxBase textBox, string format, params object[] args)
        {
            if (textBox.InvokeRequired) {
                Action<TextBoxBase, string> action = new Action<TextBoxBase, string>(AppendLineCrossThread);
                textBox.Invoke(action, new object[] { textBox, string.Format(format, args) });
            } else {
                textBox.AppendText(string.Format(format, args) + "\n");
            }
        }

        public static void AppendTextCrossThread(this TextBoxBase textBox, string text)
        {
            if (textBox.InvokeRequired) {
                Action<TextBoxBase, string> action = new Action<TextBoxBase, string>(AppendTextCrossThread);
                textBox.Invoke(action, new object[] { textBox, text });
            } else {
                textBox.AppendText(text);
            }
        }

        public static void AppendTextCrossThread(this TextBoxBase textBox, string format, params object[] args)
        {
            if (textBox.InvokeRequired) {
                Action<TextBoxBase, string> action = new Action<TextBoxBase, string>(AppendTextCrossThread);
                textBox.Invoke(action, new object[] { textBox, string.Format(format, args) });
            } else {
                textBox.AppendText(string.Format(format, args));
            }
        }

        public static int GetLineCount(this TextBoxBase textBox)
        {
            return textBox.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
