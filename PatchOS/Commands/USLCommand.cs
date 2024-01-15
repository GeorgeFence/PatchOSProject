using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    public class USLCommand : Command
    {
        public USLCommand()
        {
            this.Name = "usl";
            this.Help = "Work with USL programming language";
        }
        public override void Execute(string line, string[] args)
        {
            if (args[1] == "tousl")
            {
                USL.TEXTtoUSL(line.Replace("usl tousl ", ""));
            }
            if (args[1] == "touslfile")
            {
                PMFAT.WriteAllText(args[2], USL.TEXTtoUSL(line.Replace("usl touslfile ", "")));
                
            }
            if (args[1] == "totext")
            {
                USL.USLtoTEXT(line.Replace("usl totext ", ""));
            }
            if (args[1] == "totextfile")
            {
                CLI.WriteLine(USL.USLtoTEXT(PMFAT.ReadText(args[2])));
            }
            if (args[1] == "execute")
            {
                USL.executeCMDs(line.Replace("usl execute ", ""));
            }
            if (args[1] == "executefile")
            {
                USL.executeCMDs(PMFAT.ReadText( PMFAT.Root + args[2].Replace(@"0:\", "")));
            }
        }
    }
}
