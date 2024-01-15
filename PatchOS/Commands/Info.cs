using System;
using System.Collections.Generic;
using System.Text;
using PatchOS.Files;

namespace PatchOS.Commands
{
    class Info : Command
    {
        public Info()
        {
            this.Name = "info";
            this.Help = "shows info about software and hardware";
        }

        public override void Execute(string line,String[] args)
        {
            CLI.WriteLine("");
            GUIConsole.WriteLine("");
            SystemInfo.ShowInfo();
        }
    }
}
