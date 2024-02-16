using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatchOS.Files.Coroutines;
using PatchOS.Process;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Diagnostics;
using IL2CPU.API.Attribs;
using Cosmos.System.Graphics;

namespace PatchOS.Files.Drivers.GUI.UI;  

public class Window : Control
{

    public WindowProcess process;
    public readonly List<Control> ShelfControls;

    public List<Control> Controls;

    internal bool IsMoving;

    public bool CanMove = true;

    public bool IsMaximized = false;

    public bool IsSelected = false;

    public static int ID;

    public string Title = "";

    public int PanelW;
    public int PanelH;
    public int WinW;
    public int WinH;

    internal int IX;
    internal int IY;

    public Bitmap Icon;

    public static Action act;
    public DesignType Wtype;
    public PermitionsType Ptype;

    public Window(int X, int Y, ushort Width, ushort Height, string TitleStr, Action action, DesignType type, PermitionsType perType, Bitmap Icon) : base(X, Y, Width, Height)
    {
        ShelfControls = new List<Control>();
        Controls = new List<Control>();
        Title = TitleStr;
        if(DesignType.Blank == type)
        {
            PanelW = Width;
            PanelH = Height;
            
        }
        else
        {
            PanelW = Width - 12;
            PanelH = Height - 39;
        }
        WinW = Width;
        WinH = Height;
        act = action;
        Wtype = type;
        Ptype = perType;
        this.Icon = Icon;
    }
    public void ProcessControls(int X, int Y, List<Control> Controls, ConsoleKeyInfo? Key, bool sel)
    {
        if(process.Continue)
        {
            for(int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Update(Kernel.Canvas, X, Y, sel);
                SYS32.ErrorStatusAdd("WINDOW " + Title + " ERR 0");
            }
        }
    }

    public override void Update(Canvas Canvas, int X, int Y, bool sel)
    {
        try
        {
            if (process.Continue)
            {
                int w = (WinW - 255) / 2;
                switch (Wtype)
                {
                    case DesignType.Default:

                        Canvas.DrawFilledRectangle(System.Drawing.Color.GhostWhite, base.X, base.Y, base.Width, base.Height);
                        if (Ptype == PermitionsType.User)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.SteelBlue, base.X + 5, base.Y + 5, (ushort)(base.Width - 10), 32);
                        }
                        else if (Ptype == PermitionsType.System)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.Red, base.X + 5, base.Y + 5, (ushort)(base.Width - 10), 32);
                        }
                        else if (Ptype == PermitionsType.Service)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.Green, base.X + 5, base.Y + 5, (ushort)(base.Width - 10), 32);
                        }

                        Canvas.DrawFilledRectangle(System.Drawing.Color.SteelBlue, base.X + 5, base.Y + 5, (ushort)(base.Width - 10), 32);
                        ConsoleKeyInfo? key = KeyboardEx.ReadKey();

                        ProcessControls(base.X + 6, base.Y + 33, Controls, key, sel);
                        ASC16.DrawACSIIString(Kernel.Canvas, Title, System.Drawing.Color.White, (uint)(base.X + 8), (uint)(base.Y + 8));
                        break;


                    case DesignType.Classic:
                        if (Ptype == PermitionsType.User)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.SteelBlue, base.X, base.Y, 0, 32);
                        }
                        else if (Ptype == PermitionsType.System)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.Red, base.X, base.Y, 0, 32);
                        }
                        else if (Ptype == PermitionsType.Service)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.Green, base.X, base.Y, 0, 32);
                        }
                        Canvas.DrawFilledRectangle(System.Drawing.Color.GhostWhite, base.X, base.Y + 32, (ushort)(base.Width - 32), base.Height);
                        ConsoleKeyInfo? key5 = KeyboardEx.ReadKey();
                        ProcessControls(base.X + 6, base.Y + 33, Controls, key5, sel);
                        ASC16.DrawACSIIString(Kernel.Canvas, Title, System.Drawing.Color.White, (uint)(base.X + 8), (uint)(base.Y + 8));
                        break;


                    case DesignType.Modern:
                        if (Ptype == PermitionsType.User)
                        {

                            Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(0, 0, 255), X, Y, (ushort)w, WinH);
                            Canvas.DrawImage(Kernel.UserApp, X + w, Y);
                            Canvas.DrawImage(Kernel.UserApp, X + w, Y + WinH - 32);
                            Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(255, 0, 255), (X + w + 255), Y, (ushort)((WinW - 255) / 2), WinH);
                            Canvas.DrawImageAlpha(Kernel.ExitApp, X + WinW - 27, Y + 5);


                            Canvas.DrawFilledRectangle(System.Drawing.Color.Black, X + 5, Y + 32, (ushort)(WinW - 10), (ushort)(WinH - 37));
                            Canvas.DrawFilledRectangle(System.Drawing.Color.White, X + 6, Y + 33, (ushort)(WinW - 12), (ushort)(WinH - 39));
                            ConsoleKeyInfo? key3 = KeyboardEx.ReadKey();
                            ProcessControls(base.X + 6, base.Y + 33, Controls, key3, sel);
                            ASC16.DrawACSIIString(Kernel.Canvas, Title, System.Drawing.Color.White, (uint)(base.X + 5), (uint)(base.Y + 8));
                        }
                        else if (Ptype == PermitionsType.System)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(255, 0, 0), X, Y, (ushort)w, WinH);
                            Canvas.DrawImage(Kernel.SystemApp, X + w, Y);
                            Canvas.DrawImage(Kernel.SystemApp, X + w, Y + WinH - 32);
                            Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(255, 0, 255), (X + w + 255), Y, (ushort)((WinW - 255) / 2), WinH);
                            Canvas.DrawImageAlpha(Kernel.ExitApp, X + WinW - 27, Y + 5);

                            Canvas.DrawFilledRectangle(System.Drawing.Color.Black, X + 5, Y + 32, (ushort)(WinW - 10), (ushort)(WinH - 37));
                            Canvas.DrawFilledRectangle(System.Drawing.Color.White, X + 6, Y + 33, (ushort)(WinW - 12), (ushort)(WinH - 39));
                            ConsoleKeyInfo? key6 = KeyboardEx.ReadKey();
                            ProcessControls(base.X + 6, base.Y + 33, Controls, key6, sel);
                            ASC16.DrawACSIIString(Kernel.Canvas, Title, System.Drawing.Color.White, (uint)(base.X + 5), (uint)(base.Y + 8));

                        }
                        else if (Ptype == PermitionsType.Service)
                        {
                            Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(0, 255, 0), X, Y, (ushort)w, WinH);
                            Canvas.DrawImage(Kernel.ServiceApp, X + w, Y);
                            Canvas.DrawImage(Kernel.ServiceApp, X + w, Y + WinH - 32);
                            Canvas.DrawFilledRectangle(System.Drawing.Color.FromArgb(255, 0, 255), (X + w + 255), Y, (ushort)((WinW - 255) / 2), WinH);
                            Canvas.DrawImageAlpha(Kernel.ExitApp, X + WinW - 27, Y + 5);

                            Canvas.DrawFilledRectangle(System.Drawing.Color.Black, X + 5, Y + 32, (ushort)(WinW - 10), (ushort)(WinH - 37));
                            Canvas.DrawFilledRectangle(System.Drawing.Color.White, X + 6, Y + 33, (ushort)(WinW - 12), (ushort)(WinH - 39));
                            ConsoleKeyInfo? key4 = KeyboardEx.ReadKey();
                            ProcessControls(base.X + 6, base.Y + 33, Controls, key4, sel);
                            ASC16.DrawACSIIString(Kernel.Canvas, Title, System.Drawing.Color.White, (uint)(base.X + 5), (uint)(base.Y + 8));
                        }
                        break;

                    case DesignType.Blank:
                        Canvas.DrawFilledRectangle(System.Drawing.Color.GhostWhite, base.X, base.Y, WinW, WinH);
                        ConsoleKeyInfo? key7 = KeyboardEx.ReadKey();
                        ProcessControls(base.X, base.Y, Controls, key7, sel);
                        break;
                }


                act();
            }
        }
        catch (Exception ex)
        {
            SYS32.KernelPanic(ex, "Desktop");
        }
    }
}
public class WindowProcess : Process.Process
{
    public static Coroutine process = new Coroutine(window());

    public bool Continue = true;
    public WindowProcess(string title) : base(title + ".win", Type.User, process)
    {
        MouseManager.ScreenHeight = (UInt32)Kernel.Canvas.Mode.Height;
        MouseManager.ScreenWidth = (UInt32)Kernel.Canvas.Mode.Width;
    }
    static IEnumerator<CoroutineControlPoint> window()
    {
        yield return null;
    }
    internal override int Start()
    {
        return 0;
    }
    internal override void Stop()
    {
        coroutine.Stop();
        Continue = false;
    }
}
