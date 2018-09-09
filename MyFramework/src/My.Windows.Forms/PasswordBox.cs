using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My.Windows.Forms
{
    public partial class PasswordBox : Form
    {
        public PasswordBox()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void PasswordBox_Shown(object sender, EventArgs e)
        {
            Password = txtPassword.Text = "";
        }

        public string Password { get; private set; }

        public DialogResult Show(string message)
        {
            PasswordBox passwordBox = new PasswordBox();
            passwordBox.SetMessage(message);
            passwordBox.ShowDialog();
            return passwordBox.DialogResult;
        }

        public DialogResult Show(string format, params object[] args)
        {
            return Show(string.Format(format, args));
        }

        public void SetMessage(string caption)
        {
            lblMessage.Text = caption;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Password = txtPassword.Text.Trim();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
