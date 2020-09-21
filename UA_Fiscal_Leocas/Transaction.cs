using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace UA_Fiscal_Leocas
{
    class Transaction
    {

        public string Path { get; set; }
        public uint amount { get; set; }
        public int type { get; set; }

        public Transaction(int type, UInt32 amount)
        {
            Path = $@"C:\FiscalFolder\{UA_Fiscal_Leocas_200.MachineID}MoneyFlow.dat";
            this.type = type;
            this.amount = amount;
        }
        public Transaction()
        {
            Path = $@"C:\FiscalFolder\{UA_Fiscal_Leocas_200.MachineID}MoneyFlow.dat";
        }

        private void Send(UInt32 amount)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, amount);
            }
        }

        public UInt32 Get()
        {
            UInt32 total = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.OpenOrCreate))
                {
                    total = (UInt32)formatter.Deserialize(fs);
                }
            }
            catch
            {
                total = 0;
            }
            return total;
        }

        public void UpdateData(Transaction tr)
        {
            UInt32 oldAmount = this.Get();
            UInt32 newAmount = 0;
            if (tr.type == 1)
                newAmount = oldAmount + tr.amount;
            if (tr.type == 2)
                newAmount = oldAmount;
            if (tr.type == 3)
                newAmount = oldAmount + tr.amount;
            if ((tr.type == 4) && (oldAmount >= tr.amount))
                newAmount = oldAmount - tr.amount;
            Send(newAmount);
        }

        public void Set(UInt32 amount)
        {
            Send(amount);
        }
    }
}
