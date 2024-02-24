using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    class Cat : Command
    {
        public Cat()
        {
            this.Name = "cat";
            this.Help = "Read content of file";

        }
        public override void Execute(string line, String[] args)
        {
            if (PMFAT.FileExists(args[1]))
            {
                GUIConsole.WriteLine(PMFAT.ReadText(args[1]));
            }
            else
            {
                GUIConsole.WriteLine("Path "+ args[1] + " cannot be opened! Is the path correct?", System.Drawing.Color.Red);
            }
        }
    }
}
