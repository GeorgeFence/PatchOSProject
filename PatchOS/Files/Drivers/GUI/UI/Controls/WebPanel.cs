using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers.GUI.UI.Controls
{
    public class WebPanel : Control
    {
        internal string InternalContents;

        AnachorType anachorType;
        public int Xpos;
        public int Ypos;
        public int Widt;
        public int Heig;
        public string URL = "";

        public WebPanel(int X, int Y,int W, int H, string url, AnachorType anachor)
            : base(X, Y, (ushort)W, (ushort)H)
        {
            anachorType = anachor;
            this.Xpos = X;
            this.Ypos = Y;
            this.Widt = W;
            this.Heig = H;
            this.URL = url;
        }

        public override void Update(Canvas Canvas, int X, int Y, bool sel)
        {
            Canvas.DrawFilledRectangle(System.Drawing.Color.White, X + Xpos, Y + Ypos, Widt, Heig);
        }
    }
}
