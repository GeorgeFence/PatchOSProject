using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using PatchOS.Files;
using Color = PatchOS.Files.ColorFile;

namespace PatchOS.Commands
{
    public class Help : Command
    {
        public Help()
        {
            this.Name = "help";
            this.Help = "shows the help menu";
        }

        public override void Execute(string line,String[] args)
        {
            drawCMDs();
        }
        public void drawCMDs()
        {
            try
            {
                if (Kernel.GUI_MODE)
                {
                    System.Drawing.Color bg = GUIConsole.BG;
                    System.Drawing.Color fg = GUIConsole.FG;
                    for (int i = 0; i < Shell.CommandsList.Count; i++)
                    {
                        GUIConsole.SetCursorPositionChar(3,1+i);
                        GUIConsole.BG = System.Drawing.Color.Blue; GUIConsole.FG = System.Drawing.Color.DarkGray;
                        GUIConsole.Write("---------------------");
                        GUIConsole.SetCursorPositionChar(3, 1 + i);
                        GUIConsole.FG = System.Drawing.Color.White;
                        GUIConsole.Write(Shell.CommandsList[i].Name + " ");
                        GUIConsole.SetCursorPositionChar(3 + 22, 1 + i);
                        GUIConsole.FG = System.Drawing.Color.LightGray;
                        GUIConsole.Write(Shell.CommandsList[i].Help);
                    }
                    GUIConsole.BG = bg;
                    GUIConsole.FG = fg;
                }
                else
                {
                    for (int i = 0; i < Shell.CommandsList.Count; i++)
                    {
                        TextGraphics.DrawString(3, 3 + i, "---------------------", Color.DarkGray, Color.Blue);
                        TextGraphics.DrawString(3, 3 + i, Shell.CommandsList[i].Name + " ", Color.White, Color.Blue);
                        TextGraphics.DrawString(3 + 22, 3 + i, Shell.CommandsList[i].Help, Color.Gray, Color.Blue);
                    }
                }
                
            }
            catch(Exception ex)
            {
                SYS32.KernelPanic(ex, "error while drawCMDs");
            }
            


        }
    }
}
