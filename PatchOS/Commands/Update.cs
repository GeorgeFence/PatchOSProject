using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    internal class Update : Command
    {
        public Update()
        {
            this.Name = "update";
            this.Help = "check for updates and install them";

        }
        public override void Execute(string line, String[] args)
        {
            Kernel.Update(null);
        }
    }
}
