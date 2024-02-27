using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Cosmos.System.Console;
using Color = System.Drawing.Color;
using PatchOS.Files.Drivers.GUI;

namespace PatchOS.Files
{
    public static class GUIConsole
    {
        public static Int32 X = 0;
        public static Int32 Y = 1;
        public static System.Drawing.Color BG = System.Drawing.Color.Black;
        public static System.Drawing.Color FG = System.Drawing.Color.White;
        public static Int32 W = 640;
        public static Int32 H = 480;    

        public static void Init()
        {
            W = (int)Kernel.Canvas.Mode.Width;
            H = (int)Kernel.Canvas.Mode.Height;
        }
        public static void Write(string text)
        {
            Kernel.Canvas.DrawFilledRectangle(BG, X * 8, Y * 16, (int)text.Length * 8, 16);
            ASC16.DrawACSIIString(Kernel.Canvas, text, FG, (uint)X * 8, (uint)Y * 16);
            X = X + text.Length;

            Global.Debugger.Send(X + " " + Y);
            Kernel.Canvas.Display();
        }
        public static void Write(string text, System.Drawing.Color color) 
        {
            Kernel.Canvas.DrawFilledRectangle(BG, X * 8, Y * 16, (int)text.Length * 8, 16);
            ASC16.DrawACSIIString(Kernel.Canvas, text, color, (uint)X * 8, (uint)Y * 16);
            X = X + text.Length;

            Global.Debugger.Send(X + " " + Y);
            Kernel.Canvas.Display();
        }

        public static void WriteLine(string text)
        {
            Write(text, FG);
            X = 0;
            Y++;
        }
        public static void WriteLine(string text, System.Drawing.Color color)
        {
            Write(text, color);
            X = 0;
            Y++;
        }

        public static void Clear()   
        {
            Kernel.Resolution((ushort)W, (ushort)H);
            Kernel.Canvas.Clear(System.Drawing.Color.Black);
            Kernel.Canvas.Display();
        }
        public static void Clear(System.Drawing.Color bg)
        {
            Kernel.Resolution((ushort)W,(ushort)H);
            Kernel.Canvas.Clear(bg);
            Kernel.Canvas.Display();
        }

        public static string ReadLine()
        {
            int Xold = GUIConsole.X;
            bool cont = true;
            string Out = "";
            DrawInput();
            while (cont)
            {
                ConsoleKeyInfo key = System.Console.ReadKey();
                if(key.Key != ConsoleKey.Enter)
                {
                    if(key.Key != ConsoleKey.Backspace)
                    {
                        Out = Out + key.KeyChar.ToString();
                        Write(key.KeyChar.ToString());
                        DrawInput();
                    }
                    else
                    {
                        string[] Out1 = Out.Split("");
                        Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.Black, Xold * 8, GUIConsole.Y * 16, Out.Length * 8, 16);
                        string Out2 = "";
                        for (int i = 0; i < (Out1.Length - 1); i++)
                        {
                            Out2 = Out2 + Out1[i];
                        }
                        GUIConsole.FG = System.Drawing.Color.White;
                        SetCursorPositionChar(Xold, Y);
                        Write(Out2);
                        DrawInput();
                    }
                }
                else
                {
                    cont = false;
                }
            }
            return Out;
        }
        public static void SetCursorPositionChar(int x_char, int y_char)
        {
            X = (x_char);
            Y = (y_char);
        }

        private static void DrawInput()
        {
            Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.White, X * 8 + 8, Y * 16 + 14, 8, 2);
        }

        private static void ScrollDown()
        {
            //Kernel.Canvas.DrawFilledRectangle(System.Drawing.Color.Black, 0, 384, 640, 16);
            //SetCursorPositionChar(0, 24);
        }

    }
}
