using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers.GUI.UI.Controls
{
    public class Label : Control
    {
        internal string InternalContents;

        public string Text = "";

        public int Xpos;
        public int Ypos;

        AnachorType AnachorType;

        public Label(int X, int Y, string Contents, AnachorType anachor)
            : base(X, Y, 0, 0)
        {
            this.Xpos = X;
            this.Ypos = Y;
            this.Text = Contents;
            AnachorType = anachor;
        }

        public override void Update(Canvas Canvas, int X, int Y, bool sel)
        {
            ASC16.DrawACSIIString(Canvas, Text, System.Drawing.Color.Black, (uint)(X + Xpos), (uint)(Y + Ypos));
        }
    }
}
