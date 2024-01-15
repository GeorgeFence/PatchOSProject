using Cosmos.HAL;
using PatchOS.Commands;
using PatchOS.Process;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files
{
    public static class REGEDIT
    {
        private static string LastPath;
        private static string lastpathNew;
        public static void Init()
        {

        }
        public static void DrawFresh(string path)
        {
            LastPath= path;
            Console.Clear();
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                string[] folders = PMFAT.GetFolders(path);
                string[] files = PMFAT.GetFiles(path);

                CLI.WriteLine("Showing Registry in \"" + path + "\"");

                // draw folders
                for (int i = 0; i < folders.Length; i++)
                {
                    CLI.WriteLine(folders[i], ColorFile.Yellow);
                }

                // draw files
                for (int i = 0; i < files.Length; i++)
                {
                    Cosmos.System.FileSystem.Listing.DirectoryEntry attr = PMFAT.GetFileInfo(path + files[i]);
                    if (attr != null)
                    {
                        CLI.Write(files[i], ColorFile.White);
                        CLI.SetCursorPos(30, CLI.CursorY);
                        CLI.WriteLine(PMFAT.ReadText(path + files[i]), ColorFile.Gray);
                    }
                    else { CLI.WriteLine("Error retrieiving file info", ColorFile.Red); }
                }

                CLI.WriteLine("");
                CLI.Write("Total folders: " + folders.Length.ToString());
                CLI.WriteLine("        Total files: " + files.Length.ToString());
            }
            catch (Exception ex) { }
#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                CLI.SetCursorPos(1,28);
                CLI.Write(path + " > ", ColorFile.Magenta);
                string input = Console.ReadLine();
                processInput(input);
            }
            catch (Exception ex) { }
#pragma warning restore CS0168 // Variable is declared but never used
        }
        public static void processInput(string input)
        {
            input = input += " ";
            string[] args = input.Split(' ');
            if (args[0] == "add")
            {
                RegMgr.AddReg(args[1], args[2]);
            }
            else if (args[0] == "del")
            {
                RegMgr.DeleteReg(args[1]);
            }
            else if (args[0] == "reset")
            {
                RegMgr.SettingUp();
            }
            else if (args[0] == "reboot")
            {
                Cosmos.System.Power.Reboot();
            }
            else if (args[0] == "install")
            {
                Install.InstallFiles();
            }
            else if (args[0] == "shell")
            {
                ProcessManager.Run(new Shell());
            }
            else if (args[0] == "cd")
            {
                try
                {
                    if (args[1].StartsWith(@"0:\"))
                    {
                        if (args[1].EndsWith(@"\"))
                        {
                            LastPath = args[1];
                        }
                        else
                        {
                            LastPath = args[1] + @"\";
                        }
                        
                    }
                    else if (args[1] == "..")
                    {
                        string[] last = LastPath.Split(@"\");
                        for (int i = 0; i < (last.Length - 1); i++)
                        {
                            lastpathNew = lastpathNew += args[i]; 
                            if(i == (last.Length - 1))
                            {
                                LastPath= lastpathNew;
                                LastPath = "";
                            }
                        }
                    }
                    else
                    {
                        LastPath = LastPath + args[1] + @"\";
                    }
                    
                }
                catch(Exception ex) { CLI.WriteLine(ex.ToString(), ColorFile.Red); }
            }
            else
            {
                CLI.WriteLine("Error while processing Reg Command", ColorFile.Red);
            }
            DrawFresh(LastPath);
        }
    }
}
