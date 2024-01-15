using System;
using System.Collections.Generic;
using System.Text;
using PatchOS.Files;

namespace PatchOS.Files
{
    public static class SystemInfo
    {
        // operating system
        public static string OSName = "PatchOS";
        public static string OSVersion = "1";
        public static string KernelVersion = "0.0.0.0.0.0";

        // ram
        public static uint TotalRAM { get { return Cosmos.Core.CPU.GetAmountOfRAM(); } }
        // show system information
        public static void ShowInfo()
        {
            if (Kernel.GUI_MODE)
            {
                CLI.WriteLine(OSName, ColorFile.Magenta);
                CLI.Write("version=" + OSVersion.ToString(), ColorFile.White);
                CLI.WriteLine("    |    user kit " + KernelVersion, ColorFile.Gray);
                CLI.WriteLine("Thanks for help: Int32_");
                ShowRAM();
                CLI.Write("\n");
            }
            else {
                GUIConsole.WriteLine(OSName, System.Drawing.Color.Magenta);
                GUIConsole.Write("version=" + OSVersion.ToString(), System.Drawing.Color.White);
                GUIConsole.WriteLine("    |    user kit " + KernelVersion, System.Drawing.Color.DarkGray);
                GUIConsole.WriteLine("Thanks for help: Int32_");
                GUIConsole.WriteLine(TotalRAM.ToString() + "MB RAM");
                GUIConsole.Write("\n");
            }
        }

        // show total ram
        public static void ShowRAM() { CLI.WriteLine(TotalRAM.ToString() + "MB RAM"); }
    }
}
