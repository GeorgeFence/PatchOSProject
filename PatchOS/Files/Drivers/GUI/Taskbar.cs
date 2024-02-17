using Cosmos.System;
using Cosmos.System.Graphics;
using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ColorS = System.Drawing.Color;
using PatchOS.Files.Drivers;
using Cosmos.System.Graphics.Fonts;
using PatchOS.Files.Coroutines;
using PatchOS.Process;
using System.Drawing;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Files.Apps;
using PatchOS.Files.Drivers.Network;

namespace PatchOS.Files.Drivers.GUI
{
    internal class Taskbar : Process.Process
    {
        public static Coroutine process = new Coroutine(taskbar());
        public static bool Yield = false;
        public static List<Control> Controls;
        public static MouseState prevMouseState;
        public static bool StartBtn = true;

        public Taskbar() : base("Taskbar", Type.User, process)
        {

        }
        static IEnumerator<CoroutineControlPoint> taskbar()
        {
            bool Make = false;
            for (int i = 0; i < ProcessManager.running.Count; i++)
            {
                if ("Desktop" == ProcessManager.running[i].name)
                {
                    Make = true;
                }
            }
            while (!Yield)
            {
                if (!Make)
                {
                    DrawTaskBar();
                }
            }
            yield return null;
        }

        public static string LastApp = "";
        public static int start = 0;
        public static bool startMenu = false;
        public static void DrawTaskBar()
        {
            int X = 90;
            Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(15,15,15), 0, 0, (int)(Kernel.Canvas.Mode.Width), 20);
            Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(30,30,30), 0, (int)(Kernel.Canvas.Mode.Height - 48), (int)(Kernel.Canvas.Mode.Width), 48);

            ASC16.DrawACSIIString(Kernel.Canvas, RTC.GetTime(), System.Drawing.Color.White, (uint)(Kernel.Canvas.Mode.Width - 72), 0);
            Wifi.DrawWifiStatus((int)(Kernel.Canvas.Mode.Width - 96), 2);
            if (MouseEx.IsMouseWithin((int)(Kernel.Canvas.Mode.Width - 96), 2, 16, 16))
            {
                if (MouseManager.MouseState == MouseState.Left && prevMouseState != MouseState.Left)
                {
                    PanelSettings.Start();   
                }
            }
            for (int i = 0; i < WindowManager.Windows.Count; i++)
            {

                if(WindowManager.Selected == WindowManager.Windows[i].Title)
                {
                    Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.Gray, X - 8, (Int32)(Kernel.Canvas.Mode.Height - 48), 48,48);
                }
                Kernel.Canvas.DrawImageAlpha(WindowManager.Windows[i].Icon, X, (int)(Kernel.Canvas.Mode.Height - 40));
                if (MouseEx.IsMouseWithin(X, (int)(Kernel.Canvas.Mode.Height - 40), 32, 32) && MouseManager.MouseState == MouseState.Left && prevMouseState != MouseState.Left)
                {
                    WindowManager.Selected = WindowManager.Windows[i].Title; 
                }
                if (MouseEx.IsMouseWithin(X, (int)(Kernel.Canvas.Mode.Height - 40), 32,32) && MouseManager.MouseState == MouseState.Right && prevMouseState != MouseState.Right)
                {
                    LastApp = WindowManager.Windows[i].Title;
                    start = (Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second);

                }
                else if(MouseEx.IsMouseWithin(X, (int)(Kernel.Canvas.Mode.Height - 40), 32, 32) && MouseManager.MouseState == MouseState.None && !((start + 3) > ((Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second))))
                {
                    Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(15, 15, 15), (X + 13) - ((WindowManager.Windows[i].Title.Length * 8) / 2) - 2, (int)(Kernel.Canvas.Mode.Height - 55), (WindowManager.Windows[i].Title.Length * 8) + 4, 18);
                    ASC16.DrawACSIIString(Kernel.Canvas, WindowManager.Windows[i].Title, System.Drawing.Color.White, (uint)(X + 13) - (uint)((WindowManager.Windows[i].Title.Length * 8) / 2), (uint)(Kernel.Canvas.Mode.Height - 53));
                }
                if (LastApp == WindowManager.Windows[i].Title && (start + 3) > ((Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second)))
                {
                    Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(15, 15, 15), X, (int)(Kernel.Canvas.Mode.Height - 80), 32,32);
                    Kernel.Canvas.DrawRectangle(System.Drawing.Color.Red, X, (int)(Kernel.Canvas.Mode.Height - 80), 32, 32);
                    Kernel.Canvas.DrawImageAlpha(Kernel.ExitApp, X + 5, (int)(Kernel.Canvas.Mode.Height - 74));
                    if (MouseManager.MouseState == MouseState.Left && prevMouseState != MouseState.Left && MouseEx.IsMouseWithin(X, (int)(Kernel.Canvas.Mode.Height - 80), 26, 26))
                    {
                        WindowManager.Stop(WindowManager.Windows[i]);
                    }
                }
                X = X + 48;
            }
            Kernel.Canvas.DrawImage(Kernel.start, 8, (int)(Kernel.Canvas.Mode.Height - 40));
            if (MouseEx.IsMouseWithin(8, (int)(Kernel.Canvas.Mode.Height - 40), 64, 32) && MouseManager.MouseState == MouseState.Left && prevMouseState != MouseState.Left)
            {
                startMenu = true;
                for (int i = 0; i < ProcessManager.running.Count; i++)
                {
                    if (ProcessManager.running[i].name == Menu.Title() + ".win" )
                    {

                        if (startMenu)
                        {
                            Menu.Stop();
                            startMenu = false;
                        }
                    }
                    else 
                    {
                        if (startMenu)
                        {
                            Menu.Start();
                            startMenu = false;
                        }
                    }
                }
                
            }
            prevMouseState = MouseManager.MouseState;
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
