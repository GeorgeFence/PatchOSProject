using System;
using System.Collections.Generic;
using System.Text;
using PatchOS.Files;

namespace PatchOS.Commands.FileSystem
{
    public class DelFile : Command
    {
        public DelFile()
        {
            Name = "del";
            Help = "Deletes a file with a specified name";
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
                        if (PMFAT.FileExists(args[1]) == true)
                        {
                            PMFAT.DeleteFile(args[1]);
                        }
                        else
                        {
                            GUIConsole.WriteLine("File is not existing! Check file path", System.Drawing.Color.Red);
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
                        if (PMFAT.FileExists(args[1]) == true)
                        {
                            PMFAT.DeleteFile(args[1]);
                        }
                        else
                        {
                            CLI.WriteLine("File is not existing! Check file path", ColorFile.Red);
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
                CLI.WriteLine("error while deleting file", ColorFile.Red);
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }

    }
}
