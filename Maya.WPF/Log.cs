using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.WPF
{
    class Log
    {
        public static void WriteToLog(object appname, string message)
        {
            Console.WriteLine($"{appname}: {message}");
        }
    }
}
