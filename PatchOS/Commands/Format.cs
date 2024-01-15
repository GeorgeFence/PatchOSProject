using Cosmos.System.FileSystem;
using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Commands
{
    public class Format : Command
    {
        public Format()
        {
            this.Name = "format";
            this.Help = "Format a disc + filesystem + bool quick";
        }
        public override void Execute(string line, string[] args)
        {
            string index = args[1];
            string filesys = args[2];
            bool quick;
            if (args[3] == "true")
            { 
                quick = true;
            }
            else
            { 
                quick = false;
            }
            if (Kernel.GUI_MODE)
            {
                GUIConsole.Write("[Processing]", System.Drawing.Color.Yellow);
                GUIConsole.Write("ChckDisk");
                Kernel.DelayCode(3000);
                GUIConsole.Write("[Done]      ", System.Drawing.Color.Green);
                GUIConsole.Write("ChckDisk");  
                Kernel.DelayCode(1000);
                GUIConsole.Write("[Processing]", System.Drawing.Color.Yellow);
                GUIConsole.Write("Format");
                PMFAT.Driver.Disks[0].FormatPartition(0, filesys, quick);
                GUIConsole.Write("[Done]      ", System.Drawing.Color.Green);   
                GUIConsole.Write("Format");
                Kernel.DelayCode(1000);
            }
            else
            {
                CLI.Write("[Processing]", ColorFile.Yellow);
                CLI.Write("ChckDisk");
                Kernel.DelayCode(3000);
                CLI.Write("[Done]      ", ColorFile.Green);
                CLI.Write("ChckDisk");
                Kernel.DelayCode(1000);
                CLI.Write("[Processing]", ColorFile.Yellow);
                CLI.Write("Format");
                PMFAT.Format(index, filesys, quick);
                CLI.Write("[Done]      ", ColorFile.Green);
                CLI.Write("Format");
                Kernel.DelayCode(1000);
            }
        }
    }
}
