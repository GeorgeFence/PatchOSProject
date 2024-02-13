using Cosmos.HAL;
using PatchOS.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using Console = System.Console;

namespace PatchOS.Files.Drivers
{
    public static class Installer
    {

        private static string DrawAskOut;

        public static void Init()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Blue;
            System.Console.Clear();
        }

        public static void Install()
        {
            try
            {
                System.Console.Clear();
                DrawTitle();

                DrawInstallSel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public static void DrawTitle()
        {
            System.Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                PatchOSInstaller                                ", Color.Red);
            System.Console.BackgroundColor = ConsoleColor.Blue;
        }

        public static void DrawUpdates(string str)
        {
            System.Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < (80 - str.Length); i++)
            {
                Console.Write(" ");
            }
            Console.Write(str);
            System.Console.BackgroundColor = ConsoleColor.Blue;
        }

        public static void DrawTable(int height, string title)
        {
            Console.SetCursorPosition(2, 6);
            Console.Write(title);
            Console.SetCursorPosition(2, 7);
            int y = 8;
            Console.WriteLine("############################################################################");
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(2, y);
                Console.WriteLine("#                                                                          #");
                y++;
            }
            Console.SetCursorPosition(2, y);
            Console.WriteLine("############################################################################");

        }
        public static string DrawAskTable(int height, string title)
        {
            bool repeat = true;
            DrawAskOut = "";
            string Out = "";
            Console.SetCursorPosition(2, 6);
            Console.Write(title);
            Console.SetCursorPosition(2, 7);
            int y = 8;
            Console.WriteLine("############################################################################");
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(2, y);
                Console.WriteLine("#                                                                          #");
                y++;
            }
            Console.SetCursorPosition(2, y);
            Console.WriteLine("############################################################################");
            Console.WriteLine("                                                                [ENTER] Next");
            Console.SetCursorPosition(3, 8);
            Console.Write("> ");
            return Console.ReadLine();
        }

        public static void DrawInstallSel()
        {
            bool once = true;
            int selection = 0;
            List<String> sel = new List<String>();
            sel.Add("Continue to PatchOS Installer Stage 2");
            sel.Add("Cancel and corrupt installation");
            DrawUpdates("Tip: Use arrows to select");
            DrawTable(2, "");
            while (true)
            {
                if (once)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (once)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(sel[i]);
                            Console.BackgroundColor = ConsoleColor.Blue;
                            once = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write("                                                                        ");
                            Console.SetCursorPosition(4, 8 + i);
                            Console.Write(sel[i]);

                        }
                    }
                    once = false;
                }

                var keyDetection = Console.ReadKey();

                for (int i = 0; i < 2; i++)
                {
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write("                                                                        ");
                    Console.SetCursorPosition(4, 8 + i);
                    Console.Write(sel[i]);
                }

                if (keyDetection.Key == ConsoleKey.UpArrow)
                {
                    if (selection == 0)
                    {
                        selection = (2 - 1);
                    }
                    else
                    {
                        selection--;
                    }

                }
                else if (keyDetection.Key == ConsoleKey.DownArrow)
                {
                    if (selection == (2 - 1))
                    {
                        selection = 0;
                    }
                    else
                    {
                        selection++;
                    }
                }
                else if (keyDetection.Key == ConsoleKey.Enter)
                {
                    if (selection == 0)
                    {
                        DrawInstalling();
                    }
                    if (selection == 1)
                    {
                        Console.Clear();
                        DrawTitle();
                        DrawUpdates("Bye, your Installation is ruined....  Im kidding");
                        Kernel.DelayCode(2000);
                        Kernel.Shutdown(0);
                    }
                    break;
                }

                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write("                                                                        ");
                Console.SetCursorPosition(4, 8 + selection);
                Console.Write(sel[selection], Color.Blue);
                Console.BackgroundColor = ConsoleColor.Blue;
            }
        }

        public static void DrawInstalling()
        {
            Console.Clear();
            DrawTitle();
            DrawUpdates("Wait for the installer to install PatchOS");
            DrawUpdates("Copying");
            
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("PatchOS 1", System.Drawing.Color.Red);
            Console.WriteLine("________________________________________________________________________________", System.Drawing.Color.Red);
            Console.Write("Completing Instalation of"); Console.WriteLine(" PatchOS", System.Drawing.Color.Purple);
            Kernel.DelayCode(2000);

            Log.Warning("Preparing REGs");
            RegMgr.ResetReg();
            Log.Warning("Preparing Files");

            PMFAT.CreateFolder(PMFAT.Root + "Files");

            int start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;

            PMFAT.CreateFile(PMFAT.Root + "Files/boot");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/boot", Commands.Install.rawBoot);
            Log.Success("Done Boot");

            PMFAT.CreateFile(PMFAT.Root + "Files/cursor");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/cursor", Commands.Install.rawCursor);
            Log.Success("Done Cursor");

            PMFAT.CreateFile(PMFAT.Root + "Files/start");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/start", Commands.Install.rawStart);
            Log.Success("Done Start");

            PMFAT.CreateFile(PMFAT.Root + "Files/wpp1fhd");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp1fhd", Commands.Install.rawWpp1fhd);
            PMFAT.CreateFile(PMFAT.Root + "Files/wpp2fhd");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp2fhd", Commands.Install.rawWpp2fhd);
            PMFAT.CreateFile(PMFAT.Root + "Files/wpp1hd");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp1hd", Commands.Install.rawWpp1hd);
            PMFAT.CreateFile(PMFAT.Root + "Files/wpp2hd");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/wpp2hd", Commands.Install.rawWpp2hd);
            Log.Success("Done Wallpapers");

            PMFAT.CreateFile(PMFAT.Root + "Files/apk");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/apk", Commands.Install.rawApk);
            PMFAT.CreateFile(PMFAT.Root + "Files/wifinc");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/wifinc", Commands.Install.rawWifiNotConn);
            PMFAT.CreateFile(PMFAT.Root + "Files/wific");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/wific", Commands.Install.rawIWifiConn);
            PMFAT.CreateFile(PMFAT.Root + "Files/wifini");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/wifini", Commands.Install.rawWifiNoInte);

            Log.Success("Done Icons");
            PMFAT.CreateFile(PMFAT.Root + "Files/user");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/user", Commands.Install.rawImageUserApp);

            PMFAT.CreateFile(PMFAT.Root + "Files/system");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/system", Commands.Install.rawImageSystemApp);

            PMFAT.CreateFile(PMFAT.Root + "Files/service");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/service", Commands.Install.rawImageServiceApp);

            PMFAT.CreateFile(PMFAT.Root + "Files/exit");
            PMFAT.WriteAllBytes(PMFAT.Root + "Files/exit", Commands.Install.rawImageExitApp);
            Log.Success("Done Windows");

            int end = (Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second);

            Log.Success("Done in " + (end - start) + " seconds");
            DrawUpdates("Done in " + (end - start) + " seconds");
            Kernel.DelayCode(500);
            DrawTable(1, "Shutting your PC down, make sure that you didnt interrupt Installer");
            Kernel.DelayCode(500);
            Kernel.Shutdown(1);
        }
    }
}
