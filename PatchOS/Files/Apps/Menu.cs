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
using System.Net;
using Cosmos.System.Graphics;
using Image = PatchOS.Files.Drivers.GUI.UI.Controls.Image;

namespace PatchOS.Files.Apps
{
    public partial class Menu
    {
        public static Button ShutdownDialog = null!;
        public static Button Console = null!;
        public static Button Settings = null!;
        public static Panel downPanel = null!;
        public static Panel fpsBgPanel = null!;
        public static Panel fpsFgPanel = null!;
        public static Label fps = null!;

        public static Image Welcome = null!;
        public static Image PatchBrowser = null!;
        public static Image DevelopStudio = null!;
        public static Drivers.GUI.UI.Window Mwindow;

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.MenuApp.bmp")] static byte[] rawImageIcon;
        public static Bitmap Icon = new Bitmap(rawImageIcon);

        public static int I = 0;

        public static string Title()
        {
            return "Menu";
        }

        public static void Update()
        {
            fps.Text = Desktop.FPS.ToString();
            fpsFgPanel.Widt = Desktop.FPS * 2;
            if(Welcome.IsClicked)
            {
                PatchOS.Files.Apps.Welcome.Start();
            }
            if (PatchBrowser.IsClicked)
            {
                PatchOS.Files.Apps.PatchBrowser.Start();
            }
            if (DevelopStudio.IsClicked)
            {
                PatchOS.Files.Apps.DeveloperStudio.Start();
            }
            if (ShutdownDialog.IsClicked)
            {
                PatchOS.Files.Apps.ShutdownDialog.Start();
            }
            if (Settings.IsClicked)
            {
                PatchOS.Files.Apps.PanelSettings.Start();
            }
            if (Console.IsClicked)
            {
                for (int i = 0;i < WindowManager.Windows.Count;i++)
                {
                    WindowManager.Stop(WindowManager.Windows[i]);
                }
                ProcessManager.StopAll();
                ProcessManager.Run(new Shell()); 
            }
        }

        public static void Start()
        {
            Mwindow = new Drivers.GUI.UI.Window(15, (int)(Kernel.Canvas.Mode.Height - 658), 400, 600, "Menu", Update, DesignType.Blank, PermitionsType.User, Icon);
            Mwindow.CanMove = false;
            ShutdownDialog = new Button(5, Mwindow.PanelH - 29, 32, 24, 0, "S/R", true, System.Drawing.Color.Blue, System.Drawing.Color.White, System.Drawing.Color.DarkBlue, Drivers.AnachorType.Left);
            Console = new Button(42, Mwindow.PanelH - 29, 64, 24, 0, "Console", true, System.Drawing.Color.Red, System.Drawing.Color.White, System.Drawing.Color.DarkBlue, Drivers.AnachorType.Left);
            Settings = new Button(Mwindow.PanelW - 101, Mwindow.PanelH - 29, 96, 24, 0, "Settings", true, System.Drawing.Color.DimGray, System.Drawing.Color.White, System.Drawing.Color.DarkBlue, Drivers.AnachorType.Left);
            downPanel = new Panel(0, 600 - 34, 400, 34,System.Drawing.Color.Gray, Drivers.AnachorType.Bottom);
            fpsBgPanel = new Panel(0, 0, 400, 16, System.Drawing.Color.Red, Drivers.AnachorType.Left);
            fpsFgPanel = new Panel(0,0,200,16,System.Drawing.Color.Green, Drivers.AnachorType.Left);
            fps = new Label((200 - (Desktop.FPS.ToString().Length / 2)), 0,"FPS", Drivers.AnachorType.Centre);
            Welcome = new Image(0,16, Kernel.apk, true, Drivers.AnachorType.Left, "Welcome");
            PatchBrowser = new Image(32, 16, Kernel.apk, true, Drivers.AnachorType.Left, "Patch Browser");
            DevelopStudio = new Image(64, 16, Kernel.apk, true, Drivers.AnachorType.Left, "Develop Studio");
            Mwindow.Controls.Add(downPanel);
            Mwindow.Controls.Add(ShutdownDialog);
            Mwindow.Controls.Add(Console);
            Mwindow.Controls.Add(Settings);
            Mwindow.Controls.Add(fpsBgPanel);
            Mwindow.Controls.Add(fpsFgPanel);
            Mwindow.Controls.Add(fps);
            Mwindow.Controls.Add(Welcome);
            Mwindow.Controls.Add(PatchBrowser);
            Mwindow.Controls.Add(DevelopStudio);
            WindowManager.Add(Mwindow);
        }

        public static void Stop()
        {
            WindowManager.Stop(Mwindow);
        }
    }
}
