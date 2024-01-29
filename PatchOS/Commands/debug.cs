using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    public class debug : Command
    {
        public debug()
        {
            this.Name = "debug";
            this.Help = "";
        }
        public unsafe override void Execute(string line, string[] args)
        {
            throw new NotImplementedException("Testing");
        }
    }
}
