using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Console = System.Console;
using Sys = Cosmos.System;
using PatchOS.Files;
using ColorF = PatchOS.Files.ColorFile;


namespace PatchOS.Commands
{
    class Dir : Command
    {
        public Dir()
        {
            this.Name = "dir";
            this.Help = "show files and folders in active directory";

        }

        public override void Execute(string line, string[] args)
        {
            if (Kernel.GUI_MODE)
            {
                if (line.Length > 3)
                {
                    // show contents of active directory
                    if (args[1].Length == 0)
                    {
                        ListContents(PMFAT.CurrentDirectory);
                    }
                    if (line.Length == 3)
                    {
                        ListContents(PMFAT.CurrentDirectory);

                    }
                    // show contents of specified directory
                    else if (line.Length > 4)
                    {
                        // parse path
                        string path = args[1];
                        if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                        path += "\\";

                        if (PMFAT.FolderExists(path))
                        {
                            if (path.StartsWith(PMFAT.CurrentDirectory)) { ListContents(path); }
                            else if (path.StartsWith(@"0:\")) { ListContents(path); }
                            else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\"))
                            {
                                if (PMFAT.FolderExists(PMFAT.CurrentDirectory + path)) ListContents(PMFAT.CurrentDirectory + path);
                                else { GUIConsole.WriteLine("Could not locate directory \"" + path + "\"", System.Drawing.Color.Red); }
                            }
                            else { GUIConsole.WriteLine("Could not locate directory \"" + path + "\"", System.Drawing.Color.Red); }
                        }
                        else { GUIConsole.WriteLine("Could not locate directory!", System.Drawing.Color.Red); }
                    }
                }
            }
            else
            {
                if (line.Length > 3)
                {
                    // show contents of active directory
                    if (args[1].Length == 0)
                    {
                        ListContents(PMFAT.CurrentDirectory);
                    }
                    if (line.Length == 3)
                    {
                        ListContents(PMFAT.CurrentDirectory);

                    }
                    // show contents of specified directory
                    else if (line.Length > 4)
                    {
                        // parse path
                        string path = args[1];
                        if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                        path += "\\";

                        if (PMFAT.FolderExists(path))
                        {
                            if (path.StartsWith(PMFAT.CurrentDirectory)) { ListContents(path); }
                            else if (path.StartsWith(@"0:\")) { ListContents(path); }
                            else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\"))
                            {
                                if (PMFAT.FolderExists(PMFAT.CurrentDirectory + path)) ListContents(PMFAT.CurrentDirectory + path);
                                else { CLI.WriteLine("Could not locate directory \"" + path + "\"", ColorFile.Red); }
                            }
                            else { CLI.WriteLine("Could not locate directory \"" + path + "\"", ColorFile.Red); }
                        }
                        else { CLI.WriteLine("Could not locate directory!", ColorFile.Red); }
                    }
                }
            }
            
        }

        private void ListContents(string path)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                if (Kernel.GUI_MODE)
                {
                    string[] folders = PMFAT.GetFolders(path);
                    string[] files = PMFAT.GetFiles(path);

                    GUIConsole.WriteLine("Showing contents of directory \"" + path + "\"");

                    // draw folders
                    for (int i = 0; i < folders.Length; i++)
                    {
                        GUIConsole.WriteLine(folders[i], System.Drawing.Color.Yellow);
                    }

                    // draw files
                    for (int i = 0; i < files.Length; i++)
                    {
                        Cosmos.System.FileSystem.Listing.DirectoryEntry attr = PMFAT.GetFileInfo(path + files[i]);
                        if (attr != null)
                        {
                            GUIConsole.Write(files[i], System.Drawing.Color.White);
                            GUIConsole.SetCursorPositionChar(30, GUIConsole.Y);
                            GUIConsole.WriteLine(attr.mSize.ToString() + " BYTES", System.Drawing.Color.LightGray);
                        }
                        else { GUIConsole.WriteLine("Error retrieiving file info", System.Drawing.Color.Red); }
                    }

                    GUIConsole.WriteLine("");
                    GUIConsole.Write("Total folders: " + folders.Length.ToString());
                    GUIConsole.WriteLine("        Total files: " + files.Length.ToString());
                }
                else
                {
                    string[] folders = PMFAT.GetFolders(path);
                    string[] files = PMFAT.GetFiles(path);

                    CLI.WriteLine("Showing contents of directory \"" + path + "\"");

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
                            CLI.WriteLine(attr.mSize.ToString() + " BYTES", ColorFile.Gray);
                        }
                        else { CLI.WriteLine("Error retrieiving file info", ColorFile.Red); }
                    }

                    CLI.WriteLine("");
                    CLI.Write("Total folders: " + folders.Length.ToString());
                    CLI.WriteLine("        Total files: " + files.Length.ToString());
                }
                
            }
            catch (Exception ex) { }
#pragma warning restore CS0168 // Variable is declared but never used
        }
    }
}

