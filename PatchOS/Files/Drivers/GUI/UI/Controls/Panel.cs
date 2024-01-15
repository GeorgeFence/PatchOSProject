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

        public Panel(int X, int Y,int W, int H, System.Drawing.Color color, AnachorType anachor)
            : base(X, Y, (ushort)W, (ushort)H)
        {
            this.col = color;
            AnachorType = anachor;
        }

        public override void Update(Canvas Canvas, int X, int Y, bool sel)
        {
            Canvas.DrawFilledRectangle(col ,X + base.X, Y + base.Y, base.Width, base.Height);
        }
    }
}
