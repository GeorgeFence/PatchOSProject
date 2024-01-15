using PatchOS.Files.Drivers.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers.Network
{
    public static class Wifi
    {
        public static string WIFI_STATUS = "N|N";

        public static void INIT()
        {
            //N(not connected) / Y(connected) | N(no internet) / Y(internet)
            WIFI_STATUS = "N|N";
        }
        public static void DrawWifiStatus(int X, int Y)
        {
            string[] status = WIFI_STATUS.Split('|');
            if (status[0] == "Y")
            {
                if (status[1] == "Y")
                {
                    Kernel.Canvas.DrawImageAlpha(Img.wifiConnected, X, Y);
                }
                else
                {
                    Kernel.Canvas.DrawImageAlpha(Img.wifiNotInternet, X, Y);
                }
            }
            else
            {
                Kernel.Canvas.DrawImageAlpha(Img.wifiNotConnected, X, Y);
            }
        }
    }
}
