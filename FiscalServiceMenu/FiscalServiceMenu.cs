using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using UA_Fiscal_Leocas;

namespace FiscalServiceMenu
{
    public partial class FiscalServiceMenu : Form
    {
        LeoCasLib printer;
        public delegate void ErrorCheckHandler();
        string errStr = "";
        bool isReady = false;
        string connectionString;// = "10.2.30.30:23000";
        List<Device> devices = new List<Device>();

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

        public FiscalServiceMenu()
        {
            InitializeComponent();
            dtpDateFrom.Value = DateTime.Today.AddDays(-1);
            dtpDateTo.Value = new DateTime(dtpDateTo.Value.Year, dtpDateTo.Value.Month, 1);
        }

        private void FiscalServiceMenu_Load(object sender, EventArgs e)
        {
            devices.Add(new Device(1, "Виїзд 41", "10.10.50.104:23000"));
            devices.Add(new Device(2, "Виїзд 42", "10.10.50.105:23000"));
            devices.Add(new Device(3, "Виїзд 43", "10.10.50.106:23000"));
            printer = new LeoCasLib();
            timer1.Start();
            foreach (Device dev in devices)
            {
                cbxSelectDevice.Items.Add(new Item(dev.Name, dev.ID));
            }
            cbxSelectDevice.SelectedIndex = 0;
            cbxRepType.Items.Add(new Item("Повний", 2));
            cbxRepType.Items.Add(new Item("Скороч.", 3));
            cbxRepType.SelectedIndex = 1;
            gbxFiscalFn.Enabled = false;
        }

        public bool ErrorAnalizer(UInt32 error)
        {
            string errMess = "";
            if (!Enumerations.DeviceErrors.TryGetValue(error, out errMess))
                errMess = "NotDefined";
            switch (error)
            {
                case 0:
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        LeoCasLib.customerDisplConnectionErr = false;
                        return true;
                    }
                case 1:
                    return false;
                case 2:
                    return false;
                case 3:
                    return false;
                case 4:
                    return false;
                case 5:
                    return false;
                case 6:
                    return false;
                case 7:
                    return false;
                case 8:
                    return false;
                case 9:
                    return false;
                case 103:  // Звіт у фіскальній памяті не знайдено.
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        Enumerations.DeviceErrors.TryGetValue(103, out errMess);
                        MessageBox.Show(new Form { TopMost = true }, "Звіт у фіскальній памяті не знайдено.");
                        return true;
                    }

                case 105:
                    return false;
                case 115:
                    return false;
                case 119:
                    return false;
                case 120:
                    return false;
                case 121:  //"Помилка в текстовій стрічці (Символ за діапазоном 0x20-0xff)"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 122:  //"Невірний номер функції"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 123:  //"Невірний номер операції"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 124:
                    return false;
                case 125:
                    return false;
                case 126: //"Помилка 24h"
                    LeoCasLib.blockedDueTo24 = true;
                    return false;
                case 128:
                    return false;
                case 131:
                    return false;
                case 137:
                    return false;
                case 200:
                    return false;
                case 201:
                    return false;
                case 202:
                    return false;
                case 203:
                    return false;
                case 204:
                    return false;
                case 205:
                    return false;

                case 224: //Зміна не відкріта
                    LeoCasLib.shiftStartedStatus = false;
                    return false;
                case 237: //"Оплата за чеком відсутня"
                    return false;
                case 244:
                    return false;
                case 245:
                    return false;
                case 246:
                    return false;
                case 413:
                    return false;
                case 258: // "Грошей в сейфі не достатньо"
                    Enumerations.DeviceErrors.TryGetValue(258, out errMess);
                    MessageBox.Show(new Form { TopMost = true }, "Грошей в сейфі не достатньо");
                    return false;
                case 260: // "Обнулення вже виконано"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 286: //"Друк відкладених звітів вже виконано"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        MessageBox.Show(new Form { TopMost = true }, "Друк відкладених звітів вже виконано");
                        return true;
                    }

                case 601: //"Система зайнята іншою командою"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 602: //"Відсутні операції для закриття дня (чи зміни)"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        return false;
                    }
                    else
                    {
                        return true; ;
                    }

                default:
                    return false;
            }
        }

        private void UpdateStatus()
        {
            printer.GetStatus();
            chbx24Hour.Checked = LeoCasLib.blockedDueTo24;
            chbx72hour.Checked = LeoCasLib.blockedDueTo72;
            chbxIsBlocked.Checked = LeoCasLib.blockedStatus;
            chbxReceiptOpened.Checked = LeoCasLib.fiscReceiptBegStatus;
            chbxLowPaper.Checked = LeoCasLib.lowPaperStatus;
            chbxOutPaper.Checked = LeoCasLib.outOfPaperStatus;
            chbxDocOpnd.Checked = LeoCasLib.docBegStatus;
            chbxPrinErr.Checked = LeoCasLib.customerDisplConnectionErr;
            chbxOpReg.Checked = LeoCasLib.operRegStatus;
            chbxShiftBeg.Checked = LeoCasLib.shiftStartedStatus;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                Item item = (Item)cbxSelectDevice.SelectedItem;
                foreach (Device dev in devices)
                {
                    if (item.Value == dev.ID)
                        connectionString = dev.ConnectionString;
                }
                isReady = ErrorAnalizer(printer.Connect(connectionString));
                if (isReady)
                {
                    ErrorAnalizer(printer.RegUser(1, 1));
                }
                if (isReady)
                {
                    ErrorAnalizer(printer.ShiftBegin());
                }
            }
            catch (Exception ex)
            {
                isReady = false;
                MessageBox.Show(ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            if (!isReady)
            { 
                MessageBox.Show("Нема з'єднання");
                this.Enabled = true;
            }
            else
            {
                gbxFiscalFn.Enabled = true;
                this.Enabled = true;
                printer.GetStatus();
                UpdateStatus();
            }
        }

        private void btnXRep_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                isReady = ErrorAnalizer(printer.PrintRep(0));
            }
            catch (Exception ex)
            {
                isReady = false;
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            if (!isReady)
                MessageBox.Show("Нема з'єднання");
            printer.GetStatus();
            UpdateStatus();
            this.Enabled = true;
        }

        private void btnPrintZ_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                isReady = ErrorAnalizer(printer.PrintRep(17));
            }
            catch (Exception ex)
            {
                isReady = false;
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            if (!isReady)
                MessageBox.Show("Нема з'єднання");
            UpdateStatus();
            this.Enabled = true;
        }

        private void btnZRep_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                isReady = ErrorAnalizer(printer.PrintRep(16));
            }
            catch (Exception ex)
            {
                isReady = false;
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            if (!isReady)
                MessageBox.Show("Нема з'єднання");
            UpdateStatus();
            this.Enabled = true;
        }

        private void btnPrintZbyNr_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            UInt16 txbRepNrStr = 1;
            try
            {
                UInt16.TryParse(txbZNr.Text, out txbRepNrStr);
                isReady = ErrorAnalizer(printer.PrintFiscRep(0, txbRepNrStr, txbRepNrStr, DateTime.Now, DateTime.Now));
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                isReady = false;
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                UpdateStatus();
                this.Enabled = true;
            }
            if (!isReady)
                MessageBox.Show(new Form { TopMost = true }, errStr);
            UpdateStatus();
            this.Enabled = true;
        }

        private void btnPrinDocNr_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            UInt16 txbRepNrStr = 0;
            try
            {
                UInt16.TryParse(txbDocNr.Text, out txbRepNrStr);
                UpdateStatus();
                isReady = ErrorAnalizer(printer.CopyChk(txbRepNrStr));
                this.Enabled = true;
                UpdateStatus();
            }
            catch (Exception ex)
            {
                isReady = false;
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
                UpdateStatus();
            }
            if (!isReady)
                MessageBox.Show(new Form { TopMost = true }, errStr);
            this.Enabled = true;
        }

        private void btnPrintPeriod_Click(object sender, EventArgs e)
        {
            Item item = (Item)cbxRepType.SelectedItem;
            byte repId = (byte)item.Value;
            try
            {
                isReady = ErrorAnalizer(printer.PrintFiscRep(repId, 1, 1, dtpDateFrom.Value, dtpDateTo.Value));
                UpdateStatus();
            }
            catch (Exception ex)
            {
                isReady = false;
                UpdateStatus();
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
            }
        }

        private void btnSSale_Click(object sender, EventArgs e)
        {
            byte type = 1;
            double sum = 0;
            if (txbSSale.Text != "")
            {
                sum = double.Parse(txbSSale.Text.Replace('.', ','));
            }
            if (cbxReturn.Checked)
            {
                isReady = ErrorAnalizer(printer.BegChk());
                isReady = ErrorAnalizer(printer.BegRet());
                if (isReady)
                    isReady = ErrorAnalizer(printer.NProd(999, Convert.ToUInt32(1000), Convert.ToUInt32(sum) * 100, 1, 1, "год", "Послуги паркування"));
                switch (chbIsCashless.Checked)
                {
                    case true:
                        type = 2;
                        break;
                    case false:
                        type = 1;
                        break;
                }
                if (isReady)
                    isReady = ErrorAnalizer(printer.Oplata(type, Convert.ToUInt32(sum * 100)));
                if (isReady)
                    isReady = ErrorAnalizer(printer.EndChk());
                UpdateStatus();
            }
            else
            {
                isReady = ErrorAnalizer(printer.BegChk());
                if (isReady)
                    isReady = ErrorAnalizer(printer.NProd(999, Convert.ToUInt32(1000), Convert.ToUInt32(sum) * 100, 1, 1, "год", "Послуги паркування"));
                switch (chbIsCashless.Checked)
                {
                    case true:
                        type = 2;
                        break;
                    case false:
                        type = 1;
                        break;
                }
                if (isReady)
                    isReady = ErrorAnalizer(printer.Oplata(type, Convert.ToUInt32(sum * 100)));
                if (isReady)
                    isReady = ErrorAnalizer(printer.EndChk());
                UpdateStatus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnDisconect_Click(object sender, EventArgs e)
        {
            printer.Disconnect();
        }
    }
}
