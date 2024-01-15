using PatchOS.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files
{
    public static class DiskMgr
    {
        public static BootMgr bootMgr;

        public static void CheckForDisks()
        {
            Kernel.DrawBootOut("Checking for Disks...");
            int ContainsCount = 0;
            string ContainsName = "";
            List<string> DriveName = new List<string>();
            List<string> DriveVolume = new List<string>();
            SYS32.ErrorStatusAdd("21");
            foreach (var drive in DriveInfo.GetDrives())
            {
                SYS32.ErrorStatusAdd("22");
                DriveName.Add(drive.Name);
                DriveVolume.Add(drive.VolumeLabel);
                SYS32.ErrorStatusAdd("23");
                if(PMFAT.FolderExists(drive.Name + "REG/"))
                {
                    ContainsCount++;
                }
            }
            SYS32.ErrorStatusAdd("24");
            if (DriveName.Count == 1)
            {
                SYS32.ErrorStatusAdd("One");
                PMFAT.CurrentDirectory = DriveName[0];
                PMFAT.Root = DriveName[0];
                Kernel.DrawBootOut("Checked Disks only 1");
                SYS32.ErrorStatusAdd("01");
            }
            else
            {
                for (int i = 0; i < DriveName.Count; i++)
                {
                    PMFAT.Root = DriveName[i];
                    PMFAT.CurrentDirectory = DriveName[i];
                    if (PMFAT.FolderExists(PMFAT.Root + "REG"))
                    {
                        bootMgr.AfterStart();
                        break;
                    }
                }
            }
        }
        public static void DrawSelectDisk()
        {
            SYS32.ErrorStatusAdd("10");
            List<string> DriveName = new List<string>();
            List<string> DriveVolume = new List<string>();
            GUIConsole.Clear();
            GUIConsole.SetCursorPositionChar(0, 0);
            SYS32.ErrorStatusAdd("11");
            Kernel.Canvas.DrawImage(Kernel.boot, 248, 88);
            GUIConsole.BG = System.Drawing.Color.Black;
            GUIConsole.WriteLine("  PatchOS Disk Manager                                                          ");
            GUIConsole.BG = System.Drawing.Color.White;
            GUIConsole.WriteLine("                                                                                ");
            GUIConsole.BG = System.Drawing.Color.Black;
            GUIConsole.WriteLine("  Write Directory to use                                                        ");
            GUIConsole.WriteLine("Directory       VolumeLabel       FreeSpace       TotalSpace                    ");
            SYS32.ErrorStatusAdd("12");
            foreach (var drive in DriveInfo.GetDrives())
            {
                GUIConsole.Write(drive.Name);
                GUIConsole.SetCursorPositionChar(16, GUIConsole.Y / 16);
                GUIConsole.Write(drive.VolumeLabel);
                GUIConsole.SetCursorPositionChar(34, GUIConsole.Y / 16);
                GUIConsole.Write(drive.TotalFreeSpace.ToString());
                GUIConsole.SetCursorPositionChar(50, GUIConsole.Y / 16);
                GUIConsole.WriteLine(drive.TotalSize.ToString());
                DriveName.Add(drive.Name);
                DriveVolume.Add(drive.VolumeLabel);

            }
            GUIConsole.SetCursorPositionChar(0,24);
            string Input = GUIConsole.ReadLine();
            for(int i = 0; i < DriveName.Count; i++)
            {
                if (DriveName[i] == Input)
                {
                    SYS32.ErrorStatusAdd(" 1");
                    PMFAT.CurrentDirectory = DriveName[0];
                    PMFAT.Root = DriveName[0];
                    SYS32.ErrorStatusAdd("13");
                    RegMgr.AddReg("REG/FS/DefaultRoot.reg", DriveName[0]);
                    RegMgr.AddReg("REG/FS/DefaultName.reg", DriveVolume[0]);
                    Kernel.DrawBootOut("Checked Disks");
                    SYS32.ErrorStatusAdd("14");
                    SYS32.ErrorStatusAdd("DISK3");
                    RegMgr.SetList();
                    SYS32.ErrorStatusAdd("DISK4");
                    bootMgr.AfterStart();
                }
            }
        }
    }
}
