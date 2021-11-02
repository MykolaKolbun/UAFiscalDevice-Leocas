using System;
using System.Globalization;
using SkiData.FiscalDevices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using System.Collections.Generic;

namespace UA_Fiscal_Leocas
{
    public class UA_Fiscal_APM : IFiscalDevice, ICash, IShift
    {
        #region Fields
        private FiscalDeviceCapabilities fiscalDeviceCapabilities;
        public static System.Timers.Timer reCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer timeCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer paymentTimeoutTimer = new System.Timers.Timer(285000);
        public bool inTransaction = false;
        public bool transactionTimeOutExceed = false;
        private bool disposed;
        //public bool isReady = true;
        public StateInfo deviceState;
        public static bool shiftNotClosed = false;
        private ServiceForm_APM sf = null;
        string transaction = "";
        private byte paymentType;
        public string paymentMachineName = "";
        public string paymentMachineId = "";
        public static string MachineID = "";
        public string connectionString = "";
        public DeviceType paymentMachineType;
        bool zrepDone = false, shiftStarted = false;
        List<Item> items;  //  Temp container for Item. Try to print two receipts on one list.
        Payment payment; // Temp container for Payment. Try to print two receipts on one list.
        LeoCasLib printer = new LeoCasLib();
        public FiscalDeviceConfiguration fiscalDeviceConfiguration;
        #endregion

        #region Construction

        public UA_Fiscal_APM()
        {
            OnTrace(new TraceEventArgs("Constructor called...", TraceLevel.Info));
            this.fiscalDeviceCapabilities = new FiscalDeviceCapabilities(true, false, true);
        }

        #endregion

        #region IFiscal Members
        public FiscalDeviceCapabilities Capabilities => this.fiscalDeviceCapabilities;

        public void Notify(int notificationId)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Notify {notificationId}");
            switch (notificationId)
            {
                case 1:
                    if (this.sf != null)
                    {
                        this.sf.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        public StateInfo DeviceState => this.deviceState;

        public Result Install(FiscalDeviceConfiguration configuration)
        {
            inTransaction = true;
            fiscalDeviceConfiguration = configuration;
            paymentMachineId = configuration.DeviceId;
            MachineID = paymentMachineId;
            paymentMachineName = configuration.DeviceName;
            connectionString = configuration.CommunicationChannel;
            Logger log = new Logger(MachineID);
            deviceState = new StateInfo(true, SkiDataErrorCode.Ok, "New connection");
            uint err = printer.Connect(connectionString);
            if (err == 0)
                this.StatusChangedEvent(true, (int)deviceState.ErrorCode, "РРО подлючен");
            else
                ErrorAnalizer(err);
            SetTimer();
            if (deviceState.FiscalDeviceReady)
            {
                try
                {
                    err = printer.PrgTime();
                    err = printer.RegUser(1, 1);
                    if (err != 0)
                        ErrorAnalizer(err);
                    if (deviceState.FiscalDeviceReady)
                    {
                        err = printer.ShiftBegin();
                        if (err != 0)
                            ErrorAnalizer(err);
                    }
                    inTransaction = false;
                }
                catch (Exception e)
                {

                    log.Write($"FD  : Install - Exception: {e.Message}");
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                    inTransaction = false;
                }
            }
            log.Write($"FD  : Install - {deviceState.FiscalDeviceReady}");
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady);
        }

        public string Name => "LeoCAS.FiscalDevice";

        public string ShortName => "LeoCAS";

        public Result OpenTransaction(TransactionData transactionData)
        {
            Logger log = new Logger(MachineID);
            items = new List<Item>();
            inTransaction = true;
            transaction = transactionData.ReferenceId;
            SetPaymentTimeoutTimerTimer();
            log.Write($"FD  : OpenTransaction: {deviceState.FiscalDeviceReady}");
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
            bool doSale = true;
            bool receiptDone = true;
            if (!transactionTimeOutExceed)
            {
                Logger log = new Logger(MachineID);
                log.Write($"FD  : Close Transaction");
                printer.GetStatusEx();
                StatusAnalizer();
                string paymentTypeStr = "";
                bool visaDiscount = false;
                try
                {
                    uint err = printer.RegUser(1, 1);
                    err = printer.ShiftBegin();
                    err = printer.BegChk();
                    if (err != 0)
                        receiptDone = ErrorAnalizer(err);
                    if (payment.PaymentType == PaymentType.CreditCard)
                    {
                        log.Write($"FD  : Print bank response");
                        #region Add Receipt from bank
                        try
                        {
                            SQLConnect sql = new SQLConnect();
                            printer.GetStatusEx();
                            StatusAnalizer();
                            printer.TextChk("---Відповідь з банку----");
                            log.Write($"FD  : Transaction ID: {transaction}");
                            string[] lines = sql.GetTransactionFromDBbyDevice(paymentMachineId, transaction).Split('\n');

                            for (int line = 0; line < lines.Length; line++)
                            {
                                if (lines[line].Length > 1)
                                {
                                    printer.TextChkEx(lines[line]);
                                }
                            }
                            printer.TextChk("------------------------");
                        }
                        catch (Exception e)
                        {
                            printer.TextChk("Відповідь не можна");
                            printer.TextChk("роздрукувати.");
                            printer.TextChkEx("Зверніться до адміністратора");
                            printer.TextChk("за чеком.");
                            printer.TextChk("------------------------");
                            log.Write($"FD  : Print bank response exception: {e.Message}");
                        }
                        #endregion
                        #region Check Visa Discount
                        try
                        {
                            SQLConnect sql = new SQLConnect();
                            visaDiscount = sql.GetDiscountProperty(transaction, paymentMachineId);
                            log.Write($"FD  : Visa discount - {visaDiscount}");
                        }
                        catch
                        {

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
                                err = printer.TextChkEx($"ID:{parkingItem.TicketId}");
                                if (err != 0)
                                    receiptDone = receiptDone & ErrorAnalizer(err);
                                err = printer.TextChkEx($"Заїзд в:{myEntryTime.ToString(@"dd.MM.yy HH:mm:ss")}");
                                if (err != 0)
                                    receiptDone = receiptDone & ErrorAnalizer(err);
                                err = printer.TextChkEx($"Виїхати:{myExitTime.ToString(@"dd.MM.yy HH:mm:ss")}");
                                if (err != 0)
                                    receiptDone = receiptDone & ErrorAnalizer(err);
                            }
                            ulong code = Convert.ToUInt64(item.Id);
                            uint quantity = Convert.ToUInt32(item.Quantity * 1000);
                            uint price = Convert.ToUInt32(item.UnitPrice * 100);
                            if (visaDiscount)
                            {
                                price = ((price / 2) >= 20000) ? (price - 20000) : (price / 2);
                            }

                            byte group = 1;
                            byte tax = 1;
                            string unit = item.QuantityUnit;
                            string name = item.Name;
                            log.Write($"FD  : Unit: {unit}, Item: {name}");
                            if (deviceState.FiscalDeviceReady)
                            {
                                err = printer.NProd(code, quantity, price, group, tax, unit, name);
                                if (err != 0)
                                    receiptDone &= ErrorAnalizer(err);
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
                        Enumerations.PaymentTypeList.TryGetValue(paymentType, out paymentTypeStr);
                        uint sum = Convert.ToUInt32(payment.Amount * 100);
                        if (visaDiscount)
                        {
                            //TODO Change name to sum after testing Visa
                            uint newsum = ((sum / 2) >= 20000) ? (sum - 20000) : (sum / 2);
                            log.Write($"FD  : Visa amount: {newsum}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        if (deviceState.FiscalDeviceReady)
                        {
                            err = printer.Oplata(paymentType, sum);
                            if (err != 0)
                            {
                                receiptDone &= ErrorAnalizer(err);
                            }
                        }
                        log.Write($"FD  : Paid amount: {sum}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        #endregion

                        #region CloseReceipt
                        if (deviceState.FiscalDeviceReady)
                        {
                            err = printer.EndChk();
                            if (err != 0)
                            {
                                receiptDone &= ErrorAnalizer(err);
                            }
                            log.Write($"FD  : Close receipt, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        if (!deviceState.FiscalDeviceReady)
                        {
                            receiptDone = false;
                            err = printer.VoidChk();
                            if (err != 0)
                            {
                                ErrorAnalizer(err);
                            }
                        }
                        if (deviceState.FiscalDeviceReady)
                        {
                            Transaction tr = new Transaction(paymentType, sum);
                            tr.UpdateData(tr);
                            log.Write($"TR  : Payment serialization: {paymentTypeStr} {sum}");
                        }
                        printer.GetStatusEx();
                        StatusAnalizer();
                        inTransaction = false;
                        log.Write($"FD  : Transaction result: {deviceState.FiscalDeviceReady & receiptDone}");
                        inTransaction = false;
                        #endregion
                    }
                    else
                    {
                        receiptDone = false;
                        err = printer.VoidChk();
                        if (err != 0)
                        {
                            ErrorAnalizer(err);
                        }
                        log.Write($"FD  : Credit Entry detected. Void receipt.");
                        return new TransactionResult(false, "Credit Entry detected");
                    }
                }
                catch (Exception e)
                {
                    uint err = printer.VoidChk();
                    receiptDone = false;
                    if (err != 0)
                    {
                        ErrorAnalizer(err);
                    }
                    printer.GetStatusEx();
                    StatusAnalizer();
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                    log.Write($"FD  : Exception for Close Transaction: {e.Message}");
                    inTransaction = false;
                }
                transaction = null;
                inTransaction = false;
                return new TransactionResult(deviceState.FiscalDeviceReady & receiptDone, $"форма оплати: {payment.PaymentType}");
            }
            else
            {
                Logger log = new Logger(MachineID);
                log.Write($"FD  : Payment time out");
                return new TransactionResult(false, "Transaction canceled by Time out");
            }
        }

        public void SetDisplayLanguage(CultureInfo cultureInfo)
        {
            new Result(true);
        }

        public void StartServiceDialog(IntPtr windowHandle, ServiceLevel serviceLevel)
        {
            try
            {
                if (this.sf == null)
                {
                    this.sf = new ServiceForm_APM(printer, MachineID, this);
                    this.sf.StatusChangedEvent += StatusChangedEvent;
                    this.sf.ShowDialog();
                    this.sf = null;
                }
            }
            catch (Exception)
            {
            }
        }

        public Result VoidTransaction()
        {
            Logger log = new Logger(MachineID);
            try
            {
                if (transaction != null)
                {
                    uint err = printer.VoidChk();
                    if (err != 0)
                        ErrorAnalizer(err);
                }
                log.Write($"FD  : Void Transaction, result: {deviceState.FiscalDeviceReady}");
                printer.GetStatus();
                StatusAnalizer();
                inTransaction = false;
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

        #region ICash Members
        public Result CashIn(Cash cash)
        {
            Logger log = new Logger(MachineID);
            bool receiptDone = true;
            inTransaction = true;
            printer.RegUser(1, 1);
            StatusAnalizer();
            UInt32 amount = 0;
            if (cash.Amount >= 0)
                amount = Convert.ToUInt32(cash.Amount) * 100;
            else
                amount = Convert.ToUInt32(-cash.Amount) * 100;
            try
            {
                uint err = printer.BegChk();
                if (err != 0)
                    receiptDone = ErrorAnalizer(err);
                printer.TextChk($"Платіжна станція: {MachineID}") ;
                printer.TextChk($"Внесення з: {cash.Source}");
                if (deviceState.FiscalDeviceReady)
                {
                    err = printer.InOut(1, 0, amount);
                    if (err != 0)
                        receiptDone &= ErrorAnalizer(err);
                }
                if (deviceState.FiscalDeviceReady)
                {
                    err = printer.EndChk();
                    if (err != 0)
                        receiptDone &= ErrorAnalizer(err);
                }
                log.Write($"FD  : CashIn amount: {cash.Amount} from source: {cash.Source.ToString()}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                StatusAnalizer();
                if (deviceState.FiscalDeviceReady)
                {
                    Transaction tr = new Transaction(3, amount);
                    tr.UpdateData(tr);
                    log.Write($"TR  : CashIn Serialization: {amount}");
                }
                if (!deviceState.FiscalDeviceReady)
                {
                    receiptDone = false;
                    err = printer.VoidChk();
                    if (err != 0)
                        ErrorAnalizer(err);
                    StatusAnalizer();
                }
            }
            catch (Exception e)
            {
                receiptDone = false;
                uint err = printer.VoidChk();
                if (err != 0)
                    ErrorAnalizer(err);
                log.Write($"FD  : CashIn exception: {e.Message}");
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                StatusAnalizer();
                inTransaction = false;
            }
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady & receiptDone);
        }

        public Result CashOut(Cash cash)
        {
            Logger log = new Logger(MachineID);
            bool receiptDone = true;
            inTransaction = true;
            printer.RegUser(1, 1);
            UInt32 amount = 0;
            if (cash.Amount >= 0)
                amount = Convert.ToUInt32(cash.Amount) * 100;
            else
                amount = Convert.ToUInt32(-cash.Amount) * 100;
            try
            {
                uint err = printer.BegChk();
                if (err != 0)
                    receiptDone = ErrorAnalizer(err);
                printer.TextChk($"Платіжна станція: {MachineID}");
                printer.TextChk($"Вилучення з: {cash.Source}");
                if (deviceState.FiscalDeviceReady)
                {
                    err = printer.InOut(1, 1, amount);
                    if (err != 0)
                        receiptDone &= ErrorAnalizer(err);
                }
                if (deviceState.FiscalDeviceReady)
                {
                    err = printer.EndChk();
                    if (err != 0)
                        receiptDone &= ErrorAnalizer(err);
                }
                log.Write($"FD  : CashOut amount: {cash.Amount} from source: {cash.Source.ToString()}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                if (deviceState.FiscalDeviceReady)
                { 
                    Transaction tr = new Transaction(4, amount);
                    tr.UpdateData(tr);
                    log.Write($"TR  : CashOut Serialization {amount}");
                }
                if (!deviceState.FiscalDeviceReady)
                {
                    receiptDone = false;
                    err = printer.VoidChk();
                    if (err != 0)
                        ErrorAnalizer(err);
                }
                printer.GetStatusEx();
                StatusAnalizer();
            }
            catch (Exception e)
            {
                receiptDone = false;
                uint err = printer.VoidChk();
                if (err != 0)
                    ErrorAnalizer(err);
                log.Write($"FD  : CashOut exception: {e.Message}");
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                inTransaction = false;
            }
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady & receiptDone);
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
                    printer.Disconnect();
                }
            }
            this.disposed = true;
        }

        ~UA_Fiscal_APM()
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
            try
            {
                if (deviceState.FiscalDeviceReady)
                {
                    uint err = printer.RegUser(1, 1);
                    if (err != 0)
                        ErrorAnalizer(err);
                }
                if (deviceState.FiscalDeviceReady)
                {
                    uint err = printer.ShiftBegin();
                    if (err != 0)
                        ErrorAnalizer(err);
                }
                StatusAnalizer();
                if (deviceState.FiscalDeviceReady)
                    shiftStarted = true;
                log.Write($"FD  : OpenShift, result: {deviceState.FiscalDeviceReady}");
            }
            catch (Exception e)
            {
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
            if(error != 0)
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
            log.Write($"SA  : Status: Blocked-{LeoCasLib.blockedStatus}, Shift Started-{LeoCasLib.shiftStartedStatus}, Out of paper-{LeoCasLib.outOfPaperStatus}, Low paper-{LeoCasLib.lowPaperStatus}");
            if (LeoCasLib.blockedStatus)
            {
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "РРО заблоковано");
            }
            if (!LeoCasLib.blockedStatus)
            {
                this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "РРО заблоковано");
            }
            if (!LeoCasLib.shiftStartedStatus)
            {
                if (!LeoCasLib.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.OtherWarning, "Зміна не відкрита");
                }
            }
            if (LeoCasLib.shiftStartedStatus)
            {
                if (!LeoCasLib.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "Ok");
                }
            }
            if (LeoCasLib.outOfPaperStatus)
            {
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.OutOfPaper, "РРО: Закінчилась чекова стрічка");
            }
            if (!LeoCasLib.outOfPaperStatus)
            {
                if (!LeoCasLib.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "Ok");
                }
            }
            if (LeoCasLib.lowPaperStatus)
            {
                if (LeoCasLib.blockedStatus || LeoCasLib.outOfPaperStatus)
                { }
                else
                    this.OnErrorOccurred(new ErrorOccurredEventArgs("РРО: Закінчується чекова стрічка", SkiDataErrorCode.PaperLowWarning, deviceState.FiscalDeviceReady));
            }
            return 0;
        }

        private void ReCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //isReady = ErrorAnalizer(printer.GetStatusEx());
            //StatusAnalizer();
        }

        private void SetTimer()
        {
            timeCheckTimer.Elapsed += OnTimedEvent;
            timeCheckTimer.AutoReset = true;
            timeCheckTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            bool receiptDone = true;
            Logger log = new Logger(MachineID);
            TimeSpan startZRep = TimeSpan.Parse("23:57");
            TimeSpan endZRep = TimeSpan.Parse("23:58");
            TimeSpan startShift = TimeSpan.Parse("00:02");
            TimeSpan endShift = TimeSpan.Parse("00:04");
            TimeSpan now = DateTime.Now.TimeOfDay;
            # region Automated End of Day
            if (!inTransaction)
            {
                if (startZRep <= endZRep)
                {
                    if ((now >= startZRep && now <= endZRep) && (!zrepDone))
                    {
                        try
                        {
                            printer.PrgTime();
                            log.Write($"FDAU: Close shift");
                            uint err = printer.PrintRep(16);
                            if (err != 0)
                                ErrorAnalizer(err);
                            if (deviceState.FiscalDeviceReady)
                            {
                                zrepDone = true;
                                shiftStarted = false;
                            }
                            printer.GetStatusEx();
                            printer.GetStatus();
                            StatusAnalizer();
                            log.Write($"FDAU: Close shift result: {deviceState.FiscalDeviceReady}");
                        }
                        catch (Exception exc)
                        {
                            log.Write($"FDAU: Close shift exception: {exc.Message}");
                            StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, exc.Message);
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
                        try
                        {
                            log.Write($"FDAU: New Shift");
                            StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "New shift");
                            uint err = printer.RegUser(1, 1);
                            if (err != 0)
                                ErrorAnalizer(err);
                            if (deviceState.FiscalDeviceReady)
                            {
                                err = printer.ShiftBegin();
                                if (err != 0)
                                    ErrorAnalizer(err);
                                if(deviceState.FiscalDeviceReady)
                                    this.shiftStarted = true;
                            }
                            printer.PrgTime();
                            if (deviceState.FiscalDeviceReady)
                            {
                                Transaction tr = new Transaction();
                                err = printer.BegChk();
                                if (err != 0)
                                    receiptDone = ErrorAnalizer(err);
                                if (deviceState.FiscalDeviceReady)
                                {
                                    err = printer.InOut(1, 0, tr.Get());
                                    if (err != 0)
                                        receiptDone &= ErrorAnalizer(err);
                                }
                                if (deviceState.FiscalDeviceReady)
                                {
                                    err = printer.EndChk();
                                    if (err != 0)
                                        receiptDone &= ErrorAnalizer(err);
                                }
                                log.Write($"FDAU: Transfer amount result: {deviceState.FiscalDeviceReady}");
                                if (!deviceState.FiscalDeviceReady)
                                {
                                    receiptDone = false;
                                    err = printer.VoidChk();
                                    if (err != 0)
                                        ErrorAnalizer(err);
                                }
                            }
                            printer.GetStatusEx();
                            printer.GetStatus();
                            StatusAnalizer();
                            zrepDone = false;
                            log.Write($"FDAU: New shift result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        catch (Exception exc)
                        {
                            log.Write($"FDAU: New shift exception: {exc.Message}");
                            printer.VoidChk();
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
                if (!deviceState.FiscalDeviceReady)
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, true));
            }
            else
            {
                if (deviceState.FiscalDeviceReady)
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, false));
                else
                {
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, false));
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, false));
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
