using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Cosmos.System.Coroutines;
using Microsoft.VisualBasic;
using System.Diagnostics.Contracts;
using Cosmos.System; 
using Console = System.Console;
using PrismAPI.Tools.Extentions;
using Cosmos.System.FileSystem.FAT;
using Cosmos.HAL.BlockDevice;
using Cosmos.HAL;  
using PrismAPI.Runtime.SShell.Scripts;
using Cosmos.Core.Memory;

namespace Installer
{
    public static class Installer
    { 

        private static string DrawAskOut;

        private static MBR mbr; 


        [ManifestResourceStream(ResourceName = "Installer.image.hdd")]
        public static byte[] boot;

        [ManifestResourceStream(ResourceName = "Installer.boot.limine.cfg")]
        public static byte[] cfg;

        [ManifestResourceStream(ResourceName = "Installer.boot.limine-bios.sys")]
        public static byte[] sys;

        [ManifestResourceStream(ResourceName = "Installer.boot.BOOTIA32.EFI")]
        public static byte[] b32;

        [ManifestResourceStream(ResourceName = "Installer.boot.BOOTX64.EFI")]
        public static byte[] b64;

        [ManifestResourceStream(ResourceName = "Installer.boot.bin.gz")]
        public static byte[] bin;


        [ManifestResourceStream(ResourceName = "Installer.empty_sector")]
        public static byte[] emptysector;


        public static int DiskInt;
        public static void Init()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Blue;
            System.Console.Clear();
        }

        public static void Install()
        {
            try
            {
                System.Console.Clear();
                DrawTitle();

                DrawInstallSel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public static void DrawTitle()
        {
            System.Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                PatchOSInstaller                                ", Color.Red);
            System.Console.BackgroundColor = ConsoleColor.Blue;
        }

        public static void DrawUpdates(string str)
        {
            System.Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < (80 - str.Length); i++)
            {
                Console.Write(" ");
            }
            Console.Write(str);
            System.Console.BackgroundColor = ConsoleColor.Blue;
        }

        public static void DrawTable(int height, string title)
        {
            Console.SetCursorPosition(2, 6);
            Console.Write(title);
            Console.SetCursorPosition(2, 7);
            int y = 8;
            Console.WriteLine("############################################################################");
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(2, y);
                Console.WriteLine("#                                                                          #");
                y++;
            }
            Console.SetCursorPosition(2, y);
            Console.WriteLine("############################################################################");

        }
        public static string DrawAskTable(int height, string title)
        {
            bool repeat = true;
            DrawAskOut = "";
            string Out = "";
            Console.SetCursorPosition(2, 6);
            Console.Write(title);
            Console.SetCursorPosition(2, 7);
            int y = 8;
            Console.WriteLine("############################################################################");
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(2, y);
                Console.WriteLine("#                                                                          #");
                y++;
            }
            Console.SetCursorPosition(2, y);
            Console.WriteLine("############################################################################");
            Console.WriteLine("                                                                [ENTER] Next");
            Console.SetCursorPosition(3, 8);
            Console.Write("> ");
            return Console.ReadLine();
        }

        public static void DrawInstallSel()
        {
            bool once = true;
            int selection = 0;
            List<String> sel = new List<String>();
            sel.Add("Install");
            sel.Add("Cancel and shutdown");
            sel.Add("Cancel");
            DrawUpdates("Tip: Use arrows to select");
            DrawTable(3, "");
            while (true)
            {
                if (once)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (once)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(sel[i]);
                            Console.BackgroundColor = ConsoleColor.Blue;
                            once = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(sel[i]);

                        }
                    }
                    once = false;
                }

                var keyDetection = Console.ReadKey();

                for (int i = 0; i < 3; i++)
                {
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write("                                                                        ");
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write(sel[i]);
                }

                if (keyDetection.Key == ConsoleKey.UpArrow)
                {
                    if (selection == 0)
                    {
                        selection = (3 - 1);
                    }
                    else
                    {
                        selection--;
                    }

                }
                else if (keyDetection.Key == ConsoleKey.DownArrow)
                {
                    if (selection == (3 - 1))
                    {
                        selection = 0;
                    }
                    else
                    {
                        selection++;
                    }
                }
                else if (keyDetection.Key == ConsoleKey.Enter)
                {
                    if (selection == 0)
                    {
                        DrawDiskSel();
                    }
                    if (selection == 1)
                    {
                        Cosmos.System.Power.Shutdown();
                    }
                    if (selection == 2)
                    {
                        Console.Clear();
                        DrawTitle();
                        DrawUpdates("Safe to shutdown your PC");
                    }

                    break;
                }

                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write("                                                                        ");
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write(sel[selection], Color.Blue);
                Console.BackgroundColor = ConsoleColor.Blue;
            }
        }

        public static void DrawDiskSel()
        {
            Console.Clear();
            DrawTitle();
            bool once = true;
            int selection = 0;
            DrawUpdates("Tip: Use arrows to select");
            List<DriveInfo> Drive = new List<DriveInfo>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                Drive.Add(drive);
            }
            DrawTable(Drive.Count, "Select disk for PatchOS");
            while (true)
            {
                if (once)
                {
                    for (int i = 0; i < Drive.Count; i++)
                    {
                        if (once)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(Drive[i].VolumeLabel.Replace("  ", "").ToString() + ", " + Drive[i].RootDirectory.ToString() + ", " + Drive[i].DriveFormat.ToString() + ", " + (Drive[i].TotalFreeSpace / 1024).ToString() + " KB free of " + (Drive[i].TotalSize / 1024).ToString() + " KB");
                            Console.BackgroundColor = ConsoleColor.Blue;
                            once = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(Drive[i].VolumeLabel.Replace("  ", "").ToString() + ", " + Drive[i].RootDirectory.ToString() + ", " + Drive[i].DriveFormat.ToString() + ", " + (Drive[i].TotalFreeSpace / 1024).ToString() + " KB free of " + (Drive[i].TotalSize / 1024).ToString() + " KB");

                        }
                    }
                    once = false;
                }

                var keyDetection = Console.ReadKey();

                for (int i = 0; i < Drive.Count; i++)
                {
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write("                                                                        ");
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write(Drive[i].VolumeLabel.Replace("  ", "").ToString() + ", " + Drive[i].RootDirectory.ToString() + ", " + Drive[i].DriveFormat.ToString() + ", " + (Drive[i].TotalFreeSpace / 1024).ToString() + " KB free of " + (Drive[i].TotalSize / 1024).ToString() + " KB");
                }

                if (keyDetection.Key == ConsoleKey.UpArrow)
                {
                    if (selection == 0)
                    {
                        selection = (Drive.Count() - 1);
                    }
                    else
                    {
                        selection--;
                    }

                }
                else if (keyDetection.Key == ConsoleKey.DownArrow)
                {
                    if (selection == (Drive.Count - 1))
                    {
                        selection = 0;
                    }
                    else
                    {
                        selection++;
                    }
                }
                else if (keyDetection.Key == ConsoleKey.Enter)
                {
                    PMFAT.Root = Drive[selection].RootDirectory.ToString();
                    PMFAT.CurrentDirectory = Drive[selection].RootDirectory.ToString();

                    DiskInt = selection;

                    //DrawPart();
                    DrawPart();
                    break;
                }

                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write("                                                                        ");
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write(Drive[selection].VolumeLabel.Replace("  ", "").ToString() + ", " + Drive[selection].RootDirectory.ToString() + ", " + Drive[selection].DriveFormat.ToString() + ", " + (Drive[selection].TotalFreeSpace / 1024).ToString() + " KB free of " + (Drive[selection].TotalSize / 1024).ToString() + " KB", Color.Blue);
                Console.BackgroundColor = ConsoleColor.Blue;
            }
        }

        public static void DrawPart()
        {
            Console.Clear();
            DrawTitle();

            DrawUpdates("Wait, working on Disk");
              

            Log.Warning("Clearing Disk " + DiskInt.ToString());
            PMFAT.Driver.Disks[DiskInt].Clear();
            Log.Success("Clearing Disk " + DiskInt.ToString());

            Log.Warning("Partition 0");
            PMFAT.Driver.Disks[DiskInt].CreatePartition(64);
            Log.Success("Partition 0");

            DrawFormatOptions();

            Log.Warning("Mounting Disk " + DiskInt.ToString());
            PMFAT.Driver.Disks[DiskInt].Mount();
            Log.Success("Mounting Disk " + DiskInt.ToString());
             
            Thread.Sleep(1000);

            Log.Success("Done Working With Disk");
            DrawUpdates("Done Working With Disk");

            Thread.Sleep(2000); 

            WriteBoot();
        }

        private static void ListContents(string path)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                string[] folders = PMFAT.GetFolders(path);
                string[] files = PMFAT.GetFiles(path);

                Console.WriteLine("Showing contents of directory \"" + path + "\"");

                // draw folders
                for (int i = 0; i < folders.Length; i++)
                {
                    Console.WriteLine(folders[i], Color.Yellow);
                }

                // draw files
                for (int i = 0; i < files.Length; i++)
                {
                    Cosmos.System.FileSystem.Listing.DirectoryEntry attr = PMFAT.GetFileInfo(path + files[i]);
                    if (attr != null)
                    {
                        Console.Write(files[i], Color.White);
                        Console.SetCursorPosition(30, Console.CursorTop);
                        Console.WriteLine(attr.mSize.ToString() + " BYTES", Color.Gray);
                    }
                    else { Console.WriteLine("Error retrieiving file info", Color.Red); }
                }

                Console.WriteLine("");
                Console.Write("Total folders: " + folders.Length.ToString());
                Console.WriteLine("        Total files: " + files.Length.ToString());
            }
            catch (Exception ex) { Log.Error(ex.ToString()); }
#pragma warning restore CS0168 // Variable is declared but never used
        }

        public static void DrawInstalling()
        {
            Thread.Sleep(1000);
            Console.Clear();
            DrawTitle(); 
            DrawUpdates("Wait for the installer to install PatchOS"); 
            DrawUpdates("Copying");
            int start = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second;

            ListContents(PMFAT.Driver.Disks[DiskInt].Partitions[0].RootPath);


            int end = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second;


            DrawUpdates("Done in " + (end - start) + " seconds");
            Thread.Sleep(3000);         
        }

        public static void DrawFormatOptions()
        {
            Console.Clear();
            DrawTitle();
            bool once = true;
            int selection = 0;
            List<String> sel = new List<String>();
            sel.Add("Format    QUICK - Data recover possible");
            sel.Add("Format    SLOW  - No data recover possible");
            sel.Add("Fill      0     - Takes long time");
            sel.Add("Nope, no format - Really risky");
            DrawUpdates("Tip: Think before doing");
            DrawTable(4, "To continue installer, u need freshly formated disk");
            while (true)
            {
                if (once)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (once)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(sel[i]);
                            Console.BackgroundColor = ConsoleColor.Blue;
                            once = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(sel[i]);

                        }
                    }
                    once = false;
                }

                var keyDetection = Console.ReadKey();

                for (int i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write("                                                                        ");
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write(sel[i]);
                }

                if (keyDetection.Key == ConsoleKey.UpArrow)
                {
                    if (selection == 0)
                    {
                        selection = (4 - 1);
                    }
                    else
                    {
                        selection--;
                    }

                }
                else if (keyDetection.Key == ConsoleKey.DownArrow)
                {
                    if (selection == (4 - 1))
                    {
                        selection = 0;
                    }
                    else
                    {
                        selection++;
                    }
                }
                else if (keyDetection.Key == ConsoleKey.Enter)
                {
                    if (selection == 0)
                    {
                        PMFAT.Driver.Disks[DiskInt].FormatPartition(0, "FAT32", true);
                    }
                    if (selection == 1)
                    {
                        PMFAT.Driver.Disks[DiskInt].FormatPartition(0, "FAT32", false);
                    }
                    if (selection == 2)
                    {
                        Console.Clear();
                        DrawTitle();
                        FillDisk();
                    }
                    if (selection == 3)
                    {
                        DrawUpdates("Lets continue...");
                        Thread.Sleep(1000);
                    }

                    break;
                }

                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write("                                                                        ");
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write(sel[selection], Color.Blue);
                Console.BackgroundColor = ConsoleColor.Blue;
            }
        } 

        public static void FillDisk()
        {
            try
            {
                DrawUpdates("Preparing for Formating disk");
                Thread.Sleep(1000);
                int start = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second;
                int end = 0;
                int one = 0;
                int eta = 0;
                for (int i = 0; i < (((PMFAT.Driver.Disks[DiskInt].Size) / 512) / 10); i++)
                {
                    PMFAT.Driver.Disks[DiskInt].Host.WriteBlock((ulong)(i - 1), 10, ref emptysector);
                    DrawUpdates("Formated "+ (i * 10) +" / " + (PMFAT.Driver.Disks[DiskInt].Size / 512) + " Run time: " + (end - start) + "s" + " ETA: " + eta.ToString() + "s");
                    Heap.Collect();
                    if (i.ToString().EndsWith("00"))
                    { 
                        end = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second;
                        eta = (((((PMFAT.Driver.Disks[DiskInt].Size) / 512) / 10) - i)/one);
                    }
                    if((end - start) == 1)
                    {
                        one = i;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Fatal("Cannot Format disk!!! " + ex.ToString());
            }
        }
        public static void WriteBoot()
        {
            Console.Clear();
            DrawTitle();
            DrawUpdates("Writing Boot Partition");
            int start = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second;

            PMFAT.Driver.Disks[DiskInt].Host.WriteBlock(0, 32768, ref boot);

            int end = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second; 


            DrawUpdates("Writed in " + (end - start) + " seconds");
            Thread.Sleep(1000);
            DrawUpdates("Formating again");
            Log.Warning("Formating");
            PMFAT.Driver.Disks[0].CreatePartition(96);
            PMFAT.Driver.Disks[0].FormatPartition(0,"FAT32", true);
            Log.Success("Formating");
            DrawUpdates("Copying system files");
            start = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second;
            Log.Warning("Copying files");
            PMFAT.CreateFolder(@"0:\ELF/BOOT");
            PMFAT.CreateFile(@"0:\limine.cfg");
            PMFAT.WriteAllBytes(@"0:\limine.cfg", cfg);
            PMFAT.CreateFile(@"0:\limine-bios.sys");
            PMFAT.WriteAllBytes(@"0:\limine-bios.sys", sys);
            PMFAT.CreateFile(@"0:\EFI/BOOT/BOOTIA32.EFI");
            PMFAT.WriteAllBytes(@"0:\EFI/BOOT/BOOTIA32.EFI", b32);
            PMFAT.CreateFile(@"0:\EFI/BOOT/BOOTX64.EFI");
            PMFAT.WriteAllBytes(@"0:\EFI/BOOT/BOOTX64.EFI", b64);
            PMFAT.CreateFile(@"0:\PatchOS.bin.gz");
            PMFAT.WriteAllBytes(@"0:\PatchOS.bin.gz", bin);
            end = RTC.Hour * 3600 + RTC.Minute * 60 + RTC.Second;
            Log.Success("Done copying files in " + (end - start));
            DrawUpdates("Done copying files in " + (end - start));
            Thread.Sleep(1000);
            DrawTable(1, "Rebooting");
            Thread.Sleep(2000);
            Cosmos.System.Power.Reboot();
        }
    }
}
