using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UA_Fiscal_Leocas
{
    class LostTransaction
    {
        string ticketNr;
        int amount;
        string time;
        string reason;
        public string path { get; set; }

        LostTransaction(string deviceid, string ticketNr, int amount, string reason)
        {
            this.path = $@"C:\FiscalFolder\{deviceid}LostTransactions.xml";
            this.amount = amount;
            this.ticketNr = ticketNr;
            this.reason = reason;
            this.time = DateTime.Now.ToString("G");
        }

        LostTransaction(string deviceid)
        {
            this.path = $@"C:\FiscalFolder\{deviceid}LostTransactions.xml";
        }

        LinkedList<LostTransaction> Get()
        {
            LinkedList<LostTransaction> ltList = new LinkedList<LostTransaction>();
            System.Xml.Serialization.XmlSerializer reader =new System.Xml.Serialization.XmlSerializer(typeof(LostTransaction));
            System.IO.StreamReader file = new System.IO.StreamReader(
                @"c:\temp\SerializationOverview.xml");
            LostTransaction overview = (LostTransaction)reader.Deserialize(file);
            file.Close();
            return ltList;
        }

        void Set(LostTransaction lt)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(LostTransaction));
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, lt);
            file.Close();
        }
    }
}
