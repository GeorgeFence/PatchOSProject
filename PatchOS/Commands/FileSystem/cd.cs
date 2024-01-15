using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = PatchOS.Files.ColorFile;
using PatchOS.Files;

namespace PatchOS.Commands.FileSystem
{
    public class cd : Command
    {

        public cd()
        {
            Name = "cd";
            Help = "go to directory";

        }

        public override void Execute(string line, string[] args)
        {
            if (Kernel.GUI_MODE)
            {
                string path = args[1].Replace("/", @"\");
                if (path.StartsWith(PMFAT.CurrentDirectory))
                {
                    if (PMFAT.FolderExists(path))
                    {
                        if (path.EndsWith(@"\"))
                        {
                            PMFAT.CurrentDirectory = path;
                        }
                        else
                        {
                            PMFAT.CurrentDirectory = path + @"\";
                        }
                    }
                    else
                    {
                        Log.Error("Could not locate directory \"" + path + "\"");
                    }
                }

                else if (path == "..")
                {
                    if (PMFAT.CurrentDirectory == PMFAT.Root)
                    {

                        GUIConsole.WriteLine("Can not go under root directory!", System.Drawing.Color.Red);
                    }
                    else
                    {
                        string[] dir = PMFAT.CurrentDirectory.Split(@"\");
                        string Out = "";
                        for (int i = 0; i < (dir.Length - 1); i++)
                        {
                            Out = Out + dir[i];
                        }
                        PMFAT.CurrentDirectory = Out;
                    }
                }
                else if (path.StartsWith(PMFAT.CurrentDirectory) == false && path != "..")
                {
                    if (PMFAT.FolderExists(PMFAT.CurrentDirectory + path))
                    {
                        if (path.EndsWith(@"\"))
                        {
                            PMFAT.CurrentDirectory = PMFAT.CurrentDirectory + path;
                        }
                        else
                        {
                            PMFAT.CurrentDirectory = PMFAT.CurrentDirectory + path + @"\";
                        }
                    }
                    else
                    {
                        GUIConsole.WriteLine("Could not locate directory \"" + PMFAT.CurrentDirectory + path + @"\", System.Drawing.Color.Red);
                    }
                }
            }
            else
            {
                string path = args[1].Replace("/", @"\");
                if (path.StartsWith(PMFAT.CurrentDirectory))
                {
                    if (PMFAT.FolderExists(path))
                    {
                        if (path.EndsWith(@"\"))
                        {
                            PMFAT.CurrentDirectory = path;
                        }
                        else
                        {
                            PMFAT.CurrentDirectory = path + @"\";
                        }
                    }
                    else
                    {
                        Log.Error("Could not locate directory \"" + path + "\"");
                    }
                }

                else if (path == "..")
                {
                    if (PMFAT.CurrentDirectory == PMFAT.Root)
                    {

                        CLI.WriteLine("Can not go under root directory!", Color.Red);
                    }
                    else
                    {
                        string[] dir = PMFAT.CurrentDirectory.Split(@"\");
                        string Out = "";
                        for (int i = 0; i < (dir.Length - 1); i++)
                        {
                            Out = Out + dir[i];
                        }
                        PMFAT.CurrentDirectory = Out;
                    }
                }
                else if (path.StartsWith(PMFAT.CurrentDirectory) == false && path != "..")
                {
                    if (PMFAT.FolderExists(path))
                    {
                        if (path.EndsWith(@"\"))
                        {
                            PMFAT.CurrentDirectory = PMFAT.CurrentDirectory + path;
                        }
                        else
                        {
                            PMFAT.CurrentDirectory = PMFAT.CurrentDirectory + path + @"\";
                        }
                    }
                    else
                    {
                        CLI.WriteLine("Could not locate directory \"" + PMFAT.CurrentDirectory + path + @"\", Color.Red);
                    }
                }
            }
            
        }
    }
}
