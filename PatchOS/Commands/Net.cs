using PatchOS.Files;
using PatchOS.Files.Drivers.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    class Net : Command
    {
        public Net()
        {
            this.Name = "net";
            this.Help = "work with network";
        }

        public override void Execute(string line, String[] args)
        {
            if (Kernel.GUI_MODE)
            {
                if (args[1] == "ip")
                {
                    GUIConsole.WriteLine(NetworkMgr.GetIP());
                }
                if (args[1] == "get")
                {
                    GUIConsole.WriteLine(NetworkMgr.DownloadFile(args[2]));
                }
                if (args[1] == "ping")
                {
                    NetworkMgr.Ping(args[2]);
                }
            }
            else
            {
                CLI.WriteLine(NetworkMgr.GetIP());
            }
        }
    }
}
