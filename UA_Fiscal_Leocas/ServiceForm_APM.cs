using SkiData.FiscalDevices;
using System;
using System.Windows.Forms;

namespace UA_Fiscal_Leocas
{
    public partial class ServiceForm_APM : Form
    {
        public delegate void StatusChanged(bool isready, int errorCode, string errorMessage);
        public event StatusChanged StatusChangedEvent;
        private LeoCasLib printer;
        UA_Fiscal_APM fd;
        string MachineId;
        public delegate void ErrorCheckHandler();
        public event ErrorCheckHandler ErrorCheckEvent;
        ConfirmationWindow cw;
        string errStr = "";

        private void OnStatusChangedEvent(bool isready, int errorCode, string errorMessage)
        {
            if (this.StatusChangedEvent != null)
                this.StatusChangedEvent(isready, errorCode, errorMessage);
        }

        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public ServiceForm_APM(LeoCasLib printer, string machineID, UA_Fiscal_APM fd)
        {
            InitializeComponent();
            this.printer = printer;
            MachineId = machineID;
            this.fd = fd;
            printer.GetStatusEx();
            UpdateStatus();
        }

        private void ServiceForm_APM_Load(object sender, EventArgs e)
        {
            timer1.Start();
            btnPrintRepNr.Enabled = true;
            btnTermReport.Enabled = true;
            btnCopyCheck.Enabled = true;
            btnZRep.Enabled = true;
            txbRepNr.Enabled = true;
            dateTimePickerFrom.Enabled = true;
            groupBoxSSale.Enabled = true;
            dateTimePickerTo.Enabled = true;
            dateTimePickerTo.Value = DateTime.Today.AddDays(-1);
            dateTimePickerFrom.Value = new DateTime(dateTimePickerTo.Value.Year, dateTimePickerTo.Value.Month, 1);
            chbx24Hour.Enabled = false;
            chbx72hour.Enabled = false;
            chbxDocOpnd.Enabled = false;
            chbxIsBlocked.Enabled = false;
            chbxOpReg.Enabled = false;
            chbxOutPaper.Enabled = false;
            chbxPrinErr.Enabled = false;
            chbxReceiptOpened.Enabled = false;
            chbxShiftBeg.Enabled = false;
            chbxLowPaper.Enabled = false;
            Tabs.SelectedTab = tabPageFiscal;
            Transaction tr = new Transaction();
            txbCashSerialized.Text = (tr.Get() / 100).ToString();
            cbxReportSel.Items.Add(new Item("Повний", 2));
            cbxReportSel.Items.Add(new Item("Скороч.", 3));
            cbxReportSel.SelectedIndex = 1;
            UpdateStatus();
            if (LeoCasLib.shiftStartedStatus)
            {
                btnNewShift.Enabled = false;
            }
            else
            {
                btnNewShift.Enabled = true;
                btnXRep.Enabled = false;
                btnCashIn.Enabled = false;
                btnCashOut.Enabled = false;
            }
        }

        private void UpdateStatus()
        {
            Logger log = new Logger(UA_Fiscal_APM.MachineID);
            printer.GetStatus();
            chbx24Hour.Checked = LeoCasLib.blockedDueTo24;
            chbxShiftBeg.Checked = LeoCasLib.shiftStartedStatus;
            chbxReceiptOpened.Checked = LeoCasLib.fiscReceiptBegStatus;
            chbxIsBlocked.Checked = LeoCasLib.blockedStatus;
            chbxOutPaper.Checked = LeoCasLib.outOfPaperStatus;
            chbxOpReg.Checked = LeoCasLib.operRegStatus;
            chbxLowPaper.Checked = LeoCasLib.lowPaperStatus;
            if (LeoCasLib.shiftStartedStatus)
            {
                btnNewShift.Enabled = false;
                btnCashIn.Enabled = true;
                btnXRep.Enabled = true;
                btnCashOut.Enabled = true;
            }
            else
            {
                btnNewShift.Enabled = true;
                btnXRep.Enabled = false;
                btnCashIn.Enabled = false;
                btnCashOut.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXRep_Click(object sender, EventArgs e)
        {
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.PrintRep(0);
                if (err != 0)
                {

                    //MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: Print XRep error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
                else
                {
                    OnStatusChangedEvent(true, 0, "Ok");
                    printer.GetStatusEx();
                }
                UpdateStatus();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: Print XRep exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            log.Write($"FDFS: Print XRep result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
        }

        private void cw_NumReadyEvent(byte type, bool isOk)
        {
            this.Enabled = false;
            bool receiptDone = true;
            Logger log = new Logger(MachineId);
            if (isOk)
            {
                if (type == 0)
                {
                    try
                    {
                        uint err = printer.BegChk();
                        if(err !=0)
                        {
                            log.Write($"FDFS: CashIn (Begin Receipt) error: {err}");
                            receiptDone = fd.ErrorAnalizer(err);
                        }
                        printer.TextChk($"Платіжна станція: {MachineId}");
                        double sum = double.Parse(txbCashIN.Text.Replace('.', ',')) * 100;
                        if (fd.deviceState.FiscalDeviceReady & receiptDone)
                        {
                            err = printer.InOut(1, type, Convert.ToUInt32(sum));
                            if(err !=0)
                            {
                                log.Write($"FDFS: CashIn (InOut) error: {err}");
                                receiptDone &= fd.ErrorAnalizer(err);
                            }
                        }
                        if (fd.deviceState.FiscalDeviceReady & receiptDone)
                        {
                            err = printer.EndChk();
                            if (err != 0)
                            {
                                log.Write($"FDFS: CashIn (Close Receipt) error: {err}");
                                receiptDone &= fd.ErrorAnalizer(err);
                            }
                        }
                        if (!fd.deviceState.FiscalDeviceReady)
                            printer.VoidChk();
                        if (fd.deviceState.FiscalDeviceReady & receiptDone)
                        {
                            Transaction tr = new Transaction(3, Convert.ToUInt32(sum));
                            tr.UpdateData(tr);
                        }
                        log.Write($"FDFS: CashIn amount: {sum}, result: {fd.deviceState.FiscalDeviceReady & receiptDone}");
                    }
                    catch (Exception ex)
                    {
                        printer.VoidChk();
                        log.Write($"FDFS: CashIn exception: {ex.Message}");
                        OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                        UpdateStatus();
                        this.Enabled = true;
                        MessageBox.Show(new Form { TopMost = true }, "Помилка!!!\n" + ex.Message);
                    }
                }
                if (type == 1)
                {
                    try
                    {
                        uint err = printer.BegChk();
                        if (err != 0)
                        {
                            log.Write($"FDFS: CashOut (Begin Receipt) error: {err}");
                            receiptDone = fd.ErrorAnalizer(err);
                        }
                        printer.TextChk($"Платіжна станція: {MachineId}");
                        double sum = double.Parse(txbCashOut.Text.Replace('.', ',')) * 100;
                        if (fd.deviceState.FiscalDeviceReady & receiptDone)
                        {
                            err = printer.InOut(1, type, Convert.ToUInt32(sum));
                            if (err != 0)
                            {
                                log.Write($"FDFS: CashOut (InOut) error: {err}");
                                receiptDone &= fd.ErrorAnalizer(err);
                            }
                        }
                        if (fd.deviceState.FiscalDeviceReady & receiptDone)
                        {
                            err = printer.EndChk();
                            if (err != 0)
                            {
                                log.Write($"FDFS: CashOut (Close Receipt) error: {err}");
                                receiptDone &= fd.ErrorAnalizer(err);
                            }
                        }
                        if (!fd.deviceState.FiscalDeviceReady)
                            printer.VoidChk();
                        
                        if (fd.deviceState.FiscalDeviceReady & receiptDone)
                        {
                            Transaction tr = new Transaction(4, Convert.ToUInt32(sum));
                            tr.UpdateData(tr);
                        }
                        log.Write($"FDFS: CashOut amount: {sum}, result: {fd.deviceState.FiscalDeviceReady & receiptDone}");
                    }
                    catch (Exception ex)
                    {
                        printer.VoidChk();
                        log.Write($"FDFS: CashOut exception: {ex.Message}");
                        OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                        UpdateStatus();
                        this.Enabled = true;
                        MessageBox.Show(new Form { TopMost = true }, "Помилка!!!\n" + ex.Message);
                    }
                }
            }
            this.Enabled = true;
            UpdateStatus();
        }

        private void txbCashIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnCashIn_Click(sender, null);
            }
        }

        private void btnCashIn_Click(object sender, EventArgs e)
        {
            if (txbCashIN.Text != "")
            {
                if (this.cw == null)
                {
                    this.cw = new ConfirmationWindow(0, double.Parse(txbCashIN.Text.Replace('.', ',')));
                    this.cw.NumReadyEvent += cw_NumReadyEvent;
                    this.cw.ShowDialog();
                    this.cw = null;
                    txbCashIN.Text = "";
                }
            }
        }

        private void txbCashOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnCashOut_Click(sender, null);
            }
        }

        private void btnCashOut_Click(object sender, EventArgs e)
        {
            if (txbCashOut.Text != "")
            {
                if (this.cw == null)
                {
                    this.cw = new ConfirmationWindow(1, double.Parse(txbCashOut.Text.Replace('.', ',')));
                    this.cw.NumReadyEvent += cw_NumReadyEvent;
                    this.cw.ShowDialog();
                    this.cw = null;
                    txbCashOut.Text = "";
                }
            }
        }

        private void btnZRep_Click(object sender, EventArgs e)
        {
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.PrintRep(16);
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: Make ZRep error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
                else
                {
                    OnStatusChangedEvent(true, 0, "Ok");
                    printer.GetStatusEx();
                }
                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: Make ZRep exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
            }
            log.Write($"FDFS: Make ZRep result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
        }

        private void btnPrintZ_Click(object sender, EventArgs e)
        {
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.PrintRep(17);
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: Print ZRep error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
                else
                {
                    OnStatusChangedEvent(true, 0, "Ok");
                    printer.GetStatusEx();
                }
                printer.BegChk();
                printer.VoidChk();
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: Print ZRep exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            log.Write($"FDFS: Print ZRep result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
            UpdateStatus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnNewShift_Click(object sender, EventArgs e)
        {
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.RegUser(1, 1);
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: New shift Reg User error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
                err = printer.ShiftBegin();
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: New shift Shift Begin error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
                if (fd.deviceState.FiscalDeviceReady)
                {
                    Transaction tr = new Transaction();
                    err = printer.BegChk();
                    if(err !=0)
                    {
                        log.Write($"FDFS: New shift Begin Receipt error: {err}");
                        fd.ErrorAnalizer(err);
                    }
                    if (fd.deviceState.FiscalDeviceReady)
                    {
                        err = printer.TextChkEx(fd.paymentMachineId);
                        if (err != 0)
                        {
                            log.Write($"FDFS: New shift Text1 error: {err}");
                            fd.ErrorAnalizer(err);
                        }
                    }
                    if (fd.deviceState.FiscalDeviceReady)
                    {
                        err = printer.TextChkEx(fd.paymentMachineName);
                        if (err != 0)
                        {
                            log.Write($"FDFS: New shift Text2 error: {err}");
                            fd.ErrorAnalizer(err);
                        }
                    }
                    if (fd.deviceState.FiscalDeviceReady)
                    {
                        err = printer.InOut(1, 0, tr.Get());
                        if (err != 0)
                        {
                            log.Write($"FDFS: New shift InOut error: {err}");
                            fd.ErrorAnalizer(err);
                        }
                    }
                    if (fd.deviceState.FiscalDeviceReady)
                    {
                        err = printer.EndChk();
                        if (err != 0)
                        {
                            log.Write($"FDFS: New shift End receipt error: {err}");
                            fd.ErrorAnalizer(err);
                        }
                    }
                    if (!fd.deviceState.FiscalDeviceReady)
                    {
                        err = printer.VoidChk();
                        if (err != 0)
                        {
                            log.Write($"FDFS: New shift Void Receipt error: {err}");
                            fd.ErrorAnalizer(err);
                        }
                    }
                }
                log.Write($"FDFS: New shift result: {fd.deviceState.FiscalDeviceReady}");
                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: New shift exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
            }
            this.Enabled = true;
        }

        private void btnTermReport_Click(object sender, EventArgs e)
        {
            Item item = (Item)cbxReportSel.SelectedItem;
            byte repId = (byte)item.Value;
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.PrintFiscRep(repId, 1, 1, dateTimePickerFrom.Value, dateTimePickerTo.Value);
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: Print Term error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: Print Term exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            log.Write($"FDFS: Print Term result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
            UpdateStatus();
        }

        private void btnCopyCheck_Click(object sender, EventArgs e)
        {
            UInt16 txbRepNrStr = 0;
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.CopyChk(txbRepNrStr);
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: Print DocCopy error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: Print DocCopy exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            log.Write($"FDFS: Print DocCopy result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
            UpdateStatus();
        }

        private void btnPrintRepNr_Click(object sender, EventArgs e)
        {
            UInt16 txbRepNrStr = 1;
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.PrintFiscRep(0, txbRepNrStr, txbRepNrStr, DateTime.Now, DateTime.Now);
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: Print ZCopy error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: Print ZCopy exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            log.Write($"FDFS: Print ZCopy result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
            UpdateStatus();
        }

        private void btnSSale_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Logger log = new Logger(MachineId);
            bool receiptDone = true;
            byte type = 1;
            double sum = 0;
            if (txbSSale.Text != "")
            {
                sum = double.Parse(txbSSale.Text.Replace('.', ','));
            }
            if (cbxReturn.Checked)
            {
                uint err = printer.BegChk();
                if(err != 0)
                {
                    log.Write($"FDFS: Special Sale(return) Begin receipt error: {err}");
                    receiptDone &= fd.ErrorAnalizer(err);
                }
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.BegRet();
                    if (err != 0)
                    {
                        log.Write($"FDFS: Special Sale(return) Begin Return error: {err}");
                        receiptDone &= fd.ErrorAnalizer(err);
                    }
                }
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.NProd(999, Convert.ToUInt32(1000), Convert.ToUInt32(sum) * 100, 1, 1, "год", "Послуги паркування");
                    if (err != 0)
                    {
                        log.Write($"FDFS: Special Sale(return) Sale error: {err}");
                        receiptDone &= fd.ErrorAnalizer(err);
                    }
                }
                switch (chbIsCash.Checked)
                {
                    case true:
                        type = 2;
                        break;
                    case false:
                        type = 1;
                        break;
                }
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.Oplata(type, Convert.ToUInt32(sum * 100));
                    if (err != 0)
                    {
                        log.Write($"FDFS: Special Sale(return) Sale error: {err}");
                        receiptDone &= fd.ErrorAnalizer(err);
                    }
                }
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.EndChk();
                    if (err != 0)
                    {
                        log.Write($"FDFS: Special Sale(return) Close error: {err}");
                        receiptDone &= fd.ErrorAnalizer(err);
                    }
                    if (receiptDone)
                    {
                        if (type == 1)
                        {
                            Transaction tr = new Transaction(4, Convert.ToUInt32(sum * 100));
                            tr.UpdateData(tr);
                        }
                    }
                }
                UpdateStatus();
            }
            else
            {
                uint err = printer.BegChk();
                if (err != 0)
                {
                    log.Write($"FDFS: Special Sale Begin receipt error: {err}");
                    receiptDone &= fd.ErrorAnalizer(err);
                }
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.NProd(999, Convert.ToUInt32(1000), Convert.ToUInt32(sum) * 100, 1, 1, "год", "Послуги паркування");
                    if (err != 0)
                    {
                        log.Write($"FDFS: Special Sale Sale error: {err}");
                        receiptDone &= fd.ErrorAnalizer(err);
                    }
                }
                switch (chbIsCash.Checked)
                {
                    case true:
                        type = 2;
                        break;
                    case false:
                        type = 1;
                        break;
                }
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.Oplata(type, Convert.ToUInt32(sum * 100));
                    if (err != 0)
                    {
                        log.Write($"FDFS: Special Sale Payment error: {err}");
                        receiptDone &= fd.ErrorAnalizer(err);
                    }
                }
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.EndChk();
                    if (err != 0)
                    {
                        log.Write($"FDFS: Special Sale Close receipt error: {err}");
                        receiptDone &= fd.ErrorAnalizer(err);
                        
                    }
                }
                if (receiptDone)
                {
                    Transaction tr = new Transaction(type, Convert.ToUInt32(sum * 100));
                    tr.UpdateData(tr);
                }
                UpdateStatus();
                log.Write($"FDFS: Special Sale amount: {sum}, type: {type} result: {fd.deviceState.FiscalDeviceReady & receiptDone }");
            }
            if (!receiptDone)
                printer.VoidChk();
            this.Enabled = true;
        }

        private void txbSSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnSSale_Click(sender, null);
            }

        }

        private void btnUpdateAmount_Click(object sender, EventArgs e)
        {
            UInt32 newAmount = 0;
            UInt32.TryParse(txbCashSerialized.Text, out newAmount);
            Transaction tr = new Transaction();
            tr.Set(newAmount * 100);
        }

        private void btnVoidRec_Click(object sender, EventArgs e)
        {
            Logger log = new Logger(MachineId);
            this.Enabled = false;
            try
            {
                uint err = printer.VoidChk();
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, err.ToString());
                    log.Write($"FDFS: Void receipt error: {err}");
                    UpdateStatus();
                    fd.ErrorAnalizer(err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                log.Write($"FDFS: Void receipt exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            log.Write($"FDFS: Void receipt result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
            UpdateStatus();
        }

        private void btnReconnect_Click(object sender, EventArgs e)
        {

        }

        private void btnTimeSync_Click(object sender, EventArgs e)
        {
            printer.PrgTime();
        }
    }
}
