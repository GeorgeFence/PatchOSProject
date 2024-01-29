using Cosmos.System;
using Cosmos.System.Graphics;
using PatchOS.Commands;
using PatchOS.Files.Drivers.GUI;
using PatchOS.Process;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = System.Console;
using Sys = Cosmos.System;


namespace PatchOS.Files
{
    public class BootMgr
    {
        public int old_X = 100;
        public int old_Y = 60;
        public void InitBoot()
        {
            //Canvas = (Canvas)FullScreenCanvas.GetFullScreenCanvas(new Mode(1280, 720, ColorDepth.ColorDepth32));

        }
        public void DrawBoot()
        {
            //canvas.Clear(Color.Black);
            //;
        }
        public void AfterStart()
        {
           CheckAndDoRegistry();
        }
        public void CheckAndDoRegistry()
        {
            try
            {
                Kernel.DrawBootOut("Checking Boot Registers...");
                SYS32.ErrorStatusAdd("2");

                if (PMFAT.FileExists(PMFAT.Root + "REG/BOOT/boot.reg"))
                {
                    if (PMFAT.ReadText(PMFAT.Root + "REG/BOOT/boot.reg") == "0")
                    {
                        Kernel.DrawBootOut("Booting to 0");
                        Kernel.DelayCode(500);
                        BootMgrStart();
                    }
                    else if (PMFAT.ReadText(PMFAT.Root + "REG/BOOT/boot.reg") == "1")
                    {
                        Kernel.DrawBootOut("Booting to 1");
                        Kernel.DelayCode(500);
                        SYS32.ErrorStatusAdd("6");
                        ProcessManager.Run(new Shell());
                    }
                    else if (PMFAT.ReadText(PMFAT.Root + "REG/BOOT/boot.reg") == "2")
                    {
                        Kernel.DrawBootOut("Booting to 2");
                        Kernel.DelayCode(500);
                        ProcessManager.Run(new Desktop());
                    }
                }
                else
                {
                    Kernel.DrawBootOut("No Boot Registry Found");
                    Kernel.DelayCode(500);
                    RegMgr.ResetReg();
                }
            }
            catch (Exception ex)
            {
                SYS32.KernelPanic(ex, "");
            }
        }
        public void BootMgrStart()
        {

            try
            {
                

                Console.BackgroundColor = ConsoleColor.Black;
                Runner.Initialize();
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.White;
                CLI.WriteLine("  PatchOS Boot Manager                                                          ");
                Console.BackgroundColor = ConsoleColor.Blue;
                CLI.WriteLine("  Select boot option                                                            " +
                              "    1; Desktop                                                                  " +
                              "    2; Shell                                                                    " +
                              "    3; Reboot                                                                   " +
                              "    4; Shutdown                                                                 " +
                              "                                                                                " +
                              "                                                                                " +
                              "                                                                                " +
                              "                                                                                " +
                              "                                                                                ");
                Console.BackgroundColor = ConsoleColor.White;
                CLI.WriteLine("  Write number of boot option to boot up                                        ");
                Console.BackgroundColor = ConsoleColor.Black;
                string inp = CLI.ReadLine();
                if (inp == "1")
                {
                    ProcessManager.Run(new MouseMgr());
                }
                else if (inp == "2")
                {
                    Console.Clear();
                    ProcessManager.Run(new Shell());
                }
                else if (inp == "3")
                {
                    Sys.Power.Reboot();
                }
                else if (inp == "4")
                {
                    Sys.Power.Shutdown();
                }

            }
            catch(Exception ex)
            {
                
                SYS32.KernelPanic(ex, "error while running BootMgr");
                
            }


        }
    }
}
