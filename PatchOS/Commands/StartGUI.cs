using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using PatchOS.Files.Drivers.GUI;
using PatchOS.Files.Drivers;
using PatchOS.Files;
using System.Drawing;
using Cosmos.HAL;
using Cosmos.System;
using PatchOS.Process;

namespace PatchOS.Commands
{
    class StartGUI : Command
    {
        public StartGUI()
        {
            this.Name = "gui";
            this.Help = "start GUI";
        }

        public override void Execute(string line,String[] args)
        {
            ProcessManager.Remove("Shell");
            ProcessManager.Run(new Desktop());
        }
    }
}
