using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Cosmos.HAL;
using PatchOS.Commands;
using PatchOS.Files;
using PatchOS.Files.Drivers;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using PatchOS.Process;
using Cosmos.Core;
using System.Resources;
using Cosmos.System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Console = System.Console;
using PatchOS.Files.Coroutines;
using ColorP = System.Drawing.Color;
using PatchOS.Files.Drivers.Network;
using PatchOS.Files.Drivers.GUI;

namespace PatchOS.Files
{
    internal class Shell : Process.Process
    {
        public static Coroutine process = new Coroutine(shell());
        public static List<Command> CommandsList = new List<Command>();
        public static bool Yield = false;
        public static bool once = true;
        private static bool IsGUI = Kernel.GUI_MODE;
        public Shell() : base("Shell", Type.Service, process)
        {
            
        }
        static IEnumerator<CoroutineControlPoint> shell()
        {
            ColorFile TitleBarColor = ColorFile.Black;
            ColorFile DateTimeColor = ColorFile.Black;
            ColorFile TitleColor = ColorFile.Black;
            System.Drawing.Color TitleBarColorP = System.Drawing.Color.Black;
            System.Drawing.Color DateTimeColorP = System.Drawing.Color.Black;
            System.Drawing.Color TitleColorP = System.Drawing.Color.Black;
            bool TitleBarVisible = true;
            string TitleBarText = "PatchOS Shell";
            string TitleBarTime = RTC.GetDateFormatted() + " " + RTC.GetTimeFormatted();
            void Initialize()
            {
                try
                {
                    GUIConsole.BG = System.Drawing.Color.Black;
                    GUIConsole.FG = System.Drawing.Color.White;
                    Kernel.Resolution(ushort.Parse(RegMgr.GetValue("REG/SHELL/W.reg")), ushort.Parse(RegMgr.GetValue("REG/SHELL/H.reg")));
                    GUIConsole.Init();
                    GUIConsole.Clear();
                    GUIConsole.SetCursorPositionChar(0, 1);
                    TC();
                    TBC();
                    DTC();
                    // add commands
                    AddCommands();

                    // clear 
                    DrawFresh();

                    //draw logo
                    DrawLogo();

                    // show info
                    SystemInfo.ShowInfo();

                    if (IsGUI)
                    {
                        GUIConsole.WriteLine("Your IP: " + NetworkMgr.GetIP());
                    }
                    else
                    {
                        CLI.WriteLine("Your IP: " + NetworkMgr.GetIP());
                    }


                    if (Directory.GetCurrentDirectory() == null)
                    {
                        if (IsGUI)
                        {
                            GUIConsole.WriteLine("Could not locate DISK");
                        }
                        else
                        {
                            Console.WriteLine("Could not locate DISK");
                        }
                    }
                    else
                    {
                        if (IsGUI)
                        {
                            GUIConsole.WriteLine(Directory.GetCurrentDirectory());
                        }
                        else
                        {
                            Console.WriteLine(Directory.GetCurrentDirectory());
                        }
                    }
                }
                catch (Exception ex)
                {
                    SYS32.KernelPanic(ex, "Shell");
                }
            }
            void AddCommands()
            {

                try
                {
                    
                    CommandsList.Add(new Commands.Dir());
                    CommandsList.Add(new Commands.Help());
                    CommandsList.Add(new Commands.Info());
                    CommandsList.Add(new Commands.Reboot());
                    CommandsList.Add(new Commands.Shutdown());
                    CommandsList.Add(new Commands.StartGUI());
                    CommandsList.Add(new Commands.PixelFix());
                    CommandsList.Add(new Commands.Game());
                    CommandsList.Add(new Commands.Edit());

                    CommandsList.Add(new Commands.FileSystem.cd());
                    CommandsList.Add(new Commands.FileSystem.MakeDir());
                    CommandsList.Add(new Commands.FileSystem.CopyFile());
                    CommandsList.Add(new Commands.FileSystem.DelFile());
                    CommandsList.Add(new Commands.FileSystem.DelDir());

                    CommandsList.Add(new Commands.Format());
                    CommandsList.Add(new Commands.Reg());
                    CommandsList.Add(new Commands.USLCommand());
                    CommandsList.Add(new Commands.Factory());
                    CommandsList.Add(new Commands.debug());
                    CommandsList.Add(new Commands.Process());
                    CommandsList.Add(new Commands.Net());
                    CommandsList.Add(new Commands.Update());
                    CommandsList.Add(new Commands.Get());
                    CommandsList.Add(new Commands.Install());
                    SYS32.ErrorStatusAdd("ADD");
                }
                catch (Exception ex)
                {
                    SYS32.KernelPanic(ex, "error while adding commands");
                }
            }
            void DrawFresh()
            {
                try
                {
                    if (IsGUI)
                    {
                        DrawTitleBar();
                    }
                    else
                    {
                        // clear screen
                        TextGraphics.Clear(CLI.BackColor);



                        // draw title bar
                        if (TitleBarVisible) { DrawTitleBar(); CLI.SetCursorPos(0, 2); }
                        else { CLI.SetCursorPos(0, 0); }
                    }
                }
                catch (Exception ex)
                {
                    SYS32.KernelPanic(ex, "error while DrawFresh");
                }

            }
            void DrawTitleBar()
            {
                if (IsGUI)
                {
                    Kernel.Canvas.DrawFilledRectangle(TitleBarColorP, 0, 0, (int)Kernel.Canvas.Mode.Width, 16);
                    int i = (GUIConsole.W * 8 - TitleBarTime.Length * 8);
                    DrawTime();
                    ASC16.DrawACSIIString(Kernel.Canvas,TitleBarText,TitleColorP,0,0);
                }
                else
                {
                    TextGraphics.DrawLineH(0, 0, CLI.Width, ' ', ColorFile.Black, TitleBarColor); // draw background
                    DrawTime(); // draw time
                    TextGraphics.DrawString(CLI.Width - TitleBarText.Length, 0, TitleBarText, TitleColor, TitleBarColor); // draw title
                }
            }
            void DrawTime()
            {
                int i = (GUIConsole.W - TitleBarTime.Length);
                if (IsGUI)
                {
                    ASC16.DrawACSIIString(Kernel.Canvas , TitleBarTime, DateTimeColorP, (uint)(Kernel.Canvas.Mode.Width - (TitleBarTime.Length * 8)), 0);
                }
                else
                {
                    TextGraphics.DrawString(i, 0, TitleBarTime, DateTimeColor, TitleBarColor);
                }
            }
            void GetInput()
            {
                if (IsGUI)
                {
                    // reset title
                    TitleBarText = "PatchOS Shell";

                    // draw input pointer
                    if (PMFAT.CurrentDirectory == PMFAT.Root) { GUIConsole.Write("root", System.Drawing.Color.Magenta); GUIConsole.Write(":- ", System.Drawing.Color.White); }
                    else
                    {
                        GUIConsole.Write("root", System.Drawing.Color.Magenta); GUIConsole.Write("@", System.Drawing.Color.White);
                        GUIConsole.Write(PMFAT.CurrentDirectory.Substring(3, PMFAT.CurrentDirectory.Length - 3), System.Drawing.Color.Yellow);
                        GUIConsole.Write(":- ", System.Drawing.Color.White);
                    }

                    SYS32.ErrorStatusAdd("Input");
                    // get input
                    string input = GUIConsole.ReadLine();
                    processInput(input);
                }
                else
                {
                    // reset title
                    TitleBarText = "PatchOS Console";

                    // draw input pointer
                    if (PMFAT.CurrentDirectory == @"0:\") { CLI.Write("root", ColorFile.Magenta); CLI.Write(":- ", ColorFile.White); }
                    else
                    {
                        CLI.Write("root", ColorFile.Magenta); CLI.Write("@", ColorFile.White);
                        CLI.Write(PMFAT.CurrentDirectory.Substring(3, PMFAT.CurrentDirectory.Length - 3), ColorFile.Yellow);
                        CLI.Write(":- ", ColorFile.White);
                    }

                    SYS32.ErrorStatusAdd("Input");
                    // get input
                    string input = CLI.ReadLine();
                    processInput(input);
                }
            }
            void processInput(string line)
            {
                try
                {
                    line += " ";
                    SYS32.ErrorStatusAdd("Shell 1");
                    // if a command has actually been enter
                    if (line.Length > 0)
                    {
                        SYS32.ErrorStatusAdd("Shell 2");
                        String[] args = line.Split(' ');
                        bool error = true;
                        for (int i = 0; i < CommandsList.Count; i++)
                        {
                            try
                            {
                                SYS32.ErrorStatusAdd("Shell 3");
                                // validate command
                                if (args[0].ToLower() == CommandsList[i].Name)
                                {
                                    SYS32.ErrorStatusAdd("4");
                                    // execute and finish
                                    SYS32.ErrorStatusAdd("EXECUTE");
                                    CommandsList[i].Execute(line, args);
                                    GUIConsole.WriteLine("");
                                    error = false;
                                    SYS32.ErrorStatusAdd("EXECUTEDSUCCESSFULLY");
                                }
                            }
                            catch (Exception ex)
                            {
                                SYS32.KernelPanic(ex, "Shell");
                            }
                        }
                        SYS32.ErrorStatusAdd("EXECUTEDSUCCESSFULLY 2");
                        // invalid command has been entered
                        if (error)
                        {
                            if (IsGUI)
                            {
                                GUIConsole.WriteLine("Invalid command or program!", System.Drawing.Color.Red);
                            }
                            else
                            {
                                CLI.WriteLine("Invalid command or program!", ColorFile.Red);
                            }
                            DrawTitleBar();
                        }
                        SYS32.ErrorStatusAdd("EXECUTEDSUCCESSFULLY 3");
                        if (!Yield)
                        {
                            DrawTitleBar();
                            GetInput();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SYS32.KernelPanic(ex, "Shell");
                }
            }
            Command GetCommand(string cmd)
            {
                for (int i = 0; i < CommandsList.Count; i++)
                {
                    if (CommandsList[i].Name == cmd.ToUpper()) { return CommandsList[i]; }
                }
                return null;
            }
            void DrawLogo()
            {
                if (IsGUI)
                {
                    GUIConsole.WriteLine("      :::::::::     ::: ::::::::::: ::::::::  :::    :::  ::::::::   :::::::: ");
                    GUIConsole.WriteLine("     :+:    :+:  :+: :+:   :+:    :+:    :+: :+:    :+: :+:    :+: :+:    :+: ");
                    GUIConsole.WriteLine("    +:+    +:+ +:+   +:+  +:+    +:+        +:+    +:+ +:+    +:+ +:+         ");
                    GUIConsole.WriteLine("   +#++:++#+ +#++:++#++: +#+    +#+        +#++:++#++ +#+    +:+ +#++:++#++   ");
                    GUIConsole.WriteLine("  +#+       +#+     +#+ +#+    +#+        +#+    +#+ +#+    +#+        +#+    ");
                    GUIConsole.WriteLine(" #+#       #+#     #+# #+#    #+#    #+# #+#    #+# #+#    #+# #+#    #+#     ");
                    GUIConsole.WriteLine("###       ###     ### ###     ########  ###    ###  ########   ########       ");
                    GUIConsole.WriteLine("                                          Better operating system. Better life");
                    GUIConsole.WriteLine(" ");
                }
                else
                {
                    CLI.WriteLine("      :::::::::     ::: ::::::::::: ::::::::  :::    :::  ::::::::   :::::::: ");
                    CLI.WriteLine("     :+:    :+:  :+: :+:   :+:    :+:    :+: :+:    :+: :+:    :+: :+:    :+: ");
                    CLI.WriteLine("    +:+    +:+ +:+   +:+  +:+    +:+        +:+    +:+ +:+    +:+ +:+         ");
                    CLI.WriteLine("   +#++:++#+ +#++:++#++: +#+    +#+        +#++:++#++ +#+    +:+ +#++:++#++   ");
                    CLI.WriteLine("  +#+       +#+     +#+ +#+    +#+        +#+    +#+ +#+    +#+        +#+    ");
                    CLI.WriteLine(" #+#       #+#     #+# #+#    #+#    #+# #+#    #+# #+#    #+# #+#    #+#     ");
                    CLI.WriteLine("###       ###     ### ###     ########  ###    ###  ########   ########       ");
                    CLI.WriteLine("                                          Better operating system. Better life");
                    CLI.WriteLine(" ");
                }
            }
            void TC()
            {
#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    if (IsGUI)
                    {
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "0")
                        {
                            TitleColorP = System.Drawing.Color.Black;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "1")
                        {
                            TitleColorP = System.Drawing.Color.White;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "2")
                        {
                            TitleColorP = System.Drawing.Color.Red;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "3")
                        {
                            TitleColorP = System.Drawing.Color.Green;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "4")
                        {
                            TitleColorP = System.Drawing.Color.Blue;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "5")
                        {
                            TitleColorP = System.Drawing.Color.Yellow;
                        }
                    }
                    else
                    {
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "0")
                        {
                            TitleColor = ColorFile.Black;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "1")
                        {
                            TitleColor = ColorFile.White;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "2")
                        {
                            TitleColor = ColorFile.Red;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "3")
                        {
                            TitleColor = ColorFile.Green;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "4")
                        {
                            TitleColor = ColorFile.Blue;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tc.reg") == "5")
                        {
                            TitleColor = ColorFile.Yellow;
                        }
                    }
                }
                catch (Exception ex)
                {
                    CLI.WriteLine("Incorrect registry");
                }
#pragma warning restore CS0168 // Variable is declared but never used
            }
            void DTC()
            {
#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    if (IsGUI)
                    {
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "0")
                        {
                            DateTimeColorP = ColorP.Black;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "1")
                        {
                            DateTimeColorP = ColorP.White;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "2")
                        {
                            DateTimeColorP = ColorP.Red;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "3")
                        {
                            DateTimeColorP = ColorP.Green;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "4")
                        {
                            DateTimeColorP = ColorP.Blue;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "5")
                        {
                            DateTimeColorP = ColorP.Yellow;
                        }
                    }
                    else
                    {
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "0")
                        {
                            DateTimeColor = ColorFile.Black;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "1")
                        {
                            DateTimeColor = ColorFile.White;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "2")
                        {
                            DateTimeColor = ColorFile.Red;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "3")
                        {
                            DateTimeColor = ColorFile.Green;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "4")
                        {
                            DateTimeColor = ColorFile.Blue;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/dtc.reg") == "5")
                        {
                            DateTimeColor = ColorFile.Yellow;
                        }
                    }
                }
                catch (Exception ex)
                {
                    CLI.WriteLine("Incorrect registry");
                }
#pragma warning restore CS0168 // Variable is declared but never used
            }
            void TBC()
            {
#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    if (IsGUI)
                    {
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "0")
                        {
                            TitleBarColorP = ColorP.Black;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "1")
                        {
                            TitleBarColorP = ColorP.White;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "2")
                        {
                            TitleBarColorP = ColorP.Red;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "3")
                        {
                            TitleBarColorP = ColorP.Green;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "4")
                        {
                            TitleBarColorP = ColorP.Blue;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "5")
                        {
                            TitleBarColorP = ColorP.Yellow;
                        }
                    }
                    else
                    {
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "0")
                        {
                            TitleBarColor = ColorFile.Black;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "1")
                        {
                            TitleBarColor = ColorFile.White;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "2")
                        {
                            TitleBarColor = ColorFile.Red;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "3")
                        {
                            TitleBarColor = ColorFile.Green;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "4")
                        {
                            TitleBarColor = ColorFile.Blue;
                        }
                        if (PMFAT.ReadText(PMFAT.CurrentDirectory + "REG/SHELL/tbc.reg") == "5")
                        {
                            TitleBarColor = ColorFile.Yellow;
                        }
                    }
                }
                catch (Exception ex)
                {
                    CLI.WriteLine("Incorrect registry");
                }
#pragma warning restore CS0168 // Variable is declared but never used
            }

            if (once)
            {
                Initialize();
                once = false;
                DrawTitleBar();
                GetInput();
            }

            yield return null;
        }


        internal override int Start()
        {
            return 0;
        }
        internal override void Stop()
        {
            Yield = true;

        }
    }
}
