using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class Settings
    {
        /// <summary>
        /// 0-APM, 1-CashDesk, 2-Column
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// Has Visa Discount programm
        /// </summary>
        public bool Visa { get; set; }

        /// <summary>
        /// Has bank terminal
        /// </summary>
        public bool HasTerminal { get; set; }

        /// <summary>
        /// Connection string to bank card reader
        /// </summary>
        public string TerminalConnectionString { get; set; }

        /// <summary>
        /// Close shift automaticaly.
        /// </summary>
        public bool AutomatedShift { get; set; }

        /// <summary>
        /// Automated Z-report time
        /// </summary>
        public string ZReportTime { get; set; }

        /// <summary>
        /// Automated New shift time
        /// </summary>
        public string ShiftBeginTime { get; set; }

        public Settings()
        {
            DeviceType = 0; //APM or not defined
            Visa = false;
            HasTerminal = false;
            TerminalConnectionString = "NoTerminal:0000";
            AutomatedShift = true;
            ZReportTime = "23:57";
            ShiftBeginTime = "00:03";
        }

        Settings GetSettings()
        {
            System.IO.FileStream fs = new System.IO.FileStream(StringValue.LeoCasFiscalSettings, System.IO.FileMode.Open);
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(fs);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(Settings));
            var settings = new Settings();
            settings = (Settings)x.Deserialize(reader);
            return settings;
        }

        public static bool SaveSettings(Settings settings)
        {
            System.IO.FileStream fs = new System.IO.FileStream(StringValue.LeoCasFiscalSettings, System.IO.FileMode.OpenOrCreate);
            System.Xml.XmlWriter writer = new System.Xml.XmlTextWriter(fs, Encoding.Unicode);
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(settings.GetType());
            serializer.Serialize(writer, settings);
            writer.Close();
            return true;
        }
    }
}
