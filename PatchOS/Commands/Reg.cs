using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    public class Reg :Command
    {
        public Reg()
        {
            this.Name = "reg";
            this.Help = "reg add [path] + [value] / del [path] / reset";

        }

        public override void Execute(string line, String[] args)
        {
            
            if (args[1] == "add")
            {
                RegMgr.AddReg(args[2], args[3]);
            }
            else if (args[1] == "del")
            {
                RegMgr.DeleteReg(args[2]);
            }
            else if (args[1] == "reset")
            {
                Kernel.Canvas.Disable();
                RegMgr.SettingUp();
            }
            else
            {
                CLI.WriteLine("Error while processing Reg Command", ColorFile.Red);
            }
        }
    }
}
