using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.Security.Cryptography.X509Certificates;
using RTC = Cosmos.HAL.RTC;
using static System.Net.Mime.MediaTypeNames;
using Cosmos.System.Graphics;
using PatchOS.Files.Drivers.GUI;
using System.Drawing;
using PatchOS.Process;
using Color = System.Drawing.Color;
using System.Threading;
using Cosmos.HAL;

namespace PatchOS
{
    public static class SYS32
    {
        public static string progress;
        public static bool End = false;
        public  static string ErrorStatus;
        private static bool onceCreate = true;
        private static string TempFileName;
        public static String StopCODE = "??x???? : ??";
        private static string LastStatus = "00";

        public static void ErrorStatusAdd(string text)
        {
            ErrorStatus += text;
            LastStatus = text;

        }
        public static void KernelPanic(Exception ex,string Info)
        {
            try
            {
                string p1 = "??";
                string p2 = "????";
                Kernel.Resolution(640, 480);
                Kernel.Canvas.Clear(System.Drawing.Color.RebeccaPurple);
                ASC16.DrawACSIIString(Kernel.Canvas, "A problem has been detected and PatchOS has been shut down to prevent any", Color.Black, 5, 5);
                ASC16.DrawACSIIString(Kernel.Canvas, "damage to your computer.", Color.Black, 5, 23);

                ASC16.DrawACSIIString(Kernel.Canvas, ex.Message, Color.Red, 5, 422);

                ASC16.DrawACSIIString(Kernel.Canvas, Info, Color.Orange, 5, 448);

                #region P1
                if (Info == "Coroutine") { p1 = "CR"; }
                else if (Info == "BeforeRun") { p1 = "00"; }
                else if (Info == "PMFAT") { p1 = "01"; }
                else if (Info == "2KERNELPANIC") { p1 = "02"; }
                else if (Info == "ProcessMgr") { p1 = "03"; }
                else if (Info == "Shell") { p1 = "04"; }
                else if (Info == "NetworkMgr") { p1 = "05"; }
                else if (Info == "Desktop") { p1 = "06"; }
                #endregion
                #region P2
                if (p1 == "00")
                {
                    if (ex.Message == "Read only file system") { p2 = "0001"; }
                    else if (ex.Message == "CANVAS") { p2 = "0002"; }
                    else if (ex.Message == "3") { p2 = "0003"; }
                    else{ p2 = "0000"; }
                } 
                #endregion

                StopCODE = p1 + "x" + p2 + " : " + LastStatus;
                ASC16.DrawACSIIString(Kernel.Canvas, "STOPCODE " + StopCODE, Color.Red, 5, 464);
                Kernel.Canvas.DrawImageAlpha(Kernel.boot, (int)(Kernel.Canvas.Mode.Width / 2 - 72), (int)Kernel.Canvas.Mode.Height / 4);
                Kernel.Canvas.Display();

                int start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;

                GUIConsole.X = 0;
                GUIConsole.Y = 2;
                Log.Warning("Waiting for Kernel responce");
                Kernel.DelayCode(1000);
                int end = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
                Log.Success("Kernel responsed in " + (end - start));
                Kernel.DelayCode(500);
                start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
                Log.Warning("Stopping Services");
                ProcessManager.StopAll();
                end = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
                Log.Success("All services stopped in " + (end - start));

            }
            catch(Exception exept)
            {
                KernelPanic(exept, "2KERNELPANICs");
                
            }
        }
        
        public static void loopkrnlpanic()
        {
            End = true;
            Kernel.DelayCode(1000);
            progress += " ";
            
            ProgressBar.Pbar40x3x20(20, 15, progress ,ColorFile.DarkGray, ColorFile.Gray, ColorFile.Blue);
            if (progress != "                    ")
            {
                loopkrnlpanic();
            }
            if (progress == "                    ")
            {
                Kernel.DelayCode(1000);
                Console.Write("" +
                    "[Done]");
                
                progress = "";
            }
        }

    }
}
