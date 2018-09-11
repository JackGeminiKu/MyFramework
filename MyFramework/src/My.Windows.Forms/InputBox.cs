using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace My.Windows.Forms
{
    internal partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Value = "";
        }

        public string Value { get; private set; }

        public DialogResult Show(string message, string title, string defaultValue)
        {
            lblMessage.Text = message;
            this.Text = title;
            txtValue.Text = defaultValue;
            ShowDialog();
            txtValue.Focus();
            return this.DialogResult;
        }

        public DialogResult Show(string message, string title)
        {
            return Show(message, title, "");
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtValue.Text == "") {
                MessageBox.Show("输入不能为空!");
                return;
            }
            Value = txtValue.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }



    public static class InputBox
    {
        static InputForm _inputBox;

        public static DialogResult Show(string message, string title, string defaultValue)
        {
            _inputBox = new InputForm();
            return _inputBox.Show(message, title, defaultValue);
        }

        public static DialogResult Show(string message, string title)
        {
            return Show(message, title, "");
        }

        public static DialogResult Show(string message)
        {
            return Show(message, "", "");
        }

        public static string Value
        {
            get { return _inputBox.Value; }
        }
    }
}