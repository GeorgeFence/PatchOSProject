using PatchOS.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

namespace PatchOS.Files
{
    public static class RegMgr
    {
        private static Random rnd = new Random();
        public static List<string> RegName = new List<string>();
        public static List<string> RegValue = new List<string>();
        public static void AddReg(string Path, string Value)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                PMFAT.CreateFile(PMFAT.Root + Path);
                RegName.Add(Path);
                RegValue.Add(Value);
                PMFAT.WriteAllText(PMFAT.Root + Path, Value);
                refresh();
            }
            catch(Exception ex)
            {
                Log.Error("Adding REGs");
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }

        public static void DeleteReg(string Path)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try{
                RegName.Remove(Path);
                RegValue.Remove(PMFAT.ReadText( PMFAT.Root + Path));
                PMFAT.DeleteFile(PMFAT.Root + Path);
                refresh(); 
            }
            catch(Exception ex)
            {
                Log.Error("Deleting REGs");
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }
        public static void refresh()
        {
            if(PMFAT.FileExists(PMFAT.Root + "name.sys") == true) { PMFAT.DeleteFile(PMFAT.Root + "name.sys"); }
            if (PMFAT.FileExists(PMFAT.Root + "value.sys") == true) { PMFAT.DeleteFile(PMFAT.Root + "value.sys"); }
            PMFAT.CreateFile(PMFAT.Root + "name.sys");
            PMFAT.CreateFile(PMFAT.Root + "value.sys");

            PMFAT.WriteAllLines(PMFAT.Root + "value.sys", RegValue.ToArray());
            PMFAT.WriteAllLines(PMFAT.Root + "name.sys", RegName.ToArray());
        }
        public static void SetList()
        {
            Kernel.DrawBootOut("Setting up Registers...");
            string[] name = PMFAT.ReadLines(PMFAT.Root + "name.sys");
            string[] value = PMFAT.ReadLines(PMFAT.Root + "value.sys");
            RegName.Clear();
            RegValue.Clear();
            foreach (string sname in name)
            {
                RegName.Add(sname);
            }
            foreach (string svalue in value)
            {
                RegValue.Add(svalue);
            }
            Kernel.DrawBootOut("Done Setting Registers");
        }

        public static void ResetReg()
        {
            // 0 = black or false
            // 1 = white or true
            // 2 = red
            // 3 = green
            // 4 = blue
            // 5 = yellow

            PMFAT.CreateFolder(PMFAT.Root + "REG");

            PMFAT.CreateFolder(PMFAT.Root + "REG/SHELL");

            //shell
            RegName.Add("REG/SHELL/dtc.reg");
            RegValue.Add("4");
            RegName.Add("REG/SHELL/tbc.reg");
            RegValue.Add("2");
            RegName.Add("REG/SHELL/tc.reg");
            RegValue.Add("5");

            RegName.Add("REG/SHELL/W.reg");
            RegValue.Add("1280");
            RegName.Add("REG/SHELL/H.reg");
            RegValue.Add("720");

            //boot
            PMFAT.CreateFolder(PMFAT.Root + "REG/BOOT");

            RegName.Add(@"REG/BOOT/boot.reg");
            RegValue.Add("2");

            //FS
            PMFAT.CreateFolder(PMFAT.Root + "REG/FS");

            RegName.Add(@"REG/FS/DefaultRoot.reg");
            RegValue.Add("");

            RegName.Add(@"REG/FS/DefaultName.reg");
            RegValue.Add("");

            //FS FORMATS
            PMFAT.CreateFolder(PMFAT.Root + "REG/FS/FILEFORMAT");

            RegName.Add(@"REG/FS/FILEFORMAT/REG.reg");
            RegValue.Add("1");

            RegName.Add(@"REG/FS/FILEFORMAT/TXT.reg");
            RegValue.Add("1");

            RegName.Add(@"REG/FS/FILEFORMAT/REG.reg");
            RegValue.Add("1");

            RegName.Add(@"REG/FS/FILEFORMAT/TXT.reg");
            RegValue.Add("1");


            //gui
            PMFAT.CreateFolder(PMFAT.Root + "REG/GUI");

            RegName.Add(@"REG/GUI/bgc.reg");
            RegValue.Add("6");
            RegName.Add("REG/GUI/resolutionW.reg");
            RegValue.Add("1920");
            RegName.Add("REG/GUI/resolutionH.reg");
            RegValue.Add("1080");
            RegName.Add("REG/GUI/fps.reg");
            RegValue.Add("60");

            RegName.Add("REG/GUI/developermode.reg");
            RegValue.Add("0");

            //gui/taskbar
            PMFAT.CreateFolder(PMFAT.Root + "REG/GUI/TASKBAR");

            RegName.Add(@"REG/GUI/TASKBAR/ShowApps.reg");
            RegValue.Add("1");

            RegName.Add(@"REG/GUI/TASKBAR/Show.reg");
            RegValue.Add("1");

            //gui/basic
            PMFAT.CreateFolder(PMFAT.Root + "REG/GUI/BASIC");

            RegName.Add(@"REG/GUI/BASIC/WindowDefaultW.reg");
            RegValue.Add("640");
            RegName.Add("REG/GUI/BASIC/WindowDefaultH.reg");
            RegValue.Add("480");
            RegName.Add("REG/GUI/WindowDefaultX.reg");
            RegValue.Add("90");
            RegName.Add("REG/GUI/WindowDefaultY.reg");
            RegValue.Add("90");

            //gui/taskbar
            PMFAT.CreateFolder(PMFAT.Root + "REG/GUI/TASKBAR");

            RegName.Add(@"REG/GUI/TASKBAR/width.reg");
            RegValue.Add("30");


            //reg uptodate 

            RegName.Add("REG/reg.reg");
            RegValue.Add("1");
            refresh();
            //for loop
            for (int i = 0; i < RegName.Count(); i++)
            {
                CLI.WriteLine(RegName[i]+ " " + RegValue[i]);
                PMFAT.CreateFile(PMFAT.Root + RegName[i]);
                PMFAT.WriteAllText(PMFAT.Root + RegName[i], RegValue[i]);
                if(i == RegName.Count())
                {
                    refresh();
                }
            }
        }

        public static void SettingUp()
        {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            int progress = 0;

            Console.BackgroundColor = ConsoleColor.Black;
            CLI.SetCursorPos(0, 0);
            CLI.WriteLine("Setting up your Registry");
            ProgressBar.Pbar40x3x20(20, 15, 500, ColorFile.DarkGray, ColorFile.Gray, ColorFile.Blue);
            ResetReg();
            
        }

        public static bool IsValueStr(string Reg, string value)
        {
            bool Out = false;
            if(PMFAT.FileExists(PMFAT.Root + Reg)) {
                if (PMFAT.ReadText(PMFAT.Root + Reg) == value)
                    Out = true;
            }

            return Out;
        }

        public static string GetValue(string Reg)
        {
            return PMFAT.ReadText(PMFAT.Root + Reg);
        }
    }
}
