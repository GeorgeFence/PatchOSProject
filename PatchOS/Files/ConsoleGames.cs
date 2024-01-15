using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace PatchOS.Files
{
    public class ConsoleGames
    {
        private int num;
        public void GuessNumberStart()
        {
            CLI.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                          "GuessNumber Game  1 - 999            " +
                          "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                          "");




            GUIConsole.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                          "GuessNumber Game  1 - 999            " +
                          "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                          "");





            Random rnd = new Random();
            num = rnd.Next(999);
            LOOPGN();
        }
        public void LOOPGN()
        {
            try
            {
                if (Kernel.GUI_MODE)
                {
                    GUIConsole.WriteLine("~~~~~~~~~~~~~~");
                    GUIConsole.WriteLine("Guess number: ");
                    int inp = Convert.ToInt32(CLI.ReadLine());

                    if (num == inp)
                    {
                        GUIConsole.WriteLine("Yaay, you did it. The ? number is: " + inp);
                        Kernel.DelayCode(3000);

                        GUIConsole.WriteLine("Write    1; for start a new game" +
                                      "         2; for exit and reboot");
                        int inpt = Convert.ToInt32(CLI.ReadLine());
                        if (inpt == 1)
                        {
                            Random rnd = new Random();
                            num = rnd.Next(999);
                            LOOPGN();
                        }
                        if (inpt == 2)
                        {
                            Sys.Power.Reboot();
                        }
                    }

                    if (num >= inp)
                    {
                        GUIConsole.WriteLine("? number is bigger");
                        LOOPGN();
                    }

                    if (num <= inp)
                    {
                        GUIConsole.WriteLine("? number is smaller");
                        LOOPGN();
                    }
                }
                else
                {
                    CLI.WriteLine("~~~~~~~~~~~~~~");
                    CLI.WriteLine("Guess number: ");
                    int inp = Convert.ToInt32(CLI.ReadLine());

                    if (num == inp)
                    {
                        CLI.WriteLine("Yaay, you did it. The ? number is: " + inp);
                        Kernel.DelayCode(3000);

                        CLI.WriteLine("Write    1; for start a new game" +
                                      "         2; for exit and reboot");
                        int inpt = Convert.ToInt32(CLI.ReadLine());
                        if (inpt == 1)
                        {
                            Random rnd = new Random();
                            num = rnd.Next(999);
                            LOOPGN();
                        }
                        if (inpt == 2)
                        {
                            Sys.Power.Reboot();
                        }
                    }

                    if (num >= inp)
                    {
                        CLI.WriteLine("? number is bigger");
                        LOOPGN();
                    }

                    if (num <= inp)
                    {
                        CLI.WriteLine("? number is smaller");
                        LOOPGN();
                    }
                }
                
            }
            catch(Exception Ex)
            {
                SYS32.KernelPanic(Ex,"");
            }

        }

    }
}
