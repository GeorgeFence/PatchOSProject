using Cosmos.System;
using PatchOS.Files.Drivers;
using PatchOS.Files.Drivers.GUI;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using PatchOS.Files.Drivers.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WindowManager = PatchOS.Files.Drivers.GUI.UI.WindowManager;

namespace PatchOS.Files.Apps
{
    public partial class PatchBrowser
    {
        public static Button btn = null!;
        public static Edittext url = null!;
        public static Label lbl = null!;
        public static WebPanel webPanel = null!;
        public static Drivers.GUI.UI.Window window;

        public static int I = 0;
         
        public static string Title()
        {
            return "Patch Browser";
        }

        public static void Update()
        {
            if(btn.IsClicked)
            {
                lbl.Text = NetworkMgr.DownloadFile(url.Text);
            }
        }

        public static void Start()
        {
            window = new Drivers.GUI.UI.Window(100, 100, 640, 400, "Patch Browser", Update, DesignType.Default, PermitionsType.User, Kernel.apk);
            url = new Edittext(0, 0, window.PanelW - 60, 20, AnachorType.Centre);
            btn = new Button(window.PanelW - 60, 0, 60, 20, 0, "BROWSE", true, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.BlueViolet, Drivers.AnachorType.Left);
            lbl = new Label(0,20,"", AnachorType.Centre);
            //webPanel = new WebPanel(0,20, 640,380,"Test", AnachorType.Bottom);
            window.Controls.Add(btn);
            window.Controls.Add(url);
            window.Controls.Add(lbl);
            WindowManager.Add(window);
        }

        public static void Stop()
        {
            WindowManager.Stop(window);
        }
    }
}
