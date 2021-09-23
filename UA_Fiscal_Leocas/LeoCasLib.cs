using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UA_Fiscal_Leocas
{
    public class LeoCasLib
    {
        #region Export

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 RegUser(IntPtr handle, byte Oper, UInt32 Passw);

        [DllImport("DriverEKKA3.dll")]
        private static extern IntPtr Connect(UInt32 AParam, string Config, UInt32 TimeOut);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 BegChk(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 BegRet(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 NProd(IntPtr handle, UInt64 Code, UInt32 Quant, UInt32 Price, byte Grp, byte Tax, string Unit, string Name);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 Oplata(IntPtr handle, byte OplID, UInt32 Sum);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 EndChk(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 PrintRep(IntPtr handle, sbyte ID);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 VoidLast(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 VoidChk(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 InOut(IntPtr handle, byte paymentType, byte type, UInt32 sum);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 BegDoc(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 End_Doc(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 TextDoc(IntPtr handle, byte param, string textStr);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 TextChk(IntPtr handle, string textStr);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 TextChkEx(IntPtr handle, string textStr);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 Discount(IntPtr handle, byte type, UInt32 value, UInt64 code);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 SmenBegin(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 Disconnect(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 PrintFiscRep(IntPtr handle, byte RepID, UInt16 ZBeg, UInt16 ZEnd, DateTime DatBeg, DateTime DatEnd);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 CopyChk(IntPtr handle, UInt32 ID);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 PrintFiscRep(IntPtr handle, byte RepID, UInt16 ZBeg, UInt16 ZEnd, UInt32 DatBeg, UInt32 DatEnd);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt16 GetStatus(IntPtr handle);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 GetStatusEx(IntPtr handle, out UInt16 statExt);

        [DllImport("DriverEKKA3.dll")]
        private static extern UInt32 PrgTime(IntPtr handle, UInt32 pass, UInt32 time);

        //[DllImport("DriverEKKA3.dll")]
        //private static extern UInt32 HardRestart(IntPtr handle, byte pass, UInt32 time);

        #endregion

        IntPtr handle = IntPtr.Zero;
        UInt16 lastStatusEx = 0;

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

        //// Printer statuses
        //public bool blockedStatus = false;
        //public bool shiftStartedStatus = false;
        //public bool fiscReceiptBegStatus = false;
        //public bool docBegStatus = false;
        //public bool operRegStatus = false;
        //public bool outOfPaperStatus = false;
        //public bool blockedDueTo72 = false;
        //public bool blockedDueTo24 = false;
        //public bool lowPaperStatus = false;
        //public bool customerDisplConnectionErr = false;

        public struct PayInfo
        {
            public UInt32 PaySaleSumm;
            public UInt32 PayRetSumm;
            public UInt32 InSumm;
            public UInt32 OutSumm;
        }

        #region Methods
        public void GetStatusEx()
        {
            UInt16 lastStatusExOld = lastStatusEx;
            GetStatusEx(this.handle, out lastStatusEx);
            if (lastStatusEx != lastStatusExOld)
            {
                ExtStatus(lastStatusEx);
            }
        }

        /// <summary>
        /// Connect to the FD
        /// </summary>
        /// <param name="connectionString">connection string("IP:port")</param>
        /// <returns></returns>
        public UInt32 Connect(string connectionString)
        {
            this.handle = Connect(1, connectionString, 5000); // Lan connection
            //this.handle = NativeMethods.Connect(0, connectionString, 5000);   // Com connection
            if (this.handle != IntPtr.Zero)
                return 0;
            else
                return 3;
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (this.handle != IntPtr.Zero)
            {
                Disconnect(this.handle);
                this.handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user">user ID</param>
        /// <param name="pass">user pass</param>
        /// <returns></returns>
        public UInt32 RegUser(byte user, UInt32 pass)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = RegUser(handle, user, pass);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = RegUser(handle, user, pass);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = RegUser(handle, user, pass);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        /// <summary>
        /// Print report. Type ID (X = 0, Z(200) = 1 Z(401) = 16, Print Z = 17)
        /// </summary>
        /// <param name="type">report type ID (X = 0, Z(200) = 1 Z(401) = 16, Print Z = 17)</param>
        /// <returns></returns>
        public UInt32 PrintRep(sbyte type)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = PrintRep(this.handle, type);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = PrintRep(this.handle, type);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = PrintRep(this.handle, type);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 BegChk()
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = BegChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = BegChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = BegChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 BegRet()
        {
            UInt32 result = 3;
            if (handle != IntPtr.Zero)
            {
                result = BegRet(this.handle);
            }
            return result;
        }

        public UInt32 NProd(UInt64 code, UInt32 quant, UInt32 price, byte group, byte tax, string unitStr, string nameStr)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = NProd(this.handle, code, quant, price, group, tax, Truncate(unitStr, 3), Truncate(nameStr, 70));
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = NProd(this.handle, code, quant, price, group, tax, Truncate(unitStr, 3), Truncate(nameStr, 70));
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = NProd(this.handle, code, quant, price, group, tax, Truncate(unitStr, 3), Truncate(nameStr, 70));
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 Oplata(byte paymentType, UInt32 sum)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = Oplata(this.handle, paymentType, sum);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = Oplata(this.handle, paymentType, sum);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = Oplata(this.handle, paymentType, sum);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 EndChk()
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = EndChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = EndChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = EndChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 VoidLast()
        {
            UInt32 result = 3;
            if (handle != IntPtr.Zero)
            {
                result = VoidLast(this.handle);
            }
            return result;
        }

        public UInt32 VoidChk()
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = VoidChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = VoidChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = VoidChk(this.handle);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 InOut(byte paymentType, byte type, UInt32 sum)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            if (handle != IntPtr.Zero)
            {
                result = InOut(this.handle, paymentType, type, sum);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = InOut(this.handle, paymentType, type, sum);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = InOut(this.handle, paymentType, type, sum);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 BegDoc()
        {
            UInt32 result = 3;
            if (handle != IntPtr.Zero)
            {
                result = BegDoc(this.handle);
            }
            return result;
        }

        public UInt32 EndDoc()
        {
            UInt32 result = 3;
            if (handle != IntPtr.Zero)
            {
                result = End_Doc(this.handle);
            }
            return result;
        }

        public UInt32 TextDoc(byte param, string textStr)
        {
            UInt32 result = 3;
            if (handle != IntPtr.Zero)
            {
                result = TextDoc(handle, param, Truncate(textStr, 64));
            }
            return result;
        }

        public UInt32 TextChk(string textStr)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");

            if (handle != IntPtr.Zero)
            {
                result = TextChk(handle, Truncate(textStr, 44));
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = TextChk(handle, Truncate(textStr, 44));
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = TextChk(handle, Truncate(textStr, 44));
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 TextChkEx(string textStr)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");

            if (handle != IntPtr.Zero)
            {
                result = TextChkEx(handle, Truncate(textStr, 64));
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = TextChkEx(handle, Truncate(textStr, 64));
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = TextChkEx(handle, Truncate(textStr, 64));
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 Discount(byte type, UInt32 value, UInt64 code)
        {
            UInt32 result = 3;
            if (handle != IntPtr.Zero)
            {
                result = Discount(handle, type, value, code);
            }
            return result;
        }

        public UInt32 ShiftBegin()
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            
            if (handle != IntPtr.Zero)
            {
                result = SmenBegin(handle);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = SmenBegin(handle);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = SmenBegin(handle);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        //public UInt32 Close()
        //{
        //    UInt32 result = 3;
        //    if (handle != IntPtr.Zero)
        //    {
        //        result = Disconnect(handle);
        //    }
        //    return result;
        //}

        public UInt32 CopyChk(UInt32 ID = 0)
        {
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            
            if (handle != IntPtr.Zero)
            {
                result = CopyChk(handle, ID);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = CopyChk(handle, ID);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = CopyChk(handle, ID);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        public UInt32 PrintFiscRep(byte RepID, UInt16 ZBeg, UInt16 ZEnd, DateTime DatBeg, DateTime DatEnd)
        {
            byte[] datBegBYTEARR = new byte[4];
            byte[] datEndBYTEARR = new byte[4];
            datBegBYTEARR[0] = Convert.ToByte(DatBeg.Day);
            datBegBYTEARR[1] = Convert.ToByte(DatBeg.Month);
            datBegBYTEARR[2] = Convert.ToByte(DatBeg.Year - 2000);
            datBegBYTEARR[3] = 0;

            datEndBYTEARR[0] = Convert.ToByte(DatEnd.Day);
            datEndBYTEARR[1] = Convert.ToByte(DatEnd.Month);
            datEndBYTEARR[2] = Convert.ToByte(DatEnd.Year - 2000);
            datEndBYTEARR[3] = 0;

            UInt32 datBegUINT32 = BitConverter.ToUInt32(datBegBYTEARR, 0);
            UInt32 datEndUINT32 = BitConverter.ToUInt32(datEndBYTEARR, 0);
            UInt32 result = 3;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            
            if (handle != IntPtr.Zero)
            {
                result = PrintFiscRep(handle, RepID, ZBeg, ZEnd, datBegUINT32, datEndUINT32);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            return result;
        }

        public void GetStatus()
        {
            this.StatusAnalizer(GetStatus(handle));
            UInt16 stat = 0;
            if (handle != IntPtr.Zero)
            {
                stat = GetStatus(handle);
                this.StatusAnalizer(stat);
            }
        }

        public UInt32 PrgTime()
        {
            UInt32 pass = 0;
            byte[] timeNow = new byte[4];
            timeNow[0] = Convert.ToByte(DateTime.Now.Hour);
            timeNow[1] = Convert.ToByte(DateTime.Now.Minute);
            timeNow[2] = Convert.ToByte(DateTime.Now.Second);
            timeNow[3] = 0;
            UInt32 timeNowUInt32 = BitConverter.ToUInt32(timeNow, 0);
            UInt32 result = 3;

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            Logger log = new Logger("Temp", "LeocasLib");
            
            if (handle != IntPtr.Zero)
            {
                result = PrgTime(handle, pass, timeNowUInt32);
                log.Write(currentMethodName.ToString(), $"Attempt 0, result: {result.ToString()}");
            }
            if((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = PrgTime(handle, pass, timeNowUInt32);
                log.Write(currentMethodName.ToString(), $"Attempt 1, result: {result.ToString()}");
            }
            if ((handle != IntPtr.Zero) && result != 0 && result <= 5)
            {
                result = PrgTime(handle, pass, timeNowUInt32);
                log.Write(currentMethodName.ToString(), $"Attempt 2, result: {result.ToString()}");
            }
            return result;
        }

        private string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        private void StatusAnalizer(UInt16 status)
        {
            //ushort lastStatus;
            if ((status & 1) == 1)
            {
                blockedStatus = true;
                //lastStatus = 1;
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
                //lastStatus = 3; // Есть незавершенные операции
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
                //lastStatus = 8; // Бумага заканчивается
                lowPaperStatus = true;
            }
            if ((status & 4096) == 0)
                lowPaperStatus = false;
            if ((status & 8192) == 8192)
            {
                //lastStatus = 9; // Бумага закончилась
                outOfPaperStatus = true;
            }
            if ((status & 8192) == 0)
            {
                outOfPaperStatus = false;
            }
            if ((status & 16384) == 16384)
            {
                //lastStatus = 10; // Открыта крышка принтера
            }
            if ((status & 32768) == 32768)
            {
                //lastStatus = 11; // РРО занят выполнением предыдущей команды
            }
            if ((status & 32768) == 0)
            {
            }
        }

        private int ExtStatus(UInt16 exStatus)
        {

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

        //private int Reset()
        //{
        //    UInt32 result = 3;
        //    if (handle != IntPtr.Zero)
        //    {
        //        result = HardRestart(handle, ID);
        //    }
        //    return result;
        //}

        private bool ErrorHandler(int error)
        {
            return true;
        }
        #endregion
    }
}
