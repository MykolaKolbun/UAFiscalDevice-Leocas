using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace UA_Fiscal_Leocas
{
    class LeoCasFunction
    {
        #region Fields
        Network printer;
        static UInt16 sync = 0;
        public static Timer receivingTimer;
        public static byte ANS { get; set; }
        private bool received { get; set; }
        public static UInt32 lastError { get; set; }
        public static int lastStatus { get; set; }
        private bool timeElapsed { get; set; }

        // Printer statuses
        public static bool blockedStatus = false;
        public static bool shiftStartedStatus = false;
        public static bool fiscReceiptBegStatus = false;
        public static bool docBegStatus = false;
        public static bool operRegStatus = false;
        public static bool outOfPaperStatus = false;
        public static bool blockedDueTo72 = false;
        public static bool blockedDueTo24 = false;
        public static bool lowPaperStatus = false;
        public static bool customerDisplConnectionErr = false;

        byte[] dataReceived;

        public uint daySum = 0;

        public delegate void StatusChanged(int status);
        public event StatusChanged StatusChangedEvent;
        #endregion

        #region Функции фискального принтера

        /// <summary>
        /// Подготовка данных для отправки на принтер и отправка
        /// </summary>
        /// <param name="command">Команда принтера</param>
        /// <param name="data">Данные команды(массив)</param>
        private UInt32 SendData(byte command, byte[] data)
        {
            //Logger log = new Logger("Transport");
            sync = 10;
            printer.ReceivedEvent += Printer_ReceivedEvent;
            received = false;
            timeElapsed = false;
            List<byte> mess = new List<byte>();
            UInt16 len = (UInt16)(6 + data.Length);
            byte[] pack = new byte[len];
            mess.Add(command);
            mess.AddRange(BitConverter.GetBytes((UInt16)(len)));
            mess.AddRange(BitConverter.GetBytes(sync));

            for (int i = 0; i < data.Length; i++)
            {
                mess.Add(data[i]);
            }
            CRC crc = new CRC();
            pack = mess.ToArray();
            byte crcbyte = (byte)crc.ComputeCrc(ref pack);
            mess.Add(crcbyte);
            string cmdStr = "?";
            Enumerations.Commands.TryGetValue(command, out cmdStr);
            //log.logWrite("SendData: Command: " + cmdStr, true);
            //log.logWrite("SendData: Data: " + BitConverter.ToString(data), true);
            //log.logWrite("SendData: Full Package: " + BitConverter.ToString(mess.ToArray()), true);
            try
            {
                lastError = 0;
                printer.Send(mess.ToArray());
                SetTimer();
                while (!timeElapsed == !received)
                { }
                if (received)
                {
                    printer.ReceivedEvent -= Printer_ReceivedEvent;
                    receivingTimer.Elapsed -= OnTimedEvent;
                    receivingTimer.Stop();
                    receivingTimer.Dispose();
                }
                if (timeElapsed)
                {
                    receivingTimer.Elapsed -= OnTimedEvent;
                    printer.ReceivedEvent -= Printer_ReceivedEvent;
                    //log.Write("Timeout error");
                    lastError = 4;
                }
                return lastError;
            }
            catch (Exception e)
            {
                receivingTimer.Elapsed -= OnTimedEvent;
                printer.ReceivedEvent -= Printer_ReceivedEvent;
                //log.Write("System error 1: " + e.Message);
                //log.Write("Inner Exception: " + e.InnerException);
                //log.Write("Stack Trace: " + e.StackTrace);
                return lastError = 3;
            }
        }

        /// <summary>
        /// Подготовка данных для отправки на принтер и отправка(команда без данных)
        /// </summary>
        /// <param name="command">Комманда</param>
        private UInt32 SendData(byte command)
        {
            sync = 10;
            //Logger log = new Logger("Transport");
            printer.ReceivedEvent += Printer_ReceivedEvent;
            received = false;
            timeElapsed = false;
            ushort len = 6;
            List<byte> mess = new List<byte>();
            byte[] pack = new byte[5];
            mess.Add(command);
            mess.AddRange(BitConverter.GetBytes(len));
            mess.AddRange(BitConverter.GetBytes(sync));
            CRC crc = new CRC();
            pack = mess.ToArray();
            byte crcbyte = (byte)crc.ComputeCrc(ref pack);
            mess.Add(crcbyte);
            string cmdStr = "?";
            Enumerations.Commands.TryGetValue(command, out cmdStr);
            //log.logWrite("SendData: Command-" + cmdStr, true);
            //log.logWrite("SendData: Full Package: " + BitConverter.ToString(mess.ToArray()), true);
            SetTimer();
            try
            {
                lastError = 0;
                printer.Send(mess.ToArray());
                
                while (!timeElapsed == !received)
                { }
                if (received)
                {
                    receivingTimer.Elapsed -= OnTimedEvent;
                    printer.ReceivedEvent -= Printer_ReceivedEvent;
                    receivingTimer.Stop();
                    receivingTimer.Dispose();
                }
                if (timeElapsed)
                {
                    receivingTimer.Elapsed -= OnTimedEvent;
                    printer.ReceivedEvent -= Printer_ReceivedEvent;
                    //log.Write("Timeout error");
                    lastError = 4;
                }
                return lastError;
            }
            catch (Exception e)
            {
                receivingTimer.Elapsed -= OnTimedEvent;
                printer.ReceivedEvent -= Printer_ReceivedEvent;
                //log.Write("System error 2: " + e.Message);
                //log.Write("Inner Exception: " + e.InnerException);
                //log.Write("Stack Trace: " + e.StackTrace);
                return lastError = 3;
            }
        }

        /// <summary>
        /// Расширений статус от фискального
        /// </summary>
        /// <returns></returns>
        public UInt32 GetStatusEx()
        {
            byte cmd = 0x63;
            return SendData(cmd);
        }

        private int ExtStatus(byte[] inData)
        {
            int exStatus = 0;
            exStatus = (int)BitConverter.ToUInt16(inData, 0);
            if ((exStatus & 1) == 1)
            {
                blockedStatus = true;
            }
            if ((exStatus & 1) == 0)
            {
                blockedStatus = false;
            }

            if ((exStatus & 2) == 2)
            {
                blockedStatus = true;
                customerDisplConnectionErr = true;
            }
            if ((exStatus & 2) == 0)
            {
                blockedStatus = false;
                customerDisplConnectionErr = false;
            }

            if ((exStatus & 128) == 128)
            {
                blockedStatus = true;
                blockedDueTo24 = true;
            }
            if ((exStatus & 128) == 0)
            {
                blockedStatus = false;
                blockedDueTo24 = false;
            }

            if ((exStatus & 256) == 256)
            {
                blockedStatus = true;
                blockedDueTo72 = true;
            }
            if ((exStatus & 256) == 0)
            {
                blockedStatus = false;
                blockedDueTo72 = false;
            }
            if (exStatus != 0)
            {
                blockedStatus = true;
            }
            if (exStatus == 0)
                blockedStatus = false;
            return exStatus;
        }

        /// <summary>
        /// Инициализация соединения
        /// </summary>
        /// <param name="ip">IP адрес фискального принтера</param>
        /// <param name="port">Порт фискального принтера для отправки сообщений</param>
        /// <returns></returns>
        public uint Connect(string ip, int port)
        {
            return (uint)printer.Connect(ip, port);
        }

        /// <summary>
        /// Инициализация соединения
        /// </summary>
        /// <param name="ip">IP адрес фискального принтера</param>
        /// <param name="port">Порт фискального принтера для отправки сообщений</param>
        /// <returns></returns>
        public uint Connect(string connectionStringt)
        {
            string[] str = connectionStringt.Split(':');
            string ip = str[0];
            int.TryParse(str[1], out int port);
            printer = new Network();
            return (uint)printer.Connect(ip, port);
        }

        /// <summary>
        /// Обработка события ответ от принтера
        /// </summary>
        /// <param name="buffer">массив данных от принтера</param>
        /// <param name="len">длина принятых данных</param>
        private void Printer_ReceivedEvent(byte[] buffer)
        {
            this.dataReceived = new byte[buffer.Length];
            for (int k = 0; k < buffer.Length; k++)
            {
                dataReceived[k] = buffer[k];
            }
            Answer answer = new Answer();
            lastError = answer.GetAnswer(sync, this.dataReceived);
            ANS = answer.ans;
            string lastErrorStr = "?";
            string ansStr = "?";
            //Logger log = new Logger("Transport");
            Enumerations.DeviceErrors.TryGetValue(lastError, out lastErrorStr);
            Enumerations.ANS.TryGetValue(answer.ans, out ansStr);
            //log.logWrite("Received: " + BitConverter.ToString(this.dataReceived), false);
            //log.logWrite("ANS: " + ansStr, false);
            //log.logWrite("Error: " + lastErrorStr, false);
            //log.logWrite("Status: " + answer.status, false);
            StatusAnalizer(answer.status);
            if (ANS == 0x02)
            {
                ExtStatus(answer.fiscDevData);
                //log.logWrite("Data Received: " + BitConverter.ToString(answer.fiscDevData), false);
            }
            if (ANS == 0x04)
            { System.Threading.Thread.Sleep(500); }
            received = true;
        }

        /// <summary>
        /// Обработка ответа. Поле STATUS
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private void StatusAnalizer(UInt16 status)
        {
            lastStatus = 0;
            if ((status & 1) == 1)
            {
                blockedStatus = true;
                lastStatus = 1;
            }
            if ((status & 1) == 0)
            {
                blockedStatus = false;
            }
            if ((status & 2) == 2)
                shiftStartedStatus = true;
            if ((status & 2) == 0)
                shiftStartedStatus = false;
            if ((status & 4) == 4)
            {
                lastStatus = 3; // Есть незавершенные операции
            }
            if ((status & 8) == 8)
                fiscReceiptBegStatus = true;
            if ((status & 8) == 0)
                fiscReceiptBegStatus = false;
            if ((status & 16) == 16)
                docBegStatus = true;
            if ((status & 16) == 0)
                docBegStatus = false;
            if ((status & 32) == 32)
                operRegStatus = true;
            if ((status & 32) == 0)
                operRegStatus = false;
            if (((status & 64) == 64) || ((status & 128) == 128) || ((status & 256) == 256) || ((status & 512) == 512) || ((status & 1024) == 1024))
            {
                // Зарезервировано
            }
            if ((status & 2048) == 2048)
            {
                //error = 7; // РРО фискализирован
            }
            if ((status & 4096) == 4096)
            {
                lastStatus = 8; // Бумага заканчивается
                lowPaperStatus = true;
            }
            if ((status & 4096) == 0)
                lowPaperStatus = false;
            if ((status & 8192) == 8192)
            {
                lastStatus = 9; // Бумага закончилась
                outOfPaperStatus = true;
            }
            if ((status & 8192) == 0)
            {
                outOfPaperStatus = false;
            }
            if ((status & 16384) == 16384)
            {
                lastStatus = 10; // Открыта крышка принтера
            }
            if ((status & 32768) == 32768)
            {
                lastStatus = 11; // РРО занят выполнением предыдущей команды
            }
            if ((status & 32768) == 0)
            {
            }
        }

        /// <summary>
        /// Регистрация пользователя в фискальном аппарате
        /// </summary>
        /// <param name="user">Номер пользователя</param>
        /// <param name="pass">Пароль пользователя</param>
        public uint RegUser(byte user, UInt32 pass)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x17;
            List<byte> mess = new List<byte>();
            mess.Add(user);
            mess.AddRange(BitConverter.GetBytes(pass));
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Печать отчета (Z или X)
        /// </summary>
        /// <param name="type">Тип отчета. 0 - X звіт, 1 - Z звіт, 2 - Продаж товарів за день, 6 - Закриття дня на банківському терміналі</param>
        public uint PrintRep(sbyte type)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x10;
            byte[] data = new byte[1];
            data[0] = (byte)type;
            return SendData(cmd, data);
        }

        /// <summary>
        /// Відкриття чека
        /// </summary>
        public uint BegChk()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x08;
            return SendData(cmd);
        }

        /// <summary>
        /// Продаж товару з занесенням тільки в ЕЖ
        /// </summary>
        /// <param name="code">Код товару</param>
        /// <param name="quant">Кількість</param>
        /// <param name="price">Ціна</param>
        /// <param name="group">Група</param>
        /// <param name="tax">Ставка податку</param>
        /// <param name="unitStr">Одиниці виміру(str3)</param>
        /// <param name="nameStr">Назва товару(str70)</param>
        public uint NProd(UInt64 code, UInt32 quant, UInt32 price, byte group, byte tax, string unitStr, string nameStr)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x03;
            byte[] unitBytes = Encoding.Default.GetBytes(unitStr);
            byte[] nameBytes = Encoding.Default.GetBytes(nameStr);
            byte[] unit = { 0x20, 0x20, 0x20 };
            byte[] name =  {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20};
            List<byte> mess = new List<byte>();
            mess.AddRange(BitConverter.GetBytes(code));
            mess.AddRange(BitConverter.GetBytes(quant));
            mess.AddRange(BitConverter.GetBytes(price));
            mess.Add(group);
            mess.Add(tax);
            for (int i = 0; (i < unitStr.Length) && (i < unit.Length); i++)
            {
                unit[i] = (byte)unitBytes[i];
            }
            for (int i = 0; (i < nameStr.Length) && (i < name.Length); i++)
            {
                name[i] = (byte)nameBytes[i];
            }
            for (int i = 0; i < unit.Length; i++)
            {
                mess.Add(unit[i]);
            }
            for (int i = 0; i < name.Length; i++)
            {
                mess.Add(name[i]);
            }
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Оплата в чеку
        /// </summary>
        /// <param name="paymentType">Номер форми оплати</param>
        /// <param name="Sum">Сума внесена покупцем</param>
        public uint Oplata(byte paymentType, UInt32 sum)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x0F;
            List<byte> mess = new List<byte>();
            mess.Add(paymentType);
            mess.AddRange(BitConverter.GetBytes(sum));
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Закриття чека
        /// </summary>
        public uint EndChk()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x09;
            return SendData(cmd);
        }

        /// <summary>
        /// Відміна останньої операції в чеку
        /// </summary>
        public uint VoidLast()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x18;
            return SendData(cmd);
        }

        /// <summary>
        /// Відміна чеку
        /// </summary>
        /// <returns>результат відправки</returns>
        public uint VoidChk()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x19;
            return SendData(cmd);
        }

        /// <summary>
        /// Службове внесення/вилучення
        /// </summary>
        /// <param name="paymentType">Номер форми оплати</param>
        /// <param name="type">Тип: 0-внесення; 1-вилучення</param>
        /// <param name="sum">Сума</param>
        public uint InOut(byte paymentType, byte type, UInt32 sum)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x21;
            List<byte> mess = new List<byte>();
            mess.Add(paymentType);
            mess.Add(type);
            mess.AddRange(BitConverter.GetBytes(sum));
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Відкриття службового документа
        /// </summary>
        public uint BegDoc()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x0A;
            return SendData(cmd);
        }

        /// <summary>
        /// Закриття службового документа
        /// </summary>
        public uint EndDoc()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x0C;
            return SendData(cmd);
        }

        /// <summary>
        /// Друк стрічки в службовому документі
        /// </summary>
        /// <param name="param">0x01 - Подвійна ширина, 0x02 - Подвійна висота, 0x04 - автоматичне центрування</param>
        /// <param name="textStr">str64</param>
        public uint TextDoc(byte param, string textStr)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x0B;
            byte[] bytes = Encoding.Default.GetBytes(textStr);
            byte[] text =  {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20};
            List<byte> mess = new List<byte>();
            mess.Add(param);
            for (int i = 0; (i < textStr.Length) && (i < text.Length); i++)
            {
                text[i] = bytes[i];
            }
            for (int i = 0; i < text.Length; i++)
            {
                mess.Add(text[i]);
            }
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Друк стрічки коментаря в чеку
        /// </summary>
        /// <param name="textStr">str44</param>
        public uint TextChk(string textStr)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x0D;
            byte[] bytes = Encoding.Default.GetBytes(textStr);
            byte[] text =  {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20};
            List<byte> mess = new List<byte>();
            for (int i = 0; (i < textStr.Length) && (i < text.Length); i++)
            {
                text[i] = bytes[i];
            }
            for (int i = 0; i < text.Length; i++)
            {
                mess.Add(text[i]);
            }
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Друк стрічки коментаря в чеку(extended)
        /// </summary>
        /// <param name="textStr">str64</param>
        public uint TextChkEx(string textStr)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x2B;
            byte[] bytes = Encoding.Default.GetBytes(textStr);
            byte[] text =  {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20};
            List<byte> mess = new List<byte>();
            for (int i = 0; (i < textStr.Length) && (i < text.Length); i++)
            {
                text[i] = bytes[i];
            }
            for (int i = 0; i < text.Length; i++)
            {
                mess.Add(text[i]);
            }
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Встановлення націнок/знижок
        /// </summary>
        /// <param name="type">Параметри знижки. Бітова маска 0-й біт:0 - знижка, 1 - націнка; 1-й біт:0 - відсоткова, 1 - сумова; 2-й біт:1 - соціальна знижка; 3-й біт:1 - дисконт на пром підсумок</param>
        /// <param name="value">Процент або сума знижки</param>
        /// <param name="code">Код товару, на який надається знижка/націнка. Якщо 0 - знижка/націнка на останню позицію</param>
        public uint Discount(byte type, UInt32 value, UInt64 code)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x2B;
            List<byte> mess = new List<byte>();
            mess.Add(type);
            mess.AddRange(BitConverter.GetBytes(value));
            mess.AddRange(BitConverter.GetBytes(code));
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Зміна по 24 год. починається від друку першого чека.
        /// </summary>
        public uint ShiftBegin()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x12;
            return SendData(cmd);
        }

        /// <summary>
        /// Закрыть соединение
        /// </summary>
        /// <returns></returns>
        public uint Disconnect()
        {
            return (uint)printer.Close();
        }

        /// <summary>
        /// Печать периодического отчета по выбраным датам, по номерам
        /// </summary>
        /// <param name="RepID">тип отчета</param>
        /// <param name="ZBeg">начальний номер отчета</param>
        /// <param name="ZEnd">конечный номер отчета</param>
        /// <param name="DatBeg">начальная дата отчета</param>
        /// <param name="DatEnd">конечная дата отчета</param>
        /// <returns></returns>
        public uint PrintFiscRep(byte RepID, UInt16 ZBeg, UInt16 ZEnd, DateTime DatBeg, DateTime DatEnd)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x22;
            List<byte> mess = new List<byte>();
            mess.Add(RepID);
            mess.AddRange(BitConverter.GetBytes(ZBeg));
            mess.AddRange(BitConverter.GetBytes(ZEnd));
            mess.Add(Convert.ToByte(DatBeg.Day));
            mess.Add(Convert.ToByte(DatBeg.Month));
            mess.Add(Convert.ToByte(DatBeg.Year - 2000));
            mess.Add(0);
            mess.Add(Convert.ToByte(DatEnd.Day));
            mess.Add(Convert.ToByte(DatEnd.Month));
            mess.Add(Convert.ToByte(DatEnd.Year - 2000));
            mess.Add(0);
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Установка времени на РРО
        /// </summary>
        /// <returns></returns>
        public uint PrgTime()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x04;
            byte sbcmd = 0x28;
            UInt32 pass = 0;
            List<byte> mess = new List<byte>();
            mess.Add(sbcmd);
            mess.AddRange(BitConverter.GetBytes(pass));
            mess.Add(Convert.ToByte(DateTime.Now.Hour));
            mess.Add(Convert.ToByte(DateTime.Now.Minute));
            mess.Add(Convert.ToByte(DateTime.Now.Second));
            mess.Add(0);
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Печать копии чека по номеру
        /// </summary>
        /// <param name="DI">номер чека(0 - повтор последнего)</param>
        /// <returns></returns>
        public uint CopyChk(UInt32 DI = 0)
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x26;
            List<byte> mess = new List<byte>();
            mess.AddRange(BitConverter.GetBytes(DI));
            return SendData(cmd, mess.ToArray());
        }

        /// <summary>
        /// Запрос операционной информации за день (суммы за день)
        /// </summary>
        /// <returns></returns>
        public uint DaylySum()
        {
            this.GetStatusEx();
            //while ((lastStatus == 11) || (ANS == 0x04))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x31;
            return SendData(cmd);
        }

        public uint Signal()
        {
            this.GetStatusEx();
            //while (lastStatus == 11)
            //{
            //    Logger log = new Logger();
            //    log.Write("GetExStatusLoop.lastStatus=" + lastStatus);
            //    System.Threading.Thread.Sleep(500);
            //    this.GetStatusEx();
            //}
            byte cmd = 0x02;
            UInt16 tone = 3000;
            UInt16 Len = 10;
            List<byte> mess = new List<byte>();
            mess.AddRange(BitConverter.GetBytes(tone));
            mess.AddRange(BitConverter.GetBytes(Len));
            return SendData(cmd, mess.ToArray());
        }

        #endregion

        /// <summary>
        /// Таймер ожидания ответа от принтера
        /// </summary>
        private void SetTimer()
        {
            receivingTimer = new System.Timers.Timer(5000);
            receivingTimer.Elapsed += OnTimedEvent;
            receivingTimer.AutoReset = false;
            receivingTimer.Enabled = true;
        }

        /// <summary>
        /// Обработчик события превышения времени ожидания ответа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            timeElapsed = true;
        }

        private void OnStatusChangedEvent(int status)
        {
            if (this.StatusChangedEvent != null)
                this.StatusChangedEvent(status);
        }
    }
}
