using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PatchOS.Files
{
    public static class PictureFormat
    {
        //FILE EXAMPLE:2;2;265;265;265;265;265;0;265;0;265;200;265;265;
        //             WH  1R  1G  1B  2R  2G 2B 3R 3G 3B  4R  4G  4B 

        public static void DrawPicture(Canvas canvas,int X, int Y, string Data)
        {
            string[] Ints = Data.Split(';');

            for (int i = 0; i < (Int32.Parse(Ints[0]) * Int32.Parse(Ints[1])); i++)
            {
                int IntsIndex = i + 2;
                int ResW = Int32.Parse(Ints[0]);
                int ResH = Int32.Parse(Ints[1]);

                int CurrX = 1;
                int CurrY = 1;

                CurrX++;

                //canvas.DrawFilledRectangle(Color.FromArgb(Int32.Parse(Ints[IntsIndex + 1]), Int32.Parse(Ints[IntsIndex + 2]), Int32.Parse(Ints[IntsIndex + 3])), X,Y, 1 , 1);

                if(CurrX == ResW)
                {
                    CurrX = 1;
                    if(CurrY == ResH)
                    {

                    }
                    else
                    {
                        CurrY++;
                    }
                }
            }
        }
    }
}
