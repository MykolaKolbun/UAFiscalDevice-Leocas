using System;
using System.Data.SqlClient;
using System.Drawing;

using System.Windows.Forms;

namespace LeoCasFDInstall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            labelDone.Visible = false;
            cbxSelectType.Items.Add("Server");
            cbxSelectType.Items.Add("Cash Desk");
            cbxSelectType.Items.Add("APM");
            cbxSelectType.Items.Add("Column");
            cbxSelectType.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            labelDone.Visible = false;
            try
            {
                if(cbxSelectType.SelectedIndex == 0)
                {
                    if(AddTextToDB())
                    {
                        checkBoxAddText.Checked = true;
                    }
                    else
                    {
                        checkBoxAddText.BackColor = Color.Red;
                    }

                    if(AddFiscalToDB())
                    {
                        checkBoxAddDevices.Checked = true;
                    }
                    else
                    {
                        checkBoxAddDevices.BackColor = Color.Red;
                    }

                    if( checkBoxAddText.Checked & checkBoxAddDevices.Checked)
                    {
                        labelDone.Visible = true;
                    }
                }

                if (cbxSelectType.SelectedIndex != 0)
                {
                    if (CopyDll())
                    {
                        checkBoxCopyFiles.Checked = true;
                    }
                    else
                    {
                        checkBoxCopyFiles.BackColor = Color.Red;
                    }
                    if (checkBoxCopyFiles.Checked)
                    {
                        labelDone.Visible = true;
                    }
                }

            }
            catch(UnauthorizedAccessException)
            { MessageBox.Show("Close and start the application as administrator"); }
            catch(SqlException ex)
            { MessageBox.Show($"SQLError: {ex.Message}"); }
        }

        private bool AddFiscalToDB()
        {
            string connectionString = $"Data Source = {textBoxServerIP.Text}; User ID = {textBoxSQLUser.Text}; Password = {textBoxSQLPass.Text}; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "INSERT INTO SDSYSFISKALDRUCKERTYP (Fiskaldruckertyp, BezTxtCode, Landescode, Typ, DLLName, ClassName) VALUES (110, 'LeoCasCD', 380, 2, 'UA_Fiscal_Leocas.dll', 'UA_Fiscal_Leocas.UA_Fiscal_Leocas_200')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO SDSYSFISKALDRUCKERTYP (Fiskaldruckertyp, BezTxtCode, Landescode, Typ, DLLName, ClassName) VALUES (111, 'LeoCasAPM', 380, 2, 'UA_Fiscal_Leocas.dll', 'UA_Fiscal_Leocas.UA_Fiscal_APM')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO SDSYSFISKALDRUCKERTYP (Fiskaldruckertyp, BezTxtCode, Landescode, Typ, DLLName, ClassName) VALUES (112, 'LeoCasEX', 380, 2, 'UA_Fiscal_Leocas.dll', 'UA_Fiscal_Leocas.UA_Fiscal_Column')";
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        private bool CopyDll()
        {
            string file1Name = @"UA_Fiscal_Leocas.dll";
            string file2Name = @"DriverEKKA3.dll";
            string skidataFolder = @"Skidata\Parking\OEM\FDI";
            string logFolder = @"C:\Log";
            string fiscalFolder = @"C:\FiscalFolder";
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string sourcePath = @"sources";
            string sourceFile1 = System.IO.Path.Combine(sourcePath, file1Name);
            string sourceFile2 = System.IO.Path.Combine(sourcePath, file2Name);
            string destFolder = System.IO.Path.Combine(targetPath, skidataFolder);
            string destFile1 = System.IO.Path.Combine(destFolder, file1Name); 
            string destFile2 = System.IO.Path.Combine(destFolder, file2Name);
            System.IO.Directory.CreateDirectory(logFolder);
            System.IO.Directory.CreateDirectory(fiscalFolder);
            System.IO.Directory.CreateDirectory(targetPath);
            System.IO.File.Copy(sourceFile1, destFile1, true);
            System.IO.File.Copy(sourceFile1, destFile1, true);
            return true;
        }

        private bool AddTextToDB()
        {
            string connectionString = $"Data Source = {textBoxServerIP.Text}; Initial Catalog = PARK_DB; User ID = {textBoxSQLUser.Text}; Password = {textBoxSQLPass.Text}; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXTDEF] ([TxtCode], [TxtLaenge], [AnzZeilen], [IstUmschaltbar]) VALUES ('LeoCasCD' ,25 ,1 ,0)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1025, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1026, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1027, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1028, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1029, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1030, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1031, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1032, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1033, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1034, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1035, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1036, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1037, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1038, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1039, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1040, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1041, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1043, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1044, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1045, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1046, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1048, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1049, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1050, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1051, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1052, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1053, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1054, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1055, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1057, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1058, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1060, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1061, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1062, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1063, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1065, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1066, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1068, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1069, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1071, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1077, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1078, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1081, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1086, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1087, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1110, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1124, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1132, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1279, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2057, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2058, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (2070, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (2074, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (3084, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (4105, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (4108, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (6153, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO [dbo].[SDSYSTXTDEF] ([TxtCode], [TxtLaenge], [AnzZeilen], [IstUmschaltbar]) VALUES ('LeoCasAPM' ,25 ,1 ,0)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1025, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1026, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1027, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1028, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1029, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1030, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1031, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1032, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1033, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1034, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1035, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1036, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1037, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1038, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1039, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1040, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1041, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1043, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1044, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1045, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1046, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1048, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1049, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1050, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1051, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1052, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1053, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1054, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1055, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1057, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1058, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1060, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1061, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1062, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1063, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1065, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1066, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1068, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1069, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1071, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1077, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1078, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1081, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1086, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1087, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1110, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1124, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1132, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1279, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2057, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2058, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (2070, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (2074, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (3084, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (4105, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (4108, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (6153, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO [dbo].[SDSYSTXTDEF] ([TxtCode], [TxtLaenge], [AnzZeilen], [IstUmschaltbar]) VALUES ('LeoCasEX' ,25 ,1 ,0)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1025, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1026, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1027, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1028, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1029, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1030, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1031, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1032, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1033, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1034, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1035, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1036, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1037, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1038, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1039, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1040, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1041, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1043, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1044, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1045, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1046, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1048, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1049, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1050, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1051, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1052, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1053, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1054, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1055, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1057, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1058, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1060, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1061, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1062, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1063, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1065, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1066, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1068, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1069, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1071, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1077, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1078, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1081, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1086, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1087, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1110, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1124, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1132, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1279, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2057, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2058, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (2070, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (2074, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (3084, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (4105, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (4108, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (6153, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        private bool CheckConnection()
        {
            string connectionString = $"Data Source = {textBoxServerIP.Text}; User ID = {textBoxSQLUser.Text}; Password = {textBoxSQLPass.Text}; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (CheckConnection())
                labelTestConnection.Text = "Successfull";
            else
                labelTestConnection.Text = "No connecion";
        }
    }
}
