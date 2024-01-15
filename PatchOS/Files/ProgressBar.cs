using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Color_ = System.Drawing.Color;
using Color = PatchOS.Files.ColorFile;

namespace PatchOS.Files
{
    public static class ProgressBar
    {
        public static void Pbar40x3x20(int x, int y,int delay, ColorFile fgcolor, ColorFile bgcolor, ColorFile progressColor)
        {
            uint delayuint = Convert.ToUInt32(delay);
            string progress = "";
            for (int i = 0; i < 20; i++)
            {
                Kernel.DelayCode(delayuint);
                progress += " ";

                TextGraphics.DrawString(x, y, ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>", fgcolor, bgcolor);
                TextGraphics.DrawString(x, y + 1, "<<<<<<<<<<                    >>>>>>>>>>", fgcolor, bgcolor);
                TextGraphics.DrawString(x, y + 2, "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<", fgcolor, bgcolor);
                TextGraphics.DrawString(x + 10, y + 1, progress, fgcolor, progressColor);
            }
            
            
        }
        public static void Pbar40x3x20(int x, int y, string progressmax20, ColorFile fgcolor, ColorFile bgcolor, ColorFile progressColor)
        {

            TextGraphics.DrawString(x, y,           ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>", fgcolor, bgcolor);
            TextGraphics.DrawString(x, y + 1, "<<<<<<<<<<                    >>>>>>>>>>", fgcolor, bgcolor);
            TextGraphics.DrawString(x, y + 2, "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<", fgcolor, bgcolor);
            TextGraphics.DrawString(x + 10, y + 1, progressmax20, fgcolor, progressColor);

        }



        public static void Pbar20x1(int x, int y, int delay, ColorFile fgcolor, ColorFile progressColor)
        {
            uint delayuint = Convert.ToUInt32(delay);
            string progress = "";
            for (int i = 0; i < 20; i++)
            {
                Kernel.DelayCode(delayuint);
                progress += " ";
                TextGraphics.DrawString(x, y, progress, fgcolor, progressColor);
            }


        }
        public static void Pbar20x1(int x, int y, string progressmax20, ColorFile fgcolor, ColorFile progressColor)
        {
            TextGraphics.DrawString(x, y, progressmax20, fgcolor, progressColor);

        }

        public static void GUI_Pbar20x1(Canvas canvas,int x, int y, int W, int H, Int32 progressmax20, Color_ fgcolor, Int32 Outline, Color_ progressColor)
        {
            canvas.DrawFilledRectangle(fgcolor, x, y, W, H);
            int draw_X = x;
            int draw_W = W / 20;
            for (int i = 0; i < progressmax20; i++)
            {
                canvas.DrawFilledRectangle(fgcolor, draw_X, y, draw_W, H);
                canvas.DrawFilledRectangle(progressColor,draw_X + Outline,y + Outline ,draw_W - (Outline * 2),H - (Outline * 2));
                draw_X = draw_X + draw_W;
            }
        }
    }  
}
