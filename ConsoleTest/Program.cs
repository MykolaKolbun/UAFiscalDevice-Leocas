using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLConnect connect = new SQLConnect();
            Console.Write($"{connect.GetTransactionFromDBbyDevice("17", "19C63E8B43")}");
        }
    }
}
