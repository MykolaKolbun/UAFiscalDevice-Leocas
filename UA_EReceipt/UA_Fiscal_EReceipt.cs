using SkiData.FiscalDevices; //4.0
using System;
using System.Globalization;

namespace UA_EReceipt
{
    public class UA_Fiscal_EReceipt : IFiscalDevice
    {
        public string Name => "EReceipt.FiscalDevice";

        public string ShortName => throw new NotImplementedException();

        public FiscalDeviceCapabilities Capabilities => throw new NotImplementedException();

        public StateInfo DeviceState => throw new NotImplementedException();

        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        public event EventHandler<ErrorClearedEventArgs> ErrorCleared;
        public event EventHandler<IrregularityDetectedEventArgs> IrregularityDetected;
        public event EventHandler<EventArgs> DeviceStateChanged;
        public event EventHandler<JournalizeEventArgs> Journalize;
        public event EventHandler<TraceEventArgs> Trace;

        public Result AddDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Result AddItem(Item item)
        {
            throw new NotImplementedException();
        }

        public Result AddPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Result CloseTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Result EndOfDay()
        {
            throw new NotImplementedException();
        }

        public Result Install(FiscalDeviceConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void Notify(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Result OpenTransaction(TransactionData transactionData)
        {
            throw new NotImplementedException();
        }

        public void SetDisplayLanguage(CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public void StartServiceDialog(IntPtr windowHandle, ServiceLevel serviceLevel)
        {
            throw new NotImplementedException();
        }

        public Result VoidTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
