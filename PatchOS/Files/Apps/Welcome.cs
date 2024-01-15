using Cosmos.System;
using PatchOS.Files.Drivers.GUI;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowManager = PatchOS.Files.Drivers.GUI.UI.WindowManager;

namespace PatchOS.Files.Apps
{
    public partial class Welcome
    {
        public static Label WelcomeLabel = null!;
        public static Label WelcomeLabel2 = null!;
        public static Button WelcomeButton = null!;
        public static Drivers.GUI.UI.Window window;

        public static int I = 0;
         
        public static string Title()
        {
            return "Welcome";
        }

        public static void Update()
        {
            if(WelcomeButton.IsClicked)
            {
                Stop();
            }
        }

        public static void Start()
        {
            window = new Drivers.GUI.UI.Window(100, 100, 320, 100, "Welcome", Update, DesignType.Modern, PermitionsType.User, Kernel.apk);
            WelcomeLabel = new Label(3 , 3, "Welcome back!", Drivers.AnachorType.Left);
            WelcomeLabel2 = new Label(3, 19, "I hope you like it there :)", Drivers.AnachorType.Left);
            WelcomeButton = new Button(window.PanelW - 42, window.PanelH - 26, 32, 16, 0, "OK", true, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Red, Drivers.AnachorType.Left);
            window.Controls.Add(WelcomeLabel);
            window.Controls.Add(WelcomeLabel2);
            window.Controls.Add(WelcomeButton);
            WindowManager.Add(window);
        }

        public static void Stop()
        {
            WindowManager.Stop(window);
        }
    }
}
