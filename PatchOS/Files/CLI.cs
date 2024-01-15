using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using PatchOS.Files;
using PatchOS.Files.Drivers;

namespace PatchOS.Files
{
    public static class CLI
    {
        // screen
        public static int Width { get { return 80; } }
        public static int Height { get { return 25; } }
        public static ColorFile BackColor = ColorFile.Black;

        // cursor
        public static int CursorX { get { return Console.CursorLeft; } set { Console.CursorLeft = value; } }
        public static int CursorY { get { return Console.CursorTop; } set { Console.CursorTop = value; } }
        public static bool CursorVisible { get { return Console.CursorVisible; } set { Console.CursorVisible = value; } }
        public static ColorFile ForeColor = ColorFile.White;

        // read keyboard input
        public static string ReadLine() { return Console.ReadLine(); }
        public static int Read() { return Console.Read(); }
        public static ConsoleKeyInfo ReadKey(bool hide) { return Console.ReadKey(hide); }

        // clear the console, overriding any other possible back color property changes
        public static void ForceClear(ColorFile c)
        {
            ConsoleColor old = Console.BackgroundColor;
            Console.BackgroundColor = (ConsoleColor)c;
            Console.Clear();
            Console.BackgroundColor = old;
        }

        // write string on current line
        public static void Write(string txt) { Console.Write(txt); }
        public static void Write(string txt, ColorFile fg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.Write(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
        }
        public static void Write(string txt, ColorFile fg, ColorFile bg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.BackgroundColor = (ConsoleColor)bg;
            Console.Write(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
            Console.BackgroundColor = (ConsoleColor)BackColor;
        }

        // write string on new line
        public static void WriteLine(string txt) { Console.WriteLine(txt); }
        public static void WriteLine(string txt, ColorFile fg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.WriteLine(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
        }
        
        public static void WriteLine(string txt, ColorFile fg, ColorFile bg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.BackgroundColor = (ConsoleColor)bg;
            Console.WriteLine(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
            Console.BackgroundColor = (ConsoleColor)BackColor;
        }

        // set cursor pos if valid location
        public static void SetCursorPos(int x, int y)
        {
            int cx = x, cy = y;
            if (x < 0) { cx = 0; }
            if (y < 0) { cy = 0; }
            if (x >= Width) { cx = Width - 1; }
            if (y >= Height) { cy = Height - 1; }
            CursorX = cx; CursorY = cy;
        }

        // convert color name to color value
        public static ColorFile StringToColor(string color)
        {
            string c = color.ToLower();
            if (c == "black") { return ColorFile.Black; }
            if (c == "blue") { return ColorFile.Blue; }
            if (c == "cyan") { return ColorFile.Cyan; }
            if (c == "darkblue") { return ColorFile.DarkBlue; }
            if (c == "darkcyan") { return ColorFile.DarkCyan; }
            if (c == "darkgray") { return ColorFile.DarkGray; }
            if (c == "darkgreen" ) { return ColorFile.DarkGreen; }
            if (c == "darkmagenta") { return ColorFile.DarkMagenta; }
            if (c == "darkred") { return ColorFile.DarkRed; }
            if (c == "darkyellow") { return ColorFile.DarkYellow; }
            if (c == "gray") { return ColorFile.Gray; }
            if (c == "green") { return ColorFile.Green; }
            if (c == "magenta") { return ColorFile.Magenta; }
            if (c == "red") { return ColorFile.Red; }
            if (c == "white") { return ColorFile.White; }
            if (c == "yellow") { return (ColorFile)14; }
            return (ColorFile)40;
        }
    }
}
