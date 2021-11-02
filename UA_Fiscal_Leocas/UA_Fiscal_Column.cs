using System;
using System.Globalization;
using SkiData.FiscalDevices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using System.Collections.Generic;
using System.Threading;

namespace UA_Fiscal_Leocas
{
    class UA_Fiscal_Column: IFiscalDevice, IShift
    {
        #region Fields
        private FiscalDeviceCapabilities fiscalDeviceCapabilities;
        public static System.Timers.Timer reCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer timeCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer paymentTimeoutTimer = new System.Timers.Timer(285000);
        public bool inTransaction = false;
        public bool transactionTimeOutExceed = false;
        private bool disposed;
        public StateInfo deviceState;
        public static bool shiftNotClosed = false;
        string transaction = "";
        private byte paymentType;
        public string paymentMachineName = "";
        public string paymentMachineId = "";
        public static string MachineID;
        public string connectionString = "";
        public DeviceType paymentMachineType;
        bool zrepDone = false, shiftStarted = false;
        List<Item> items;
        Payment payment;
        public FiscalDeviceConfiguration fiscalDeviceConfiguration;
        //LeoCasLib printer;
        #endregion

        #region Construction

        public UA_Fiscal_Column()
        {
            OnTrace(new TraceEventArgs("Constructor called...", TraceLevel.Info));
            this.fiscalDeviceCapabilities = new FiscalDeviceCapabilities(true, false, true);
        }

        #endregion

        #region IFiscal Members
        public FiscalDeviceCapabilities Capabilities => this.fiscalDeviceCapabilities;

        public void Notify(int notificationId)
        {
            switch (notificationId)
            {
                case 1:
                    break;
                default:
                    break;
            }
        }

        public StateInfo DeviceState => this.deviceState;

        public Result Install(FiscalDeviceConfiguration configuration)
        {
            inTransaction = true;
            LeoCasLib printer = new LeoCasLib();
            fiscalDeviceConfiguration = configuration;
            paymentMachineId = configuration.DeviceId;
            MachineID = paymentMachineId;
            Logger log = new Logger(MachineID);
            paymentMachineName = configuration.DeviceName;
            connectionString = configuration.CommunicationChannel;
            deviceState = new StateInfo(false, SkiDataErrorCode.Ok, "New connection");
            uint err = printer.Connect(connectionString);
            if (err == 0)
                this.StatusChangedEvent(true, (int)deviceState.ErrorCode, "РРО подлючен");
            SetTimer();
            try
            {
                //printer.PrgTime();
                err = printer.RegUser(1, 1);
                if (err != 0)
                    ErrorAnalizer(err);
                if (deviceState.FiscalDeviceReady)
                {
                    err = printer.ShiftBegin();
                    if (err != 0)
                        ErrorAnalizer(err);
                }
                printer.Disconnect();
            }
            catch (Exception e)
            {
                log.Write($"FD  : Install - Exception: {e.Message}");
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                inTransaction = false;
                printer.Disconnect();
            }
            log.Write($"FD  : Install - {deviceState.FiscalDeviceReady}");
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady);
        }

        public string Name => "LeoCAS.FiscalDevice";

        public string ShortName => "LeoCAS";

        public Result OpenTransaction(TransactionData transactionData)
        {
            //LeoCasLib printer = new LeoCasLib();
            //LeoCasFunction printer = new LeoCasFunction();
            Logger log = new Logger(MachineID);
            items = new List<Item>();
            inTransaction = true;
            transaction = transactionData.ReferenceId;
            SetPaymentTimeoutTimerTimer();
            try
            {
                //uint err = printer.Connect(connectionString);
                //printer.PrgTime();
                log.Write($"FD  : OpenTransaction result: {deviceState.FiscalDeviceReady}");
               // printer.Disconnect();
            }
            catch (Exception e)
            {
                //printer.Disconnect();
                log.Write($"FD  : OpenTransaction Exception: {e.Message}");
            }
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result AddItem(Item item)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Add Item - Item ID: {item.Id}, Item Name: {item.Name}, Item Quantity: {item.Quantity}");
            items.Add(item);
            return new Result(true);
        }

        public Result AddDiscount(Discount discount)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Add discount {discount.Amount}");
            return new Result(true);
        }

        public Result AddPayment(Payment payment)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Add Payment - Payment Type: {payment.PaymentType}, Payment Amount: {payment.Amount}");
            this.payment = payment;
            return new Result(true);
        }

        public Result CloseTransaction()
        {
            Logger log = new Logger(MachineID);
            //LeoCasFunction printer = new LeoCasFunction();
            //printer = new LeoCasFunction();
            LeoCasLib printer = new LeoCasLib();
            bool doSale = true;
            bool receiptDone = true;
            log.Write($"FD  : Close Transaction -- -- -- -- -- ");
            if (!transactionTimeOutExceed)
            {
                try
                {
                    uint err = printer.Connect(connectionString);
                    log.Write($"FDAU: Connect: {err}");
                    err = printer.RegUser(1, 1);
                    err = printer.ShiftBegin();
                    err = printer.BegChk();
                    log.Write($"FD  : BegCheck: {err}");
                    if (err != 0)
                        receiptDone = ErrorAnalizer(err);
                    else
                    {
                        StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "");
                    }
                    if (payment.PaymentType == PaymentType.CreditCard)
                    {
                        #region Add Receipt from bank
                        try
                        {
                            SQLConnect sql = new SQLConnect();
                            err = printer.TextChk("---Відповідь з банку----");
                            string[] lines = sql.GetTransactionFromDBbyDevice(paymentMachineId, transaction).Split('\n');
                            for (int line = 0; line < lines.Length; line++)
                            {
                                if (lines[line].Length > 1)
                                {
                                    err = printer.TextChk(lines[line]);
                                }
                            }
                            err = printer.TextChk("------------------------");
                        }
                        catch
                        {
                            err = printer.TextChk("Відповідь не можна");
                            err = printer.TextChk("роздрукувати.");
                            err = printer.TextChk("Зверніться до адміністратора");
                            err = printer.TextChk("за чеком.");
                            err = printer.TextChk("------------------------");
                        }
                        #endregion
                    }
                    #region Add Item
                    foreach (Item item in items)
                    {
                        if (item.TotalPrice < 0)
                            doSale = doSale & false;
                    }
                    if (doSale)
                    {
                        foreach (Item item in items)
                        {
                            ParkingItem parkingItem = item as ParkingItem;
                            if (parkingItem != null)
                            {
                                DateTime myEntryTime = parkingItem.EntryTime;
                                DateTime myExitTime = parkingItem.PaidUntil;
                                err = printer.TextChkEx(string.Format("ID:{0}", parkingItem.TicketId));
                                err = printer.TextChkEx(string.Format("Час заїзду: {0}", myEntryTime.ToString(@"dd.MM.yy HH:mm")));
                                err = printer.TextChkEx(string.Format("  Виїзд до: {0}", myExitTime.ToString(@"dd.MM.yy HH:mm")));
                            }
                            ulong code = Convert.ToUInt64(item.Id);
                            uint quantity = Convert.ToUInt32(item.Quantity * 1000);
                            uint price = Convert.ToUInt32(item.UnitPrice * 100);
                            byte group = 1;
                            byte tax = 1;
                            string unit = item.QuantityUnit;
                            string name = item.Name;
                            log.Write($"FD  : Unit: {unit}, Item: {name}");
                            if (deviceState.FiscalDeviceReady & receiptDone)
                            {
                                err = printer.NProd(code, quantity, price, group, tax, unit, name);
                                log.Write($"FD  : NProd: {err}");
                                if (err != 0)
                                    receiptDone &= ErrorAnalizer(err);
                                StatusAnalizer();
                            }
                            log.Write($"FD  : Total price: {price}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        #endregion

                        #region Add Payment
                        switch (payment.PaymentType)
                        {
                            case PaymentType.Cash:
                                paymentType = 1;
                                break;
                            case PaymentType.CreditCard:
                                paymentType = 2;
                                break;
                            default:
                                paymentType = 1;
                                break;
                        }
                        uint sum = Convert.ToUInt32(payment.Amount * 100);
                        if (deviceState.FiscalDeviceReady & receiptDone)
                        {
                            err = printer.Oplata(paymentType, sum);
                            log.Write($"Payment: {err}");
                            if (err != 0)
                            {
                                receiptDone &= ErrorAnalizer(err);
                            }
                            printer.GetStatusEx();
                            StatusAnalizer();
                        }
                        log.Write($"FD  : Paid amount: {sum}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        #endregion

                        #region CloseReceipt
                        if (deviceState.FiscalDeviceReady & receiptDone)
                        {
                            err = printer.EndChk();
                            log.Write($"EndCheck: {err}");
                            if (err != 0)
                            {
                                receiptDone &= ErrorAnalizer(err);
                            }
                            printer.GetStatusEx();
                            StatusAnalizer();
                            log.Write($"FD  : Close receipt, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        else
                        {
                            printer.VoidChk();
                        }
                    }
                    else
                    {
                        err = printer.VoidChk();
                        if (err != 0)
                        {
                            ErrorAnalizer(err);
                        }
                        log.Write($"FD  : Credit Entry detected. Void receipt.");
                        return new TransactionResult(false, "Credit Entry detected");
                    }
                    log.Write($"FD  : Transaction result: {deviceState.FiscalDeviceReady & receiptDone}");
                    inTransaction = false;
                    #endregion
                }
                catch (Exception e)
                {
                    receiptDone = false;
                    //printer.Connect(connectionString);
                    uint err = printer.VoidChk();
                    if (err != 0)
                    {
                        ErrorAnalizer(err);
                    }
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                    log.Write($"FD  : Exception for Close Transaction: {e.Message}");
                    inTransaction = false;
                    printer.Disconnect();
                }
                transaction = null;
                inTransaction = false;
                printer.Disconnect();
                return new TransactionResult(deviceState.FiscalDeviceReady & receiptDone, $"форма оплати: {payment.PaymentType}");
            }
            else
            {
                log.Write($"FD  : Payment timed out");
                return new TransactionResult(false, "Transaction canceled by Time out");
            }
        }

        public void SetDisplayLanguage(CultureInfo cultureInfo)
        {
            new Result(true);
        }

        public void StartServiceDialog(IntPtr windowHandle, ServiceLevel serviceLevel)
        {

        }

        public Result VoidTransaction()
        {
            Logger log = new Logger(MachineID);
            try
            {
                if (transaction != null)
                {
                    //LeoCasFunction printer = new LeoCasFunction();
                    LeoCasLib printer = new LeoCasLib();
                    printer.Connect(connectionString);
                    uint err = printer.VoidChk();
                    if (err != 0)
                        ErrorAnalizer(err);
                    //printer.GetStatus();
                    StatusAnalizer();
                    printer.Disconnect();
                    inTransaction = false;
                }
            }
            catch (Exception e)
            {
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                log.Write($"FD  : Exception for VoidTransaction: {e.Message}");
                inTransaction = false;
            }
            return new Result(deviceState.FiscalDeviceReady);
        }
        #endregion

        #region Destructors
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }
            this.disposed = true;
        }

        ~UA_Fiscal_Column()
        {
            this.Dispose(false);
        }
        #endregion

        #region IShift Members
        public Result EndOfDay()
        {
            return new Result(true);
        }

        public Result OpenShift(Cashier cashier)
        {
            Logger log = new Logger(MachineID);
            LeoCasLib printer = new LeoCasLib();
            try
            {
                log.Write($"FD  : Open Shift -- -- -- -- -- -- --");
                uint err = printer.Connect(connectionString);
                log.Write($"FD  : Connect: {err}");
                if (err != 0)
                    ErrorAnalizer(err);
                if (deviceState.FiscalDeviceReady)
                {
                    err = printer.RegUser(1, 1);
                    log.Write($"FD  : RegUser: {err}");
                    if (err != 0)
                        ErrorAnalizer(err);
                }
                if (deviceState.FiscalDeviceReady)
                {
                    err = printer.ShiftBegin();
                    log.Write($"FD  : ShiftBegin: {err}");
                    if (err != 0)
                        ErrorAnalizer(err);
                }
                printer.Disconnect();
                if (deviceState.FiscalDeviceReady)
                    shiftStarted = true;
                log.Write($"FD  : OpenShift, result: {deviceState.FiscalDeviceReady}");
            }
            catch (Exception e)
            {
                printer.Disconnect();
                log.Write($"FD  : OpenShift exception: {e.Message}");
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
            }
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result CloseShift()
        {
            return new Result(true);
        }

        #endregion

        #region Fiscal Device Events
        public event EventHandler<EventArgs> DeviceStateChanged;
        public event EventHandler<ErrorClearedEventArgs> ErrorCleared;
        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        public event EventHandler<IrregularityDetectedEventArgs> IrregularityDetected;
        public event EventHandler<JournalizeEventArgs> Journalize;
        public event EventHandler<TraceEventArgs> Trace;

        public void OnTrace(TraceEventArgs args)
        {
            if (Trace != null)
                Trace(this, args);
        }

        public void OnDeviceStateChanged(EventArgs args)
        {
            if (DeviceStateChanged != null)
                DeviceStateChanged(this, args);
        }

        public void OnErrorOccurred(ErrorOccurredEventArgs args)
        {
            OnTrace(new TraceEventArgs($"Error {args.ErrorMessage} occurred", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, args.ErrorMessage);
            if (ErrorOccurred != null)
                ErrorOccurred(this, args);
        }

        public void OnErrorCleared(ErrorClearedEventArgs args)
        {
            OnTrace(new TraceEventArgs($"Error {deviceState.ErrorCode} cleared", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, "Ok");
            if (ErrorCleared != null)
                ErrorCleared(this, args);
        }

        public void OnIrregularityDetected(IrregularityDetectedEventArgs args)
        {
            if (IrregularityDetected != null)
                IrregularityDetected(this, args);
        }

        public void OnJournalize(JournalizeEventArgs args)
        {
            if (Journalize != null)
                Journalize(this, args);
        }
        #endregion

        #region Custom methods
        public bool ErrorAnalizer(UInt32 error)
        {
            Logger log = new Logger(MachineID);
            if (error != 0)
                log.Write($"EA  : Error: ({error.ToString()})");
            if (!Enumerations.DeviceErrors.TryGetValue(error, out string errMess))
                errMess = "NotDefined";
            switch (error)
            {
                case 1:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 2:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 3:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 4:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 5:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 6:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 7:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 8:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 9:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 103:  // Звіт у фіскальній памяті не знайдено.
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                        return false;
                    }
                    else
                    {
                        OnTrace(new TraceEventArgs(errMess, System.Diagnostics.TraceLevel.Info));
                        MessageBox.Show(new Form { TopMost = true }, "Звіт у фіскальній памяті не знайдено.");
                        return true;
                    }

                case 105:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 115:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 119:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 120:
                    LeoCasLib.customerDisplConnectionErr = true;
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 121:  //"Помилка в текстовій стрічці (Символ за діапазоном 0x20-0xff)"
                    {
                        OnTrace(new TraceEventArgs(errMess, System.Diagnostics.TraceLevel.Info));
                        return true;
                    }
                case 122:  //"Невірний номер функції"
                    {
                        OnTrace(new TraceEventArgs(errMess, System.Diagnostics.TraceLevel.Info));
                        return true;
                    }
                case 123:  //"Невірний номер операції"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                        return false;
                    }
                    else
                    {
                        OnTrace(new TraceEventArgs(errMess, System.Diagnostics.TraceLevel.Info));
                        return true;
                    }
                case 124:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 125:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 126: //"Помилка 24h"
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    LeoCasLib.blockedDueTo24 = true;
                    return false;
                case 128:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 131:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 137:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 200:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 201:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 202:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 203:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 204:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 205:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;

                case 224: //Зміна не відкріта
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    LeoCasLib.shiftStartedStatus = false;
                    return false;
                case 237: //"Оплата за чеком відсутня"
                    return false;
                case 244:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 245:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 246:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 413:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 258: // "Грошей в сейфі не достатньо"
                    Enumerations.DeviceErrors.TryGetValue(258, out errMess);
                    OnTrace(new TraceEventArgs(errMess, System.Diagnostics.TraceLevel.Info));
                    MessageBox.Show(new Form { TopMost = true }, "Грошей в сейфі не достатньо");
                    return false;
                case 260: // "Обнулення вже виконано"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                        return false;
                    }
                    else
                    {
                        this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, errMess);
                        return true;
                    }
                case 286: //"Друк відкладених звітів вже виконано"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                        return false;
                    }
                    else
                    {
                        this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, errMess);
                        MessageBox.Show(new Form { TopMost = true }, "Друк відкладених звітів вже виконано");
                        return true;
                    }

                case 601: //"Система зайнята іншою командою"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 602: //"Відсутні операції для закриття дня (чи зміни)"
                    if (LeoCasLib.blockedDueTo24 || LeoCasLib.blockedDueTo72 || LeoCasLib.blockedStatus)
                    {
                        this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                        return false;
                    }
                    else
                    {
                        OnTrace(new TraceEventArgs("Відсутні операції для закриття дня (чи зміни)", System.Diagnostics.TraceLevel.Info));
                        return true; ;
                    }

                default:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
            }
        }

        public int StatusAnalizer()
        {
            Logger log = new Logger(MachineID);
            log.Write($"SA  : Status: Blocked-{LeoCasFunction.blockedStatus}, Shift Started-{LeoCasFunction.shiftStartedStatus}, Out of paper-{LeoCasFunction.outOfPaperStatus}, Low paper-{LeoCasFunction.lowPaperStatus}");

            if (!LeoCasFunction.blockedStatus)
            {
                this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "РРО заблоковано");
            }
            if (LeoCasFunction.shiftStartedStatus)
            {
                if (!LeoCasFunction.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "Ok");
                }
            }
            if (!LeoCasFunction.outOfPaperStatus)
            {
                if (!LeoCasFunction.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "Ok");
                }
            }
            if (LeoCasFunction.lowPaperStatus)
            {
                if (LeoCasFunction.blockedStatus || LeoCasLib.outOfPaperStatus)
                { }
                else
                    this.OnErrorOccurred(new ErrorOccurredEventArgs("РРО: Закінчується чекова стрічка", SkiDataErrorCode.PaperLowWarning, deviceState.FiscalDeviceReady));
            }
            if (!LeoCasFunction.shiftStartedStatus)
            {
                if (!LeoCasFunction.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.OtherWarning, "Зміна не відкрита");
                }
            }
            if (LeoCasFunction.outOfPaperStatus)
            {
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.OutOfPaper, "РРО: Закінчилась чекова стрічка");
            }
            if (LeoCasFunction.blockedStatus)
            {
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "РРО заблоковано");
            }
            return 0;
        }

        private void ReCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
        }

        private void SetTimer()
        {
            timeCheckTimer.Elapsed += OnTimedEvent;
            timeCheckTimer.AutoReset = true;
            timeCheckTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Logger log = new Logger(MachineID);
            TimeSpan startZRep = TimeSpan.Parse("23:57");
            TimeSpan endZRep = TimeSpan.Parse("23:59");
            TimeSpan startShift = TimeSpan.Parse("00:02");
            TimeSpan endShift = TimeSpan.Parse("00:04");
            TimeSpan now = DateTime.Now.TimeOfDay;

            #region ClearError
            //if (!deviceState.FiscalDeviceReady)
            //{
            //    LeoCasFunction printer = new LeoCasFunction();
            //    try
            //    {
            //        log.Write("FDAU: Clear Error");
            //        printer.Connect(connectionString);
            //        if (printer.GetStatusEx() == 0)
            //            StatusAnalizer();
            //        printer.Disconnect();
            //    }
            //    catch (Exception ex)
            //    {
            //        log.Write(ex.Message);
            //        printer.Disconnect();
            //    }
            //}

            #endregion

            #region Time sync
            if (!inTransaction)
            {
                if (now.Minutes == 0)
                {
                    if (now.Hours == 19)
                    {
                        LeoCasLib printer = new LeoCasLib();
                        try
                        {
                            log.Write($"FDAU: Set Time -- -- -- -- -- -- -- ");
                            
                            uint err = printer.Connect(connectionString);
                            log.Write($"FDAU: Connect: {err}");
                            err = printer.PrgTime();
                            log.Write($"FDAU: ProgTime: {err}");
                            printer.Disconnect();
                            log.Write($"FDAU: Disconnect");
                        }
                        catch(Exception ex)
                        {
                            log.Write(ex.Message);
                            printer.Disconnect();
                        }
                    }
                }
            }
            #endregion

            #region Automated End of Day
            if (!inTransaction)
            {
                if (startZRep <= endZRep)
                {
                    if ((now >= startZRep && now <= endZRep) && (!zrepDone))
                    {
                        inTransaction = true;
                        LeoCasLib printer = new LeoCasLib();
                        try
                        {
                            log.Write($"FDAU: Close shift - - - - - - - -");
                            uint err = printer.Connect(connectionString);
                            log.Write($"FDAU: Connect: {err}");
                            if (err != 0)
                                ErrorAnalizer(err);
                            err = printer.RegUser(1, 1);
                            log.Write($"FDAU: RegUser: {err}");
                            if (err != 0)
                                ErrorAnalizer(err);
                            err = printer.PrintRep(16);
                            log.Write($"FDAU: PrintRep(16): {err}");
                            if (err != 0)
                                ErrorAnalizer(err);
                            if ((err == 0)||(err==260))
                            {
                                zrepDone = true;
                                shiftStarted = false;
                            }
                            printer.GetStatusEx();
                            StatusAnalizer();
                            printer.Disconnect();
                            inTransaction = false;
                            log.Write($"FDAU: Close shift result: {deviceState.FiscalDeviceReady}");
                        }
                        catch (Exception exc)
                        {
                            inTransaction = false;
                            log.Write($"FDAU: Close shift exception: {exc.Message}");
                            StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, exc.Message);
                            printer.Disconnect();
                        }

                    }
                }
            }
            #endregion

            #region Automated New day
            if (!inTransaction)
            {
                if (startShift <= endShift)
                {
                    if ((now >= startShift && now <= endShift) && (!shiftStarted) && (zrepDone))
                    {
                        inTransaction = true;
                        LeoCasLib printer = new LeoCasLib();
                        try
                        {
                            log.Write($"FDAU: New Shift -- -- -- -- -- -- -- --");
                            uint err = printer.Connect(connectionString);
                            log.Write($"FDAU: Connect: {err}");
                            StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "New shift");
                            err = printer.RegUser(1, 1);
                            log.Write($"FDAU: RegUser: {err}");
                            if (err != 0)
                                ErrorAnalizer(err);
                            if (deviceState.FiscalDeviceReady)
                            {
                                err = printer.ShiftBegin();
                                log.Write($"FDAU: ShiftBegin: {err}");
                                if (err != 0)
                                    ErrorAnalizer(err);
                                if (deviceState.FiscalDeviceReady)
                                    this.shiftStarted = true;
                            }
                            printer.GetStatusEx();
                            StatusAnalizer();
                            printer.Disconnect();
                            inTransaction = false;
                            zrepDone = false;
                            log.Write($"FDAU: New shift result: {deviceState.FiscalDeviceReady}");
                        }
                        catch (Exception exc)
                        {
                            log.Write($"FDAU: New shift exception: {exc.Message}");
                            printer.Disconnect();
                            inTransaction = false;
                            StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, exc.Message);
                        }
                    }
                }
            }
            #endregion
        }

        private void StatusChangedEvent(bool isready, int errorCode, string errorMessage)
        {
            if (isready)
            {
                //if (!deviceState.FiscalDeviceReady)
                {
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, true));
                    OnDeviceStateChanged(new ErrorClearedEventArgs(SkiDataErrorCode.Ok, true));
                }

            }
            else
            {
                if (deviceState.FiscalDeviceReady)
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, true));
                else
                {
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, isready));
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, isready));
                }
            }
            Logger log = new Logger(MachineID);
            log.Write($"FDST: Device State: {deviceState.FiscalDeviceReady}, description: {deviceState.Description}");
        }

        private void SetPaymentTimeoutTimerTimer()
        {
            paymentTimeoutTimer.Elapsed += OnPaymentTimeOutTimerEvent;
            paymentTimeoutTimer.AutoReset = false;
            paymentTimeoutTimer.Enabled = true;
            transactionTimeOutExceed = false;
        }

        private void OnPaymentTimeOutTimerEvent(object sender, ElapsedEventArgs e)
        {
            transactionTimeOutExceed = true;
            paymentTimeoutTimer.Enabled = false;
            paymentTimeoutTimer.Elapsed -= OnPaymentTimeOutTimerEvent;
        }
        #endregion
    }
}
