using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    internal class Get : Command
    {
        public Get()
        {
            this.Name = "get";
            this.Help = "get Strings Ints";
        }
        public override void Execute(string line, string[] args)
        {
            string[] dest = args[1].Split(".");
            if (dest[0] == "Canvas")
            {
                if (dest[1] == "Internal")
                {
                    GUIConsole.WriteLine("NOT IMPLEMETED");
                }
            }
            else if (dest[0] == "Reg")
            {
                
            }
            
        }
    }
}
