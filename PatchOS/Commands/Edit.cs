using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    class Edit : Command
    {
        public Edit()
        {
            this.Name = "cat";
            this.Help = "edit files";

        }
        public override void Execute(string line, String[] args)
        {
            txtedit.StartTXTEDIT(args[1].ToString());
        }
    }
}
