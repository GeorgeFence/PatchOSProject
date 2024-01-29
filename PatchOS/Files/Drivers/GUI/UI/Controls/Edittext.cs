using Cosmos.System;
using Cosmos.System.Graphics;
using PatchOS.Files.Coroutines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace PatchOS.Files.Drivers.GUI.UI.Controls
{
    public class Edittext : Control
    {
        internal string InternalContents;

        public string Text = "";

        public int Xpos;
        public int Ypos;

        public int W;
        public int H;

        AnachorType AnachorType;

        public Edittext(int X, int Y, int W, int H, AnachorType anachor)
            : base(X, Y, 0, 0)
        {
            this.Xpos = X;
            this.Ypos = Y;
            this.W = W;
            this.H = H;
            AnachorType = anachor;
        }

        public override void Update(Canvas Canvas, int X, int Y, bool sel)
        {
            Canvas.DrawFilledRectangle(System.Drawing.Color.White, Xpos + X, Ypos + Y, W, H);
            Canvas.DrawRectangle(System.Drawing.Color.Black, Xpos + X, Ypos + Y, W, H);
            if (KeyboardEx.keypressed.KeyChar.ToString() != "") 
            {
                Text = Text + KeyboardEx.keypressed.KeyChar;
            }
            else
            {

            }
            ASC16.DrawACSIIString(Canvas, Text, System.Drawing.Color.Black, (uint)(X + Xpos + 3), (uint)(Y + Ypos + 3));
        }
    }
}
