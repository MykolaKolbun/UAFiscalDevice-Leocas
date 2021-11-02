using System;
using System.Globalization;
using SkiData.FiscalDevices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Timers;

namespace UA_Fiscal_Leocas
{
    public class UA_Fiscal_Leocas_200: IFiscalDevice, ICash, IShift
    {
        #region Fields
        private FiscalDeviceCapabilities fiscalDeviceCapabilities;
        public static System.Timers.Timer reCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer timeCheckTimer = new System.Timers.Timer(300000);
        public static System.Timers.Timer paymentTimeoutTimer = new System.Timers.Timer(285000);
        private bool disposed;
        //public bool hasCCTerminal = false;
        //public bool isReady;
        public bool inTransaction = false;
        public bool transactionTimeOutExceed = false;
        public StateInfo deviceState;
        public static bool shiftNotClosed = false;
        public static bool shiftOpened = false;
        private ServiceForm_Leocas200 sf = null;
        string transaction = "";
        private byte paymentType;
        public string paymentMachineName = "";
        public string paymentMachineId = "";
        public DeviceType paymentMachineType;
        public string connectionString = "";
        public static string MachineID;
        List<Item> items;  //  Temp container for Item. Try to print two receipts on one list.
        Payment payment; // Temp container for Payment. Try to print two receipts on one list.
        LeoCasLib printer = new LeoCasLib();
        public FiscalDeviceConfiguration fiscalDeviceConfiguration;
        //TransactionData currentTransaction;

        #endregion

        #region Construction

        public UA_Fiscal_Leocas_200()
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
                    if (this.sf != null)
                    {
                        sf.Close();
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

                    log.Write($"FD  : Instal Exception: {e.Message}");
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                    inTransaction = false;
                }
            }
            log.Write($"FD  : Install - {deviceState.FiscalDeviceReady}");
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady);
        }

        public string Name => "LeoCAS_200.FiscalDevice";

        public string ShortName => "LeoCAS200";

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
            bool receiptClosed = true;
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
                        receiptClosed &= ErrorAnalizer(err);
                    if (payment.PaymentType == PaymentType.CreditCard)
                    {
                        log.Write($"FD  : Print bank response");
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
                                    err = printer.TextChkEx(lines[line]);
                                }
                            }
                            err = printer.TextChk("------------------------");
                        }
                        catch (Exception e)
                        {
                            err = printer.TextChk("Відповідь не можна");
                            err = printer.TextChk("роздрукувати.");
                            err = printer.TextChkEx("Зверніться до адміністратора");
                            err = printer.TextChk("за чеком.");
                            err = printer.TextChk("------------------------");
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
                                err = printer.TextChkEx($"ID: {parkingItem.TicketId}");
                                if (err != 0)
                                    ErrorAnalizer(err);
                                err = printer.TextChkEx($"Заїзд в:{myEntryTime.ToString(@"dd.MM.yy HH:mm:ss")}");
                                if (err != 0)
                                    ErrorAnalizer(err);
                                err = printer.TextChkEx($"Виїхати:{myExitTime.ToString(@"dd.MM.yy HH:mm:ss")}");
                                if (err != 0)
                                    ErrorAnalizer(err);
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
                            if (deviceState.FiscalDeviceReady & receiptClosed)
                            {
                                err = printer.NProd(code, quantity, price, group, tax, unit, name);
                                if (err != 0)
                                    receiptClosed = receiptClosed & ErrorAnalizer(err);
                            }
                            log.Write($"FD  : Total price: {price}, result: {deviceState.FiscalDeviceReady & receiptClosed}");
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
                            sum = ((sum / 2) >= 20000) ? (sum - 20000) : (sum / 2);
                        }
                        if (deviceState.FiscalDeviceReady & receiptClosed)
                        {
                            err = printer.Oplata(paymentType, sum);
                            if (err != 0)
                            {
                                receiptClosed &= ErrorAnalizer(err);
                            }
                        }
                        log.Write($"FD  : Paid amount: {sum}, result: {deviceState.FiscalDeviceReady & receiptClosed}");
                        #endregion

                        #region CloseReceipt
                        
                        if (deviceState.FiscalDeviceReady & receiptClosed)
                        {
                            err = printer.EndChk();
                            if (err != 0)
                            {
                                receiptClosed &= ErrorAnalizer(err);
                            }
                            log.Write($"FD  : Close receipt, result: {deviceState.FiscalDeviceReady & receiptClosed}");
                        }
                        if (!deviceState.FiscalDeviceReady)
                        {
                            receiptClosed = false;
                            err = printer.VoidChk();
                            if (err != 0)
                            {
                                ErrorAnalizer(err);
                            }
                        }
                        if (deviceState.FiscalDeviceReady & receiptClosed)
                        {
                            Transaction tr = new Transaction(paymentType, sum);
                            tr.UpdateData(tr);
                            log.Write($"TR  : Payment serialization: {paymentTypeStr} {sum}");
                        }
                        printer.GetStatusEx();
                        StatusAnalizer();
                        inTransaction = false;
                        log.Write($"FD  : Transaction result: {deviceState.FiscalDeviceReady & receiptClosed}");
                        inTransaction = false;
                        #endregion
                    }
                    else
                    {
                        receiptClosed = false;
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
                    receiptClosed = false;
                    uint err = printer.VoidChk();
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
                return new TransactionResult(deviceState.FiscalDeviceReady & receiptClosed, $"форма оплати: {payment.PaymentType}");
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
                    this.sf = new ServiceForm_Leocas200(printer, MachineID, this);
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
            printer.GetStatusEx();
            StatusAnalizer();
            uint err = printer.RegUser(1, 1);
            UInt32 amount = 0;
            if (cash.Amount >= 0)
                amount = Convert.ToUInt32(cash.Amount) * 100;
            else
                amount = Convert.ToUInt32(-cash.Amount) * 100;
            try
            {
                err = printer.BegChk();
                if (err != 0)
                    receiptDone = ErrorAnalizer(err);
                err = printer.TextChk($"Платіжна станція: {MachineID}");
                err = printer.TextChk($"Внесення з: {cash.Source}");
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
                printer.GetStatusEx();
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
                    printer.GetStatusEx();
                    StatusAnalizer();
                }
            }
            catch (Exception e)
            {
                err = printer.VoidChk();
                receiptDone = false;
                if (err != 0)
                    ErrorAnalizer(err);
                log.Write($"FD  : CashIn exception: {e.Message}");
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                printer.GetStatusEx();
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
            uint err = printer.RegUser(1, 1);
            UInt32 amount = 0;
            if (cash.Amount >= 0)
                amount = Convert.ToUInt32(cash.Amount) * 100;
            else
                amount = Convert.ToUInt32(-cash.Amount) * 100;
            try
            {
                err = printer.BegChk();
                if (err != 0)
                    receiptDone = ErrorAnalizer(err);
                err = printer.TextChk($"Платіжна станція: {MachineID}");
                err = printer.TextChk($"Вилучення з: {cash.Source}");
                if (deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.InOut(1, 1, amount);
                    if (err != 0)
                        receiptDone = receiptDone & ErrorAnalizer(err);
                }
                if (deviceState.FiscalDeviceReady & receiptDone)
                {
                    err = printer.EndChk();
                    if (err != 0)
                        receiptDone = receiptDone & ErrorAnalizer(err);
                }
                log.Write($"FD  : CashOut amount: {cash.Amount} from source: {cash.Source.ToString()}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                if (deviceState.FiscalDeviceReady & receiptDone)
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
                err = printer.VoidChk();
                receiptDone = false;
                if (err != 0)
                    ErrorAnalizer(err);
                log.Write($"FD  : CashOut exception: {e.Message}");
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                inTransaction = false;
            }
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady & receiptDone );
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

        ~UA_Fiscal_Leocas_200()
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

        private void OnTrace(TraceEventArgs args)
        {
            if (Trace != null)
                Trace(this, args);
        }

        private void OnDeviceStateChanged(EventArgs args)
        {
            if (DeviceStateChanged != null)
                DeviceStateChanged(this, args);
        }

        private void OnErrorOccurred(ErrorOccurredEventArgs args)
        {
            OnTrace(new TraceEventArgs($"Error {args.ErrorMessage} occurred", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, args.ErrorMessage);
            if (ErrorOccurred != null)
                ErrorOccurred(this, args);
        }

        private void OnErrorCleared(ErrorClearedEventArgs args)
        {
            OnTrace(new TraceEventArgs($"Error {deviceState.ErrorCode} cleared", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, "Ok");
            if (ErrorCleared != null)
                ErrorCleared(this, args);
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
            log.Write($"SA  : Status: Blocked-{LeoCasLib.blockedStatus}, Shift Started-{LeoCasLib.shiftStartedStatus}, Out of paper-{LeoCasLib.outOfPaperStatus}, Low paper-{LeoCasLib.lowPaperStatus}");

            if (!LeoCasLib.blockedStatus)
            {
                this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "РРО заблоковано");
            }
            if (LeoCasLib.shiftStartedStatus)
            {
                if (!LeoCasLib.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "Ok");
                }
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
            if (!LeoCasLib.shiftStartedStatus)
            {
                if (!LeoCasLib.blockedStatus)
                {
                    this.StatusChangedEvent(true, (int)SkiDataErrorCode.OtherWarning, "Зміна не відкрита");
                }
            }
            if (LeoCasLib.outOfPaperStatus)
            {
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.OutOfPaper, "РРО: Закінчилась чекова стрічка");
            }
            if (LeoCasLib.blockedStatus)
            {
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "РРО заблоковано");
            }
            return 0;
        }

        private void ReCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
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
