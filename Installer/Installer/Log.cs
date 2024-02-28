using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installer
{
    public static class Log
    {
        public static void Doing(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write("[  Doing ]");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(message);
        }

        public static void Success(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write("[   OK   ]");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(message);
        }
        public static void Warning(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Write("[  WARN  ]");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(message);
        }
        public static void Checking(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Write("[  CHCK  ]");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(message);
        }
        public static void Error(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write("[ ERROR! ]");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(message);
        }
        public static void Fatal(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Black;
            System.Console.BackgroundColor = ConsoleColor.Red;
            System.Console.Write("[ FATAL! ]");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(message);
            System.Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
