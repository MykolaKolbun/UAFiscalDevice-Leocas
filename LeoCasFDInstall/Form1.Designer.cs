namespace LeoCasFDInstall
{
    partial class Form1
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
            if (disposing && (components != null))
            {
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxSelectType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSQLUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSQLPass = new System.Windows.Forms.TextBox();
            this.labelTestConnection = new System.Windows.Forms.Label();
            this.checkBoxCopyFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxAddText = new System.Windows.Forms.CheckBox();
            this.checkBoxAddDevices = new System.Windows.Forms.CheckBox();
            this.labelDone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(280, 230);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(5, 230);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(280, 135);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Installation type";
            // 
            // cbxSelectType
            // 
            this.cbxSelectType.FormattingEnabled = true;
            this.cbxSelectType.Location = new System.Drawing.Point(10, 40);
            this.cbxSelectType.Name = "cbxSelectType";
            this.cbxSelectType.Size = new System.Drawing.Size(120, 21);
            this.cbxSelectType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server IP:";
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(10, 100);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxServerIP.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "SQL user name:";
            // 
            // textBoxSQLUser
            // 
            this.textBoxSQLUser.Location = new System.Drawing.Point(200, 40);
            this.textBoxSQLUser.Name = "textBoxSQLUser";
            this.textBoxSQLUser.Size = new System.Drawing.Size(155, 20);
            this.textBoxSQLUser.TabIndex = 8;
            this.textBoxSQLUser.Text = "Administrator";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "SQL password:";
            // 
            // textBoxSQLPass
            // 
            this.textBoxSQLPass.Location = new System.Drawing.Point(200, 95);
            this.textBoxSQLPass.Name = "textBoxSQLPass";
            this.textBoxSQLPass.Size = new System.Drawing.Size(155, 20);
            this.textBoxSQLPass.TabIndex = 10;
            this.textBoxSQLPass.Text = "Hey1U9I4am1from0Skidata!";
            this.textBoxSQLPass.UseSystemPasswordChar = true;
            // 
            // labelTestConnection
            // 
            this.labelTestConnection.AutoSize = true;
            this.labelTestConnection.Location = new System.Drawing.Point(185, 140);
            this.labelTestConnection.Name = "labelTestConnection";
            this.labelTestConnection.Size = new System.Drawing.Size(84, 13);
            this.labelTestConnection.TabIndex = 11;
            this.labelTestConnection.Text = "Test connection";
            // 
            // checkBoxCopyFiles
            // 
            this.checkBoxCopyFiles.AutoSize = true;
            this.checkBoxCopyFiles.Enabled = false;
            this.checkBoxCopyFiles.Location = new System.Drawing.Point(10, 140);
            this.checkBoxCopyFiles.Name = "checkBoxCopyFiles";
            this.checkBoxCopyFiles.Size = new System.Drawing.Size(71, 17);
            this.checkBoxCopyFiles.TabIndex = 12;
            this.checkBoxCopyFiles.Text = "Copy files";
            this.checkBoxCopyFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddText
            // 
            this.checkBoxAddText.AutoSize = true;
            this.checkBoxAddText.Enabled = false;
            this.checkBoxAddText.Location = new System.Drawing.Point(10, 160);
            this.checkBoxAddText.Name = "checkBoxAddText";
            this.checkBoxAddText.Size = new System.Drawing.Size(95, 17);
            this.checkBoxAddText.TabIndex = 13;
            this.checkBoxAddText.Text = "Add text to DB";
            this.checkBoxAddText.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddDevices
            // 
            this.checkBoxAddDevices.AutoSize = true;
            this.checkBoxAddDevices.Enabled = false;
            this.checkBoxAddDevices.Location = new System.Drawing.Point(10, 180);
            this.checkBoxAddDevices.Name = "checkBoxAddDevices";
            this.checkBoxAddDevices.Size = new System.Drawing.Size(115, 17);
            this.checkBoxAddDevices.TabIndex = 14;
            this.checkBoxAddDevices.Text = "Add devices to DB";
            this.checkBoxAddDevices.UseVisualStyleBackColor = true;
            // 
            // labelDone
            // 
            this.labelDone.AutoSize = true;
            this.labelDone.Location = new System.Drawing.Point(315, 205);
            this.labelDone.Name = "labelDone";
            this.labelDone.Size = new System.Drawing.Size(33, 13);
            this.labelDone.TabIndex = 15;
            this.labelDone.Text = "Done";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 259);
            this.Controls.Add(this.labelDone);
            this.Controls.Add(this.checkBoxAddDevices);
            this.Controls.Add(this.checkBoxAddText);
            this.Controls.Add(this.checkBoxCopyFiles);
            this.Controls.Add(this.labelTestConnection);
            this.Controls.Add(this.textBoxSQLPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSQLUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxSelectType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnClose);
            this.Name = "Form1";
            this.Text = "Install FD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxSelectType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSQLUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSQLPass;
        private System.Windows.Forms.Label labelTestConnection;
        private System.Windows.Forms.CheckBox checkBoxCopyFiles;
        private System.Windows.Forms.CheckBox checkBoxAddText;
        private System.Windows.Forms.CheckBox checkBoxAddDevices;
        private System.Windows.Forms.Label labelDone;
    }
}

