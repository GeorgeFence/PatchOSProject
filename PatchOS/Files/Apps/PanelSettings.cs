using IL2CPU.API.Attribs;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Cosmos.System.Graphics;
using PatchOS.Files.Drivers.GUI;

namespace PatchOS.Files.Apps
{
    public partial class PanelSettings
    {
        public static Drivers.GUI.UI.Window window;
        public static Panel panel;
        public static Button MENUbtnDesktop;
        public static Button back;

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.settings.bmp")] static byte[] rawImage14;

        public static string ACTIVITY = "MENU";
        public static string lastACTIVITY = "";

        public static int I = 0;

        public static string Title()
        {
            return "Settings";
        }

        public static void Update()
        {
            if(lastACTIVITY != ACTIVITY)
            {
                if (ACTIVITY == "MENU")
                {
                    window.Controls.Add(panel);
                    window.Controls.Add(MENUbtnDesktop);
                }
                if (ACTIVITY == "DESKTOP")
                {
                    window.Controls.Add(panel);
                    window.Controls.Add(back);
                }
            }
            if (ACTIVITY == "MENU")
            {
                if (MENUbtnDesktop.IsClicked)
                {
                    REMOVEACTIVITY();
                    ACTIVITY = "DESKTOP";
                }
            }
            if (ACTIVITY == "DESKTOP")
            {
                if(back.IsClicked)
                {
                    REMOVEACTIVITY();
                    ACTIVITY = "MENU";
                }
            }

            lastACTIVITY = ACTIVITY;
        }

        public static void REMOVEACTIVITY()
        {
            window.Controls.Remove(panel);
            window.Controls.Remove(MENUbtnDesktop);
            window.Controls.Remove(back);
        }

        public static void Start()
        {
            window = new Drivers.GUI.UI.Window((int)(Kernel.Canvas.Mode.Width - 300), 20, 300, (ushort)(Kernel.Canvas.Mode.Height - 68), "Settings", Update, DesignType.Blank, PermitionsType.Service, Kernel.apk);
            panel = new Panel(10,10,280,600, System.Drawing.Color.FromArgb(30, 30, 30), Drivers.AnachorType.Centre);
            MENUbtnDesktop = new Button(15,15,64,24,0,"DESKTOP", true, System.Drawing.Color.DarkGray , System.Drawing.Color.LightGray, System.Drawing.Color.Black ,Drivers.AnachorType.Left);

            back = new Button(15, 15, 48, 24, 0, "Back", true, System.Drawing.Color.DarkGray, System.Drawing.Color.LightGray, System.Drawing.Color.Red, Drivers.AnachorType.Left);
            window.CanMove = false;
            WindowManager.Add(window);
        }

        public static void Stop()
        {
            WindowManager.Stop(window);
        }
    }
}
