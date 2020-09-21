using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServiceMenu
{
    class Device
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        public Device (int id, string name, string connectionString)
        {
            this.ID = id;
            this.Name = name;
            this.ConnectionString = connectionString;
        }

        public string GetConnectionString(int id, List<Device> devices)
        {
            string connStr = "";
            foreach (Device dev in devices)
            {
                if (id == dev.ID)
                    connStr = dev.ConnectionString;
            }
            return connStr;
        }
    }
}
