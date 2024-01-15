using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files
{
    public static class Log
    {
        public static void Success(string message)
        {
            if (Kernel.GUI_MODE)
            {
                GUIConsole.FG = System.Drawing.Color.Green;
                GUIConsole.Write("[   OK   ]");
                GUIConsole.FG = System.Drawing.Color.White;
                GUIConsole.WriteLine(message);
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.Write("[   OK   ]");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(message);
            }

        }
        public static void Warning(string message)
        {
            if(Kernel.GUI_MODE)
            {
                GUIConsole.FG = System.Drawing.Color.Yellow;
                GUIConsole.Write("[  WARN  ]");
                GUIConsole.FG = System.Drawing.Color.White;
                GUIConsole.WriteLine(message);
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("[  WARN  ]");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(message);
            }
            
        }
        public static void Checking(string message)
        {
            if (Kernel.GUI_MODE)
            {
                GUIConsole.FG = System.Drawing.Color.Yellow;
                GUIConsole.Write("[  CHCK  ]");
                GUIConsole.FG = System.Drawing.Color.White;
                GUIConsole.WriteLine(message);
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("[  CHCK  ]");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(message);
            }
            
        }
        public static void Error(string message)
        {
            if (Kernel.GUI_MODE)
            {
                GUIConsole.FG = System.Drawing.Color.Red;
                GUIConsole.Write("[ ERROR! ]");
                GUIConsole.FG = System.Drawing.Color.White;
                GUIConsole.WriteLine(message);
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write("[ ERROR! ]");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(message);
            }
            
        }
        public static void Fatal(string message)
        {
            if (Kernel.GUI_MODE)
            {
                GUIConsole.FG = System.Drawing.Color.Black;
                GUIConsole.BG = System.Drawing.Color.Red;
                GUIConsole.Write("[ FATAL! ]");
                GUIConsole.FG = System.Drawing.Color.White;
                GUIConsole.WriteLine(message);
                GUIConsole.BG = System.Drawing.Color.Black;
            }
            else
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
}
