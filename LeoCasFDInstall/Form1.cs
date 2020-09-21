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
            string file2Name = @"DriverEKKA2.dll";
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
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1033, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1058, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1031, 'LeoCasCD', N'Каса (Leocas 200)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2057, 'LeoCasCD', N'Каса (Leocas 200)')";

                command.CommandText = "INSERT INTO [dbo].[SDSYSTXTDEF] ([TxtCode], [TxtLaenge], [AnzZeilen], [IstUmschaltbar]) VALUES ('LeoCasAPM' ,25 ,1 ,0)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1033, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1058, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1031, 'LeoCasAPM', N'Паркомат (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2057, 'LeoCasAPM', N'Паркомат (Leocas 401)')";

                command.CommandText = "INSERT INTO [dbo].[SDSYSTXTDEF] ([TxtCode], [TxtLaenge], [AnzZeilen], [IstUmschaltbar]) VALUES ('LeoCasEX' ,25 ,1 ,0)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1033, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1058, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1031, 'LeoCasEX', N'Стійка (Leocas 401)')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2057, 'LeoCasEX', N'Стійка (Leocas 401)')";
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
