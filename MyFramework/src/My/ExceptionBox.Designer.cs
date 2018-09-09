namespace My
{
    partial class ExceptionBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelStatementMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.labelThanksMessage = new System.Windows.Forms.Label();
            this.textBoxExceptionMessage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLogo.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.tableLayoutPanel.SetRowSpan(this.pictureBoxLogo, 4);
            this.pictureBoxLogo.Size = new System.Drawing.Size(229, 419);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45748F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54252F));
            this.tableLayoutPanel.Controls.Add(this.pictureBoxLogo, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelStatementMessage, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.labelThanksMessage, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.textBoxExceptionMessage, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(682, 425);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // labelStatementMessage
            // 
            this.labelStatementMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatementMessage.Location = new System.Drawing.Point(238, 3);
            this.labelStatementMessage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.labelStatementMessage.Name = "labelStatementMessage";
            this.labelStatementMessage.Size = new System.Drawing.Size(441, 69);
            this.labelStatementMessage.TabIndex = 1;
            this.labelStatementMessage.Text = "应用程序出现了未处理的异常. 此异常未经处理, 我们希望您报告此异常给开发小组, 以改进应用程序.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonContinue);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonReport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(238, 395);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(441, 27);
            this.panel1.TabIndex = 3;
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(216, 3);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 2;
            this.buttonContinue.Text = "继续运行";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(299, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(140, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "退出程式";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.Location = new System.Drawing.Point(3, 3);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(206, 23);
            this.buttonReport.TabIndex = 0;
            this.buttonReport.Text = "向开发小组报告错误";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // labelThanksMessage
            // 
            this.labelThanksMessage.AutoSize = true;
            this.labelThanksMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelThanksMessage.Location = new System.Drawing.Point(238, 72);
            this.labelThanksMessage.Name = "labelThanksMessage";
            this.labelThanksMessage.Size = new System.Drawing.Size(441, 32);
            this.labelThanksMessage.TabIndex = 4;
            this.labelThanksMessage.Text = "非常感谢您为使改应用程序成为更出色的工具而做出贡献!";
            this.labelThanksMessage.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxExceptionMessage
            // 
            this.textBoxExceptionMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExceptionMessage.Location = new System.Drawing.Point(238, 107);
            this.textBoxExceptionMessage.Multiline = true;
            this.textBoxExceptionMessage.Name = "textBoxExceptionMessage";
            this.textBoxExceptionMessage.ReadOnly = true;
            this.textBoxExceptionMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExceptionMessage.Size = new System.Drawing.Size(441, 282);
            this.textBoxExceptionMessage.TabIndex = 5;
            // 
            // ExceptionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 425);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "发生了未处理的异常";
            this.Shown += new System.EventHandler(this.ExceptionBox_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelStatementMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Label labelThanksMessage;
        private System.Windows.Forms.TextBox textBoxExceptionMessage;
    }
}