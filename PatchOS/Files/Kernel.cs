using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using PatchOS.Commands;
using Cosmos.System.Graphics;
using PatchOS.Files.Drivers;
using PatchOS.Files;
using PatchOS.Files.Drivers.GUI;
using System.Runtime.ExceptionServices;
using System.ComponentModel;
using PatchOS.Process;
using PatchOS.Files.Coroutines;
using IL2CPU.API.Attribs;
using PatchOS.Files.Drivers.Network;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Diagnostics;
using System.Collections; 
using Cosmos.Core;
using System.Threading;
using RTC = Cosmos.HAL.RTC;
using Cosmos.Core.MemoryGroup;

namespace PatchOS
{
    public class Kernel : Sys.Kernel
    {
        public static Bitmap boot;
        public static PATCHVGACanvas Canvas; // if  real hw use VGACanvas!!! VERY IMPORTANT - edit: yeah VGACanvas not working :(
        public BootMgr bootMgr;
        private static List<string> Out = new List<string>();
        public static  int Y = 0;
        public static bool GUI_MODE = false;
        public static bool IsFileSystem = true;
        private static ConsoleKeyInfo o;
        private static int ShutdownMode = 0;
        #region IMAGES
        public static Bitmap Cursor;
        public static Bitmap Wpp1FHD;
        public static Bitmap Wpp2FHD;
        public static Bitmap Wpp1HD;
        public static Bitmap Wpp2HD;
        public static Bitmap start;
        public static Bitmap apk;
        public static Bitmap UserApp;
        public static Bitmap SystemApp;
        public static Bitmap ServiceApp;
        public static Bitmap ExitApp;
        public static Bitmap WifiNotConn;
        public static Bitmap WifiConn;
        public static Bitmap WifiNotInte;
        #endregion

        protected override void BeforeRun()
        {
            try
            {
                Canvas = (PATCHVGACanvas)FullScreenCanvas.GetFullScreenCanvas(new Mode(1280, 720, ColorDepth.ColorDepth32));
                Canvas.Clear(System.Drawing.Color.Black);
                SYS32.TempFileName = "log";

                PMFAT.Initialize();
                if (!PMFAT.FileExists(PMFAT.Root + SYS32.TempFileName + ".txt"))
                {
                    PMFAT.CreateFile(PMFAT.Root + SYS32.TempFileName + ".txt");
                }
                SYS32.ErrorStatusAdd("KERNEL STARTS WORKING");
                DrawBootOut("Checking Requirments");
                DelayCode(500);
                if (!PMFAT.FolderExists(PMFAT.Root + "Files") || !PMFAT.FolderExists(PMFAT.Root + "REG") )
                {
                    SYS32.ErrorStatusAdd("!1");
                    Kernel.Canvas.Clear(System.Drawing.Color.Black);
                    Install.InstallFiles();
                    SYS32.ErrorStatusAdd("!2");
                    //DelayCode(1000);
                    GUIConsole.Write("PatchOS is now safe to"); GUIConsole.WriteLine(" turn OFF", System.Drawing.Color.Red);
                    //Shutdown(1);
                }
                else
                {
                    boot = new Bitmap(PMFAT.Root + "Files/boot");
                    GUI_MODE = true;
                    Canvas.Clear(System.Drawing.Color.Black);
                    Canvas.DrawImageAlpha(boot, 1280 / 2 - 72, 100);
                   
                    ASC16.DrawACSIIString(Canvas, "any key Regedit", System.Drawing.Color.Red, 0, 688);
                    ASC16.DrawACSIIString(Canvas, "else    Boot", System.Drawing.Color.Green, 0, 704);
                    
                    DrawBootOut("IsFileSystem = " + IsFileSystem.ToString());
                    DrawBootOut("Wait for input 1s");
                    DelayCode(1000);
                    try
                    {
                        if(Console.KeyAvailable)
                        {
                            Kernel.Canvas.Disable();
                            Kernel.GUI_MODE = false;
                            REGEDIT.DrawFresh(@"0:\REG\");
                        }
                        else
                        {
                            SYS32.ErrorStatusAdd("1");
                            Files.Factory.Init();
                            SYS32.ErrorStatusAdd("2");
                            DrawBootOut("Init Images");
                            #region Img
                            Cursor = new Bitmap(PMFAT.Root + "Files/cursor");
                            DrawBootOut("   cursor");
                            start = new Bitmap(PMFAT.Root + "Files/start");
                            DrawBootOut("   start");
                            Wpp1FHD = new Bitmap(PMFAT.Root + "Files/wpp1fhd");
                            DrawBootOut("   wpp1fhd");
                            Wpp2FHD = new Bitmap(PMFAT.Root + "Files/wpp2fhd");
                            DrawBootOut("   wpp2fhd");
                            Wpp1HD = new Bitmap(PMFAT.Root + "Files/wpp1hd");
                            DrawBootOut("   wpp1hd");
                            Wpp2HD = new Bitmap(PMFAT.Root + "Files/wpp2hd");
                            DrawBootOut("   wpp2hd");
                            apk = new Bitmap(PMFAT.Root + "Files/apk");
                            DrawBootOut("   apk");
                            UserApp = new Bitmap(PMFAT.Root + "Files/user");
                            DrawBootOut("   userapp");
                            SystemApp = new Bitmap(PMFAT.Root + "Files/system");
                            DrawBootOut("   systemapp");
                            ServiceApp = new Bitmap(PMFAT.Root + "Files/service");
                            DrawBootOut("   serviceapp");
                            ExitApp = new Bitmap(PMFAT.Root + "Files/exit");
                            DrawBootOut("   exitapp");
                            WifiConn = new Bitmap(PMFAT.Root + "Files/wific");
                            WifiNotConn = new Bitmap(PMFAT.Root + "Files/wifinc");
                            WifiNotInte = new Bitmap(PMFAT.Root + "Files/wifini");
                            DrawBootOut("   WiFi");
                            #endregion
                            DrawBootOut("Init Network");
                            NetworkMgr.Initialize();
                            SYS32.ErrorStatusAdd("3");
                            if (IsFileSystem)
                            {
                                DiskMgr.CheckForDisks();
                            }
                            else
                            {
                                Kernel.DrawBootOut("Booting to 2");
                                Kernel.DelayCode(1000);
                                ProcessManager.Run(new Desktop());
                            }
                            SYS32.ErrorStatusAdd("4");
                            ProcessManager.Start();
                        }
                    }
                    catch (Exception ex) { SYS32.KernelPanic(ex, "BeforeRun"); }
                }
            }
            catch (Exception ex)
            {
                SYS32.KernelPanic(ex,"BeforeRun");
            }
        }
        protected override void Run()
        {

        }

        public static void DrawBootOut(string NewOut)
        {
            GUIConsole.FG = System.Drawing.Color.White;
            string space = "";
            Out.Add(NewOut);
            if(Y == 480)
            {
                Y = 0;
                GUIConsole.SetCursorPositionChar(0, (Y / 16));
                GUIConsole.Write(Out[Out.Count - 1]);
                Y = Y + 16;
                if (Y != 480)
                {
                    GUIConsole.SetCursorPositionChar(0, (Y / 16));
                    for(int i = 0;i<NewOut.Length;i++)
                    {
                        space = space + " ";
                    }
                    GUIConsole.Write(space); 
                }
            }
            else
            {
                GUIConsole.SetCursorPositionChar(0, (Y / 16));
                GUIConsole.Write(Out[Out.Count - 1]);
                Y = Y + 16;
                if (Y != 480)
                {
                    GUIConsole.SetCursorPositionChar(0, (Y / 16));
                    for (int i = 0; i < NewOut.Length; i++)
                    { 
                        space = space + " ";
                    }
                    GUIConsole.Write(space);
                }
            } 
        }
        public static void Resolution(ushort W,ushort H) {Canvas = (PATCHVGACanvas)FullScreenCanvas.GetFullScreenCanvas(new Mode(W, H, ColorDepth.ColorDepth32)); }
        public static void DelayCode(uint milliseconds)
        {
            Cosmos.HAL.PIT pit = new Cosmos.HAL.PIT();
            pit.Wait(milliseconds);
            pit = new Cosmos.HAL.PIT();
        }

        public static void Shutdown(int mode, string PackagePath = null)
        {
            if (mode == 2)
            {
                Update(PackagePath);
            }
            else
            {
                int start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;

                GUIConsole.X = 0;
                GUIConsole.Y = 2;
                Log.Warning("Waiting for Kernel responce");
                DelayCode(1000);
                int end = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
                Log.Success("Kernel responsed in " + (end - start));
                DelayCode(500);
                start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
                Log.Warning("Stopping Services");
                ProcessManager.StopAll();
                end = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
                Log.Success("All services stopped in " + (end - start));
                Log.Warning("Waiting for ACPI for Shutdown");
                ShutdownMode = mode;
                StopKernel();

            }
            
        }
        public static void StopKernel()
        {
            Canvas.Disable();

            Console.WriteLine("");
            Console.WriteLine("Turning system down! Please wait");
            Thread.Sleep(2000);
            if (ShutdownMode == 0)
            {
                ACPI.Shutdown();
            }
            else
            {
                Sys.Power.Reboot();
            }
        }
        public static void Update(string PackagePath)
        {
            Y = 384;
            Kernel.Resolution(1280, 720);
            Canvas.Clear(0);
            Canvas.DrawImageAlpha(boot, 248, 50);
            ;
            Kernel.DrawBootOut("Stopping services");
            Process.ProcessManager.StopAll();
            DelayCode(1000);
            Y = 384;
            Canvas.Clear(0);
            Canvas.DrawImageAlpha(boot, 248, 50);
            ;
            Kernel.DrawBootOut("Preparing system for updates");
            DelayCode(1000);

        }
    }
}
