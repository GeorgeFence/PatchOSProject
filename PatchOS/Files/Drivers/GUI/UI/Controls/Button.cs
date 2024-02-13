using Cosmos.System;
using Cosmos.System.Graphics;
using PatchOS.Process;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PatchOS.Files.Drivers.GUI.UI.Controls
{
    public class Button : Control
    {
        private System.Drawing.Color Bg;

        private System.Drawing.Color Fg;

        private System.Drawing.Color Outline;

        public bool IsClicked = false;

        private bool Center;

        AnachorType AnachorType;

        public string Text;

        public int Xpos;
        public int Ypos;
        public int W;
        public int H;

        public Button(int X, int Y, ushort Width, ushort Height, ushort Radius, string Text, bool Center, System.Drawing.Color Bg, System.Drawing.Color Fg, System.Drawing.Color Outline, AnachorType anachor)
            : base(X, Y, Width, Height)
        {
            this.Bg = Bg;
            this.Fg = Fg;
            this.Xpos = X;
            this.Ypos = Y;
            this.W = Width;
            this.H = Height;
            this.Outline = Outline;
            base.Radius = Radius;
            this.Text = Text;
            this.Center = Center;
            AnachorType = anachor;
        }


        public override void Update(Canvas canvas, int X, int Y, bool sel)
        {
            canvas.DrawFilledRectangle(Bg, X + Xpos , Y + Ypos , W, H);
            canvas.DrawRectangle(Outline, X + Xpos , Y + Ypos , W, H);
            ASC16.DrawACSIIString(canvas, Text, Fg,  (uint)(X + W / 2 + Xpos) - (uint)((Text.Length / 2) * 8), (uint)(Y + H / 4 + Ypos - 1));
            if (MouseEx.IsMouseWithin(X + Xpos, Y + Ypos, (ushort)W, (ushort)H))
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    IsClicked = true;
                    canvas.DrawFilledRectangle(System.Drawing.Color.SteelBlue, X + Xpos , Y + Ypos , W, H );
                    canvas.DrawRectangle(Outline, X + Xpos , Y + Ypos , W, H);
                    ASC16.DrawACSIIString(canvas, Text, Fg, (uint)(X + W / 2 + Xpos) - (uint)((Text.Length / 2) * 8), (uint)(Y + H / 4 + Ypos - 1));
                }
                else
                {
                    IsClicked = false;
                }
            }
            else
            {
                IsClicked = false;
            }
        }
    }  
}
