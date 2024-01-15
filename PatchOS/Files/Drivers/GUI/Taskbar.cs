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
                    Kernel.Canvas.Display();
                }
            }
            yield return null;
        }

        public static void DrawTaskBar()
        {
            int X = 90;
            Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(15,15,15), 0, 0, (int)(Kernel.Canvas.Mode.Width), 20);
            Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(15,15,15), 0, (int)(Kernel.Canvas.Mode.Height - 48), (int)(Kernel.Canvas.Mode.Width), 48);

            ASC16.DrawACSIIString(Kernel.Canvas, RTC.GetTime(), System.Drawing.Color.White, (uint)(Kernel.Canvas.Mode.Width - 72), 0);
            //Wifi.DrawWifiStatus((int)(Kernel.Canvas.Mode.Width - 96), 2);
            if (MouseEx.IsMouseWithin((int)(Kernel.Canvas.Mode.Width - 96), 2, 16, 16))
            {
                if (MouseManager.MouseState == MouseState.Left && prevMouseState != MouseState.Left)
                {
                    PanelSettings.Start();   
                }
            }

            /*for (int i = 0; i < WindowManager.Windows.Count; i++)
            {
                Kernel.Canvas.DrawImageAlpha(WindowManager.Windows[i].Icon, X, (int)(Kernel.Canvas.Mode.Height - 37));
                if (MouseEx.IsMouseWithin(X, (int)(Kernel.Canvas.Mode.Height - 37), 26,26))
                {
                    Kernel.Canvas.DrawImageAlpha(Img.exitApp, X + 2, (int)(Kernel.Canvas.Mode.Height - 35));
                    if (MouseManager.MouseState == MouseState.Left && prevMouseState != MouseState.Left)
                    {
                        WindowManager.Stop(WindowManager.Windows[i]);
                    }
                }
                X = X + 32;
            }*/
            Kernel.Canvas.DrawImage(Kernel.start, 8, (int)(Kernel.Canvas.Mode.Height - 40));
            if (MouseEx.IsMouseWithin(8, (int)(Kernel.Canvas.Mode.Height - 40), 64, 32))
            {
                if(MouseManager.MouseState == MouseState.Left && prevMouseState != MouseState.Left)
                {
                    Menu.Start();
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
