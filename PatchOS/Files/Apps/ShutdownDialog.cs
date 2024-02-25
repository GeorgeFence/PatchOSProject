﻿using IL2CPU.API.Attribs;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowManager = PatchOS.Files.Drivers.GUI.UI.WindowManager;
using Cosmos.System.Graphics;
using PatchOS.Process;
using System.Diagnostics.Tracing;
using PatchOS.Files.Drivers.GUI;

namespace PatchOS.Files.Apps
{
    public partial class ShutdownDialog
    {
        public static Button ShutdownButton = null!;
        public static Button RebootButton = null!;
        public static Drivers.GUI.UI.Window Swindow;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.ShutdownDialogApp.bmp")] static byte[] rawImageIcon;
        public static Bitmap Icon = new Bitmap(26, 26, rawImageIcon, ColorDepth.ColorDepth32);

        public static int I = 0;

        public static string Title()
        {
            return "Shutdown Dialog";
        }

        public static void Update()
        {
            if (ShutdownButton.IsClicked)
            {
                ProcessManager.Remove("Taskbar");
                Kernel.Canvas.Display();
                Kernel.Shutdown(0);
            }
            if (RebootButton.IsClicked)
            {
                ProcessManager.Remove("Taskbar");
                Kernel.Shutdown(1);
                Kernel.Canvas.Display();
            }
        }

        public static void Start()
        {
            Swindow = new Drivers.GUI.UI.Window((int)(Kernel.Canvas.Mode.Width / 2 - 150), (int)(Kernel.Canvas.Mode.Height / 2 - 64), 300, 128, "Shutdown Dialog", Update, DesignType.Modern, PermitionsType.Service, Icon);
            Swindow.CanMove = false;
            ShutdownButton = new Button(5,5, 273, 44, 0, "SHUTDOWN", true, System.Drawing.Color.Blue, System.Drawing.Color.White, System.Drawing.Color.DarkBlue, Drivers.AnachorType.Left);
            RebootButton = new Button(5, 44, 273, 44, 0, "REBOOT", true, System.Drawing.Color.Blue, System.Drawing.Color.White, System.Drawing.Color.DarkBlue, Drivers.AnachorType.Left);
            Swindow.Controls.Add(ShutdownButton);
            Swindow.Controls.Add(RebootButton);
            WindowManager.Add(Swindow);
        }

        public static void Stop()
        {
            WindowManager.Stop(Swindow);
        }
    }
}
