using System;
using System.Collections.Generic;
using System.Globalization;
using SkiData.FiscalDevices;

namespace UA_Fiscal_Leocas
{
    class EmptyTracing : IFiscalDevice, ICash
    {
        string machineId = "00";
        List<Item> items;

        public string Name => throw new NotImplementedException();

        public string ShortName => throw new NotImplementedException();

        public FiscalDeviceCapabilities Capabilities => throw new NotImplementedException();

        public StateInfo DeviceState => throw new NotImplementedException();

        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        public event EventHandler<ErrorClearedEventArgs> ErrorCleared;
        public event EventHandler<IrregularityDetectedEventArgs> IrregularityDetected;
        public event EventHandler<EventArgs> DeviceStateChanged;
        public event EventHandler<JournalizeEventArgs> Journalize;
        public event EventHandler<TraceEventArgs> Trace;

        public Result Install(FiscalDeviceConfiguration configuration)
        {
            machineId = configuration.DeviceId;
            Logger log = new Logger(machineId);
            log.Write("Install");
            return new Result(true);
        }

        public Result OpenTransaction(TransactionData transactionData)
        {
            Logger log = new Logger(machineId);
            items = new List<Item>();
            log.Write("Open transaction");
            log.Write($"Transaction data. Reference ID: {transactionData.ReferenceId}");
            return new Result(true);
        }

        public Result AddDiscount(Discount discount)
        {
            Logger log = new Logger(machineId);
            log.Write("Add discount");
            log.Write($"Discount: {discount.Amount}; Discount description: {discount.Description}");
            return new Result(true);
        }

        public Result AddItem(Item item)
        {
            Logger log = new Logger(machineId);
            log.Write("Add Item");
            items.Add(item);
            log.Write($"Item ID: {item.Id}; Item Name: {item.Name}; Item Type: {item.ItemType}; Item Price: {item.TotalPrice}.");
            return new Result(true);
        }

        public Result AddPayment(Payment payment)
        {
            Logger log = new Logger(machineId);
            log.Write("Add Payment");
            log.Write($"Payment Amount: {payment.Amount}; Payment Type: {payment.PaymentType}; Payment Description: {payment.Description}.");
            return new Result(true);
        }

        public Result CloseTransaction()
        {
            Logger log = new Logger(machineId);
            log.Write("Close transaction");
            return new Result(true);
        }

        public Result VoidTransaction()
        {
            Logger log = new Logger(machineId);
            log.Write("Void transaction");
            return new Result(true);
        }

        public void Dispose()
        {
            Logger log = new Logger(machineId);
            log.Write("Dispose");
        }

        public Result EndOfDay()
        {
            Logger log = new Logger(machineId);
            log.Write("End of Day");
            return new Result(true);
        }

        public void Notify(int notificationId)
        {
            Logger log = new Logger(machineId);
            log.Write($"Notify. Notification ID: {notificationId.ToString()}");
        }

        public void SetDisplayLanguage(CultureInfo cultureInfo)
        {
            Logger log = new Logger(machineId);
            log.Write("Set language");
        }

        public void StartServiceDialog(IntPtr windowHandle, ServiceLevel serviceLevel)
        {
            Logger log = new Logger(machineId);
            log.Write($"Start service dialog. Handler {windowHandle.ToString()}");
        }

        public Result CashIn(Cash cash)
        {
            Logger log = new Logger(machineId);
            log.Write("Cash In");
            log.Write($"Cash In amount: {cash.Amount} from {cash.Source}");
            return new Result(true);
        }

        public Result CashOut(Cash cash)
        {
            Logger log = new Logger(machineId);
            log.Write("Cash Out");
            log.Write($"Cash Out amount: {cash.Amount} from {cash.Source}");
            return new Result(true);
        }
    }
}
