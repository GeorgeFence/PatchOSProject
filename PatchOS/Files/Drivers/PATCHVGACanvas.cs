using System.Collections.Generic;
using System.Drawing;
using Cosmos.Debug.Kernel;
using Cosmos.HAL.Drivers.Video;
using Cosmos.HAL.Drivers.Video.SVGAII;
using Cosmos.System.Graphics.Fonts;
using PatchOS.Files;
using Color = System.Drawing.Color;

namespace Cosmos.System.Graphics;

//
// Summary:
//     Defines a VMWare SVGAII canvas implementation. Please note that this implementation
//     of Cosmos.System.Graphics.Canvas can only be used with virtualizers that do implement
//     SVGAII, meaning that this class will not work on regular hardware.
public class PATCHVGACanvas : Canvas
{
    internal Debugger debugger = new Debugger("PatchVGAScreen");

    private static readonly Mode defaultMode = new Mode(1024u, 768u, ColorDepth.ColorDepth32);

    private Mode mode;

    private bool enabled;

    private readonly VGADriver driver;

    //
    // Summary:
    //     Get and set graphics mode.
    //
    // Exceptions:
    //   T:System.ArgumentOutOfRangeException:
    //     (set) Thrown if mode is not suppoted.
    public override Mode Mode
    {
        get
        {
            return mode;
        }
        set
        {
            mode = value;
            SetGraphicsMode(mode);
        }
    }

    public override Mode DefaultGraphicsMode => defaultMode;

    //
    // Summary:
    //     Available SVGA 2 supported video modes.
    //
    //     SD:
    //
    //     • 320x200x32.
    //     • 320x240x32.
    //     • 640x480x32.
    //     • 720x480x32.
    //     • 800x600x32.
    //     • 1024x768x32.
    //     • 1152x768x32.
    //
    //     HD:
    //
    //     • 1280x720x32.
    //     • 1280x768x32.
    //     • 1280x800x32.
    //     • 1280x1024x32.
    //
    //     HDR:
    //
    //     • 1360x768x32.
    //     • 1366x768x32.
    //     • 1440x900x32.
    //     • 1400x1050x32.
    //     • 1600x1200x32.
    //     • 1680x1050x32.
    //
    //     HDTV:
    //
    //     • 1920x1080x32.
    //     • 1920x1200x32.
    //
    //     2K:
    //
    //     • 2048x1536x32.
    //     • 2560x1080x32.
    //     • 2560x1600x32.
    //     • 2560x2048x32.
    //     • 3200x2048x32.
    //     • 3200x2400x32.
    //     • 3840x2400x32.
    public override List<Mode> AvailableModes { get; } = new List<Mode>
    {
        new Mode(320u, 200u, ColorDepth.ColorDepth32),
        new Mode(320u, 240u, ColorDepth.ColorDepth32),
        new Mode(640u, 480u, ColorDepth.ColorDepth32),
        new Mode(720u, 480u, ColorDepth.ColorDepth32),
        new Mode(800u, 600u, ColorDepth.ColorDepth32),
        new Mode(1024u, 768u, ColorDepth.ColorDepth32),
        new Mode(1152u, 768u, ColorDepth.ColorDepth32),
        new Mode(1280u, 720u, ColorDepth.ColorDepth32),
        new Mode(1280u, 768u, ColorDepth.ColorDepth32),
        new Mode(1280u, 800u, ColorDepth.ColorDepth32),
        new Mode(1280u, 1024u, ColorDepth.ColorDepth32),
        new Mode(1360u, 768u, ColorDepth.ColorDepth32),
        new Mode(1440u, 900u, ColorDepth.ColorDepth32),
        new Mode(1400u, 1050u, ColorDepth.ColorDepth32),
        new Mode(1600u, 1200u, ColorDepth.ColorDepth32),
        new Mode(1680u, 1050u, ColorDepth.ColorDepth32),
        new Mode(1920u, 1080u, ColorDepth.ColorDepth32),
        new Mode(1920u, 1200u, ColorDepth.ColorDepth32),
        new Mode(2048u, 1536u, ColorDepth.ColorDepth32),
        new Mode(2560u, 1080u, ColorDepth.ColorDepth32),
        new Mode(2560u, 1600u, ColorDepth.ColorDepth32),
        new Mode(2560u, 2048u, ColorDepth.ColorDepth32),
        new Mode(3200u, 2048u, ColorDepth.ColorDepth32),
        new Mode(3200u, 2400u, ColorDepth.ColorDepth32),
        new Mode(3840u, 2400u, ColorDepth.ColorDepth32)
    };

    public bool Enabled
    {
        get
        {
            return enabled;
        }
        private set
        {
            enabled = value;
        }
    }

    //
    // Summary:
    //     Initializes a new instance of the Cosmos.System.Graphics.SVGAIICanvas class.
    public PATCHVGACanvas()
        : this(defaultMode)
    {
        Enabled = true;
    }

    //
    // Summary:
    //     Initializes a new instance of the Cosmos.System.Graphics.SVGAIICanvas class.
    //
    //
    // Parameters:
    //   aMode:
    //     The graphics mode.
    public PATCHVGACanvas(Mode aMode)
        : base(aMode)
    {
        ThrowIfModeIsNotValid(aMode);
        driver = new VGADriver();
        Mode = aMode;
        Enabled = true;
    }

    public override string Name()
    {
        return "PatchVGA";
    }

    public override void Disable()
    {
        if (Enabled)
        {
            Enabled = false;
        }
    }

    public void SetMode(Mode aMode)
    {
        
    }
    public override void DrawPoint(Color color, int x, int y)
    {
        if (color.A < byte.MaxValue)
        {
            if (color.A == 0)
            {
                return;
            }

            color = Canvas.AlphaBlend(color, GetPointColor(x, y), color.A);
        }

        driver.SetPixel((uint)x, (uint)y, (uint)color.ToArgb());
    }

    public override void DrawFilledRectangle(Color aColor, int aXStart, int aYStart, int aWidth, int aHeight)
    {
        driver.DrawFilledRectangle(aXStart, aYStart, aWidth, aHeight, driver.GetClosestColorInPalette(aColor));
    }

    //
    // Summary:
    //     Sets the graphics mode to the specified value.
    private void SetGraphicsMode(Mode mode)
    {
        ThrowIfModeIsNotValid(mode);
        uint width = mode.Width;
        uint height = mode.Height;
        this.mode = new Mode(width, height, mode.ColorDepth);
    }

    public override void Clear(int color)
    {
        driver.DrawFilledRectangle(0, 0, driver.PixelWidth, driver.PixelHeight, (uint)color);

    }

    public override void Clear(Color color)
    {
        driver.DrawFilledRectangle(0, 0, driver.PixelWidth, driver.PixelHeight, driver.GetClosestColorInPalette(color));

    }

    public Color GetPixel(int x, int y)
    {
        uint pixel = driver.GetPixel((uint)x, (uint)y);
        return Color.FromArgb((int)pixel);
    }

    public override Color GetPointColor(int x, int y)
    {
        return Color.FromArgb((int)driver.GetPixel((uint)x, (uint)y));
    }

    public override void Display()
    {
        
    }

    public override void DrawString(string str, Font font, Color color, int x, int y)
    {
        int length = str.Length;
        byte width = font.Width;
        for (int i = 0; i < length; i++)
        {
            DrawChar(str[i], font, color, x, y);
            x += width;
        }
    }

    public override void DrawChar(char c, Font font, Color color, int x, int y)
    {
        byte height = font.Height;
        byte width = font.Width;
        byte[] data = font.Data;
        int num = height * (byte)c;
        for (int i = 0; i < height; i++)
        {
            for (byte b = 0; b < width; b++)
            {
                if (font.ConvertByteToBitAddress(data[num + i], b + 1))
                {
                    DrawPoint(color, (ushort)(x + b), (ushort)(y + i));
                }
            }
        }
    }

    public override void DrawImage(Image image, int x, int y)
    {
        int width = (int)image.Width;
        int height = (int)image.Height;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                driver.DrawFilledRectangle(x+j,y+i,1,1,(uint)image.RawData[i + j]);
            }
        }
    }
}