using System.Windows.Forms;

namespace UA_Fiscal_Leocas
{
    partial class ServiceForm_Leocas200
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm_Leocas200));
            this.btnXRep = new System.Windows.Forms.Button();
            this.btnCashIn = new System.Windows.Forms.Button();
            this.txbCashOut = new System.Windows.Forms.TextBox();
            this.btnCashOut = new System.Windows.Forms.Button();
            this.txbCashIN = new System.Windows.Forms.TextBox();
            this.groupBoxFP = new System.Windows.Forms.GroupBox();
            this.groupBoxFPCashInOut = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxFPRep = new System.Windows.Forms.GroupBox();
            this.cbxReportSel = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txbCheckNr = new System.Windows.Forms.TextBox();
            this.btnCopyCheck = new System.Windows.Forms.Button();
            this.btnZRep = new System.Windows.Forms.Button();
            this.btnNewShift = new System.Windows.Forms.Button();
            this.btnTermReport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txbRepNr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.btnPrintRepNr = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbxDeviceList = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPrintBank = new System.Windows.Forms.Button();
            this.groupBoxCC = new System.Windows.Forms.GroupBox();
            this.chbPrintAll = new System.Windows.Forms.CheckBox();
            this.chbIsPrinted = new System.Windows.Forms.CheckBox();
            this.dgvDBTable = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TicketNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxTransactionTypeSelect = new System.Windows.Forms.ComboBox();
            this.btnSearchTicket = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.quantity = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txbTicketNR = new System.Windows.Forms.TextBox();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnUpdateAmount = new System.Windows.Forms.Button();
            this.txbCashSerialized = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnVoidDoc = new System.Windows.Forms.Button();
            this.btnVoidRec = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.timeToStartShift = new System.Windows.Forms.DateTimePicker();
            this.timeFromStartShift = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.timeFromEndShift = new System.Windows.Forms.DateTimePicker();
            this.timeToEndShift = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.CardTab = new System.Windows.Forms.TabPage();
            this.FiscTab = new System.Windows.Forms.TabPage();
            this.groupBoxSSale = new System.Windows.Forms.GroupBox();
            this.cbxReturn = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txbSSale = new System.Windows.Forms.TextBox();
            this.btnSSale = new System.Windows.Forms.Button();
            this.chbIsCash = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbxLowPaper = new System.Windows.Forms.CheckBox();
            this.chbx24Hour = new System.Windows.Forms.CheckBox();
            this.chbx72hour = new System.Windows.Forms.CheckBox();
            this.chbxPrinErr = new System.Windows.Forms.CheckBox();
            this.chbxIsBlocked = new System.Windows.Forms.CheckBox();
            this.chbxOutPaper = new System.Windows.Forms.CheckBox();
            this.chbxOpReg = new System.Windows.Forms.CheckBox();
            this.chbxDocOpnd = new System.Windows.Forms.CheckBox();
            this.chbxReceiptOpened = new System.Windows.Forms.CheckBox();
            this.chbxShiftBeg = new System.Windows.Forms.CheckBox();
            this.lblSysTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.btnTimeSync = new System.Windows.Forms.Button();
            this.groupBoxFP.SuspendLayout();
            this.groupBoxFPCashInOut.SuspendLayout();
            this.groupBoxFPRep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDBTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quantity)).BeginInit();
            this.Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.CardTab.SuspendLayout();
            this.FiscTab.SuspendLayout();
            this.groupBoxSSale.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnXRep
            // 
            this.btnXRep.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnXRep.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnXRep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXRep.Location = new System.Drawing.Point(10, 19);
            this.btnXRep.Name = "btnXRep";
            this.btnXRep.Size = new System.Drawing.Size(83, 23);
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
            this.txbCashOut.Location = new System.Drawing.Point(100, 48);
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
            this.txbCashIN.Location = new System.Drawing.Point(100, 18);
            this.txbCashIN.Name = "txbCashIN";
            this.txbCashIN.Size = new System.Drawing.Size(85, 24);
            this.txbCashIN.TabIndex = 1;
            this.txbCashIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbCashIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbCashIN_KeyPress);
            // 
            // groupBoxFP
            // 
            this.groupBoxFP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBoxFP.Controls.Add(this.groupBoxFPCashInOut);
            this.groupBoxFP.Controls.Add(this.groupBoxFPRep);
            this.groupBoxFP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBoxFP.Location = new System.Drawing.Point(277, 3);
            this.groupBoxFP.Name = "groupBoxFP";
            this.groupBoxFP.Size = new System.Drawing.Size(288, 325);
            this.groupBoxFP.TabIndex = 10;
            this.groupBoxFP.TabStop = false;
            this.groupBoxFP.Text = "Додаткові фіскальні функції";
            // 
            // groupBoxFPCashInOut
            // 
            this.groupBoxFPCashInOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFPCashInOut.Controls.Add(this.label5);
            this.groupBoxFPCashInOut.Controls.Add(this.label4);
            this.groupBoxFPCashInOut.Controls.Add(this.txbCashIN);
            this.groupBoxFPCashInOut.Controls.Add(this.btnCashOut);
            this.groupBoxFPCashInOut.Controls.Add(this.txbCashOut);
            this.groupBoxFPCashInOut.Controls.Add(this.btnCashIn);
            this.groupBoxFPCashInOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxFPCashInOut.Location = new System.Drawing.Point(6, 19);
            this.groupBoxFPCashInOut.Name = "groupBoxFPCashInOut";
            this.groupBoxFPCashInOut.Size = new System.Drawing.Size(276, 80);
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
            this.groupBoxFPRep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBoxFPRep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxFPRep.Controls.Add(this.cbxReportSel);
            this.groupBoxFPRep.Controls.Add(this.label14);
            this.groupBoxFPRep.Controls.Add(this.label12);
            this.groupBoxFPRep.Controls.Add(this.txbCheckNr);
            this.groupBoxFPRep.Controls.Add(this.btnCopyCheck);
            this.groupBoxFPRep.Controls.Add(this.btnZRep);
            this.groupBoxFPRep.Controls.Add(this.btnNewShift);
            this.groupBoxFPRep.Controls.Add(this.btnTermReport);
            this.groupBoxFPRep.Controls.Add(this.label3);
            this.groupBoxFPRep.Controls.Add(this.txbRepNr);
            this.groupBoxFPRep.Controls.Add(this.label2);
            this.groupBoxFPRep.Controls.Add(this.label1);
            this.groupBoxFPRep.Controls.Add(this.dateTimePickerTo);
            this.groupBoxFPRep.Controls.Add(this.dateTimePickerFrom);
            this.groupBoxFPRep.Controls.Add(this.btnPrintRepNr);
            this.groupBoxFPRep.Controls.Add(this.btnXRep);
            this.groupBoxFPRep.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxFPRep.Location = new System.Drawing.Point(6, 110);
            this.groupBoxFPRep.Name = "groupBoxFPRep";
            this.groupBoxFPRep.Size = new System.Drawing.Size(277, 207);
            this.groupBoxFPRep.TabIndex = 9;
            this.groupBoxFPRep.TabStop = false;
            this.groupBoxFPRep.Text = "Звіти";
            // 
            // cbxReportSel
            // 
            this.cbxReportSel.FormattingEnabled = true;
            this.cbxReportSel.Location = new System.Drawing.Point(174, 110);
            this.cbxReportSel.Name = "cbxReportSel";
            this.cbxReportSel.Size = new System.Drawing.Size(97, 21);
            this.cbxReportSel.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(106, 189);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(165, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "0 - останній роздрукований чек";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 168);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Номер чеку:";
            // 
            // txbCheckNr
            // 
            this.txbCheckNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txbCheckNr.Location = new System.Drawing.Point(107, 161);
            this.txbCheckNr.Name = "txbCheckNr";
            this.txbCheckNr.Size = new System.Drawing.Size(63, 24);
            this.txbCheckNr.TabIndex = 21;
            this.txbCheckNr.Text = "0";
            this.txbCheckNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCopyCheck
            // 
            this.btnCopyCheck.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCopyCheck.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCopyCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyCheck.Location = new System.Drawing.Point(174, 161);
            this.btnCopyCheck.Name = "btnCopyCheck";
            this.btnCopyCheck.Size = new System.Drawing.Size(97, 23);
            this.btnCopyCheck.TabIndex = 20;
            this.btnCopyCheck.Text = "Копия чеку";
            this.btnCopyCheck.UseVisualStyleBackColor = false;
            this.btnCopyCheck.Click += new System.EventHandler(this.btnCopyCheck_Click);
            // 
            // btnZRep
            // 
            this.btnZRep.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnZRep.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnZRep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZRep.Location = new System.Drawing.Point(188, 19);
            this.btnZRep.Name = "btnZRep";
            this.btnZRep.Size = new System.Drawing.Size(83, 23);
            this.btnZRep.TabIndex = 19;
            this.btnZRep.Text = "Z-Звіт";
            this.btnZRep.UseVisualStyleBackColor = false;
            this.btnZRep.Click += new System.EventHandler(this.btnZRep_Click);
            // 
            // btnNewShift
            // 
            this.btnNewShift.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnNewShift.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnNewShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewShift.Location = new System.Drawing.Point(99, 19);
            this.btnNewShift.Name = "btnNewShift";
            this.btnNewShift.Size = new System.Drawing.Size(83, 23);
            this.btnNewShift.TabIndex = 17;
            this.btnNewShift.Text = "Нова зміна";
            this.btnNewShift.UseVisualStyleBackColor = false;
            this.btnNewShift.Click += new System.EventHandler(this.btnNewShift_Click);
            // 
            // btnTermReport
            // 
            this.btnTermReport.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnTermReport.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnTermReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTermReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTermReport.Location = new System.Drawing.Point(174, 134);
            this.btnTermReport.Name = "btnTermReport";
            this.btnTermReport.Size = new System.Drawing.Size(97, 23);
            this.btnTermReport.TabIndex = 16;
            this.btnTermReport.Text = "Період. звіт";
            this.btnTermReport.UseVisualStyleBackColor = false;
            this.btnTermReport.Click += new System.EventHandler(this.btnTermReport_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Номер Z-звіту:";
            // 
            // txbRepNr
            // 
            this.txbRepNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txbRepNr.Location = new System.Drawing.Point(106, 80);
            this.txbRepNr.Name = "txbRepNr";
            this.txbRepNr.Size = new System.Drawing.Size(64, 24);
            this.txbRepNr.TabIndex = 14;
            this.txbRepNr.Text = "1";
            this.txbRepNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "До:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "З:";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dateTimePickerTo.Location = new System.Drawing.Point(31, 137);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(139, 20);
            this.dateTimePickerTo.TabIndex = 11;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dateTimePickerFrom.Location = new System.Drawing.Point(31, 111);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(139, 20);
            this.dateTimePickerFrom.TabIndex = 10;
            // 
            // btnPrintRepNr
            // 
            this.btnPrintRepNr.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnPrintRepNr.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrintRepNr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintRepNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPrintRepNr.Location = new System.Drawing.Point(174, 80);
            this.btnPrintRepNr.Name = "btnPrintRepNr";
            this.btnPrintRepNr.Size = new System.Drawing.Size(97, 23);
            this.btnPrintRepNr.TabIndex = 9;
            this.btnPrintRepNr.Text = "Друк Z по №";
            this.btnPrintRepNr.UseVisualStyleBackColor = false;
            this.btnPrintRepNr.Click += new System.EventHandler(this.btnPrintRepNr_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UA_Fiscal_Leocas.Properties.Resources.periprotect;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(364, 365);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 48);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbxDeviceList
            // 
            this.cbxDeviceList.FormattingEnabled = true;
            this.cbxDeviceList.Location = new System.Drawing.Point(6, 34);
            this.cbxDeviceList.Name = "cbxDeviceList";
            this.cbxDeviceList.Size = new System.Drawing.Size(52, 21);
            this.cbxDeviceList.TabIndex = 0;
            this.cbxDeviceList.SelectedIndexChanged += new System.EventHandler(this.cbxDeviceList_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Пристрій";
            // 
            // btnPrintBank
            // 
            this.btnPrintBank.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnPrintBank.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrintBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPrintBank.Location = new System.Drawing.Point(27, 299);
            this.btnPrintBank.Name = "btnPrintBank";
            this.btnPrintBank.Size = new System.Drawing.Size(88, 25);
            this.btnPrintBank.TabIndex = 6;
            this.btnPrintBank.Text = "Друк";
            this.btnPrintBank.UseVisualStyleBackColor = false;
            this.btnPrintBank.Click += new System.EventHandler(this.btnPrintBank_Click);
            // 
            // groupBoxCC
            // 
            this.groupBoxCC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCC.BackColor = System.Drawing.Color.DarkGray;
            this.groupBoxCC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBoxCC.Controls.Add(this.chbPrintAll);
            this.groupBoxCC.Controls.Add(this.chbIsPrinted);
            this.groupBoxCC.Controls.Add(this.dgvDBTable);
            this.groupBoxCC.Controls.Add(this.label11);
            this.groupBoxCC.Controls.Add(this.cbxTransactionTypeSelect);
            this.groupBoxCC.Controls.Add(this.btnSearchTicket);
            this.groupBoxCC.Controls.Add(this.label9);
            this.groupBoxCC.Controls.Add(this.label8);
            this.groupBoxCC.Controls.Add(this.btnPrintBank);
            this.groupBoxCC.Controls.Add(this.quantity);
            this.groupBoxCC.Controls.Add(this.label10);
            this.groupBoxCC.Controls.Add(this.txbTicketNR);
            this.groupBoxCC.Controls.Add(this.label7);
            this.groupBoxCC.Controls.Add(this.cbxDeviceList);
            this.groupBoxCC.Location = new System.Drawing.Point(3, 0);
            this.groupBoxCC.Name = "groupBoxCC";
            this.groupBoxCC.Size = new System.Drawing.Size(565, 330);
            this.groupBoxCC.TabIndex = 9;
            this.groupBoxCC.TabStop = false;
            this.groupBoxCC.Text = "Звіти банківського терміналу";
            // 
            // chbPrintAll
            // 
            this.chbPrintAll.AutoSize = true;
            this.chbPrintAll.Location = new System.Drawing.Point(5, 241);
            this.chbPrintAll.Name = "chbPrintAll";
            this.chbPrintAll.Size = new System.Drawing.Size(97, 17);
            this.chbPrintAll.TabIndex = 18;
            this.chbPrintAll.Text = "Друкувати всі";
            this.chbPrintAll.UseVisualStyleBackColor = true;
            // 
            // chbIsPrinted
            // 
            this.chbIsPrinted.AutoSize = true;
            this.chbIsPrinted.Location = new System.Drawing.Point(6, 112);
            this.chbIsPrinted.Name = "chbIsPrinted";
            this.chbIsPrinted.Size = new System.Drawing.Size(98, 17);
            this.chbIsPrinted.TabIndex = 17;
            this.chbIsPrinted.Text = "Роздруковано";
            this.chbIsPrinted.UseVisualStyleBackColor = true;
            this.chbIsPrinted.CheckStateChanged += new System.EventHandler(this.chbIsPrinted_CheckStateChanged);
            // 
            // dgvDBTable
            // 
            this.dgvDBTable.AllowUserToAddRows = false;
            this.dgvDBTable.AllowUserToDeleteRows = false;
            this.dgvDBTable.AllowUserToResizeColumns = false;
            this.dgvDBTable.AllowUserToResizeRows = false;
            this.dgvDBTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDBTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.DeviceID,
            this.TicketNR,
            this.Date_Time,
            this.CardNR});
            this.dgvDBTable.Location = new System.Drawing.Point(144, 32);
            this.dgvDBTable.Name = "dgvDBTable";
            this.dgvDBTable.ReadOnly = true;
            this.dgvDBTable.RowHeadersVisible = false;
            this.dgvDBTable.Size = new System.Drawing.Size(417, 292);
            this.dgvDBTable.TabIndex = 16;
            this.dgvDBTable.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDBTable_CellContentClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ID.Width = 21;
            // 
            // DeviceID
            // 
            this.DeviceID.HeaderText = "DeviceID";
            this.DeviceID.MinimumWidth = 2;
            this.DeviceID.Name = "DeviceID";
            this.DeviceID.ReadOnly = true;
            this.DeviceID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeviceID.Width = 21;
            // 
            // TicketNR
            // 
            this.TicketNR.HeaderText = "TicketNR";
            this.TicketNR.Name = "TicketNR";
            this.TicketNR.ReadOnly = true;
            this.TicketNR.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TicketNR.Width = 151;
            // 
            // Date_Time
            // 
            this.Date_Time.HeaderText = "DateTime";
            this.Date_Time.Name = "Date_Time";
            this.Date_Time.ReadOnly = true;
            this.Date_Time.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Date_Time.Width = 109;
            // 
            // CardNR
            // 
            this.CardNR.HeaderText = "CardNR";
            this.CardNR.Name = "CardNR";
            this.CardNR.ReadOnly = true;
            this.CardNR.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CardNR.Width = 112;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Тип транзакції";
            // 
            // cbxTransactionTypeSelect
            // 
            this.cbxTransactionTypeSelect.AllowDrop = true;
            this.cbxTransactionTypeSelect.FormattingEnabled = true;
            this.cbxTransactionTypeSelect.Location = new System.Drawing.Point(6, 85);
            this.cbxTransactionTypeSelect.Name = "cbxTransactionTypeSelect";
            this.cbxTransactionTypeSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbxTransactionTypeSelect.Size = new System.Drawing.Size(129, 21);
            this.cbxTransactionTypeSelect.TabIndex = 14;
            this.cbxTransactionTypeSelect.SelectedIndexChanged += new System.EventHandler(this.cbxTransactionTypeSelect_SelectedIndexChanged);
            // 
            // btnSearchTicket
            // 
            this.btnSearchTicket.Location = new System.Drawing.Point(81, 202);
            this.btnSearchTicket.Name = "btnSearchTicket";
            this.btnSearchTicket.Size = new System.Drawing.Size(54, 23);
            this.btnSearchTicket.TabIndex = 12;
            this.btnSearchTicket.Text = "Пошук";
            this.btnSearchTicket.UseVisualStyleBackColor = true;
            this.btnSearchTicket.Click += new System.EventHandler(this.btnSearchTicket_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(151, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Чек(-и)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 261);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Кількість:";
            // 
            // quantity
            // 
            this.quantity.Location = new System.Drawing.Point(67, 259);
            this.quantity.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.quantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.quantity.Name = "quantity";
            this.quantity.Size = new System.Drawing.Size(29, 20);
            this.quantity.TabIndex = 7;
            this.quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.quantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Пошук за номером квитка";
            // 
            // txbTicketNR
            // 
            this.txbTicketNR.Location = new System.Drawing.Point(6, 176);
            this.txbTicketNR.Name = "txbTicketNR";
            this.txbTicketNR.Size = new System.Drawing.Size(129, 20);
            this.txbTicketNR.TabIndex = 10;
            this.txbTicketNR.Text = "введіть номер квитка...";
            this.txbTicketNR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbTicketNR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbTicketNR_KeyPress);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Controls.Add(this.CardTab);
            this.Tabs.Controls.Add(this.FiscTab);
            this.Tabs.Location = new System.Drawing.Point(-3, -1);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(579, 360);
            this.Tabs.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 334);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Service";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 328);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service Assistance";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.btnUpdate);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(4, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 302);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fiscal Parameters";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnUpdateAmount);
            this.groupBox8.Controls.Add(this.txbCashSerialized);
            this.groupBox8.Location = new System.Drawing.Point(6, 230);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(166, 66);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Готівка в сейфі (Серіалізовано)";
            // 
            // btnUpdateAmount
            // 
            this.btnUpdateAmount.Location = new System.Drawing.Point(82, 38);
            this.btnUpdateAmount.Name = "btnUpdateAmount";
            this.btnUpdateAmount.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateAmount.TabIndex = 12;
            this.btnUpdateAmount.Text = "Оновити";
            this.btnUpdateAmount.UseVisualStyleBackColor = true;
            this.btnUpdateAmount.Click += new System.EventHandler(this.btnUpdateAmount_Click);
            // 
            // txbCashSerialized
            // 
            this.txbCashSerialized.Location = new System.Drawing.Point(6, 40);
            this.txbCashSerialized.Name = "txbCashSerialized";
            this.txbCashSerialized.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txbCashSerialized.Size = new System.Drawing.Size(75, 20);
            this.txbCashSerialized.TabIndex = 10;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(96, 15);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnVoidDoc);
            this.groupBox7.Controls.Add(this.btnVoidRec);
            this.groupBox7.Location = new System.Drawing.Point(6, 172);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(165, 52);
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
            this.btnVoidDoc.Click += new System.EventHandler(this.btnVoidDoc_Click);
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
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.timeToStartShift);
            this.groupBox6.Controls.Add(this.timeFromStartShift);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Location = new System.Drawing.Point(6, 109);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(165, 57);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Shift Start Time";
            // 
            // timeToStartShift
            // 
            this.timeToStartShift.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeToStartShift.Location = new System.Drawing.Point(88, 32);
            this.timeToStartShift.Name = "timeToStartShift";
            this.timeToStartShift.Size = new System.Drawing.Size(65, 20);
            this.timeToStartShift.TabIndex = 3;
            // 
            // timeFromStartShift
            // 
            this.timeFromStartShift.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeFromStartShift.Location = new System.Drawing.Point(7, 32);
            this.timeFromStartShift.Name = "timeFromStartShift";
            this.timeFromStartShift.Size = new System.Drawing.Size(64, 20);
            this.timeFromStartShift.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(88, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(20, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "To";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(30, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "From";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.timeFromEndShift);
            this.groupBox5.Controls.Add(this.timeToEndShift);
            this.groupBox5.Location = new System.Drawing.Point(6, 42);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(165, 61);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Shift Close Time";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "From";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(88, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "To";
            // 
            // timeFromEndShift
            // 
            this.timeFromEndShift.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeFromEndShift.Location = new System.Drawing.Point(6, 33);
            this.timeFromEndShift.Name = "timeFromEndShift";
            this.timeFromEndShift.Size = new System.Drawing.Size(65, 20);
            this.timeFromEndShift.TabIndex = 2;
            // 
            // timeToEndShift
            // 
            this.timeToEndShift.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeToEndShift.Location = new System.Drawing.Point(88, 33);
            this.timeToEndShift.Name = "timeToEndShift";
            this.timeToEndShift.Size = new System.Drawing.Size(65, 20);
            this.timeToEndShift.TabIndex = 3;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Auto On/Off";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CardTab
            // 
            this.CardTab.BackColor = System.Drawing.Color.DarkGray;
            this.CardTab.Controls.Add(this.groupBoxCC);
            this.CardTab.Location = new System.Drawing.Point(4, 22);
            this.CardTab.Name = "CardTab";
            this.CardTab.Padding = new System.Windows.Forms.Padding(3);
            this.CardTab.Size = new System.Drawing.Size(571, 334);
            this.CardTab.TabIndex = 0;
            this.CardTab.Text = "Банківські Карти";
            // 
            // FiscTab
            // 
            this.FiscTab.BackColor = System.Drawing.Color.DarkGray;
            this.FiscTab.Controls.Add(this.groupBoxSSale);
            this.FiscTab.Controls.Add(this.groupBox1);
            this.FiscTab.Controls.Add(this.groupBoxFP);
            this.FiscTab.Location = new System.Drawing.Point(4, 22);
            this.FiscTab.Name = "FiscTab";
            this.FiscTab.Padding = new System.Windows.Forms.Padding(3);
            this.FiscTab.Size = new System.Drawing.Size(571, 334);
            this.FiscTab.TabIndex = 1;
            this.FiscTab.Text = "Фіскальні функції";
            // 
            // groupBoxSSale
            // 
            this.groupBoxSSale.Controls.Add(this.cbxReturn);
            this.groupBoxSSale.Controls.Add(this.label13);
            this.groupBoxSSale.Controls.Add(this.txbSSale);
            this.groupBoxSSale.Controls.Add(this.btnSSale);
            this.groupBoxSSale.Controls.Add(this.chbIsCash);
            this.groupBoxSSale.Location = new System.Drawing.Point(7, 257);
            this.groupBoxSSale.Name = "groupBoxSSale";
            this.groupBoxSSale.Size = new System.Drawing.Size(264, 71);
            this.groupBoxSSale.TabIndex = 14;
            this.groupBoxSSale.TabStop = false;
            this.groupBoxSSale.Text = "Продаж";
            // 
            // cbxReturn
            // 
            this.cbxReturn.AutoSize = true;
            this.cbxReturn.Location = new System.Drawing.Point(47, 48);
            this.cbxReturn.Name = "cbxReturn";
            this.cbxReturn.Size = new System.Drawing.Size(88, 17);
            this.cbxReturn.TabIndex = 4;
            this.cbxReturn.Text = "Повернення";
            this.cbxReturn.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(44, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Сума:";
            // 
            // txbSSale
            // 
            this.txbSSale.Location = new System.Drawing.Point(86, 15);
            this.txbSSale.Name = "txbSSale";
            this.txbSSale.Size = new System.Drawing.Size(83, 20);
            this.txbSSale.TabIndex = 2;
            this.txbSSale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSSale_KeyPress);
            // 
            // btnSSale
            // 
            this.btnSSale.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSSale.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnSSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSSale.Location = new System.Drawing.Point(175, 12);
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
            this.chbIsCash.Location = new System.Drawing.Point(175, 48);
            this.chbIsCash.Name = "chbIsCash";
            this.chbIsCash.Size = new System.Drawing.Size(81, 17);
            this.chbIsCash.TabIndex = 0;
            this.chbIsCash.Text = "Безготівка";
            this.chbIsCash.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbxLowPaper);
            this.groupBox1.Controls.Add(this.chbx24Hour);
            this.groupBox1.Controls.Add(this.chbx72hour);
            this.groupBox1.Controls.Add(this.chbxPrinErr);
            this.groupBox1.Controls.Add(this.chbxIsBlocked);
            this.groupBox1.Controls.Add(this.chbxOutPaper);
            this.groupBox1.Controls.Add(this.chbxOpReg);
            this.groupBox1.Controls.Add(this.chbxDocOpnd);
            this.groupBox1.Controls.Add(this.chbxReceiptOpened);
            this.groupBox1.Controls.Add(this.chbxShiftBeg);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 250);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Статус ФР";
            // 
            // chbxLowPaper
            // 
            this.chbxLowPaper.AutoSize = true;
            this.chbxLowPaper.Location = new System.Drawing.Point(5, 227);
            this.chbxLowPaper.Name = "chbxLowPaper";
            this.chbxLowPaper.Size = new System.Drawing.Size(91, 17);
            this.chbxLowPaper.TabIndex = 9;
            this.chbxLowPaper.Text = "Мало паперу";
            this.chbxLowPaper.UseVisualStyleBackColor = true;
            // 
            // chbx24Hour
            // 
            this.chbx24Hour.AutoSize = true;
            this.chbx24Hour.Location = new System.Drawing.Point(5, 204);
            this.chbx24Hour.Name = "chbx24Hour";
            this.chbx24Hour.Size = new System.Drawing.Size(125, 17);
            this.chbx24Hour.TabIndex = 8;
            this.chbx24Hour.Text = "Зміна довше 24 год";
            this.chbx24Hour.UseVisualStyleBackColor = true;
            // 
            // chbx72hour
            // 
            this.chbx72hour.AutoSize = true;
            this.chbx72hour.Location = new System.Drawing.Point(5, 181);
            this.chbx72hour.Name = "chbx72hour";
            this.chbx72hour.Size = new System.Drawing.Size(139, 17);
            this.chbx72hour.TabIndex = 7;
            this.chbx72hour.Text = "Блокування 72 години";
            this.chbx72hour.UseVisualStyleBackColor = true;
            // 
            // chbxPrinErr
            // 
            this.chbxPrinErr.AutoSize = true;
            this.chbxPrinErr.Location = new System.Drawing.Point(5, 158);
            this.chbxPrinErr.Name = "chbxPrinErr";
            this.chbxPrinErr.Size = new System.Drawing.Size(122, 17);
            this.chbxPrinErr.TabIndex = 6;
            this.chbxPrinErr.Text = "Помилка принтера";
            this.chbxPrinErr.UseVisualStyleBackColor = true;
            // 
            // chbxIsBlocked
            // 
            this.chbxIsBlocked.AutoSize = true;
            this.chbxIsBlocked.Location = new System.Drawing.Point(5, 135);
            this.chbxIsBlocked.Name = "chbxIsBlocked";
            this.chbxIsBlocked.Size = new System.Drawing.Size(117, 17);
            this.chbxIsBlocked.TabIndex = 5;
            this.chbxIsBlocked.Text = "РРО заблоковано";
            this.chbxIsBlocked.UseVisualStyleBackColor = true;
            // 
            // chbxOutPaper
            // 
            this.chbxOutPaper.AutoSize = true;
            this.chbxOutPaper.Location = new System.Drawing.Point(5, 112);
            this.chbxOutPaper.Name = "chbxOutPaper";
            this.chbxOutPaper.Size = new System.Drawing.Size(120, 17);
            this.chbxOutPaper.TabIndex = 4;
            this.chbxOutPaper.Text = "Закінчення паперу";
            this.chbxOutPaper.UseVisualStyleBackColor = true;
            // 
            // chbxOpReg
            // 
            this.chbxOpReg.AutoSize = true;
            this.chbxOpReg.Location = new System.Drawing.Point(5, 89);
            this.chbxOpReg.Name = "chbxOpReg";
            this.chbxOpReg.Size = new System.Drawing.Size(161, 17);
            this.chbxOpReg.TabIndex = 3;
            this.chbxOpReg.Text = "Оператор зареєстрований";
            this.chbxOpReg.UseVisualStyleBackColor = true;
            // 
            // chbxDocOpnd
            // 
            this.chbxDocOpnd.AutoSize = true;
            this.chbxDocOpnd.Location = new System.Drawing.Point(5, 66);
            this.chbxDocOpnd.Name = "chbxDocOpnd";
            this.chbxDocOpnd.Size = new System.Drawing.Size(163, 17);
            this.chbxDocOpnd.TabIndex = 2;
            this.chbxDocOpnd.Text = "Відкрито нефіскальний чек";
            this.chbxDocOpnd.UseVisualStyleBackColor = true;
            // 
            // chbxReceiptOpened
            // 
            this.chbxReceiptOpened.AutoSize = true;
            this.chbxReceiptOpened.Location = new System.Drawing.Point(5, 43);
            this.chbxReceiptOpened.Name = "chbxReceiptOpened";
            this.chbxReceiptOpened.Size = new System.Drawing.Size(151, 17);
            this.chbxReceiptOpened.TabIndex = 1;
            this.chbxReceiptOpened.Text = "Відкрито фіскальний чек";
            this.chbxReceiptOpened.UseVisualStyleBackColor = true;
            // 
            // chbxShiftBeg
            // 
            this.chbxShiftBeg.AutoSize = true;
            this.chbxShiftBeg.Location = new System.Drawing.Point(5, 20);
            this.chbxShiftBeg.Name = "chbxShiftBeg";
            this.chbxShiftBeg.Size = new System.Drawing.Size(101, 17);
            this.chbxShiftBeg.TabIndex = 0;
            this.chbxShiftBeg.Text = "Зміна відкрита";
            this.chbxShiftBeg.UseVisualStyleBackColor = true;
            // 
            // lblSysTime
            // 
            this.lblSysTime.AutoSize = true;
            this.lblSysTime.Location = new System.Drawing.Point(93, 397);
            this.lblSysTime.Name = "lblSysTime";
            this.lblSysTime.Size = new System.Drawing.Size(49, 13);
            this.lblSysTime.TabIndex = 20;
            this.lblSysTime.Text = "00:00:00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 396);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Системний час:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 365);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 13);
            this.label19.TabIndex = 22;
            this.label19.Text = "Готівка в касі:";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Location = new System.Drawing.Point(85, 365);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(19, 13);
            this.lblCash.TabIndex = 24;
            this.lblCash.Text = "00";
            // 
            // btnTimeSync
            // 
            this.btnTimeSync.Location = new System.Drawing.Point(148, 391);
            this.btnTimeSync.Name = "btnTimeSync";
            this.btnTimeSync.Size = new System.Drawing.Size(95, 23);
            this.btnTimeSync.TabIndex = 25;
            this.btnTimeSync.Text = "Синхронизація";
            this.btnTimeSync.UseVisualStyleBackColor = true;
            this.btnTimeSync.Click += new System.EventHandler(this.btnTimeSync_Click);
            // 
            // ServiceForm_Leocas200
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(580, 419);
            this.Controls.Add(this.btnTimeSync);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblSysTime);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceForm_Leocas200";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додаткові операції";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ServiceForm_Leocas200_Load);
            this.groupBoxFP.ResumeLayout(false);
            this.groupBoxFPCashInOut.ResumeLayout(false);
            this.groupBoxFPCashInOut.PerformLayout();
            this.groupBoxFPRep.ResumeLayout(false);
            this.groupBoxFPRep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxCC.ResumeLayout(false);
            this.groupBoxCC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDBTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quantity)).EndInit();
            this.Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.CardTab.ResumeLayout(false);
            this.FiscTab.ResumeLayout(false);
            this.groupBoxSSale.ResumeLayout(false);
            this.groupBoxSSale.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnXRep;
        private System.Windows.Forms.Button btnCashIn;
        private System.Windows.Forms.TextBox txbCashOut;
        private System.Windows.Forms.Button btnCashOut;
        private System.Windows.Forms.TextBox txbCashIN;
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
        private System.Windows.Forms.ComboBox cbxDeviceList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPrintBank;
        private System.Windows.Forms.GroupBox groupBoxCC;
        private System.Windows.Forms.Button btnSearchTicket;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txbTicketNR;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown quantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxTransactionTypeSelect;
        private System.Windows.Forms.CheckBox chbIsPrinted;
        private System.Windows.Forms.DataGridView dgvDBTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TicketNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNR;
        private System.Windows.Forms.Button btnZRep;
        private System.Windows.Forms.CheckBox chbPrintAll;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txbCheckNr;
        private System.Windows.Forms.Button btnCopyCheck;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage CardTab;
        private System.Windows.Forms.TabPage FiscTab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbxLowPaper;
        private System.Windows.Forms.CheckBox chbx24Hour;
        private System.Windows.Forms.CheckBox chbx72hour;
        private System.Windows.Forms.CheckBox chbxPrinErr;
        private System.Windows.Forms.CheckBox chbxIsBlocked;
        private System.Windows.Forms.CheckBox chbxOutPaper;
        private System.Windows.Forms.CheckBox chbxOpReg;
        private System.Windows.Forms.CheckBox chbxDocOpnd;
        private System.Windows.Forms.CheckBox chbxReceiptOpened;
        private System.Windows.Forms.CheckBox chbxShiftBeg;
        private System.Windows.Forms.Label lblSysTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxSSale;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txbSSale;
        private System.Windows.Forms.Button btnSSale;
        private System.Windows.Forms.CheckBox chbIsCash;
        private Label label14;
        private ComboBox cbxReportSel;
        private TabPage tabPage1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox6;
        private DateTimePicker timeToStartShift;
        private DateTimePicker timeFromStartShift;
        private Label label18;
        private Label label17;
        private GroupBox groupBox5;
        private Label label15;
        private Label label16;
        private DateTimePicker timeFromEndShift;
        private DateTimePicker timeToEndShift;
        private CheckBox checkBox1;
        private Button btnUpdate;
        private Label label19;
        private Label lblCash;
        private GroupBox groupBox7;
        private Button btnVoidRec;
        private Button btnVoidDoc;
        private GroupBox groupBox8;
        private Button btnUpdateAmount;
        private TextBox txbCashSerialized;
        private CheckBox cbxReturn;
        private Button btnTimeSync;

    }
}