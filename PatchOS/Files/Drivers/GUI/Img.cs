using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace PatchOS.Files.Drivers.GUI
{
    public static class Img
    {
        public static Bitmap CursorNone;

        public static Bitmap wpp1;
        public static Bitmap wpp2;

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.userapp.bmp")] static byte[] rawImageUserApp;
        public static Bitmap userApp = new Bitmap(225,32,rawImageUserApp, ColorDepth.ColorDepth32);

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.serviceapp.bmp")] static byte[] rawImageServiceApp;
        public static Bitmap serviceApp = new Bitmap(255,32,rawImageServiceApp,ColorDepth.ColorDepth32);

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.systemapp.bmp")] static byte[] rawImageSystemApp;
        public static Bitmap systemApp = new Bitmap(255,32,rawImageSystemApp, ColorDepth.ColorDepth32);

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.exit.bmp")] static byte[] rawImage4;
        public static Bitmap exitApp = new Bitmap(22,22,rawImage4,ColorDepth.ColorDepth32);

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.START.bmp")] static byte[] rawImageSTART;
        public static Bitmap start = new Bitmap(64,32,rawImageSTART,ColorDepth.ColorDepth32);



        //Files
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.WIN.bmp")] static byte[] rawImage5;
        public static Bitmap WIN = new Bitmap(64,64,rawImage5,ColorDepth.ColorDepth32);
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.APP.bmp")] static byte[] rawImage6;
        public static Bitmap APP = new Bitmap(64, 64, rawImage6, ColorDepth.ColorDepth32);
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.REG.bmp")] static byte[] rawImage7;
        public static Bitmap REG = new Bitmap(64, 64, rawImage7, ColorDepth.ColorDepth32);
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.NONE.bmp")] static byte[] rawImage8;
        public static Bitmap NONE = new Bitmap(64, 64, rawImage8, ColorDepth.ColorDepth32);
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.TXT.bmp")] static byte[] rawImage9;
        public static Bitmap TXT = new Bitmap(64, 64, rawImage9, ColorDepth.ColorDepth32);
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.apkApp.bmp")] static byte[] rawImage10;
        public static Bitmap apk = new Bitmap(26, 26, rawImage10, ColorDepth.ColorDepth32);

        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.wifi.connected.bmp")] static byte[] rawImage11;
        public static Bitmap wifiConnected = new Bitmap(16, 16, rawImage11, ColorDepth.ColorDepth32);
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.wifi.notconnected.bmp")] static byte[] rawImage12;
        public static Bitmap wifiNotConnected = new Bitmap(16, 16, rawImage12, ColorDepth.ColorDepth32);
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.wifi.nointernet.bmp")] static byte[] rawImage13;
        public static Bitmap wifiNotInternet = new Bitmap(16, 16, rawImage13, ColorDepth.ColorDepth32);

        public static void Init()
        {
            CursorNone = new Bitmap(12, 19, PMFAT.ReadBytes(@"0:\Files/cursor"), ColorDepth.ColorDepth32);
            //wpp1 = new Bitmap(1920, 1080, PMFAT.ReadBytes(PMFAT.Root + "Files/wpp1"), ColorDepth.ColorDepth32);
            //wpp2 = new Bitmap(1920, 1080, PMFAT.ReadBytes(PMFAT.Root + "Files/wpp2"), ColorDepth.ColorDepth32);

            

            Kernel.DrawBootOut("Initialized Imgs");
        }
    }
}

