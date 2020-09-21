using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace UA_Fiscal_Leocas
{
    /// <summary>
    /// Класс для работы с SQL сервером.
    /// </summary>
    class SQLConnect : IDisposable
    {

        string connectionString;
        SqlConnection conn;
        SqlCommand cmd;
        private bool disposed;

        public SQLConnect()
        {
            string compName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            try
            {
                string srvName = compName.Split('-')[0] + "-01";
                this.connectionString = string.Format(StringValue.SQLServerConnectionString, srvName);
            }
            catch (Exception e)
            {
                Logger log = new Logger(compName.Split('-')[1]);
                log.Write($"FDDB: SQL Exception: {e.Message}");
            }

        }

        public string GetTransactionFromDBbyDevice(string deviceID, string transactionNr)
        {
            string receipt = "";
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand("GetTransactionFromDevice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeviceID", deviceID);
            cmd.Parameters.AddWithValue("@TransactionNr", transactionNr);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    receipt = Convert.ToString(reader.GetValue(0));
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                Logger log = new Logger(deviceID);
                log.Write($"FDDB: SQL Exception: {e.Message}");
            }
            finally
            {
                conn.Close();
            }
            return receipt;
        }

        /// <summary>
        /// Получить последний чек из банка
        /// </summary>
        /// <returns>тело чека</returns>
        public string GetLastTransactionFromDBbyDevice(string deviceID)
        {
            string receipt = "";
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand("GetLastTransactionFromDevice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeviceID", deviceID);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    receipt = Convert.ToString(reader.GetValue(0));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Logger log = new Logger(deviceID);
                log.Write($"FDDB: SQL Exception: {e.Message}");
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return receipt;
        }

        /// <summary>
        /// Получить транзакцию по ID
        /// </summary>
        /// <param name="id">ID транзакции</param>
        /// <returns></returns>
        public string GetTranasactionFromDBByID(string id)
        {
            string receipt = "";
            if (id != "")
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                cmd = new SqlCommand("SearchByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
                DataTable dt = new DataTable();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        receipt = Convert.ToString(reader.GetValue(0));
                    }
                    reader.Close();
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
            return receipt;
        }

        /// <summary>
        /// Получить список транзакций с устройства
        /// </summary>
        /// <param name="deviceID">ID устройства</param>
        /// <returns></returns>
        public LinkedList<TableContent> GetListOfTransactionFromDBbyDevice(string deviceID)
        {
            LinkedList<TableContent> tcll = new LinkedList<TableContent>();
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand("SearchByDevice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeviceID", Convert.ToInt32(deviceID));
            DataTable dt = new DataTable();
            try
            {
                dt.Load(cmd.ExecuteReader());
                LinkedList<TableContent> od = new LinkedList<TableContent>();
                object[] o = new object[5];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        o[j] = dt.Rows[i].ItemArray[j];
                    }
                    TableContent tc = new TableContent((int)o[0], (string)o[1], (string)o[2], (DateTime)o[4], (string)o[3]);
                    tcll.AddLast(tc);
                }
            }
            catch (Exception e)
            {
                Logger log = new Logger(deviceID);
                log.Write($"FDDB: SQL Exception: {e.Message}");
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return tcll;
        }

        /// <summary>
        /// Получить список транзакций из базы по номеру устройства
        /// </summary>
        /// <param name="deviceID">Номер устройства</param>
        /// <param name="transactionType">тип транзакции (чек или закрытие дня)</param>
        /// <param name="isPrinted">статус распечатки</param>
        /// <returns></returns>
        public LinkedList<TableContent> GetListOfTransactionFromDBbyDevice(string deviceID, string transactionType, int isPrinted)
        {
            LinkedList<TableContent> tcll = new LinkedList<TableContent>();
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand("SearchByDevice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeviceID", deviceID);
            cmd.Parameters.AddWithValue("@TransactionType", transactionType);
            cmd.Parameters.AddWithValue("@IsPrinted", isPrinted);
            DataTable dt = new DataTable();
            try
            {
                dt.Load(cmd.ExecuteReader());
                LinkedList<TableContent> od = new LinkedList<TableContent>();
                object[] o = new object[5];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        o[j] = dt.Rows[i].ItemArray[j];
                    }
                    TableContent tc = new TableContent((int)o[0], (string)o[1], (string)o[2], (DateTime)o[4], (string)o[3]);
                    tcll.AddLast(tc);
                }
            }
            catch (Exception e)
            {
                Logger log = new Logger(deviceID);
                log.Write($"FDDB: SQL Exception: {e.Message}");
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return tcll;
        }

        /// <summary>
        /// Поиск транзакций по номеру билета
        /// </summary>
        /// <param name="ticketNR">номер билета</param>
        /// <returns>список транзакций</returns>
        public LinkedList<TableContent> SearchTransactionsFromDBbyTicketNR(string ticketNR)
        {
            LinkedList<TableContent> tcll = new LinkedList<TableContent>();
            if (ticketNR != "")
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                cmd = new SqlCommand("SearchByTicket", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TicketNR", ticketNR);
                DataTable dt = new DataTable();
                try
                {
                    dt.Load(cmd.ExecuteReader());
                    LinkedList<TableContent> od = new LinkedList<TableContent>();
                    object[] o = new object[5];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            o[j] = dt.Rows[i].ItemArray[j];
                        }
                        TableContent tc = new TableContent((int)o[0], (string)o[1], (string)o[2], (DateTime)o[4], (string)o[3]);
                        tcll.AddLast(tc);
                    }
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
            return tcll;
        }

        /// <summary>
        /// Проверка скидки VISA
        /// </summary>
        /// <param name="transaction">номер ртанзакции</param>
        /// <param name="paymentMachineId">номер платежной станции</param>
        /// <returns></returns>
        internal bool GetDiscountProperty(string transaction, string paymentMachineId)
        {
            bool hasDiscount = false;
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand("GetDiscountProperty", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TransactionNr", transaction);
            cmd.Parameters.AddWithValue("@DeviceID", paymentMachineId);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    int discountProperty = Convert.ToInt32(reader.GetValue(0));
                    if(discountProperty!=0)
                    {
                        hasDiscount = true;
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
            }
            return hasDiscount;
        }

        /// <summary>
        /// Поиск ID устройств с банковскими транзакциями
        /// </summary>
        /// <returns>список устройств</returns>
        public LinkedList<string> GetDevicesList()
        {
            LinkedList<string> od = new LinkedList<string>();
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand("SearchDevices", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                dt.Load(cmd.ExecuteReader());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    od.AddLast(dt.Rows[i].ItemArray[0].ToString());
                }
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return od;
        }

        /// <summary>
        /// Обновить статус записи
        /// </summary>
        /// <param name="id"></param>
        public void UpdateIsPrinted(string id)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand("UpdateIsPrinted", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
            DataTable dt = new DataTable();
            try
            {
                cmd.ExecuteReader();
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

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
                    conn.Close();
                }
            }
            this.disposed = true;
        }

        ~SQLConnect()
        {
            this.Dispose(false);
        }
    }
}
