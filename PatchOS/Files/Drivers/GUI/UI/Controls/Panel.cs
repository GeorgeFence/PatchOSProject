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
        AnachorType anachorType;
        public int Xpos;
        public int Ypos;
        public int Widt;
        public int Heig;

        public Panel(int X, int Y,int W, int H, System.Drawing.Color color, AnachorType anachor)
            : base(X, Y, (ushort)W, (ushort)H)
        {
            this.col = color;
            anachorType = anachor;
            this.Xpos = X;
            this.Ypos = Y;
            this.Widt = W;
            this.Heig = H;
        }

        public override void Update(Canvas Canvas, int X, int Y, bool sel)
        {
            Canvas.DrawFilledRectangle(col, X + Xpos, Y + Ypos, Widt, Heig);
        }
    }
}
