using System;
using System.Collections.Generic;
using System.Text;
using PatchOS.Files;

namespace PatchOS.Files
{
    public static class Runner
    {
        public static bool IsRunning = false;

        // init
        public static void Initialize()
        {
            // init instruction set
            Instructions.Initialize();

            // init memory
            Memory.Initialize();
            Memory.Clear();

            // init cpu
            CPU.Initialize();
            CPU.Halt();
        }

        // reset
        public static void Reset(bool clearRAM)
        {
            if (clearRAM) { Memory.Clear(); }
            CPU.Reset();
            CPU.Halt();
        }

        // stop
        public static void Stop()
        {
            CPU.Halt();
        }

        // start
        public static void Start()
        {
            Reset(false);
            CPU.Continue();
            IsRunning = true;

            while (true)
            {
                CPU.Clock();
                
                // exit
                if (!IsRunning) { break; }
            }
        }
    }
}
