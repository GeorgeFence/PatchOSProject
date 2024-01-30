using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using PatchOS.Files;
using PatchOS.Files.Drivers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Sys = Cosmos.System;

namespace PatchOS.Commands
{
    class Install : Command
    {
        [ManifestResourceStream(ResourceName = "PatchOS.BootAnimation.boot_img.bmp")] public static byte[] rawBoot;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.cursor.bmp")] public static byte[] rawCursor;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.START.bmp")] public static byte[] rawStart;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.wallpaper1_FHD.bmp")] public static byte[] rawWpp1fhd;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.wallpaper2_FHD.bmp")] public static byte[] rawWpp2fhd;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.wallpaper1_HD.bmp")] public static byte[] rawWpp1hd;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.wallpaper2_HD.bmp")] public static byte[] rawWpp2hd;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.apkApp.bmp")] public static byte[] rawApk;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.userapp.bmp")] public static byte[] rawImageUserApp;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.serviceapp.bmp")] public static byte[] rawImageServiceApp;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.systemapp.bmp")] public static byte[] rawImageSystemApp;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.exit.bmp")] public static byte[] rawImageExitApp;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.wifi.connected.bmp")] public static byte[] rawIWifiConn;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.wifi.notconnected.bmp")] public static byte[] rawWifiNotConn;
        [ManifestResourceStream(ResourceName = "PatchOS.Files.Drivers.GUI.fileIcons.wifi.nointernet.bmp")] public static byte[] rawWifiNoInte;

        public Install()
        {
            this.Name = "install";
            this.Help = "installs PatchOS onto this PC";
        }

        public override void Execute(string line, String[] args)
        {
            InstallFiles();
        }
        
		public static void InstallFiles()
        {
            try
            {
                Kernel.Canvas.Disable();
                Installer.Init();
                Installer.Install();


                /*Kernel.Resolution(640, 480);
                Kernel.Canvas.Clear(System.Drawing.Color.Black);
                GUIConsole.Init();
                GUIConsole.SetCursorPositionChar(0, 1);
                GUIConsole.WriteLine("PatchOS 1", System.Drawing.Color.Red);
                GUIConsole.WriteLine("________________________________________________________________________________", System.Drawing.Color.Red);
                GUIConsole.Write("Completing Instalation of"); GUIConsole.WriteLine(" PatchOS", System.Drawing.Color.Purple);
                Kernel.DelayCode(2000);

                GUIConsole.WriteLine("Preparing Regs", System.Drawing.Color.Green);
                RegMgr.ResetReg();
                GUIConsole.WriteLine("Preparing Files", System.Drawing.Color.Green);

                PMFAT.CreateFolder(PMFAT.Root + "Files");

                GUIConsole.WriteLine("boot", System.Drawing.Color.Purple);
                PMFAT.CreateFile(PMFAT.Root +    "Files/boot");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/boot", rawBoot);
                GUIConsole.WriteLine("cursor", System.Drawing.Color.Purple);
                PMFAT.CreateFile(PMFAT.Root +    "Files/cursor");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/cursor", rawCursor);
                GUIConsole.WriteLine("start", System.Drawing.Color.Purple);
                PMFAT.CreateFile(PMFAT.Root +    "Files/start");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/start", rawStart);

                GUIConsole.WriteLine("Wallpapers", System.Drawing.Color.Purple);
                PMFAT.CreateFile(PMFAT.Root +    "Files/wpp1fhd");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp1fhd", rawWpp1fhd);
                PMFAT.CreateFile(PMFAT.Root +    "Files/wpp2fhd");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp2fhd", rawWpp2fhd);
                PMFAT.CreateFile(PMFAT.Root +    "Files/wpp1hd");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp1hd", rawWpp1hd);
                PMFAT.CreateFile(PMFAT.Root +    "Files/wpp2hd");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp2hd", rawWpp2hd);

                GUIConsole.WriteLine("Icons", System.Drawing.Color.Purple);
                PMFAT.CreateFile(PMFAT.Root +    "Files/apk");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/apk", rawApk);
                PMFAT.CreateFile(PMFAT.Root + "Files/wifinc");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/wifinc", rawWifiNotConn);
                PMFAT.CreateFile(PMFAT.Root + "Files/wific");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/wific", rawIWifiConn); 
                PMFAT.CreateFile(PMFAT.Root + "Files/wifini");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/wifini", rawWifiNoInte);

                GUIConsole.WriteLine("Windows", System.Drawing.Color.Purple);
                PMFAT.CreateFile(PMFAT.Root +    "Files/user");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/user", rawImageUserApp);

                PMFAT.CreateFile(PMFAT.Root +    "Files/system");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/system", rawImageSystemApp);

                PMFAT.CreateFile(PMFAT.Root +    "Files/service");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/service", rawImageServiceApp);

                PMFAT.CreateFile(PMFAT.Root +    "Files/exit");
                PMFAT.WriteAllBytes(PMFAT.Root + "Files/exit", rawImageExitApp);*/

            }
            catch (Exception e) { SYS32.KernelPanic(e,"Install"); }
            
        }
    }
}
