﻿using PatchOS.Files.Coroutines;
using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PatchOS.Process;
using Microsoft.VisualBasic;
using IL2CPU.API.Attribs;
using ColorP = System.Drawing.Color;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using PatchOS.Files.Apps;
using Cosmos.Core.Memory;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PatchOS.Files.Drivers.GUI
{
    internal class Desktop : Process.Process
    {
        public static ColorP BGColor;
        public static Coroutine process = new Coroutine(desktop());
        private static int H = 0;
        private static int W = 0;
        public static bool Yield = false;
        private static int Count = 0;
        private static int wait = 0;
        private static int regfps;
        private static bool regfpsonce = true;
        public static bool DrawMouse = false;
        public static List<String> ListPar = new List<String>();
        public static Bitmap Wallpaper;
        public Desktop() : base("Desktop", Type.User, process)
        {
            //ProcessManager.Run(new MouseMgr());
            //ProcessManager.Run(new Taskbar());
        }
        
        static IEnumerator<CoroutineControlPoint> desktop()
        {
            int i = 0;
            void DrawPar()
            {
                ASC16.DrawACSIIString(Kernel.Canvas, "PatchOS Version 1", System.Drawing.Color.Orange, 5, 452);
                ASC16.DrawACSIIString(Kernel.Canvas, "GeorgeFence @Jirkovic", System.Drawing.Color.Orange, 5, 468);
                int p = 1;
                for (int i = 0; i < WindowManager.Windows.Count; i++)
                {
                    ASC16.DrawACSIIString(Kernel.Canvas, WindowManager.Windows[i].Title, System.Drawing.Color.Orange, 5, (uint)(500 + (i * 16)));
                    p++;
                }
                ASC16.DrawACSIIString(Kernel.Canvas, WindowManager.Selected, System.Drawing.Color.Blue, 5, (uint)(584));
                for (int i = 0; i < ListPar.Count; i++)
                {
                    ASC16.DrawACSIIString(Kernel.Canvas, ListPar[i], System.Drawing.Color.Green, 5, (uint)(600 + (i * 16)));
                }
            }
            bool Fill = true;
            void BGC()
            {
                try
                {
                    if (PMFAT.ReadText(PMFAT.Root + "REG/GUI/bgc.reg") == "0")
                    {
                        BGColor = ColorP.Black;
                    }
                    else if (PMFAT.ReadText(PMFAT.Root + "REG/GUI/bgc.reg") == "1")
                    {
                        BGColor = ColorP.White;
                    }
                    else if (PMFAT.ReadText(PMFAT.Root + "REG/GUI/bgc.reg") == "2")
                    {
                        BGColor = ColorP.Red;
                    }
                    else if (PMFAT.ReadText(PMFAT.Root + "REG/GUI/bgc.reg") == "3")
                    {
                        BGColor = ColorP.Green;
                    }
                    else if (PMFAT.ReadText(PMFAT.Root + "REG/GUI/bgc.reg") == "4")
                    {
                        BGColor = ColorP.Blue;
                    }
                    else if (PMFAT.ReadText(PMFAT.Root + "REG/GUI/bgc.reg") == "5")
                    {
                        BGColor = ColorP.Yellow;
                    }
                    else
                    {
                        Fill  = false;
                        //Wallpaper = Kernel.Wpp1FHD;
                    }
                    
                }
                catch (Exception ex)
                {
                    SYS32.KernelPanic(ex, "Desktop");
                }
            }

            if (Kernel.IsFileSystem)
            {
                //BGC();
                H = Int32.Parse(PMFAT.ReadText(PMFAT.Root + "REG/GUI/resolutionH.reg"));
                SYS32.ErrorStatusAdd("2");
                W = Int32.Parse(PMFAT.ReadText(PMFAT.Root + "REG/GUI/resolutionW.reg"));
            }
            else
            {
                Fill = false;
                H = 1080;
                W = 1920;
            }

            Kernel.Resolution((ushort)W,(ushort)H);
            MouseManager.ScreenHeight = (UInt32)Kernel.Canvas.Mode.Height;
            MouseManager.ScreenWidth = (UInt32)Kernel.Canvas.Mode.Width;

            BGC();
            Welcome.Start();
            bool once = true;
            int start = 0;
            int fps = 0;
            while (true)
            {
                if(once)
                {
                    start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second + 1;
                    once = false;
                }
                if (start == ((Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second)))
                {
                    once = true;
                    fps = Count;
                    Count = 0; 
                    if (Cosmos.HAL.RTC.Second.ToString().EndsWith("0"))
                    {
                        Heap.Collect();
                    }
                }
                Kernel.Canvas.Clear();
                if (Fill)
                {
                    Kernel.Canvas.Clear(BGColor);
                }
                else
                {
                    Kernel.Canvas.DrawImage(Kernel.Wpp1FHD, 0, 0);
                }
                Taskbar.DrawTaskBar();
                ASC16.DrawACSIIString(Kernel.Canvas, fps.ToString(), System.Drawing.Color.Green, 0, 0);
                WindowManager.Update(Kernel.Canvas);
                MouseMgr.DrawMouse(); 
                Kernel.Canvas.Display();
                Count++;
            } 
            yield return null;
        }
        public static void ReduceFPS(bool FromRegistry, int fps)
        {
            //1s == 1000ms
            if (regfpsonce)
            {
                regfps = Int32.Parse(PMFAT.ReadText(PMFAT.Root + "REG/GUI/fps.reg"));
                regfpsonce = false;
                if (FromRegistry)
                {
                    wait = 1000 / regfps;
                }
                else if (fps != 0)
                {
                    wait = 1000 / fps;
                }
            }
            Kernel.DelayCode((UInt32)wait);
        }

        internal override int Start()
        {
            return 0;
        }
        internal override void Stop()
        {
            Yield = true;
        }
    }
}
