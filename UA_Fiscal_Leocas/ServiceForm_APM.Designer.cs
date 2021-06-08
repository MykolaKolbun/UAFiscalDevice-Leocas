namespace UA_Fiscal_Leocas
{
    partial class ServiceForm_APM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm_APM));
            this.btnZRep = new System.Windows.Forms.Button();
            this.btnXRep = new System.Windows.Forms.Button();
            this.btnCashIn = new System.Windows.Forms.Button();
            this.txbCashOut = new System.Windows.Forms.TextBox();
            this.btnCashOut = new System.Windows.Forms.Button();
            this.txbCashIN = new System.Windows.Forms.TextBox();
            this.btnPrintZ = new System.Windows.Forms.Button();
            this.groupBoxFP = new System.Windows.Forms.GroupBox();
            this.btnTimeSync = new System.Windows.Forms.Button();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxFPCashInOut = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxFPRep = new System.Windows.Forms.GroupBox();
            this.cbxReportSel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbReceiptNr = new System.Windows.Forms.TextBox();
            this.btnNewShift = new System.Windows.Forms.Button();
            this.btnCopyCheck = new System.Windows.Forms.Button();
            this.btnTermReport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txbRepNr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.btnPrintRepNr = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chbxIsBlocked = new System.Windows.Forms.CheckBox();
            this.chbxShiftBeg = new System.Windows.Forms.CheckBox();
            this.chbxReceiptOpened = new System.Windows.Forms.CheckBox();
            this.chbxDocOpnd = new System.Windows.Forms.CheckBox();
            this.chbxOpReg = new System.Windows.Forms.CheckBox();
            this.chbxOutPaper = new System.Windows.Forms.CheckBox();
            this.chbxPrinErr = new System.Windows.Forms.CheckBox();
            this.chbx72hour = new System.Windows.Forms.CheckBox();
            this.chbx24Hour = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbxLowPaper = new System.Windows.Forms.CheckBox();
            this.groupBoxSSale = new System.Windows.Forms.GroupBox();
            this.cbxReturn = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txbSSale = new System.Windows.Forms.TextBox();
            this.btnSSale = new System.Windows.Forms.Button();
            this.chbIsCash = new System.Windows.Forms.CheckBox();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPageService = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUpdateAmount = new System.Windows.Forms.Button();
            this.txbCashSerialized = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnVoidDoc = new System.Windows.Forms.Button();
            this.btnVoidRec = new System.Windows.Forms.Button();
            this.tabPageFiscal = new System.Windows.Forms.TabPage();
            this.groupBoxFP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxFPCashInOut.SuspendLayout();
            this.groupBoxFPRep.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxSSale.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.tabPageService.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPageFiscal.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnZRep
            // 
            this.btnZRep.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnZRep.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnZRep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnZRep.Location = new System.Drawing.Point(184, 19);
            this.btnZRep.Name = "btnZRep";
            this.btnZRep.Size = new System.Drawing.Size(83, 22);
            this.btnZRep.TabIndex = 6;
            this.btnZRep.Text = "Z-Звіт";
            this.btnZRep.UseVisualStyleBackColor = false;
            this.btnZRep.Click += new System.EventHandler(this.btnZRep_Click);
            // 
            // btnXRep
            // 
            this.btnXRep.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnXRep.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnXRep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXRep.Location = new System.Drawing.Point(6, 19);
            this.btnXRep.Name = "btnXRep";
            this.btnXRep.Size = new System.Drawing.Size(83, 22);
            this.btnXRep.TabIndex = 5;
            this.btnXRep.Text = "X-Звіт";
            this.btnXRep.UseVisualStyleBackColor = false;
            this.btnXRep.Click += new System.EventHandler(this.btnXRep_Click);
            // 
            // btnCashIn
            // 
            this.btnCashIn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCashIn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnCashIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCashIn.Location = new System.Drawing.Point(190, 19);
            this.btnCashIn.Name = "btnCashIn";
            this.btnCashIn.Size = new System.Drawing.Size(73, 23);
            this.btnCashIn.TabIndex = 2;
            this.btnCashIn.Text = "Внести";
            this.btnCashIn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCashIn.UseVisualStyleBackColor = false;
            this.btnCashIn.Click += new System.EventHandler(this.btnCashIn_Click);
            // 
            // txbCashOut
            // 
            this.txbCashOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txbCashOut.Location = new System.Drawing.Point(99, 47);
            this.txbCashOut.Name = "txbCashOut";
            this.txbCashOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txbCashOut.Size = new System.Drawing.Size(85, 24);
            this.txbCashOut.TabIndex = 3;
            this.txbCashOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbCashOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbCashOut_KeyPress);
            // 
            // btnCashOut
            // 
            this.btnCashOut.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCashOut.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnCashOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCashOut.Location = new System.Drawing.Point(190, 48);
            this.btnCashOut.Name = "btnCashOut";
            this.btnCashOut.Size = new System.Drawing.Size(73, 23);
            this.btnCashOut.TabIndex = 4;
            this.btnCashOut.Text = "Вилучити";
            this.btnCashOut.UseVisualStyleBackColor = false;
            this.btnCashOut.Click += new System.EventHandler(this.btnCashOut_Click);
            // 
            // txbCashIN
            // 
            this.txbCashIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txbCashIN.Location = new System.Drawing.Point(99, 18);
            this.txbCashIN.Name = "txbCashIN";
            this.txbCashIN.Size = new System.Drawing.Size(85, 24);
            this.txbCashIN.TabIndex = 1;
            this.txbCashIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbCashIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbCashIN_KeyPress);
            // 
            // btnPrintZ
            // 
            this.btnPrintZ.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnPrintZ.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrintZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPrintZ.Location = new System.Drawing.Point(95, 19);
            this.btnPrintZ.Name = "btnPrintZ";
            this.btnPrintZ.Size = new System.Drawing.Size(83, 22);
            this.btnPrintZ.TabIndex = 7;
            this.btnPrintZ.Text = "Друк Z";
            this.btnPrintZ.UseVisualStyleBackColor = false;
            this.btnPrintZ.Click += new System.EventHandler(this.btnPrintZ_Click);
            // 
            // groupBoxFP
            // 
            this.groupBoxFP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxFP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBoxFP.Controls.Add(this.btnTimeSync);
            this.groupBoxFP.Controls.Add(this.lblCurrentTime);
            this.groupBoxFP.Controls.Add(this.label7);
            this.groupBoxFP.Controls.Add(this.pictureBox1);
            this.groupBoxFP.Controls.Add(this.groupBoxFPCashInOut);
            this.groupBoxFP.Controls.Add(this.groupBoxFPRep);
            this.groupBoxFP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBoxFP.Location = new System.Drawing.Point(191, 6);
            this.groupBoxFP.Name = "groupBoxFP";
            this.groupBoxFP.Size = new System.Drawing.Size(288, 335);
            this.groupBoxFP.TabIndex = 10;
            this.groupBoxFP.TabStop = false;
            this.groupBoxFP.Text = "Додаткові фіскальні функції";
            // 
            // btnTimeSync
            // 
            this.btnTimeSync.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnTimeSync.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTimeSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimeSync.Location = new System.Drawing.Point(190, 254);
            this.btnTimeSync.Name = "btnTimeSync";
            this.btnTimeSync.Size = new System.Drawing.Size(89, 25);
            this.btnTimeSync.TabIndex = 15;
            this.btnTimeSync.Text = "Синхронізація";
            this.btnTimeSync.UseVisualStyleBackColor = false;
            this.btnTimeSync.Click += new System.EventHandler(this.btnTimeSync_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Location = new System.Drawing.Point(98, 260);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(43, 13);
            this.lblCurrentTime.TabIndex = 14;
            this.lblCurrentTime.Text = "0:00:00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Системний час:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::UA_Fiscal_Leocas.Properties.Resources.periprotect;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(41, 284);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 48);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // groupBoxFPCashInOut
            // 
            this.groupBoxFPCashInOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxFPCashInOut.Controls.Add(this.label5);
            this.groupBoxFPCashInOut.Controls.Add(this.label4);
            this.groupBoxFPCashInOut.Controls.Add(this.txbCashIN);
            this.groupBoxFPCashInOut.Controls.Add(this.btnCashOut);
            this.groupBoxFPCashInOut.Controls.Add(this.txbCashOut);
            this.groupBoxFPCashInOut.Controls.Add(this.btnCashIn);
            this.groupBoxFPCashInOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxFPCashInOut.Location = new System.Drawing.Point(6, 14);
            this.groupBoxFPCashInOut.Name = "groupBoxFPCashInOut";
            this.groupBoxFPCashInOut.Size = new System.Drawing.Size(273, 78);
            this.groupBoxFPCashInOut.TabIndex = 10;
            this.groupBoxFPCashInOut.TabStop = false;
            this.groupBoxFPCashInOut.Text = "Внесення/Видача";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(23, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Сума видачі:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Сума внесення:";
            // 
            // groupBoxFPRep
            // 
            this.groupBoxFPRep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxFPRep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxFPRep.Controls.Add(this.cbxReportSel);
            this.groupBoxFPRep.Controls.Add(this.label6);
            this.groupBoxFPRep.Controls.Add(this.txbReceiptNr);
            this.groupBoxFPRep.Controls.Add(this.btnNewShift);
            this.groupBoxFPRep.Controls.Add(this.btnCopyCheck);
            this.groupBoxFPRep.Controls.Add(this.btnTermReport);
            this.groupBoxFPRep.Controls.Add(this.label3);
            this.groupBoxFPRep.Controls.Add(this.txbRepNr);
            this.groupBoxFPRep.Controls.Add(this.label2);
            this.groupBoxFPRep.Controls.Add(this.label1);
            this.groupBoxFPRep.Controls.Add(this.dateTimePickerTo);
            this.groupBoxFPRep.Controls.Add(this.btnXRep);
            this.groupBoxFPRep.Controls.Add(this.dateTimePickerFrom);
            this.groupBoxFPRep.Controls.Add(this.btnPrintRepNr);
            this.groupBoxFPRep.Controls.Add(this.btnZRep);
            this.groupBoxFPRep.Controls.Add(this.btnPrintZ);
            this.groupBoxFPRep.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxFPRep.Location = new System.Drawing.Point(6, 93);
            this.groupBoxFPRep.Name = "groupBoxFPRep";
            this.groupBoxFPRep.Size = new System.Drawing.Size(273, 160);
            this.groupBoxFPRep.TabIndex = 9;
            this.groupBoxFPRep.TabStop = false;
            this.groupBoxFPRep.Text = "Звіти";
            // 
            // cbxReportSel
            // 
            this.cbxReportSel.FormattingEnabled = true;
            this.cbxReportSel.Location = new System.Drawing.Point(184, 105);
            this.cbxReportSel.Name = "cbxReportSel";
            this.cbxReportSel.Size = new System.Drawing.Size(83, 21);
            this.cbxReportSel.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(101, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "№:";
            // 
            // txbReceiptNr
            // 
            this.txbReceiptNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txbReceiptNr.Location = new System.Drawing.Point(128, 75);
            this.txbReceiptNr.Name = "txbReceiptNr";
            this.txbReceiptNr.Size = new System.Drawing.Size(50, 24);
            this.txbReceiptNr.TabIndex = 20;
            this.txbReceiptNr.Text = "0";
            this.txbReceiptNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnNewShift
            // 
            this.btnNewShift.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnNewShift.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnNewShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewShift.Location = new System.Drawing.Point(6, 47);
            this.btnNewShift.Name = "btnNewShift";
            this.btnNewShift.Size = new System.Drawing.Size(83, 23);
            this.btnNewShift.TabIndex = 17;
            this.btnNewShift.Text = "Нова зміна";
            this.btnNewShift.UseVisualStyleBackColor = false;
            this.btnNewShift.Click += new System.EventHandler(this.btnNewShift_Click);
            // 
            // btnCopyCheck
            // 
            this.btnCopyCheck.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCopyCheck.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnCopyCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyCheck.Location = new System.Drawing.Point(184, 77);
            this.btnCopyCheck.Name = "btnCopyCheck";
            this.btnCopyCheck.Size = new System.Drawing.Size(83, 23);
            this.btnCopyCheck.TabIndex = 19;
            this.btnCopyCheck.Text = "Копия док";
            this.btnCopyCheck.UseVisualStyleBackColor = false;
            this.btnCopyCheck.Click += new System.EventHandler(this.btnCopyCheck_Click);
            // 
            // btnTermReport
            // 
            this.btnTermReport.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnTermReport.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnTermReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTermReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTermReport.Location = new System.Drawing.Point(184, 131);
            this.btnTermReport.Name = "btnTermReport";
            this.btnTermReport.Size = new System.Drawing.Size(83, 23);
            this.btnTermReport.TabIndex = 16;
            this.btnTermReport.Text = "Період. звіт";
            this.btnTermReport.UseVisualStyleBackColor = false;
            this.btnTermReport.Click += new System.EventHandler(this.btnTermReport_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "№:";
            // 
            // txbRepNr
            // 
            this.txbRepNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txbRepNr.Location = new System.Drawing.Point(128, 48);
            this.txbRepNr.Name = "txbRepNr";
            this.txbRepNr.Size = new System.Drawing.Size(50, 24);
            this.txbRepNr.TabIndex = 14;
            this.txbRepNr.Text = "1";
            this.txbRepNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "До:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "З:";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.dateTimePickerTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.dateTimePickerTo.Location = new System.Drawing.Point(63, 134);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(116, 20);
            this.dateTimePickerTo.TabIndex = 11;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.dateTimePickerFrom.Location = new System.Drawing.Point(62, 105);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(116, 20);
            this.dateTimePickerFrom.TabIndex = 18;
            // 
            // btnPrintRepNr
            // 
            this.btnPrintRepNr.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnPrintRepNr.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrintRepNr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintRepNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPrintRepNr.Location = new System.Drawing.Point(184, 49);
            this.btnPrintRepNr.Name = "btnPrintRepNr";
            this.btnPrintRepNr.Size = new System.Drawing.Size(83, 23);
            this.btnPrintRepNr.TabIndex = 9;
            this.btnPrintRepNr.Text = "Друк Z по №";
            this.btnPrintRepNr.UseVisualStyleBackColor = false;
            this.btnPrintRepNr.Click += new System.EventHandler(this.btnPrintRepNr_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chbxIsBlocked
            // 
            this.chbxIsBlocked.AutoSize = true;
            this.chbxIsBlocked.Location = new System.Drawing.Point(3, 126);
            this.chbxIsBlocked.Name = "chbxIsBlocked";
            this.chbxIsBlocked.Size = new System.Drawing.Size(117, 17);
            this.chbxIsBlocked.TabIndex = 12;
            this.chbxIsBlocked.Text = "РРО заблоковано";
            this.chbxIsBlocked.UseVisualStyleBackColor = true;
            // 
            // chbxShiftBeg
            // 
            this.chbxShiftBeg.AutoSize = true;
            this.chbxShiftBeg.Location = new System.Drawing.Point(3, 21);
            this.chbxShiftBeg.Name = "chbxShiftBeg";
            this.chbxShiftBeg.Size = new System.Drawing.Size(101, 17);
            this.chbxShiftBeg.TabIndex = 13;
            this.chbxShiftBeg.Text = "Зміна відкрита";
            this.chbxShiftBeg.UseVisualStyleBackColor = true;
            // 
            // chbxReceiptOpened
            // 
            this.chbxReceiptOpened.AutoSize = true;
            this.chbxReceiptOpened.Location = new System.Drawing.Point(3, 42);
            this.chbxReceiptOpened.Name = "chbxReceiptOpened";
            this.chbxReceiptOpened.Size = new System.Drawing.Size(151, 17);
            this.chbxReceiptOpened.TabIndex = 14;
            this.chbxReceiptOpened.Text = "Відкрито фіскальний чек";
            this.chbxReceiptOpened.UseVisualStyleBackColor = true;
            // 
            // chbxDocOpnd
            // 
            this.chbxDocOpnd.AutoSize = true;
            this.chbxDocOpnd.Location = new System.Drawing.Point(3, 63);
            this.chbxDocOpnd.Name = "chbxDocOpnd";
            this.chbxDocOpnd.Size = new System.Drawing.Size(163, 17);
            this.chbxDocOpnd.TabIndex = 15;
            this.chbxDocOpnd.Text = "Відкрито нефіскальний чек";
            this.chbxDocOpnd.UseVisualStyleBackColor = true;
            // 
            // chbxOpReg
            // 
            this.chbxOpReg.AutoSize = true;
            this.chbxOpReg.Location = new System.Drawing.Point(3, 84);
            this.chbxOpReg.Name = "chbxOpReg";
            this.chbxOpReg.Size = new System.Drawing.Size(161, 17);
            this.chbxOpReg.TabIndex = 16;
            this.chbxOpReg.Text = "Оператор зареєстрований";
            this.chbxOpReg.UseVisualStyleBackColor = true;
            // 
            // chbxOutPaper
            // 
            this.chbxOutPaper.AutoSize = true;
            this.chbxOutPaper.Location = new System.Drawing.Point(3, 105);
            this.chbxOutPaper.Name = "chbxOutPaper";
            this.chbxOutPaper.Size = new System.Drawing.Size(120, 17);
            this.chbxOutPaper.TabIndex = 17;
            this.chbxOutPaper.Text = "Закінчення паперу";
            this.chbxOutPaper.UseVisualStyleBackColor = true;
            // 
            // chbxPrinErr
            // 
            this.chbxPrinErr.AutoSize = true;
            this.chbxPrinErr.Location = new System.Drawing.Point(3, 147);
            this.chbxPrinErr.Name = "chbxPrinErr";
            this.chbxPrinErr.Size = new System.Drawing.Size(122, 17);
            this.chbxPrinErr.TabIndex = 18;
            this.chbxPrinErr.Text = "Помилка принтера";
            this.chbxPrinErr.UseVisualStyleBackColor = true;
            // 
            // chbx72hour
            // 
            this.chbx72hour.AutoSize = true;
            this.chbx72hour.Location = new System.Drawing.Point(3, 168);
            this.chbx72hour.Name = "chbx72hour";
            this.chbx72hour.Size = new System.Drawing.Size(139, 17);
            this.chbx72hour.TabIndex = 19;
            this.chbx72hour.Text = "Блокування 72 години";
            this.chbx72hour.UseVisualStyleBackColor = true;
            // 
            // chbx24Hour
            // 
            this.chbx24Hour.AutoSize = true;
            this.chbx24Hour.Location = new System.Drawing.Point(3, 189);
            this.chbx24Hour.Name = "chbx24Hour";
            this.chbx24Hour.Size = new System.Drawing.Size(125, 17);
            this.chbx24Hour.TabIndex = 20;
            this.chbx24Hour.Text = "Зміна довше 24 год";
            this.chbx24Hour.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbxLowPaper);
            this.groupBox1.Controls.Add(this.chbx24Hour);
            this.groupBox1.Controls.Add(this.chbxIsBlocked);
            this.groupBox1.Controls.Add(this.chbxShiftBeg);
            this.groupBox1.Controls.Add(this.chbxPrinErr);
            this.groupBox1.Controls.Add(this.chbx72hour);
            this.groupBox1.Controls.Add(this.chbxOutPaper);
            this.groupBox1.Controls.Add(this.chbxReceiptOpened);
            this.groupBox1.Controls.Add(this.chbxOpReg);
            this.groupBox1.Controls.Add(this.chbxDocOpnd);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 232);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Статус ФР";
            // 
            // chbxLowPaper
            // 
            this.chbxLowPaper.AutoSize = true;
            this.chbxLowPaper.Location = new System.Drawing.Point(3, 210);
            this.chbxLowPaper.Name = "chbxLowPaper";
            this.chbxLowPaper.Size = new System.Drawing.Size(91, 17);
            this.chbxLowPaper.TabIndex = 21;
            this.chbxLowPaper.Text = "Мало паперу";
            this.chbxLowPaper.UseVisualStyleBackColor = true;
            // 
            // groupBoxSSale
            // 
            this.groupBoxSSale.Controls.Add(this.cbxReturn);
            this.groupBoxSSale.Controls.Add(this.label13);
            this.groupBoxSSale.Controls.Add(this.txbSSale);
            this.groupBoxSSale.Controls.Add(this.btnSSale);
            this.groupBoxSSale.Controls.Add(this.chbIsCash);
            this.groupBoxSSale.Location = new System.Drawing.Point(6, 239);
            this.groupBoxSSale.Name = "groupBoxSSale";
            this.groupBoxSSale.Size = new System.Drawing.Size(179, 97);
            this.groupBoxSSale.TabIndex = 22;
            this.groupBoxSSale.TabStop = false;
            this.groupBoxSSale.Text = "Продаж";
            // 
            // cbxReturn
            // 
            this.cbxReturn.AutoSize = true;
            this.cbxReturn.Location = new System.Drawing.Point(6, 42);
            this.cbxReturn.Name = "cbxReturn";
            this.cbxReturn.Size = new System.Drawing.Size(88, 17);
            this.cbxReturn.TabIndex = 4;
            this.cbxReturn.Text = "Повернення";
            this.cbxReturn.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(59, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Сума:";
            // 
            // txbSSale
            // 
            this.txbSSale.Location = new System.Drawing.Point(101, 19);
            this.txbSSale.Name = "txbSSale";
            this.txbSSale.Size = new System.Drawing.Size(72, 20);
            this.txbSSale.TabIndex = 2;
            this.txbSSale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSSale_KeyPress);
            // 
            // btnSSale
            // 
            this.btnSSale.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSSale.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnSSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSSale.Location = new System.Drawing.Point(90, 65);
            this.btnSSale.Name = "btnSSale";
            this.btnSSale.Size = new System.Drawing.Size(83, 24);
            this.btnSSale.TabIndex = 1;
            this.btnSSale.Text = "Продаж";
            this.btnSSale.UseVisualStyleBackColor = false;
            this.btnSSale.Click += new System.EventHandler(this.btnSSale_Click);
            // 
            // chbIsCash
            // 
            this.chbIsCash.AutoSize = true;
            this.chbIsCash.Location = new System.Drawing.Point(6, 70);
            this.chbIsCash.Name = "chbIsCash";
            this.chbIsCash.Size = new System.Drawing.Size(81, 17);
            this.chbIsCash.TabIndex = 0;
            this.chbIsCash.Text = "Безготівка";
            this.chbIsCash.UseVisualStyleBackColor = true;
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tabPageService);
            this.Tabs.Controls.Add(this.tabPageFiscal);
            this.Tabs.Location = new System.Drawing.Point(1, -1);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(493, 370);
            this.Tabs.TabIndex = 22;
            // 
            // tabPageService
            // 
            this.tabPageService.BackColor = System.Drawing.Color.DarkGray;
            this.tabPageService.Controls.Add(this.groupBox3);
            this.tabPageService.Location = new System.Drawing.Point(4, 22);
            this.tabPageService.Name = "tabPageService";
            this.tabPageService.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageService.Size = new System.Drawing.Size(485, 344);
            this.tabPageService.TabIndex = 0;
            this.tabPageService.Text = "Service";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Location = new System.Drawing.Point(3, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 332);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fiscal Parameters";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUpdateAmount);
            this.groupBox2.Controls.Add(this.txbCashSerialized);
            this.groupBox2.Location = new System.Drawing.Point(6, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 84);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Готівка в сейфі (Серіалізовано)";
            // 
            // btnUpdateAmount
            // 
            this.btnUpdateAmount.Location = new System.Drawing.Point(35, 55);
            this.btnUpdateAmount.Name = "btnUpdateAmount";
            this.btnUpdateAmount.Size = new System.Drawing.Size(100, 23);
            this.btnUpdateAmount.TabIndex = 12;
            this.btnUpdateAmount.Text = "Оновити";
            this.btnUpdateAmount.UseVisualStyleBackColor = true;
            this.btnUpdateAmount.Click += new System.EventHandler(this.btnUpdateAmount_Click);
            // 
            // txbCashSerialized
            // 
            this.txbCashSerialized.Location = new System.Drawing.Point(35, 29);
            this.txbCashSerialized.Name = "txbCashSerialized";
            this.txbCashSerialized.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txbCashSerialized.Size = new System.Drawing.Size(100, 20);
            this.txbCashSerialized.TabIndex = 10;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnVoidDoc);
            this.groupBox7.Controls.Add(this.btnVoidRec);
            this.groupBox7.Location = new System.Drawing.Point(6, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(166, 54);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Others";
            // 
            // btnVoidDoc
            // 
            this.btnVoidDoc.Location = new System.Drawing.Point(82, 19);
            this.btnVoidDoc.Name = "btnVoidDoc";
            this.btnVoidDoc.Size = new System.Drawing.Size(75, 23);
            this.btnVoidDoc.TabIndex = 9;
            this.btnVoidDoc.Text = "VoidDoc";
            this.btnVoidDoc.UseVisualStyleBackColor = true;
            // 
            // btnVoidRec
            // 
            this.btnVoidRec.Location = new System.Drawing.Point(6, 19);
            this.btnVoidRec.Name = "btnVoidRec";
            this.btnVoidRec.Size = new System.Drawing.Size(75, 23);
            this.btnVoidRec.TabIndex = 8;
            this.btnVoidRec.Text = "VoidReceipt";
            this.btnVoidRec.UseVisualStyleBackColor = true;
            this.btnVoidRec.Click += new System.EventHandler(this.btnVoidRec_Click);
            // 
            // tabPageFiscal
            // 
            this.tabPageFiscal.BackColor = System.Drawing.Color.DarkGray;
            this.tabPageFiscal.Controls.Add(this.groupBox1);
            this.tabPageFiscal.Controls.Add(this.groupBoxFP);
            this.tabPageFiscal.Controls.Add(this.groupBoxSSale);
            this.tabPageFiscal.Location = new System.Drawing.Point(4, 22);
            this.tabPageFiscal.Name = "tabPageFiscal";
            this.tabPageFiscal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFiscal.Size = new System.Drawing.Size(485, 344);
            this.tabPageFiscal.TabIndex = 1;
            this.tabPageFiscal.Text = "Фіскальні функції";
            // 
            // ServiceForm_APM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(494, 370);
            this.Controls.Add(this.Tabs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceForm_APM";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додаткові операції";
            this.Load += new System.EventHandler(this.ServiceForm_APM_Load);
            this.groupBoxFP.ResumeLayout(false);
            this.groupBoxFP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxFPCashInOut.ResumeLayout(false);
            this.groupBoxFPCashInOut.PerformLayout();
            this.groupBoxFPRep.ResumeLayout(false);
            this.groupBoxFPRep.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxSSale.ResumeLayout(false);
            this.groupBoxSSale.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.tabPageService.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tabPageFiscal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnZRep;
        private System.Windows.Forms.Button btnXRep;
        private System.Windows.Forms.Button btnCashIn;
        private System.Windows.Forms.TextBox txbCashOut;
        private System.Windows.Forms.Button btnCashOut;
        private System.Windows.Forms.TextBox txbCashIN;
        private System.Windows.Forms.Button btnPrintZ;
        private System.Windows.Forms.GroupBox groupBoxFP;
        private System.Windows.Forms.GroupBox groupBoxFPCashInOut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxFPRep;
        private System.Windows.Forms.Button btnPrintRepNr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.TextBox txbRepNr;
        private System.Windows.Forms.Button btnTermReport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnNewShift;
        private System.Windows.Forms.Button btnCopyCheck;
        private System.Windows.Forms.CheckBox chbxIsBlocked;
        private System.Windows.Forms.CheckBox chbxShiftBeg;
        private System.Windows.Forms.CheckBox chbxReceiptOpened;
        private System.Windows.Forms.CheckBox chbxDocOpnd;
        private System.Windows.Forms.CheckBox chbxOpReg;
        private System.Windows.Forms.CheckBox chbxOutPaper;
        private System.Windows.Forms.CheckBox chbxPrinErr;
        private System.Windows.Forms.CheckBox chbx72hour;
        private System.Windows.Forms.CheckBox chbx24Hour;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbxLowPaper;
        private System.Windows.Forms.GroupBox groupBoxSSale;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txbSSale;
        private System.Windows.Forms.Button btnSSale;
        private System.Windows.Forms.CheckBox chbIsCash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbReceiptNr;
        private System.Windows.Forms.ComboBox cbxReportSel;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabPageService;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnVoidDoc;
        private System.Windows.Forms.Button btnVoidRec;
        private System.Windows.Forms.TabPage tabPageFiscal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUpdateAmount;
        private System.Windows.Forms.TextBox txbCashSerialized;
        private System.Windows.Forms.Button btnTimeSync;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbxReturn;
    }
}