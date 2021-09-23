namespace FiscalServiceMenu
{
    partial class FiscalServiceMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiscalServiceMenu));
            this.cbxSelectDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbxStatus = new System.Windows.Forms.GroupBox();
            this.chbxLowPaper = new System.Windows.Forms.CheckBox();
            this.chbxShiftBeg = new System.Windows.Forms.CheckBox();
            this.chbx24Hour = new System.Windows.Forms.CheckBox();
            this.chbxDocOpnd = new System.Windows.Forms.CheckBox();
            this.chbxIsBlocked = new System.Windows.Forms.CheckBox();
            this.chbxOpReg = new System.Windows.Forms.CheckBox();
            this.chbxReceiptOpened = new System.Windows.Forms.CheckBox();
            this.chbxPrinErr = new System.Windows.Forms.CheckBox();
            this.chbxOutPaper = new System.Windows.Forms.CheckBox();
            this.chbx72hour = new System.Windows.Forms.CheckBox();
            this.gbxFiscalFn = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbxReturn = new System.Windows.Forms.CheckBox();
            this.txbSSale = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chbIsCashless = new System.Windows.Forms.CheckBox();
            this.btnSSale = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.cbxRepType = new System.Windows.Forms.ComboBox();
            this.btnPrintPeriod = new System.Windows.Forms.Button();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txbDocNr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPrinDocNr = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txbZNr = new System.Windows.Forms.TextBox();
            this.btnPrintZbyNr = new System.Windows.Forms.Button();
            this.btnZRep = new System.Windows.Forms.Button();
            this.btnPrintZ = new System.Windows.Forms.Button();
            this.btnXRep = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnDisconect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gbxStatus.SuspendLayout();
            this.gbxFiscalFn.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxSelectDevice
            // 
            this.cbxSelectDevice.FormattingEnabled = true;
            resources.ApplyResources(this.cbxSelectDevice, "cbxSelectDevice");
            this.cbxSelectDevice.Name = "cbxSelectDevice";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDisconect);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxSelectDevice);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnConnect, "btnConnect");
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // gbxStatus
            // 
            this.gbxStatus.Controls.Add(this.chbxLowPaper);
            this.gbxStatus.Controls.Add(this.chbxShiftBeg);
            this.gbxStatus.Controls.Add(this.chbx24Hour);
            this.gbxStatus.Controls.Add(this.chbxDocOpnd);
            this.gbxStatus.Controls.Add(this.chbxIsBlocked);
            this.gbxStatus.Controls.Add(this.chbxOpReg);
            this.gbxStatus.Controls.Add(this.chbxReceiptOpened);
            this.gbxStatus.Controls.Add(this.chbxPrinErr);
            this.gbxStatus.Controls.Add(this.chbxOutPaper);
            this.gbxStatus.Controls.Add(this.chbx72hour);
            resources.ApplyResources(this.gbxStatus, "gbxStatus");
            this.gbxStatus.Name = "gbxStatus";
            this.gbxStatus.TabStop = false;
            // 
            // chbxLowPaper
            // 
            resources.ApplyResources(this.chbxLowPaper, "chbxLowPaper");
            this.chbxLowPaper.Name = "chbxLowPaper";
            this.chbxLowPaper.UseVisualStyleBackColor = true;
            // 
            // chbxShiftBeg
            // 
            resources.ApplyResources(this.chbxShiftBeg, "chbxShiftBeg");
            this.chbxShiftBeg.Name = "chbxShiftBeg";
            this.chbxShiftBeg.UseVisualStyleBackColor = true;
            // 
            // chbx24Hour
            // 
            resources.ApplyResources(this.chbx24Hour, "chbx24Hour");
            this.chbx24Hour.Name = "chbx24Hour";
            this.chbx24Hour.UseVisualStyleBackColor = true;
            // 
            // chbxDocOpnd
            // 
            resources.ApplyResources(this.chbxDocOpnd, "chbxDocOpnd");
            this.chbxDocOpnd.Name = "chbxDocOpnd";
            this.chbxDocOpnd.UseVisualStyleBackColor = true;
            // 
            // chbxIsBlocked
            // 
            resources.ApplyResources(this.chbxIsBlocked, "chbxIsBlocked");
            this.chbxIsBlocked.Name = "chbxIsBlocked";
            this.chbxIsBlocked.UseVisualStyleBackColor = true;
            // 
            // chbxOpReg
            // 
            resources.ApplyResources(this.chbxOpReg, "chbxOpReg");
            this.chbxOpReg.Name = "chbxOpReg";
            this.chbxOpReg.UseVisualStyleBackColor = true;
            // 
            // chbxReceiptOpened
            // 
            resources.ApplyResources(this.chbxReceiptOpened, "chbxReceiptOpened");
            this.chbxReceiptOpened.Name = "chbxReceiptOpened";
            this.chbxReceiptOpened.UseVisualStyleBackColor = true;
            // 
            // chbxPrinErr
            // 
            resources.ApplyResources(this.chbxPrinErr, "chbxPrinErr");
            this.chbxPrinErr.Name = "chbxPrinErr";
            this.chbxPrinErr.UseVisualStyleBackColor = true;
            // 
            // chbxOutPaper
            // 
            resources.ApplyResources(this.chbxOutPaper, "chbxOutPaper");
            this.chbxOutPaper.Name = "chbxOutPaper";
            this.chbxOutPaper.UseVisualStyleBackColor = true;
            // 
            // chbx72hour
            // 
            resources.ApplyResources(this.chbx72hour, "chbx72hour");
            this.chbx72hour.Name = "chbx72hour";
            this.chbx72hour.UseVisualStyleBackColor = true;
            // 
            // gbxFiscalFn
            // 
            this.gbxFiscalFn.Controls.Add(this.groupBox5);
            this.gbxFiscalFn.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.gbxFiscalFn, "gbxFiscalFn");
            this.gbxFiscalFn.Name = "gbxFiscalFn";
            this.gbxFiscalFn.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbxReturn);
            this.groupBox5.Controls.Add(this.txbSSale);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.chbIsCashless);
            this.groupBox5.Controls.Add(this.btnSSale);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // cbxReturn
            // 
            resources.ApplyResources(this.cbxReturn, "cbxReturn");
            this.cbxReturn.Name = "cbxReturn";
            this.cbxReturn.UseVisualStyleBackColor = true;
            // 
            // txbSSale
            // 
            resources.ApplyResources(this.txbSSale, "txbSSale");
            this.txbSSale.Name = "txbSSale";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // chbIsCashless
            // 
            resources.ApplyResources(this.chbIsCashless, "chbIsCashless");
            this.chbIsCashless.Name = "chbIsCashless";
            this.chbIsCashless.UseVisualStyleBackColor = true;
            // 
            // btnSSale
            // 
            this.btnSSale.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSSale.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnSSale, "btnSSale");
            this.btnSSale.Name = "btnSSale";
            this.btnSSale.UseVisualStyleBackColor = false;
            this.btnSSale.Click += new System.EventHandler(this.btnSSale_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.dtpDateTo);
            this.groupBox4.Controls.Add(this.cbxRepType);
            this.groupBox4.Controls.Add(this.btnPrintPeriod);
            this.groupBox4.Controls.Add(this.dtpDateFrom);
            this.groupBox4.Controls.Add(this.txbDocNr);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.btnPrinDocNr);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txbZNr);
            this.groupBox4.Controls.Add(this.btnPrintZbyNr);
            this.groupBox4.Controls.Add(this.btnZRep);
            this.groupBox4.Controls.Add(this.btnPrintZ);
            this.groupBox4.Controls.Add(this.btnXRep);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // dtpDateTo
            // 
            resources.ApplyResources(this.dtpDateTo, "dtpDateTo");
            this.dtpDateTo.Name = "dtpDateTo";
            // 
            // cbxRepType
            // 
            this.cbxRepType.FormattingEnabled = true;
            resources.ApplyResources(this.cbxRepType, "cbxRepType");
            this.cbxRepType.Name = "cbxRepType";
            // 
            // btnPrintPeriod
            // 
            this.btnPrintPeriod.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrintPeriod.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPrintPeriod, "btnPrintPeriod");
            this.btnPrintPeriod.Name = "btnPrintPeriod";
            this.btnPrintPeriod.UseVisualStyleBackColor = false;
            this.btnPrintPeriod.Click += new System.EventHandler(this.btnPrintPeriod_Click);
            // 
            // dtpDateFrom
            // 
            resources.ApplyResources(this.dtpDateFrom, "dtpDateFrom");
            this.dtpDateFrom.Name = "dtpDateFrom";
            // 
            // txbDocNr
            // 
            resources.ApplyResources(this.txbDocNr, "txbDocNr");
            this.txbDocNr.Name = "txbDocNr";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnPrinDocNr
            // 
            this.btnPrinDocNr.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrinDocNr.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPrinDocNr, "btnPrinDocNr");
            this.btnPrinDocNr.Name = "btnPrinDocNr";
            this.btnPrinDocNr.UseVisualStyleBackColor = false;
            this.btnPrinDocNr.Click += new System.EventHandler(this.btnPrinDocNr_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txbZNr
            // 
            resources.ApplyResources(this.txbZNr, "txbZNr");
            this.txbZNr.Name = "txbZNr";
            // 
            // btnPrintZbyNr
            // 
            this.btnPrintZbyNr.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrintZbyNr.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPrintZbyNr, "btnPrintZbyNr");
            this.btnPrintZbyNr.Name = "btnPrintZbyNr";
            this.btnPrintZbyNr.UseVisualStyleBackColor = false;
            this.btnPrintZbyNr.Click += new System.EventHandler(this.btnPrintZbyNr_Click);
            // 
            // btnZRep
            // 
            this.btnZRep.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnZRep.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnZRep, "btnZRep");
            this.btnZRep.Name = "btnZRep";
            this.btnZRep.UseVisualStyleBackColor = false;
            this.btnZRep.Click += new System.EventHandler(this.btnZRep_Click);
            // 
            // btnPrintZ
            // 
            this.btnPrintZ.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrintZ.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPrintZ, "btnPrintZ");
            this.btnPrintZ.Name = "btnPrintZ";
            this.btnPrintZ.UseVisualStyleBackColor = false;
            this.btnPrintZ.Click += new System.EventHandler(this.btnPrintZ_Click);
            // 
            // btnXRep
            // 
            this.btnXRep.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnXRep.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnXRep, "btnXRep");
            this.btnXRep.Name = "btnXRep";
            this.btnXRep.UseVisualStyleBackColor = false;
            this.btnXRep.Click += new System.EventHandler(this.btnXRep_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::FiscalServiceMenu.Properties.Resources.periprotect;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnDisconect
            // 
            this.btnDisconect.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnDisconect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnDisconect, "btnDisconect");
            this.btnDisconect.Name = "btnDisconect";
            this.btnDisconect.UseVisualStyleBackColor = false;
            this.btnDisconect.Click += new System.EventHandler(this.btnDisconect_Click);
            // 
            // FiscalServiceMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.gbxFiscalFn);
            this.Controls.Add(this.gbxStatus);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FiscalServiceMenu";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FiscalServiceMenu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxStatus.ResumeLayout(false);
            this.gbxStatus.PerformLayout();
            this.gbxFiscalFn.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxSelectDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox gbxStatus;
        private System.Windows.Forms.GroupBox gbxFiscalFn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.ComboBox cbxRepType;
        private System.Windows.Forms.Button btnPrintPeriod;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.TextBox txbDocNr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPrinDocNr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbZNr;
        private System.Windows.Forms.Button btnPrintZbyNr;
        private System.Windows.Forms.Button btnZRep;
        private System.Windows.Forms.Button btnPrintZ;
        private System.Windows.Forms.Button btnXRep;
        private System.Windows.Forms.CheckBox cbxReturn;
        private System.Windows.Forms.TextBox txbSSale;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chbIsCashless;
        private System.Windows.Forms.Button btnSSale;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chbxLowPaper;
        private System.Windows.Forms.CheckBox chbxShiftBeg;
        private System.Windows.Forms.CheckBox chbx24Hour;
        private System.Windows.Forms.CheckBox chbxDocOpnd;
        private System.Windows.Forms.CheckBox chbxIsBlocked;
        private System.Windows.Forms.CheckBox chbxOpReg;
        private System.Windows.Forms.CheckBox chbxReceiptOpened;
        private System.Windows.Forms.CheckBox chbxPrinErr;
        private System.Windows.Forms.CheckBox chbxOutPaper;
        private System.Windows.Forms.CheckBox chbx72hour;
        private System.Windows.Forms.Button btnDisconect;
    }
}