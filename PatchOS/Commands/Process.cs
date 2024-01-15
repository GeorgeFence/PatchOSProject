using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PatchOS.Process;
using PatchOS.Files.Coroutines;
using PatchOS.Files.Drivers.GUI;
using PatchOS.Files.Drivers.GUI.UI;
using Cosmos.System.Audio.IO;

namespace PatchOS.Commands
{
    class Process : Command
    {
        public Process()
        {
            this.Name = "process";
            this.Help = "get | kill [process name]| run";

        }
        public override void Execute(string line, String[] args)
        {
            if (Kernel.GUI_MODE)
            {
                switch (args[1])
                {
                    case "get":
                        {
                            GUIConsole.WriteLine("Processes");
                            GUIConsole.WriteLine("Name        |Id    |Type      ");
                            foreach (var process in ProcessManager.running)
                            {
                                GUIConsole.SetCursorPositionChar(0, GUIConsole.Y /16);
                                GUIConsole.Write(process.name.ToString());
                                GUIConsole.SetCursorPositionChar(13, GUIConsole.Y / 16);
                                GUIConsole.Write(process.id.ToString());
                                GUIConsole.SetCursorPositionChar(20, GUIConsole.Y / 16);
                                switch (process.type)
                                {
                                    case PatchOS.Process.Process.Type.User:
                                        GUIConsole.Write("User");
                                        break;
                                    case PatchOS.Process.Process.Type.System:
                                        GUIConsole.Write("System");
                                        break;
                                    case PatchOS.Process.Process.Type.Service:
                                        GUIConsole.Write("Service");
                                        break;
                                    case PatchOS.Process.Process.Type.Application:
                                        GUIConsole.Write("App");
                                        break;
                                }
                            }

                            return;
                        }

                    case "kill":
                        {
                            ProcessManager.Remove(args[2]);
                            return;
                        }

                    case "run":
                        {
                            for (int i = 0; i < ProcessManager.running.Count; i++)
                            {
                                if (args[2] == "Desktop")
                                {
                                    ProcessManager.Run(new Desktop());
                                }
                                if (args[2] == "Shell")
                                {
                                    ProcessManager.Run(new Shell());
                                }
                                if (args[2] == "MouseMgr")
                                {
                                    ProcessManager.Run(new MouseMgr());
                                }
                                if (args[2] == "Window")
                                {
                                    ProcessManager.Run(new MouseMgr());
                                    while (true)
                                    {
                                        WindowManager.Update(Kernel.Canvas);
                                        MouseMgr.DrawMouse();
                                    }
                                }
                            }
                            return;
                        }
                }
            }
            else
            {
                switch (args[1])
                {
                    case "get":
                        {
                            CLI.WriteLine("Processes");
                            CLI.WriteLine("Name        |Id    |Type      ");
                            foreach (var process in ProcessManager.running)
                            {
                                CLI.SetCursorPos(0, CLI.CursorY);
                                CLI.Write(process.name.ToString());
                                CLI.SetCursorPos(13, CLI.CursorY);
                                CLI.Write(process.id.ToString());
                                CLI.SetCursorPos(20, CLI.CursorY);
                                CLI.WriteLine(process.type.ToString());
                            }

                            return;
                        }

                    case "kill":
                        {
                            ProcessManager.Remove(args[2]);
                            return;
                        }

                    case "run":
                        {
                            for (int i = 0; i < ProcessManager.running.Count; i++)
                            {
                                if (args[2] == "Desktop")
                                {
                                    ProcessManager.Run(new Desktop());
                                }
                                if (args[2] == "Shell")
                                {
                                    ProcessManager.Run(new Shell());
                                }
                                if (args[2] == "MouseMgr")
                                {
                                    ProcessManager.Run(new MouseMgr());
                                }
                            }
                            return;
                        }
                }
            }
         
        }
    }
}
