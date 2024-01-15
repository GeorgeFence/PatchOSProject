using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;
using PatchOS.Files;

namespace PatchOS.Files
{
    public static class TextGraphics
    {
        
        // set bg and clear screen
        public static void Clear(ColorFile c) { CLI.BackColor = c; System.Console.Clear(); }

        // draw blank char at position
        public static void SetPixel(int x, int y, ColorFile c) { DrawChar(x, y, ' ', ColorFile.Black, c); }

        // draw horizontal line
        public static void DrawLineH(int x, int y, int w, char c, ColorFile fg, ColorFile bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }
            for (int i = 0; i < w; i++) { DrawChar(x + i, y, drawChar, fg, bg); }
        }

        // draw vertical line
        public static void DrawLineV(int x, int y, int h, char c, ColorFile fg, ColorFile bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }
            for (int i = 0; i < h; i++) { DrawChar(x, y + i, drawChar, fg, bg); }
        }

        // fill rectangle
        public static void FillRect(int x, int y, int w, int h, char c, ColorFile fg, ColorFile bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; } // if char is invalid, replace with blank

            // fill area with character
            for (int i = 0; i < w * h; i++)
            {
                int xx = x + (i % w);
                int yy = y + (i / w);
                if (xx * yy < CLI.Width * CLI.Height) { DrawChar(xx, yy, drawChar, fg, bg); }
            }
        }

        // draw rectangle
        public static void DrawRect(int x, int y, int w, int h, char c, ColorFile fg, ColorFile bg)
        {
            // if char is invalid, replace with blank
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }

            // horizontal lines
            DrawLineH(x, y, w, c, fg, bg);
            DrawLineH(x, y + h, w, c, fg, bg);

            // vertical lines
            DrawLineV(x, y, h, c, fg, bg);
            DrawLineV(x + w, y, h, c, fg, bg);

            // if drawn to the very last location in the buffer(bottom, right)
            // pixel will not be visible, otherwise the screen will auto scroll and shift everything up 1 row
        }

        // draw character to position on screen
        public static bool DrawChar(int x, int y, char c, ColorFile fg, ColorFile bg)
        {
            if (x >= 0 && x < CLI.Width && y >= 0 && y < CLI.Height)
            {
                int oldX = CLI.CursorX, oldY = CLI.CursorY;
                CLI.SetCursorPos(x, y);
                CLI.Write(c.ToString(), fg, bg);
                CLI.SetCursorPos(oldX, oldY);
                return true;
            }
            else { return false; }
        }

        // draw string to position on screen
        public static void DrawString(int x, int y, string txt, ColorFile fg, ColorFile bg)
        {
            for (int i = 0; i < txt.Length; i++) { DrawChar(x + i, y, txt[i], fg, bg); }
        }

        public static void DrawLineHGUI(int x, int y, int w, char c, System.Drawing.Color fg, System.Drawing.Color bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }
            for (int i = 0; i < w; i++) { DrawCharGUI(x + i, y, drawChar, fg, bg); }
        }
        public static void DrawStringGUI(int x, int y, string txt, System.Drawing.Color fg, System.Drawing.Color bg)
        {
            for (int i = 0; i < txt.Length; i++) { DrawCharGUI(x + i, y, txt[i], fg, bg); }
        }
        public static bool DrawCharGUI(int x, int y, char c, System.Drawing.Color fg, System.Drawing.Color bg)
        {
            if (x >= 0 && x < GUIConsole.W && y >= 0 && y < GUIConsole.H)
            {
                int oldX = GUIConsole.X, oldY = GUIConsole.Y;
                System.Drawing.Color BGOld = bg;
                GUIConsole.SetCursorPositionChar(x *8, y*16);
                GUIConsole.BG = bg;
                GUIConsole.Write(c.ToString(), fg);
                GUIConsole.BG = BGOld;
                GUIConsole.SetCursorPositionChar(oldX/8, oldY/16);
                return true;
            }
            else { return false; }
        }
    }
}
