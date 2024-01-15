using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace PatchOS.Commands
{
    class Shutdown : Command
    {
        public Shutdown()
        {
            this.Name = "shutdown";
            this.Help = "shutdowns this pc";
        }

        public override void Execute(string line,String[] args)
        {
            Kernel.Shutdown(0);
        }
    }
}
