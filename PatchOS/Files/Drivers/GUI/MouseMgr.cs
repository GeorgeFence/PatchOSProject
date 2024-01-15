using Cosmos.Core;
using Cosmos.Core.IOGroup;
using Cosmos.Core.Memory;
using Cosmos.Debug.Kernel;
using Cosmos.HAL;
using Cosmos.HAL.BlockDevice.Registers;
using Cosmos.System;
using PatchOS.Files.Coroutines;
using Cosmos.System.Graphics;
using PatchOS.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Sys = Cosmos.System;
using PatchOS.Files.Coroutines;
using PatchOS.Process;
using System.Diagnostics.CodeAnalysis;

namespace PatchOS.Files.Drivers.GUI
{
    internal class MouseMgr : Process.Process
    {
        public static Coroutine process = new Coroutine(mouse());
        public static bool Yield = false;
        public static bool Outside = false;
        public MouseMgr() : base("MouseMgr", Type.User, process)
        {
            MouseManager.ScreenHeight = (UInt32)(int)(Kernel.Canvas.Mode.Height);
            MouseManager.ScreenWidth = (UInt32)(int)(Kernel.Canvas.Mode.Width);
        }
        static IEnumerator<CoroutineControlPoint> mouse()
        {
            bool Make = false;
            for (int i = 0; i < ProcessManager.running.Count; i++)
            {
                if ("Desktop" == ProcessManager.running[i].name)
                {
                    Make = true;
                }
            }
            if (Make)
            {
                Outside = true;
            }
            if(Make == false)
            {
                while (Yield == false)
                {
                    DrawMouse();
                }
            }
            yield return null;
        }

        public static void DrawMouse()
        {
            Kernel.Canvas.DrawImageAlpha(Kernel.Cursor, (Int32)MouseManager.X, (Int32)MouseManager.Y);
        }
        internal override int Start()
        {
            return 0;
        }
        internal override void Stop()
        {
            Yield = true;
            Outside = false;
        }
    }
}
