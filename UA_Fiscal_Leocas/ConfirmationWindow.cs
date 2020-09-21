using System;
using System.Windows.Forms;

namespace UA_Fiscal_Leocas
{
    public partial class ConfirmationWindow : Form
    {
        public delegate void NumReady(byte type, bool isOk);
        public event NumReady NumReadyEvent;
        double summ, prevSumm;
        byte cashFlowType;

        public void OnNumReady(byte type, bool isOk)
        {
            if (this.NumReadyEvent != null)
                this.NumReadyEvent(type, isOk);
        }

        public ConfirmationWindow(byte type, double prevSum)
        {
            InitializeComponent();
            //btnConfirmConfirm.Enabled = false;
            this.prevSumm = prevSum;
            cashFlowType = type;

        }

        private void txbConfirmSumm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && ((e.KeyChar != 46));
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnConfirmConfirm_Click(sender, null);
            }
            if (e.KeyChar == 27)
            {
                btnCancel_Click(sender, null);
            }
        }

        private void txbConfirmSumm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string str = txbConfirmSumm.Text.Replace('.', ',');
                summ = double.Parse(str);
            }
            catch
            {
            }
        }

        private void btnConfirmConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string str = txbConfirmSumm.Text.Replace('.', ',');
                summ = double.Parse(str);
                OnNumReady(cashFlowType, true);
                this.Close();
            }
            catch
            {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
