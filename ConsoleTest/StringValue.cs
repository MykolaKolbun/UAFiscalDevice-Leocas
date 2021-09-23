
namespace ConsoleTest
{
    public static class StringValue
    {
        #region Visa
        public const string VISAFilePath = @"\\10.1.13.11\Exchange\VISA\Premium CIF parking_.xlsx";
        public const string XMLFilePathServer = @"C:\Log\Cards.xml";
        public const string XMLFilePathLocal = @"C:\Log\Cards.xml";
        public const string XMLHashFilePathServer = @"C:\Log\VisaXMLHash.hash";
        public const string XMLHashFilePathLocal = @"C:\Log\VisaXMLHash.hash";
        #endregion

        #region SQL
        //public const string SQLServerConnectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = PARK_DB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public const string SQLServerConnectionString = @"Data Source = 10.1.13.11; Initial Catalog = PARK_DB; User ID = qwert; Password=P@ran01d";
        //public const string SQLServerConnectionString = @"Data Source = " + "10.10.50.1" + "; Initial Catalog = PARK_DB; User ID = qwert; Password=P@ran01d";
        #endregion

        #region Settings file
        public const string LeoCasFiscalSettings = @"C:\FiscalFolder\LeoCasFiscalSettings.xml";

        #endregion
    }
}
