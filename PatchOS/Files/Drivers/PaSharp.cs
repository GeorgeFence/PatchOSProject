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
        public static List<string> Using = new List<string>();
        public static void Build(List<string> code,ref List<string> output)
        {
            bool error = false;
            output = new List<string>();
            output.Add("[ WARN ] Initialising PaSharp");
            output.Add("[  OK  ] PaSharp Initialised");
            output.Add("[ CHCK ] Analising code");
            int start = Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second;
            SYS32.ErrorStatusAdd("RAS 1");

            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].EndsWith(";") == false && code[i] != "")
                {
                    DrawError("Missing ';'", i + 1,ref output);
                    error = true;
                }
            }
            if(!error)
            {
                SYS32.ErrorStatusAdd("RAS 2");
                output.Add("[  OK  ] Analising code done in " + ((Cosmos.HAL.RTC.Hour * 3600 + Cosmos.HAL.RTC.Minute * 60 + Cosmos.HAL.RTC.Second) - start) + " s");
                output.Add("[ WARN ] Building code");
                build(code, ref output);
                output.Add("[  OK  ] Building code");
            }
            else
            {
                SYS32.ErrorStatusAdd("RAS ERR");
                output.Add("Cannot continue building your code. Check Required!");

            }
        }

        public static void build(List<string> code, ref List<string> ou)
        {
            for (int i = 0;i < code.Count;i++)
            {
                if (code[i].StartsWith("using"))
                {
                    string[] cod = code[i].Replace("using ", "").Replace(";", "").Split('.');
                    if (cod[0] == "PaSharp")
                    {
                        if (cod[1] == "System")
                        {
                            if (cod[2] == "Console")
                            {
                                Using.Add("PaSharp.System:Console");
                            }
                            else
                            {
                                Using.Add("PaSharp.System");
                            } 
                        }
                        else if (cod[1] == "Desktop")
                        {
                            Using.Add("PaSharp.Desktop");
                        }
                        else
                        {
                            DrawError("Library '" + cod[1] + "' not found! Check entered code!", i + 1, ref ou);
                        }
                    }
                    else
                    {
                        DrawError("Library '" + cod[0] + "' not found! Check entered code!", i + 1, ref ou);
                    }

                    if (code[i].Length < 7)
                    {
                        DrawError("Missing library, Please enter valid Library to Use!", i + 1, ref ou);
                    }
                }
            }
        }

        public static void DrawError(string error, int line,ref List<string> ou)
        {
            ou.Add("[ ERR  ] Error found in line " + line.ToString() + " . " + error);
        }
    }
}
