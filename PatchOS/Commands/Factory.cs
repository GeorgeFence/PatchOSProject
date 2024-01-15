using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    public class Factory : Command
    {
        public Factory()
        {
            this.Name = "factory";
            this.Help = "Work with Factory code: execute[f] write[f,t] read[f]";
        }
        public override void Execute(string line, string[] args)
        {
            if (args[1] == "execute")
            {
                Files.Factory.Execute(line.Replace("factory execute ", ""));
            }
            else if (args[1] == "write")
            {
                Files.Factory.Write(line.Replace("factory write " + args[2] + " ", ""), args[2]);
            }
            else if (args[1] == "read")
            {
                if (Kernel.GUI_MODE)
                {
                    GUIConsole.WriteLine(Files.Factory.Read(args[2]));
                }
                else
                {
                    CLI.WriteLine(Files.Factory.Read(args[2]));
                }
            }
            else if (args[1] == "encode")
            {
                string encoded = Files.Factory.Encode(PMFAT.ReadText(args[2]));
                CLI.WriteLine(encoded);
                //PMFAT.WriteAllText(args[2], encoded);
            }
        }
    }
}
