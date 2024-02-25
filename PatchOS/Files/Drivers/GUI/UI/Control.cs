using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers.GUI.UI
{
    public abstract class Control
    {
        public Action<int, int, MouseState> OnClick;

        public Action<ConsoleKeyInfo> OnKey;

        public System.Drawing.Color Foreground;

        public System.Drawing.Color Background;

        public bool IsEnabled;

        public ushort Radius;

        public Color Accent;

        public Action act;

        public int X;
        public int Y;

        public int Width;
        public int Height;   

        internal Control(int X, int Y, ushort Width, ushort Height, Action action = null)
        {
            OnClick = delegate
            {
            };
            OnKey = delegate
            {
            };
            Background = System.Drawing.Color.White;
            Foreground = System.Drawing.Color.Black;
            IsEnabled = true;
            Radius = 0;
            Accent = Color.CoolGreen;
            this.X = X;
            this.Y = Y;
            act = action;
        }

        public abstract void Update(Canvas Canvas, int X,int Y, bool sel);
    }
}
