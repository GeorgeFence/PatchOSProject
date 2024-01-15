using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PatchOS.Files;

namespace PatchOS.Files
{
    public class txtedit
    {
        public static void printTXTEDITStartScreen()
        {
            GUIConsole.Clear();
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("                                         Cat");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("                     type :help<Enter>          for information");
            GUIConsole.WriteLine("                     type :q<Enter>             to exit");
            GUIConsole.WriteLine("                     type :wq<Enter>            save to file and exit");
            GUIConsole.WriteLine("                     press i                    to write");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("");
            GUIConsole.Write("");
        }

        public static String stringCopy(String value)
        {
            String newString = String.Empty;

            for (int i = 0; i < value.Length - 1; i++)
            {
                newString += value[i];
            }

            return newString;
        }

        public static void printTXTEDITScreen(char[] chars, int pos, String infoBar, Boolean editMode)
        {
            int countNewLine = 0;
            int countChars = 0;
            delay(5000);
            GUIConsole.Clear();

            for (int i = 0; i < pos; i++)
            {
                if (chars[i] == '\n')
                {
                    GUIConsole.WriteLine("");
                    countNewLine++;
                    countChars = 0;
                }
                else
                {
                    GUIConsole.Write(chars[i].ToString());
                    countChars++;
                    if (countChars % 80 == 79)
                    {
                        countNewLine++;
                    }
                }
            }

            GUIConsole.Write("/");

            for (int i = 0; i < 23 - countNewLine; i++)
            {
                GUIConsole.WriteLine("");
                GUIConsole.Write("~");
            }

            //PRINT INSTRUCTION
            GUIConsole.WriteLine("");
            for (int i = 0; i < 72; i++)
            {
                if (i < infoBar.Length)
                {
                    GUIConsole.Write(infoBar[i].ToString());
                }
                else
                {
                    GUIConsole.Write(" ");
                }
            }

            if (editMode)
            {
                GUIConsole.Write(countNewLine + 1 + "," + countChars);
            }

        }

        public static String TXTEDIT(String start)
        {
            Boolean editMode = false;
            int pos = 0;
            char[] chars = new char[2000];
            String infoBar = String.Empty;

            if (start == null)
            {
                printTXTEDITStartScreen();
            }
            else
            {
                pos = start.Length;

                for (int i = 0; i < start.Length; i++)
                {
                    chars[i] = start[i];
                }
                printTXTEDITScreen(chars, pos, infoBar, editMode);
            }

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (isForbiddenKey(keyInfo.Key)) continue;

                else if (!editMode && keyInfo.KeyChar == ':')
                {
                    infoBar = ":";
                    printTXTEDITScreen(chars, pos, infoBar, editMode);
                    do
                    {
                        keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            if (infoBar == ":wq")
                            {
                                String returnString = String.Empty;
                                for (int i = 0; i < pos; i++)
                                {
                                    returnString += chars[i];
                                }
                                return returnString;
                            }
                            else if (infoBar == ":q")
                            {
                                return null;

                            }
                            else if (infoBar == ":help")
                            {
                                printTXTEDITStartScreen();
                                break;
                            }
                            else
                            {
                                infoBar = "ERROR: No such command";
                                printTXTEDITScreen(chars, pos, infoBar, editMode);
                                break;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            infoBar = stringCopy(infoBar);
                            printTXTEDITScreen(chars, pos, infoBar, editMode);
                        }
                        else if (keyInfo.KeyChar == 'q')
                        {
                            infoBar += "q";
                        }
                        else if (keyInfo.KeyChar == ':')
                        {
                            infoBar += ":";
                        }
                        else if (keyInfo.KeyChar == 'w')
                        {
                            infoBar += "w";
                        }
                        else if (keyInfo.KeyChar == 'h')
                        {
                            infoBar += "h";
                        }
                        else if (keyInfo.KeyChar == 'e')
                        {
                            infoBar += "e";
                        }
                        else if (keyInfo.KeyChar == 'l')
                        {
                            infoBar += "l";
                        }
                        else if (keyInfo.KeyChar == 'p')
                        {
                            infoBar += "p";
                        }
                        else
                        {
                            continue;
                        }
                        printTXTEDITScreen(chars, pos, infoBar, editMode);



                    } while (keyInfo.Key != ConsoleKey.Escape);
                }

                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    editMode = false;
                    infoBar = String.Empty;
                    printTXTEDITScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.I && !editMode)
                {
                    editMode = true;
                    infoBar = "-- INSERT --";
                    printTXTEDITScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.Enter && editMode && pos >= 0)
                {
                    chars[pos++] = '\n';
                    printTXTEDITScreen(chars, pos, infoBar, editMode);
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && editMode && pos >= 0)
                {
                    if (pos > 0) pos--;

                    chars[pos] = '\0';

                    printTXTEDITScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                if (editMode && pos >= 0)
                {
                    chars[pos++] = keyInfo.KeyChar;
                    printTXTEDITScreen(chars, pos, infoBar, editMode);
                }

            } while (true);
        }

        public static bool isForbiddenKey(ConsoleKey key)
        {
            ConsoleKey[] forbiddenKeys = { ConsoleKey.Print, ConsoleKey.PrintScreen, ConsoleKey.Pause, ConsoleKey.Home, ConsoleKey.PageUp, ConsoleKey.PageDown, ConsoleKey.End, ConsoleKey.NumPad0, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.NumPad4, ConsoleKey.NumPad5, ConsoleKey.NumPad6, ConsoleKey.NumPad7, ConsoleKey.NumPad8, ConsoleKey.NumPad9, ConsoleKey.Insert, ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.F4, ConsoleKey.F5, ConsoleKey.F6, ConsoleKey.F7, ConsoleKey.F8, ConsoleKey.F9, ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12, ConsoleKey.Add, ConsoleKey.Divide, ConsoleKey.Multiply, ConsoleKey.Subtract, ConsoleKey.LeftWindows, ConsoleKey.RightWindows };
            for (int i = 0; i < forbiddenKeys.Length; i++)
            {
                if (key == forbiddenKeys[i]) return true;
            }
            return false;
        }

        public static void delay(int time)
        {
            for (int i = 0; i < time; i++) ;
        }
        public static void StartTXTEDIT(string Path)
        {
            GUIConsole.WriteLine("Enter file's filename to open:");
            GUIConsole.WriteLine("If the specified file does not exist, it will be created.");
            try
            {
                if (File.Exists(Path))
                {
                    GUIConsole.WriteLine("Found file!");
                }
                else if (!File.Exists(Path))
                {
                    GUIConsole.WriteLine("Creating file!");
                    File.Create(Path);
                }
                GUIConsole.Clear();
            }
            catch (Exception ex)
            {
                GUIConsole.WriteLine(ex.Message);
            }

            String text = String.Empty;
            GUIConsole.WriteLine("Do you want to open " + Path + " content? (Yes/No)");    
            if (Console.ReadLine().ToLower() == "yes" || Console.ReadLine().ToLower() == "y")
            {
                text = TXTEDIT(File.ReadAllText(Path));
            }
            else
            {
                text = TXTEDIT(null);
            }

            GUIConsole.Clear();

            if (text != null)
            {
                File.WriteAllText(Path, text);
                GUIConsole.WriteLine("Content has been saved to " + Path);
            }
            GUIConsole.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
