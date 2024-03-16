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

        public bool Selected = false;

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
            if (MouseEx.IsMouseWithin(Xpos + X, Ypos + Y, (ushort)W,(ushort)H))
            {
                if(MouseManager.MouseState == MouseState.Left && Desktop.prevMouseState != MouseState.Left)
                {
                    Selected = true;
                }
            }
            if (!sel)
            {
                Selected = false;
            }
            if(Key.KeyPressed && Selected)
            {
                if(Key.keyevent.Key == ConsoleKey.Backspace && Text != "")
                {
                    Text = Text.Remove(Text.Length - 1);
                }
                else
                {
                    Text = Text + Key.keyevent.KeyChar.ToString();
                }
            }

            if (Text.Length > (W / 8 - 3))
            {
                string s = " ";
                for (int i = MouseManager.ScrollDelta; i < Text.Length; i++)
                {
                    if (MouseManager.ScrollDelta < 0)
                    {
                        if (i - MouseManager.ScrollDelta < (W / 8 - 3))
                        {
                            s = s + Text[i];
                        }
                    }
                }
                ASC16.DrawACSIIString(Canvas, s, System.Drawing.Color.Black, (uint)(X + Xpos + 3), (uint)(Y + Ypos + 3));
                Canvas.DrawFilledRectangle(System.Drawing.Color.Blue,(X + Xpos + (W - (Text.Length -(W / 8)))), (Y + Ypos + H - 2),20, 2);
            }
            else
            {
                ASC16.DrawACSIIString(Canvas, Text, System.Drawing.Color.Black, (uint)(X + Xpos + 3), (uint)(Y + Ypos + 3));
            }

            if (Selected)
            {
                Canvas.DrawFilledRectangle(System.Drawing.Color.DimGray,(X + Xpos + 3) + ((Text.Length) * 8), (Y + Ypos + 3), 2,16);
            }
        }
    }
}
