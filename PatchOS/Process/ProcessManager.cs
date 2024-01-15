using Cosmos.Core;
using Cosmos.System;
using PatchOS.Files.Coroutines;
using PatchOS.Files.Drivers.GUI;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Process
{
    public static class ProcessManager
    {
        internal static List<Process> running = new();
        public static int ID = 0;
        public static void Start()
        {
            try
            {
                CoroutinePool.Main.StartPool();
                SYS32.ErrorStatusAdd("Start");

            }
            catch (Exception ex)
            {
                SYS32.KernelPanic(ex,"ProcessMgr");
            }
        }

        public static void DrawProcess(int X, int Y)
        {
            for (int i = 0; i < ProcessManager.running.Count; i++)
            {
                ASC16.DrawACSIIString(Kernel.Canvas, ProcessManager.running[i].name, Color.Green, 0, (uint)(60 + (i * 16)));
            }
        }

        internal static void Run(Process process)
        {
            SYS32.ErrorStatusAdd("Run 1");
            process.id = ID;
            int code = process.Start();
            process.coroutine.Start();
            SYS32.ErrorStatusAdd("Run 2");
            running.Add(process);
            SYS32.ErrorStatusAdd("Run 3");
            running[running.Count - 1].id = ID;
            SYS32.ErrorStatusAdd("Run 4");
            ID++;
        }
        public static int Remove(string process)
        {
            int j = new int();
            for (int i = 0; i < running.Count; i++){
                if(process == running[i].name)
                {
                    j = i; break;
                }
            }
            ProcessManager.Remove(j);
            return 0;
        }
        public static int Remove(int i)
        {
            running[i].Stop();
            running[i].coroutine.Stop();
            CoroutinePool.Main.RemoveCoroutine(running[i].coroutine);
            running.Remove(running[i]);
            return 0;
        }
        public static string StopAll()
        {
            string result = null;
            for (int i = 0; i < running.Count; i++)
            {
                if (Remove(i) == -1)
                {
                    if (result == null)
                    {
                        result = running[i].name;
                    }
                    else { result += "; " + running[i].name; }
                }
            }

            return result;
        }
    }
}
