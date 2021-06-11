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
            Console.Write($"{connect.GetDiscountProperty("488E10F001", "112")}");
        }
    }
}
