using Cosmos.System;
using PatchOS.Files.Drivers;
using PatchOS.Files.Drivers.GUI;
using PatchOS.Files.Drivers.GUI.UI;
using PatchOS.Files.Drivers.GUI.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WindowManager = PatchOS.Files.Drivers.GUI.UI.WindowManager;

namespace PatchOS.Files.Apps
{
    public partial class DeveloperStudio
    {
        public static Label title = null!;
        public static Button New = null!;
        public static Button Open = null!;
        public static Drivers.GUI.UI.Window Main;

        public static Panel PanelCode = null!;
        public static Panel PanelToolBox = null!;
        public static Panel PanelOutput = null!;
        public static Edittext code = null!;
        public static Drivers.GUI.UI.Window Code;


        public static int I = 0;
         
        public static string Title()
        {
            return "Developer Studio";
        }

        public static void UpdateMain()
        {
            if (New.IsClicked)
            {
                WindowManager.Stop(Main);
                WindowManager.Add(Code);
            }
        }
        public static void UpdateCode()
        {
            
        }

        public static void Start()
        {
            Main = new Drivers.GUI.UI.Window(400, 400, 480, 200, "Develop Studio", UpdateMain, DesignType.Default, PermitionsType.User, Kernel.apk);
            New = new Button(Main.PanelW - 101, Main.PanelH - 58, 96, 24, 0, "New", true, System.Drawing.Color.AliceBlue, System.Drawing.Color.Purple, System.Drawing.Color.Black, AnachorType.Right);
            Open = new Button(Main.PanelW - 101, Main.PanelH - 29, 96, 24, 0, "Open", true, System.Drawing.Color.AliceBlue, System.Drawing.Color.Purple, System.Drawing.Color.Black, AnachorType.Right);
            title = new Label(5 , 5, "Develop Studio", Drivers.AnachorType.Left);

            Code = new Window(300, 300, 720, 480, "Develop Studio", UpdateCode, DesignType.Default, PermitionsType.User, Kernel.apk);
            PanelCode = new Panel(120,0,480,Code.PanelH - 120, System.Drawing.Color.LightGray, AnachorType.Centre);
            PanelOutput = new Panel(120, Code.PanelH - 120, 480, 120, System.Drawing.Color.Aqua, AnachorType.Bottom);
            PanelToolBox = new Panel(0,0,120,Code.PanelH,System.Drawing.Color.HotPink,AnachorType.Left);
            code = new Edittext(120,0,480,Code.PanelH - 120, AnachorType.Centre);

            Main.Controls.Add(title);
            Main.Controls.Add(Open);
            Main.Controls.Add(New);
             
            Code.Controls.Add(PanelCode);
            Code.Controls.Add(PanelOutput);
            Code.Controls.Add(PanelToolBox);
            Code.Controls.Add(code);

            WindowManager.Add(Main);
        }

        public static void Stop()
        {
            WindowManager.Stop(Main);
            WindowManager.Stop(Code);
        }
    }
}
