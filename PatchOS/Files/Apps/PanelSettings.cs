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

namespace PatchOS.Files.Apps
{
    public partial class PanelSettings
    {
        public static Drivers.GUI.UI.Window window;

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.settings.bmp")] static byte[] rawImage14;
        

        public static int I = 0;

        public static string Title()
        {
            return "Settings";
        }

        public static void Update()
        {
            
        }

        public static void Start()
        {
            window = new Drivers.GUI.UI.Window((int)(Kernel.Canvas.Mode.Width - 300), 20, 300, (ushort)(Kernel.Canvas.Mode.Height - 68), "Settings", Update, DesignType.Blank, PermitionsType.Service, Kernel.apk);
            window.CanMove = false;
            WindowManager.Add(window);
        }

        public static void Stop()
        {
            WindowManager.Stop(window);
        }
    }
}
