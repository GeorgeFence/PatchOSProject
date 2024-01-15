using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers
{
    public static class MouseEx
    {
        public static bool IsClickFired
        {
            get
            {
                if (MouseManager.MouseState == MouseState.None)
                {
                    return MouseManager.LastMouseState != MouseState.None;
                }

                return false;
            }
        }

        public static bool IsClicked => MouseManager.MouseState != MouseState.None;

        public static bool IsMouseWithin(int X, int Y, ushort Width, ushort Height)
        {
            checked
            {
                if (MouseManager.X >= X && MouseManager.X <= X + unchecked((int)Width) && MouseManager.Y >= Y)
                {
                    return MouseManager.Y <= Y + unchecked((int)Height);
                }

                return false;
            }
        }
    }
}
