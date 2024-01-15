using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace PatchOS.Commands
{
    class Reboot : Command
    {
        public Reboot()
        {
            this.Name = "reboot";
            this.Help = "reboots this pc";

        }

        public override void Execute(string line,String[] args)
        {
            if (args[1] == "reg") 
            {
                Kernel.Canvas.Disable();
                Kernel.GUI_MODE = false;
                REGEDIT.DrawFresh(@"0:\REG\");
            }
            else
            {
                Kernel.Shutdown(1);
            }
            
        }
    }
}
