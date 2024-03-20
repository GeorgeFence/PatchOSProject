using PatchOS.Files.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers
{
    public static class PaSharp
    {
        public static void Build(List<string> code)
        {
            bool error = false;
            DeveloperStudio.output.text.Add("[ CHCK ] Analising code");
            int start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
            SYS32.ErrorStatusAdd("RAS 1");

            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].EndsWith(";") == false && code[i] != "")
                {
                    DrawError("Missing ';'", i + 1);
                    error = true;
                }
            }
            if(!error)
            {
                SYS32.ErrorStatusAdd("RAS 2");
                DeveloperStudio.output.text.Add("[  OK  ] Analising code done in " + ((Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second) - start) + " s");
            }
            else
            {
                SYS32.ErrorStatusAdd("RAS ERR");
                DeveloperStudio.output.text.Add("Cannot continue building your code. Check Required!");

            }
        }

        public static void DrawError(string error, int line)
        {
            DeveloperStudio.output.text.Add("[ ERR  ] Error found in line " + line.ToString() + " . " + error);
        }
    }
}
