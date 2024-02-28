using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;

namespace Installer
{
    public class Kernel : Sys.Kernel
    {
        [PlugMethod(Signature = "System_Void__System_String__ctor_System_ReadOnlySpan_1_System_Char__")]
        protected override void BeforeRun()
        {

            Installer.Init();
            Log.Success("Init Installer");
            Log.Warning("Init PMFAT");
            PMFAT.Initialize();
            Log.Success("Init PMFAT");
            Log.Warning("Starting Installer...");
            Thread.Sleep(2000);
            Installer.Install();
        }

        protected override void Run()
        {
            
        }
    }
}
