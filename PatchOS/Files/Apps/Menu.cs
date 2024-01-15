using IL2CPU.API.Attribs;
using PatchOS.Files.Drivers.GUI;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using PatchOS.Process;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Apps
{
    public partial class Menu
    {
        public static Button ShutdownDialog = null!;
        public static Button Console = null!;
        public static Panel downPanel = null!;
        public static Drivers.GUI.UI.Window Mwindow;

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.MenuApp.bmp")] static byte[] rawImageIcon;
        //public static Bitmap Icon = Image.FromBitmap(rawImageIcon, false);

        public static int I = 0;

        public static string Title()
        {
            return "Menu";
        }

        public static void Update()
        {
            if (ShutdownDialog.IsClicked)
            {
                PatchOS.Files.Apps.ShutdownDialog.Start();
            }
            if (Console.IsClicked)
            {
                for (int i = 0;i < WindowManager.Windows.Count;i++)
                {
                    ProcessManager.Remove(WindowManager.Windows[i].Title + ".win");
                }
                ProcessManager.Remove("Desktop");
                ProcessManager.Run(new Shell()); 
            }
        }

        public static void Start()
        {
            Mwindow = new Drivers.GUI.UI.Window(5, 53, 400, 600, "Menu", Update, DesignType.Blank, PermitionsType.User, null);
            Mwindow.CanMove = false;
            ShutdownDialog = new Button(5, Mwindow.PanelH - 29, 32, 24, 0, "S/R", true, System.Drawing.Color.Blue, System.Drawing.Color.White, System.Drawing.Color.DarkBlue, Drivers.AnachorType.Left);
            Console = new Button(42, Mwindow.PanelH - 29, 64, 24, 0, "Console", true, System.Drawing.Color.Red, System.Drawing.Color.White, System.Drawing.Color.DarkBlue, Drivers.AnachorType.Left);
            downPanel = new Panel(0, Mwindow.PanelH - 34, Mwindow.PanelW, 34,System.Drawing.Color.LightGray, Drivers.AnachorType.Bottom);
            Mwindow.Controls.Add(downPanel);
            Mwindow.Controls.Add(ShutdownDialog);
            Mwindow.Controls.Add(Console);
            WindowManager.Add(Mwindow);
        }

        public static void Stop()
        {
            WindowManager.Stop(Mwindow);
        }
    }
}
