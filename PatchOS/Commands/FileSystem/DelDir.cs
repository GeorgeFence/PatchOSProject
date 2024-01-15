using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using PatchOS.Files;

namespace PatchOS.Commands.FileSystem
{
    public class DelDir : Command
    {
        public DelDir()
        {
            Name = "rmdir";
            Help = "Deletes a directory with a specified name";
        }

        public override void Execute(string line, string[] args)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                if (Kernel.GUI_MODE)
                {
                    if (line.Length > 4)
                    {
                        if (PMFAT.FolderExists(args[1]) == true)
                        {
                            PMFAT.DeleteFolder(args[1]);
                        }
                        else
                        {
                            GUIConsole.WriteLine("Directory is not existing! Check directory path", System.Drawing.Color.Red);
                        }
                    }
                    else
                    {
                        GUIConsole.WriteLine("Path is missing!", System.Drawing.Color.Red);
                    }
                }
                else
                {
                    if (line.Length > 4)
                    {
                        if (PMFAT.FolderExists(args[1]) == true)
                        {
                            PMFAT.DeleteFolder(args[1]);
                        }
                        else
                        {
                            CLI.WriteLine("Directory is not existing! Check directory path", ColorFile.Red);
                        }
                    }
                    else
                    {
                        CLI.WriteLine("Path is missing!", ColorFile.Red);
                    }
                }
                
            }
            catch (Exception ex)
            {
                CLI.WriteLine("error while deleting directory", ColorFile.Red);
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }
    }
}
