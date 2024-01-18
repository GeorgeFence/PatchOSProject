using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            KeyEvent o = new KeyEvent();
            if(o.Key == ConsoleKeyEx.Backspace) 
            {
                string[] st = Text.Split("");
                Text = "";
                for (int i = 0; i < (st.Length - 1); i++)
                {
                    Text = Text + st[i];
                }
            }
            else
            {
                Text = Text + o.KeyChar.ToString();
            }
            ASC16.DrawACSIIString(Canvas, Text, System.Drawing.Color.Black, (uint)(X + Xpos + 3), (uint)(Y + Ypos + 3));
        }
    }
}
