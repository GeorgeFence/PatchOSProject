using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PatchOS.Files.Drivers.GUI.UI.Controls
{
    public class Image : Control
    {
        internal string InternalContents;

        public static Bitmap image;

        public bool IsClicked = false;

        public int Xpos;
        public int Ypos;
        public bool Alpha = false;
        public string Text = "";

        AnachorType AnachorType;

        public Image(int X, int Y, Bitmap img, bool alpha, AnachorType anachor, string title = "")
            : base(X, Y, 0, 0)
        {
            this.Xpos = X;
            this.Ypos = Y;
            image = img;
            this.Alpha = alpha;
            this.Text = title;
            this.AnachorType = anachor;
        }

        public override void Update(Canvas Canvas, int X, int Y, bool sel)
        {
            if (Alpha)
            {
                Kernel.Canvas.DrawImageAlpha(image,X + Xpos,Y + Ypos);
            }
            else
            {
                Kernel.Canvas.DrawImage(image, X + Xpos, Y + Ypos);
            }

            if (MouseEx.IsMouseWithin(X + Xpos, Y + Ypos, (ushort)image.Width, (ushort)image.Height))
            {
                if(Text != "")
                {
                    Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.DarkGray,(int)(X + Xpos -((Text.Length /2 ) * 8) + (image.Height / 2)) ,(int)(Y + Ypos + image.Height - 16), Text.Length * 8, 16);
                    ASC16.DrawACSIIString(Kernel.Canvas, Text, System.Drawing.Color.White,(uint)((X + Xpos - ((Text.Length / 2) * 8)) + (image.Height / 2)), (uint)(Y + Ypos + image.Height - 16));
                }
                if (MouseManager.MouseState == MouseState.Left && sel)
                {
                    IsClicked = true;
                    
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
