using System;
using System.IO;
using System.Text;

namespace UA_Fiscal_Leocas
{
    public class Logger
    {
        /// <summary>
        /// Полный путь к лог файлу.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Конструктор класса Logger. Создает папку и файл.
        /// </summary>
        public Logger(string machineID)
        {
            this.FilePath =  $@"C:\Log\{machineID}_FiscalTrace-{DateTime.Now:yyyy-MM-dd}.txt";
        }
        public Logger(string machineID, string source)
        {
           // this.FilePath = $@"C:\Log\{machineID}_FiscalTrace-{DateTime.Now:yyyy-MM-dd}.txt";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd}";
            this.FilePath = string.Format(StringValue.LogFile, machineID, source, dateTime);
        }

        /// <summary>
        /// Запись строки в лог файл. Перегруженый.
        /// </summary>
        /// <param name="mess">Сообщение (массив байтов)</param>
        /// <param name="direction">Индикатор направления. True - к фискальному принтеру; False - ответ от фискального принтера</param>
        public void logWrite(byte[] mess, bool direction)
        {
            //System.Text.Encoding.Default.GetString(mess);
            string dateTime = DateTime.Now.ToString();
            string dir;
            StreamWriter sw = new StreamWriter(FilePath, true, System.Text.Encoding.UTF8);
            if (direction)
            { dir = ">>"; } // true if to printer
            else { dir = "<<"; } // false if from printer
            //StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine("{0}: {1} {2}", dateTime, dir, Encoding.UTF8.GetString(mess));
            sw.Close();
        }

        /// <summary>
        /// Запись строки в лог файл. Перегруженый.
        /// </summary>
        /// <param name="mess">Сообщение (строка)</param>
        /// <param name="direction">Индикатор направления. True - к фискальному принтеру; False - ответ от фискального принтера</param>
        public void logWrite(string mess, bool direction)
        {
            //System.Text.Encoding.Default.GetString(mess);
            string dateTime = DateTime.Now.ToString();
            string dir;
            StreamWriter sw = new StreamWriter(FilePath, true, Encoding.UTF8);
            if (direction)
            { dir = ">>"; } // true if to printer
            else { dir = "<<"; } // false if from printer
            //StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine("{0}: {1} {2}", dateTime, dir, mess);
            sw.Close();
        }

        public void Write(string mess)
        {
            string dateTime = DateTime.Now.ToString("dd’-‘MM’-‘yy’T’HH’:’mm’:’ss.ff");
            StreamWriter sw = new StreamWriter(FilePath, true, Encoding.UTF8);
            sw.WriteLine("{0}: {1}", dateTime, mess);
            sw.Close();
        }

        public void Write(string source, string mess)
        {
            string dateTime = DateTime.Now.ToString("dd’-‘MM’-‘yy’T’HH’:’mm’:’ss.ff");
            StreamWriter sw = new StreamWriter(FilePath, true, Encoding.UTF8);
            sw.WriteLine("{0}: method: {1} {2}", dateTime, source, mess);
            sw.Close();
        }
    }
}
