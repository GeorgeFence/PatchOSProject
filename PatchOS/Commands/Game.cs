using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace PatchOS.Commands
{
    public class Game : Command
    {

        public ConsoleGames CGames;

        public Game()
        {
            
            
            this.Name = "game";
            this.Help = "lets play games";
        }

        public override void Execute(string line, String[] args)
        {
            if (Kernel.GUI_MODE)
            {
                GUIConsole.WriteLine("GAMES management");
                GUIConsole.WriteLine("Enter    1; GuessNumber " +
                              "         2; Exit and Reboot");
                int inp = Convert.ToInt32(GUIConsole.ReadLine());
                if (inp == 1)
                {
                    CGames.GuessNumberStart();
                }
                if (inp == 2)
                {
                    Sys.Power.Reboot();
                }
            }
            else
            {
                CLI.WriteLine("GAMES management");
                CLI.WriteLine("Enter    1; GuessNumber " +
                              "         2; Exit and Reboot");
                int inp = Convert.ToInt32(CLI.ReadLine());
                if (inp == 1)
                {
                    CGames.GuessNumberStart();
                }
                if (inp == 2)
                {
                    Sys.Power.Reboot();
                }
            }
            
        }
    }
}
