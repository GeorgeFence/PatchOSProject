using System;
using System.Collections.Generic;
using System.Text;
using PatchOS.Files;


namespace PatchOS.Commands.FileSystem
{
    public class MakeDir : Command
    {
        public MakeDir()
        {
            this.Name = "mkdir";
            this.Help = "Creates a directory with a specified name";
        }

        public override void Execute(string line, string[] args)
        {
            if (Kernel.GUI_MODE)
            {
                bool success = false;
                string path = "";
                if (line.Length > 6)
                {
                    path = line.Substring(6, line.Length - 6);
                    if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                    path += "\\";

                    if (path.StartsWith(PMFAT.CurrentDirectory)) { PMFAT.CreateFolder(path); success = true; }
                    else if (path.StartsWith(@"0:\")) { PMFAT.CreateFolder(path); success = true; }
                    else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\"))
                    {
                        PMFAT.CreateFolder(PMFAT.CurrentDirectory + path);
                        success = true;
                    }
                    else { GUIConsole.WriteLine("Could not locate directory!", System.Drawing.Color.Red); }
                }
                else { GUIConsole.WriteLine("Invalid argument! Path expected.", System.Drawing.Color.Red); }

                if (success) { GUIConsole.WriteLine("Successfully created directory \"" + path + "\"", System.Drawing.Color.Green); }
            }
            else
            {
                bool success = false;
                string path = "";
                if (line.Length > 6)
                {
                    path = line.Substring(6, line.Length - 6);
                    if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                    path += "\\";

                    if (path.StartsWith(PMFAT.CurrentDirectory)) { PMFAT.CreateFolder(path); success = true; }
                    else if (path.StartsWith(@"0:\")) { PMFAT.CreateFolder(path); success = true; }
                    else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\"))
                    {
                        PMFAT.CreateFolder(PMFAT.CurrentDirectory + path);
                        success = true;
                    }
                    else { CLI.WriteLine("Could not locate directory!", ColorFile.Red); }
                }
                else { CLI.WriteLine("Invalid argument! Path expected.", ColorFile.Red); }

                if (success) { CLI.WriteLine("Successfully created directory \"" + path + "\"", ColorFile.Green); }
            }
            
        }
    }
}
