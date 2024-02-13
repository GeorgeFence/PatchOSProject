using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers.GUI.UI.Controls
{
    public class Panel : Control
    {
        internal string InternalContents;

        public System.Drawing.Color col;
        AnachorType AnachorType;
        public int Xpos;
        public int Ypos;
        public int W;
        public int H;

        public Panel(int X, int Y,int W, int H, System.Drawing.Color color, AnachorType anachor)
            : base(X, Y, (ushort)W, (ushort)H)
        {
            this.col = color;
            AnachorType = anachor;
            this.Xpos = X;
            this.Ypos = Y;
            this.W = Width;
            this.H = Height;
        }

        public override void Update(Canvas Canvas, int X, int Y, bool sel)
        {
            Canvas.DrawFilledRectangle(System.Drawing.Color.Orange, X + Xpos, Y + Ypos, W, H);
        }
    }
}
