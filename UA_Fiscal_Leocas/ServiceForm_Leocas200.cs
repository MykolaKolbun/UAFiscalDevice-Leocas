using SkiData.FiscalDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace UA_Fiscal_Leocas
{
    public partial class ServiceForm_Leocas200 : Form
    {
        public delegate void StatusChanged(bool isready, int errorCode, string errorMessage);
        public event StatusChanged StatusChangedEvent;
        private LeoCasLib printer;
        UA_Fiscal_Leocas_200 fd;
        //public delegate void ErrorCheckHandler();
        ////public event ErrorCheckHandler ErrorCheckEvent;
        string MachineId;
        ConfirmationWindow cw;
        SQLConnect sqlcon = new SQLConnect();
        string errStr = "";

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
                // Generates the text shown in the combo box
                return Name;
            }
        }

        private void OnStatusChangedEvent(bool isready, int errorCode, string errorMessage)
        {
            if (this.StatusChangedEvent != null)
                this.StatusChangedEvent(isready, errorCode, errorMessage);
        }

        public ServiceForm_Leocas200(LeoCasLib printer, string machineID, UA_Fiscal_Leocas_200 fd)
        {
            InitializeComponent();
            this.printer = printer;
            MachineId = machineID;
            this.fd = fd;
            printer.GetStatusEx();
            UpdateStatus();
        }

        private void ServiceForm_Leocas200_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Text = $"{Text}";
            btnPrintRepNr.Enabled = true;
            btnTermReport.Enabled = true;
            btnCopyCheck.Enabled = true;
            txbCheckNr.Enabled = true;
            txbRepNr.Enabled = true;
            dateTimePickerFrom.Enabled = true;
            dateTimePickerTo.Enabled = true;
            groupBoxSSale.Enabled = true;
            Transaction tr = new Transaction();
            lblCash.Text = (tr.Get() / 100).ToString();
            txbCashSerialized.Text = (tr.Get() / 100).ToString();
            dateTimePickerTo.Value = DateTime.Today.AddDays(-1);
            dateTimePickerFrom.Value = new DateTime(dateTimePickerTo.Value.Year, dateTimePickerTo.Value.Month, 1);
            Tabs.SelectedTab = FiscTab;
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
            cbxTransactionTypeSelect.Items.Add(new Item("Чек", 1));
            cbxTransactionTypeSelect.Items.Add(new Item("Закриття Дня", 2));
            cbxTransactionTypeSelect.SelectedIndex = 0;
            cbxReportSel.Items.Add(new Item("Повний", 2));
            cbxReportSel.Items.Add(new Item("Скороч.", 3));
            cbxReportSel.SelectedIndex = 1;
            cbxDeviceList.SelectedText = fd.paymentMachineId;
            try
            {
                LinkedList<string> ll = sqlcon.GetDevicesList();
                foreach (string device in ll)
                {
                    cbxDeviceList.Items.Add(device);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(new Form { TopMost = true }, exc.Message);
            }
        }

        private void UpdateStatus()
        {
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
                btnZRep.Enabled = true;
            }
            else
            {
                btnNewShift.Enabled = true;
                btnXRep.Enabled = false;
                btnCashIn.Enabled = false;
                btnCashOut.Enabled = false;
                btnZRep.Enabled = false;
            }
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
                    MessageBox.Show(new Form { TopMost = true }, errStr);
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
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
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
                        if (err != 0)
                        {
                            log.Write($"FDFS: CashIn (Begin Receipt) error: {err}");
                            receiptDone &= fd.ErrorAnalizer(err);
                        }
                        printer.TextChkEx($"Платіжна станція: {MachineId}");
                        double sum = double.Parse(txbCashIN.Text.Replace('.', ',')) * 100;
                        if (fd.deviceState.FiscalDeviceReady & receiptDone)
                        {
                            err = printer.InOut(1, type, Convert.ToUInt32(sum));
                            if (err != 0)
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
                            receiptDone &= fd.ErrorAnalizer(err);
                        }
                        printer.TextChkEx($"Платіжна станція: {MachineId}");
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
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && ((e.KeyChar != 46));
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
            if (this.cw == null)
            {
                if (txbCashIN.Text != "")
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
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && ((e.KeyChar != 46));
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
                uint err = printer.PrintRep(1);
                if (err != 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, errStr);
                    log.Write($"FDFS: ZRep error: {err}");
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
                log.Write($"FDFS: ZRep exception: {ex.Message}");
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                UpdateStatus();
            }
            log.Write($"FDFS: ZRep result: {fd.deviceState.FiscalDeviceReady}");
            this.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSysTime.Text = DateTime.Now.ToLongTimeString();
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
                    if (err != 0)
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

        private void ShowTable(LinkedList<TableContent> tc)
        {
            dgvDBTable.Rows.Clear();
            for (int i = 0; i < tc.Count; i++)
            {
                TableContent tctemp = tc.ElementAt(i);
                string[] s = new string[5];
                s[0] = tctemp.ID.ToString();
                s[1] = tctemp.deviceID.ToString();
                s[2] = tctemp.ticketNR;
                s[3] = tctemp.transactionDate.ToString();
                s[4] = tctemp.cardNR;
                dgvDBTable.Rows.Add(s);
            }
        }

        private void CollectData()
        {
            Item item = (Item)cbxTransactionTypeSelect.SelectedItem;
            byte transactionType = (byte)item.Value;
            this.Enabled = false;
            LinkedList<TableContent> tc = new LinkedList<TableContent>();
            try
            {
                tc = sqlcon.GetListOfTransactionFromDBbyDevice(cbxDeviceList.Text, transactionType.ToString(), Convert.ToInt16(chbIsPrinted.Checked));
            }
            catch { }
            this.ShowTable(tc);
            this.Enabled = true;
        }

        private void cbxDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CollectData();
        }

        private void cbxTransactionTypeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CollectData();
        }

        private void chbIsPrinted_CheckStateChanged(object sender, EventArgs e)
        {
            this.CollectData();
        }

        private void txbTicketNR_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && ((e.KeyChar != 46));
            if (e.KeyChar == 13)
            {
                btnSearchTicket_Click(sender, null);
            }
        }

        private void btnSearchTicket_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            SQLConnect sql = new SQLConnect();
            LinkedList<TableContent> tc = new LinkedList<TableContent>();
            if (txbTicketNR.Text != "")
            {
                tc = sql.SearchTransactionsFromDBbyTicketNR(txbTicketNR.Text);
            }
            else
                MessageBox.Show(new Form { TopMost = true }, "Введіть номер квитка для пошуку");
            this.ShowTable(tc);
            this.Enabled = true;
        }

        private void btnPrintBank_Click(object sender, EventArgs e)
        {
            if (!chbPrintAll.Checked)
            {
                Thread myThread = new Thread(PrintSingleReceipt);
                myThread.Start();
                this.Enabled = false;
            }
            else
            {
                Thread myThread = new Thread(PrintAll);
                myThread.Start();
                this.Enabled = false;
            }
        }

        private void PrintSingleReceipt()
        {
            int row = dgvDBTable.CurrentCell.RowIndex;
            string id = dgvDBTable.Rows[row].Cells[0].Value.ToString();
            for (int i = 0; i < (int)quantity.Value; i++)
            {
                try
                {
                    SQLConnect sql = new SQLConnect();
                    printer.GetStatusEx();
                    UpdateStatus();
                    uint err = printer.BegDoc();
                    if (err != 0)
                        fd.ErrorAnalizer(err);
                    string[] lines = sql.GetTranasactionFromDBByID(id).Split('\n');
                    for (int line = 0; line < lines.Length; line++)
                    {
                        if (lines[line].Length > 1)
                        {
                            printer.TextDoc(0, lines[line]);
                        }
                    }
                    err = printer.EndDoc();
                    if (err != 0)
                        fd.ErrorAnalizer(err);
                    printer.GetStatusEx();
                    sql.UpdateIsPrinted(id);
                    UpdateStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(new Form { TopMost = true }, ex.Message);
                    printer.EndDoc();
                    this.Enabled = true;
                    UpdateStatus();
                }
                this.Enabled = true;
            }
        }

        private void PrintAll()
        {
            for (int i = 0; i < dgvDBTable.RowCount; i++)
            {
                string id = dgvDBTable.Rows[i].Cells[0].Value.ToString();
                for (int j = 0; j < (int)quantity.Value; j++)
                {
                    try
                    {
                        SQLConnect sql = new SQLConnect();
                        printer.GetStatusEx();
                        UpdateStatus();
                        uint err = printer.BegDoc();
                        if (err != 0)
                            fd.ErrorAnalizer(err);
                        string[] lines = sql.GetTranasactionFromDBByID(id).Split('\n');
                        for (int line = 0; line < lines.Length; line++)
                        {
                            if (lines[line].Length > 1)
                            {
                                printer.TextDoc(0, lines[line]);
                            }
                        }
                        err = printer.EndDoc();
                        if (err != 0)
                            fd.ErrorAnalizer(err);
                        printer.GetStatusEx();
                        sql.UpdateIsPrinted(id);
                        UpdateStatus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(new Form { TopMost = true }, ex.Message);
                        this.Enabled = true;
                        printer.EndDoc();
                        UpdateStatus();
                    }
                }
            }
            this.Enabled = true;
        }

        private void btnTermReport_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Item item = (Item)cbxReportSel.SelectedItem;
            byte repId = (byte)item.Value;
            try
            {
                uint err =printer.PrintFiscRep(repId, 1, 1, dateTimePickerFrom.Value, dateTimePickerTo.Value);
                if (err != 0)
                    fd.ErrorAnalizer(err);
                printer.GetStatusEx();
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                UpdateStatus();
            }
            if (!fd.deviceState.FiscalDeviceReady)
                MessageBox.Show(new Form { TopMost = true }, fd.deviceState.Description);
            UpdateStatus();
            this.Enabled = true;
        }

        private void btnPrintRepNr_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            UInt16 txbRepNrStr = 1;
            try
            {
                UInt16.TryParse(txbRepNr.Text, out txbRepNrStr);
                uint err = printer.PrintFiscRep(0, txbRepNrStr, txbRepNrStr, DateTime.Now, DateTime.Now);
                if (err != 0)
                    fd.ErrorAnalizer(err);
                printer.GetStatusEx();
            }
            catch (Exception ex)
            {
                OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
            }
            if (!fd.deviceState.FiscalDeviceReady)
                MessageBox.Show(new Form { TopMost = true }, fd.deviceState.Description);
            UpdateStatus();
            this.Enabled = true;
        }

        private void btnSSale_Click(object sender, EventArgs e)
        {
            byte type = 1;
            double sum = 0;
            bool receiptDone = true;
            if (txbSSale.Text != "")
            {
                sum = double.Parse(txbSSale.Text.Replace('.', ','));
            }
            printer.GetStatusEx();
            UpdateStatus();
            if (cbxReturn.Checked)
            {
                uint err = printer.BegChk();
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                    err = printer.BegRet();
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                    err = printer.NProd(999, Convert.ToUInt32(1000), Convert.ToUInt32(sum) * 100, 1, 1, "год", "Послуги паркування");
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);   
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
                    err = printer.Oplata(type, Convert.ToUInt32(sum * 100));
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                    err = printer.EndChk();
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
                if (receiptDone)
                {
                    if (type == 1)
                    {
                        Transaction tr = new Transaction(4, Convert.ToUInt32(sum * 100));
                        tr.UpdateData(tr);
                    }
                }
            }
            else
            {
                uint err = printer.BegChk();
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
                if(fd.deviceState.FiscalDeviceReady)
                    err=printer.NProd(999, Convert.ToUInt32(1000), Convert.ToUInt32(sum) * 100, 1, 1, "год", "Послуги паркування");
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
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
                    err=printer.Oplata(type, Convert.ToUInt32(sum * 100));
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                    err = printer.EndChk();
                if (err != 0)
                    receiptDone &= fd.ErrorAnalizer(err);
                if (fd.deviceState.FiscalDeviceReady & receiptDone)
                {
                    Transaction tr = new Transaction(type, Convert.ToUInt32(sum * 100));
                    tr.UpdateData(tr);
                }
            }
            printer.GetStatusEx();
            UpdateStatus();
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

        private void dgvDBTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvDBTable.CurrentCell.RowIndex;
            string id = dgvDBTable.Rows[row].Cells[0].Value.ToString();
            try
            {
                SQLConnect sql = new SQLConnect();
                string lines = sql.GetTranasactionFromDBByID(id);
                MessageBox.Show(lines);
            }
            catch { }
        }

        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void btnCopyCheck_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                UInt16.TryParse(txbCheckNr.Text, out UInt16 txbRepNrStr);
                printer.GetStatusEx();
                UpdateStatus();
                uint err = printer.CopyChk(txbRepNrStr);
                if (err != 0)
                    fd.ErrorAnalizer(err);
                printer.GetStatusEx();
                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                UpdateStatus();
            }
            if (!fd.deviceState.FiscalDeviceReady)
                MessageBox.Show(new Form { TopMost = true }, errStr);
            this.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //fd.startZRep = this.timeFromEndShift.Value.TimeOfDay;
            //fd.endZRep = this.timeToEndShift.Value.TimeOfDay;
            //fd.startShift = this.timeFromStartShift.Value.TimeOfDay;
            //fd.endShift = this.timeToStartShift.Value.TimeOfDay;

        }

        private void btnVoidRec_Click(object sender, EventArgs e)
        {
            printer.VoidChk();
        }

        private void btnVoidDoc_Click(object sender, EventArgs e)
        {}

        private void btnUpdateAmount_Click(object sender, EventArgs e)
        {
            UInt32.TryParse(txbCashSerialized.Text, out UInt32 newAmount);
            Transaction tr = new Transaction();
            tr.Set(newAmount * 100);
        }

        private void btnTimeSync_Click(object sender, EventArgs e)
        {
            printer.PrgTime();
        }
    }
}
