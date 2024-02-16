using Cosmos.System;
using Cosmos.System.Graphics;
using PatchOS.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers.GUI.UI
{
    public static class WindowManager
    {
        public static List<Window> Windows;

        public static string Selected = "";

        internal static bool IsDragging;

        static WindowManager()
        {
            Windows = new List<Window>();
        }

        public static void Add(Window window)
        {
            Windows.Add(window);
            ProcessManager.Run(new WindowProcess(window.Title));
        }

        public static void Stop(Window window)
        {
            ProcessManager.running[ProcessManager.running.IndexOf(window.process)].Stop();
            Windows.RemoveAt(Windows.IndexOf(window));
            Desktop.ListPar.Add("Stop");
        }

        public static void Update(Canvas Canvas)
        {
            checked
            {
                bool SelDone = false;
                bool Once = false;
                //Selected = "";
                foreach (Window window in Windows)
                {
                    window.IsSelected = false;
                    if (MouseManager.MouseState != MouseState.Left)
                    {
                        IsDragging = false;
                        window.IsMoving = false;
                    }

                    if(window.Wtype != DesignType.Blank)
                    {
                        if (MouseEx.IsMouseWithin(window.X + window.WinW - 27, window.Y + 5, 24, 24))
                        {
                            if (MouseManager.MouseState == MouseState.Left && Desktop.prevMouseState != MouseState.Left)
                            {
                                Stop(window);
                            }
                        }
                        else
                        {
                            if (MouseEx.IsMouseWithin(window.X, window.Y, (ushort)window.WinW, 32) && !window.IsMoving && !IsDragging && MouseManager.MouseState == MouseState.Left && !SelDone)
                            {
                                Windows.Remove(window);
                                Windows.Insert(Windows.Count, window);
                                SelDone = true;
                                Selected = window.Title;
                                if (window.CanMove)
                                {
                                    window.IX = (int)MouseManager.X - window.X;
                                    window.IY = (int)MouseManager.Y - window.Y;
                                    IsDragging = true;
                                    window.IsMoving = true;
                                }
                            }

                            if (window.IsMoving)
                            {
                                window.X = (int)MouseManager.X - window.IX;
                                window.Y = (int)MouseManager.Y - window.IY;
                            }
                        }
                    }

                    if (MouseEx.IsMouseWithin(window.X, window.Y, (ushort)window.WinW, (ushort)window.WinH) && !window.IsMoving && !IsDragging && MouseManager.MouseState == MouseState.Left && Desktop.prevMouseState !=MouseState.Left && !SelDone && window.CanMove)
                    {
                        Windows.Remove(window);
                        Windows.Insert(Windows.Count, window);
                        SelDone = true;
                        Selected = window.Title;
                    }

                    
                    if (!Once && SelDone)
                    {
                        window.IsSelected = true;
                        Selected = window.Title;
                        Once = true;
                    }
                    else { window.IsSelected = false;}

                    window.Update(Canvas, window.X, window.Y, Once);

                }
            }
            
        }
    }
}
