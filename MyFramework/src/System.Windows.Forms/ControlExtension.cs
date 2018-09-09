using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace System.Windows.Forms
{
    public static class ControlExtension
    {
        /// <summary>
        /// 设置Control对象的Text属性, 线程安全!
        /// </summary>
        public static void SetTextCrossThread(this Control control, string text)
        {
            if (control.InvokeRequired) {
                Action<Control, string> d = new Action<Control, string>(SetTextCrossThread);
                control.Invoke(d, new object[] { control, text });
            } else {
                control.Text = text;
            }
        }

        /// <summary>
        /// 获取Control对象的Text属性, 线程安全!
        /// </summary>
        public static string GetTextCrossThread(this Control control)
        {
            if (control.InvokeRequired) {
                Func<Control, string> func = new Func<Control, string>(GetTextCrossThread);
                return (string)control.Invoke(func, new object[] { control });
            } else {
                return control.Text;
            }
        }

        public static void SetEnabledCrossThread(this Control control, bool enabled)
        {
            if (control.InvokeRequired) {
                Action<Control, bool> d = new Action<Control, bool>(SetEnabledCrossThread);
                control.Invoke(d, new object[] { control, enabled });
            } else {
                control.Enabled = enabled;
            }
        }

        public static void ClearCrossThread(this TextBoxBase textBox)
        {
            if (textBox.InvokeRequired) {
                Action<TextBoxBase> action = new Action<TextBoxBase>(ClearCrossThread);
                textBox.Invoke(action, new object[] { textBox });
            } else {
                textBox.Clear();
            }
        }

        public static void SetBackgroundColorCrossThread(this Control control, Color backColor)
        {
            if (control.InvokeRequired) {
                Action<TextBoxBase, Color> action = new Action<TextBoxBase, Color>(SetBackgroundColorCrossThread);
                control.Invoke(action, new object[] { control, backColor });
            } else {
                control.BackColor = backColor;
            }
        }

        public static void SetForeColorCrossThread(this Control control, Color foreColor)
        {
            if (control.InvokeRequired) {
                Action<Control, Color> action = new Action<Control, Color>(SetForeColorCrossThread);
                control.Invoke(action, new object[] { control, foreColor });
            } else {
                control.ForeColor = foreColor;
            }
        }
    }
}
