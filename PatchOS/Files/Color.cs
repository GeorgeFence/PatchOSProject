using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files
{
    public struct Color
    {
        public static readonly Color White = new Color(255f, 255f, 255f, 255f);

        public static readonly Color Black = new Color(255f, 0f, 0f, 0f);

        public static readonly Color Cyan = new Color(255f, 0f, 255f, 255f);

        public static readonly Color Red = new Color(255f, 255f, 0f, 0f);

        public static readonly Color Green = new Color(255f, 0f, 255f, 0f);

        public static readonly Color Blue = new Color(255f, 0f, 0f, 255f);

        public static readonly Color CoolGreen = new Color(255f, 54f, 94f, 53f);

        public static readonly Color Magenta = new Color(255f, 255f, 0f, 255f);

        public static readonly Color Yellow = new Color(255f, 255f, 255f, 0f);

        public static readonly Color HotPink = new Color(255f, 230f, 62f, 109f);

        public static readonly Color UbuntuPurple = new Color(255f, 66f, 5f, 22f);

        public static readonly Color GoogleBlue = new Color(255f, 66f, 133f, 244f);

        public static readonly Color GoogleGreen = new Color(255f, 52f, 168f, 83f);

        public static readonly Color GoogleYellow = new Color(255f, 251f, 188f, 5f);

        public static readonly Color GoogleRed = new Color(255f, 234f, 67f, 53f);

        public static readonly Color DeepOrange = new Color(255f, 255f, 64f, 0f);

        public static readonly Color RubyRed = new Color(255f, 204f, 52f, 45f);

        public static readonly Color Transparent = new Color(0f, 0f, 0f, 0f);

        public static readonly Color StackOverflowOrange = new Color(255f, 244f, 128f, 36f);

        public static readonly Color StackOverflowBlack = new Color(255f, 34f, 36f, 38f);

        public static readonly Color GhostWhite = new Color(255f, 188f, 187f, 187f);

        public static readonly Color DarkGray = new Color(255f, 25f, 25f, 25f);

        public static readonly Color LightGray = new Color(255f, 125f, 125f, 125f);

        public static readonly Color SuperOrange = new Color(255f, 255f, 99f, 71f);

        public static readonly Color FakeGrassGreen = new Color(255f, 60f, 179f, 113f);

        public static readonly Color DarkBlue = new Color(255f, 51f, 47f, 208f);

        public static readonly Color BloodOrange = new Color(255f, 255f, 123f, 0f);

        public static readonly Color LightBlack = new Color(255f, 25f, 25f, 25f);

        public static readonly Color LighterBlack = new Color(255f, 50f, 50f, 50f);

        public static readonly Color SteelBlue = new Color(255f, 52f, 86f, 139f);

        public static readonly Color LivingCoral = new Color(255f, 255f, 111f, 97f);

        public static readonly Color UltraViolet = new Color(255f, 107f, 91f, 149f);

        public static readonly Color Greenery = new Color(255f, 136f, 176f, 75f);

        public static readonly Color Emerald = new Color(255f, 0f, 155f, 119f);

        public static readonly Color LightPurple = new Color(4288718301u);

        public static readonly Color Minty = new Color(4285843083u);

        public static readonly Color SunsetRed = new Color(4292900210u);

        public static readonly Color LightYellow = new Color(4294560128u);

        private uint _ARGB;

        private float _A;

        private float _R;

        private float _G;

        private float _B;

        public float Brightness => (Max(this) + Min(this)) / 510f;

        public uint ARGB
        {
            readonly get
            {
                return _ARGB;
            }
            set
            {
                _ARGB = value;
                _A = (value >> 24) & 0xFFu;
                _R = (value >> 16) & 0xFFu;
                _G = (value >> 8) & 0xFFu;
                _B = value & 0xFFu;
            }
        }

        public float A
        {
            readonly get
            {
                return _A;
            }
            set
            {
                _A = Math.Clamp(value, 0f, 255f);
                _ARGB = GetPacked(_A, _R, _G, _B);
            }
        }

        public float R
        {
            readonly get
            {
                return _R;
            }
            set
            {
                _R = Math.Clamp(value, 0f, 255f);
                _ARGB = GetPacked(_A, _R, _G, _B);
            }
        }

        public float G
        {
            readonly get
            {
                return _G;
            }
            set
            {
                _G = Math.Clamp(value, 0f, 255f);
                _ARGB = GetPacked(_A, _R, _G, _B);
            }
        }

        public float B
        {
            readonly get
            {
                return _B;
            }
            set
            {
                _B = Math.Clamp(value, 0f, 255f);
                _ARGB = GetPacked(_A, _R, _G, _B);
            }
        }

        public Color(float A, float R, float G, float B)
        {
            _ARGB = GetPacked(A, R, G, B);
            _A = A;
            _R = R;
            _G = G;
            _B = B;
        }

        public Color(float R, float G, float B)
        {
            _ARGB = GetPacked(255f, R, G, B);
            _A = 255f;
            _R = R;
            _G = G;
            _B = B;
        }

        public Color(string ColorInfo)
        {
            _ARGB = 0u;
            _A = 0f;
            _R = 0f;
            _G = 0f;
            _B = 0f;
            if (string.IsNullOrEmpty(ColorInfo))
            {
                return;
            }

            if (ColorInfo.StartsWith("cymk("))
            {
                string text = ColorInfo;
                string[] array = text.Substring(5, text.Length - 5).Split(',');
                byte b = byte.Parse(array[0]);
                byte b2 = byte.Parse(array[1]);
                byte b3 = byte.Parse(array[2]);
                byte b4 = byte.Parse(array[3]);
                _A = 255f;
                if (b4 != byte.MaxValue)
                {
                    _R = checked((255 - unchecked((int)b)) * (255 - unchecked((int)b4))) / 255;
                    _G = checked((255 - unchecked((int)b3)) * (255 - unchecked((int)b4))) / 255;
                    _B = checked((255 - unchecked((int)b2)) * (255 - unchecked((int)b4))) / 255;
                }
                else
                {
                    checked
                    {
                        _R = 255 - unchecked((int)b);
                        _G = 255 - unchecked((int)b3);
                        _B = 255 - unchecked((int)b2);
                    }
                }

                _ARGB = GetPacked(_A, _R, _G, _B);
                return;
            }

            if (ColorInfo.StartsWith("argb("))
            {
                string text;
                if (!ColorInfo.Contains(','))
                {
                    text = ColorInfo;
                    ARGB = uint.Parse(text.Substring(5, text.Length - 5));
                    return;
                }

                text = ColorInfo;
                string[] array2 = text.Substring(5, text.Length - 5).Split(',');
                try
                {
                    _A = (int)byte.Parse(array2[0]);
                    _R = (int)byte.Parse(array2[1]);
                    _G = (int)byte.Parse(array2[2]);
                    _B = (int)byte.Parse(array2[3]);
                }
                catch
                {
                    _A = float.Parse(array2[0]);
                    _R = float.Parse(array2[1]);
                    _G = float.Parse(array2[2]);
                    _B = float.Parse(array2[3]);
                }

                _ARGB = GetPacked(_A, _R, _G, _B);
                return;
            }

            if (ColorInfo.StartsWith("rgb("))
            {
                string text = ColorInfo;
                string[] array3 = text.Substring(5, text.Length - 5).Split(',');
                _A = 255f;
                try
                {
                    _R = (int)byte.Parse(array3[0]);
                    _G = (int)byte.Parse(array3[1]);
                    _B = (int)byte.Parse(array3[2]);
                }
                catch
                {
                    _R = float.Parse(array3[0]);
                    _G = float.Parse(array3[1]);
                    _B = float.Parse(array3[2]);
                }

                _ARGB = GetPacked(255f, _R, _G, _B);
                return;
            }

            if (ColorInfo.StartsWith("hsl("))
            {
                string text = ColorInfo;
                string[] array4 = text.Substring(5, text.Length - 5).Split(',');
                _A = 255f;
                float num = float.Parse(array4[0]);
                float num2 = float.Parse(array4[1]);
                float num3 = float.Parse(array4[2]);
                num2 = (float)Math.Clamp(num2, 0.0, 1.0);
                num3 = (float)Math.Clamp(num3, 0.0, 1.0);
                if (num2 == 0f)
                {
                    _R = num3;
                    _G = num3;
                    _B = num3;
                    _ARGB = GetPacked(_A, _R, _G, _B);
                }
                else
                {
                    float num4 = (((double)num3 < 0.5) ? (num3 * num2 + num3) : (num3 + num2 - num3 * num2));
                    float p = 2f * num3 - num4;
                    _R = FromHue(p, num4, num + 0f);
                    _G = FromHue(p, num4, num);
                    _B = FromHue(p, num4, num - 0f);
                    _ARGB = GetPacked(_A, _R, _G, _B);
                }

                return;
            }

            if (ColorInfo.StartsWith('#'))
            {
                switch (ColorInfo.Length)
                {
                    case 9:
                        _A = (int)byte.Parse(ColorInfo.Substring(1, 2), NumberStyles.HexNumber);
                        _R = (int)byte.Parse(ColorInfo.Substring(3, 2), NumberStyles.HexNumber);
                        _G = (int)byte.Parse(ColorInfo.Substring(5, 2), NumberStyles.HexNumber);
                        _B = (int)byte.Parse(ColorInfo.Substring(7, 2), NumberStyles.HexNumber);
                        break;
                    case 7:
                        _A = 255f;
                        _R = (int)byte.Parse(ColorInfo.Substring(1, 2), NumberStyles.HexNumber);
                        _G = (int)byte.Parse(ColorInfo.Substring(3, 2), NumberStyles.HexNumber);
                        _B = (int)byte.Parse(ColorInfo.Substring(5, 2), NumberStyles.HexNumber);
                        break;
                    default:
                        throw new FormatException("Hex value is not in correct format!");
                }

                _ARGB = GetPacked(_A, _R, _G, _B);
                return;
            }

            ARGB = ColorInfo switch
            {
                "AliceBlue" => 4293982463u,
                "AntiqueWhite" => 4294634455u,
                "Aqua" => 4278255615u,
                "Aquamarine" => 4286578644u,
                "Azure" => 4293984255u,
                "Beige" => 4294309340u,
                "Bisque" => 4294960324u,
                "Black" => 4278190080u,
                "BlanchedAlmond" => 4294962125u,
                "Blue" => 4278190335u,
                "BlueViolet" => 4287245282u,
                "Brown" => 4289014314u,
                "BurlyWood" => 4292786311u,
                "CadetBlue" => 4284456608u,
                "Chartreuse" => 4286578432u,
                "Chocolate" => 4291979550u,
                "Coral" => 4294934352u,
                "CornflowerBlue" => 4284782061u,
                "Cornsilk" => 4294965468u,
                "Crimson" => 4292613180u,
                "Cyan" => 4278255615u,
                "DarkBlue" => 4278190219u,
                "DarkCyan" => 4278225803u,
                "DarkGoldenRod" => 4290283019u,
                "DarkGray" => 4289309097u,
                "DarkGrey" => 4289309097u,
                "DarkGreen" => 4278215680u,
                "DarkKhaki" => 4290623339u,
                "DarkMagenta" => 4287299723u,
                "DarkOliveGreen" => 4283788079u,
                "DarkOrange" => 4294937600u,
                "DarkOrchid" => 4288230092u,
                "DarkRed" => 4287299584u,
                "DarkSalmon" => 4293498490u,
                "DarkSeaGreen" => 4287609999u,
                "DarkSlateBlue" => 4282924427u,
                "DarkSlateGray" => 4281290575u,
                "DarkSlateGrey" => 4281290575u,
                "DarkTurquoise" => 4278243025u,
                "DarkViolet" => 4287889619u,
                "DeepPink" => 4294907027u,
                "DeepSkyBlue" => 4278239231u,
                "DimGray" => 4285098345u,
                "DimGrey" => 4285098345u,
                "DodgerBlue" => 4280193279u,
                "FireBrick" => 4289864226u,
                "FloralWhite" => 4294966000u,
                "ForestGreen" => 4280453922u,
                "Fuchsia" => 4294902015u,
                "Gainsboro" => 4292664540u,
                "GhostWhite" => 4294506751u,
                "Gold" => 4294956800u,
                "GoldenRod" => 4292519200u,
                "Gray" => 4286611584u,
                "Grey" => 4286611584u,
                "Green" => 4278222848u,
                "GreenYellow" => 4289593135u,
                "HoneyDew" => 4293984240u,
                "HotPink" => 4294928820u,
                "IndianRed" => 4291648604u,
                "Indigo" => 4283105410u,
                "Ivory" => 4294967280u,
                "Khaki" => 4293977740u,
                "Lavender" => 4293322490u,
                "LavenderBlush" => 4294963445u,
                "LawnGreen" => 4286381056u,
                "LemonChiffon" => 4294965965u,
                "LightBlue" => 4289583334u,
                "LightCoral" => 4293951616u,
                "LightCyan" => 4292935679u,
                "LightGoldenRodYellow" => 4294638290u,
                "LightGray" => 4292072403u,
                "LightGrey" => 4292072403u,
                "LightGreen" => 4287688336u,
                "LightPink" => 4294948545u,
                "LightSalmon" => 4294942842u,
                "LightSeaGreen" => 4280332970u,
                "LightSkyBlue" => 4287090426u,
                "LightSlateGray" => 4286023833u,
                "LightSlateGrey" => 4286023833u,
                "LightSteelBlue" => 4289774814u,
                "LightYellow" => 4294967264u,
                "Lime" => 4278255360u,
                "LimeGreen" => 4281519410u,
                "Linen" => 4294635750u,
                "Magenta" => 4294902015u,
                "Maroon" => 4286578688u,
                "MediumAquaMarine" => 4284927402u,
                "MediumBlue" => 4278190285u,
                "MediumOrchid" => 4290401747u,
                "MediumPurple" => 4287852763u,
                "MediumSeaGreen" => 4282168177u,
                "MediumSlateBlue" => 4286277870u,
                "MediumSpringGreen" => 4278254234u,
                "MediumTurquoise" => 4282962380u,
                "MediumVioletRed" => 4291237253u,
                "MidnightBlue" => 4279834992u,
                "MintCream" => 4294311930u,
                "MistyRose" => 4294960353u,
                "Moccasin" => 4294960309u,
                "NavajoWhite" => 4294958765u,
                "Navy" => 4278190208u,
                "OldLace" => 4294833638u,
                "Olive" => 4286611456u,
                "OliveDrab" => 4285238819u,
                "Orange" => 4294944000u,
                "OrangeRed" => 4294919424u,
                "Orchid" => 4292505814u,
                "PaleGoldenRod" => 4293847210u,
                "PaleGreen" => 4288215960u,
                "PaleTurquoise" => 4289720046u,
                "PaleVioletRed" => 4292571283u,
                "PapayaWhip" => 4294963157u,
                "PeachPuff" => 4294957753u,
                "Peru" => 4291659071u,
                "Pink" => 4294951115u,
                "Plum" => 4292714717u,
                "PowderBlue" => 4289781990u,
                "Purple" => 4286578816u,
                "RebeccaPurple" => 4284887961u,
                "Red" => 4294901760u,
                "RosyBrown" => 4290547599u,
                "RoyalBlue" => 4282477025u,
                "SaddleBrown" => 4287317267u,
                "Salmon" => 4294606962u,
                "SandyBrown" => 4294222944u,
                "SeaGreen" => 4281240407u,
                "SeaShell" => 4294964718u,
                "Sienna" => 4288696877u,
                "Silver" => 4290822336u,
                "SkyBlue" => 4287090411u,
                "SlateBlue" => 4285160141u,
                "SlateGray" => 4285563024u,
                "SlateGrey" => 4285563024u,
                "Snow" => 4294966010u,
                "SpringGreen" => 4278255487u,
                "SteelBlue" => 4282811060u,
                "Tan" => 4291998860u,
                "Teal" => 4278222976u,
                "Thistle" => 4292394968u,
                "Tomato" => 4294927175u,
                "Turquoise" => 4282441936u,
                "Violet" => 4293821166u,
                "Wheat" => 4294303411u,
                "White" => uint.MaxValue,
                "WhiteSmoke" => 4294309365u,
                "Yellow" => 4294967040u,
                "YellowGreen" => 4288335154u,
                _ => throw new Exception("Color '" + ColorInfo + "' does not exist!"),
            };
        }

        public Color(uint ARGB)
        {
            _ARGB = 0u;
            _A = 0f;
            _R = 0f;
            _G = 0f;
            _B = 0f;
            this.ARGB = ARGB;
        }

        public static Color operator +(Color Original, Color Value)
        {
            return new Color(Original.A + Value.A, Original.R + Value.R, Original.G + Value.G, Original.B + Value.B);
        }

        public static Color operator -(Color Original, Color Value)
        {
            return new Color(Original.A - Value.A, Original.R - Value.R, Original.G - Value.G, Original.B - Value.B);
        }

        public static Color operator *(Color Original, Color Value)
        {
            return new Color(Original.A * Value.A, Original.R * Value.R, Original.G * Value.G, Original.B * Value.B);
        }

        public static Color operator /(Color Original, Color Value)
        {
            return new Color(Original.A / Value.A, Original.R / Value.R, Original.G / Value.G, Original.B / Value.B);
        }

        public static Color operator +(Color Original, float Value)
        {
            return new Color(Original.A + Value, Original.R + Value, Original.G + Value, Original.B + Value);
        }

        public static Color operator -(Color Original, float Value)
        {
            return new Color(Original.A - Value, Original.R - Value, Original.G - Value, Original.B - Value);
        }

        public static Color operator *(Color Original, float Value)
        {
            return new Color(Original.A * Value, Original.R * Value, Original.G * Value, Original.B * Value);
        }

        public static Color operator /(Color Original, float Value)
        {
            return new Color(Original.A / Value, Original.R / Value, Original.G / Value, Original.B / Value);
        }

        public static Color operator +(float Value, Color Original)
        {
            return new Color(Value + Original.A, Value + Original.R, Value + Original.G, Value + Original.B);
        }

        public static Color operator -(float Value, Color Original)
        {
            return new Color(Value - Original.A, Value - Original.R, Value - Original.G, Value - Original.B);
        }

        public static Color operator *(float Value, Color Original)
        {
            return new Color(Value * Original.A, Value * Original.R, Value * Original.G, Value * Original.B);
        }

        public static Color operator /(float Value, Color Original)
        {
            return new Color(Value / Original.A, Value / Original.R, Value / Original.G, Value / Original.B);
        }

        public static bool operator ==(Color C1, Color C2)
        {
            return C1.ARGB == C2.ARGB;
        }

        public static bool operator !=(Color C1, Color C2)
        {
            return C1.ARGB != C2.ARGB;
        }

        public static Color AlphaBlend(Color Source, Color NewColor)
        {
            if (NewColor.A == 255f)
            {
                return NewColor;
            }

            if (NewColor.A == 0f)
            {
                return Source;
            }

            Color result = default(Color);
            result.A = 255f;
            checked
            {
                result.R = (int)NewColor.R + (int)(Source.R * NewColor.A) >> 8;
                result.G = (int)NewColor.G + (int)(Source.G * NewColor.A) >> 8;
                result.B = (int)NewColor.B + (int)(Source.B * NewColor.A) >> 8;
                return result;
            }
        }

        private static uint GetPacked(float A, float R, float G, float B)
        {
            return BitConverter.ToUInt32(checked(new byte[4]
            {
                (byte)B,
                (byte)G,
                (byte)R,
                (byte)A
            }));
        }

        public static Color Normalize(Color ToNormalize)
        {
            return ToNormalize / 255f;
        }

        private static float FromHue(float P, float Q, float T)
        {
            if (T < 0f)
            {
                T += 1f;
            }

            if (T > 1f)
            {
                T -= 1f;
            }

            if (T < 0f)
            {
                return P + (Q - P) * 6f * T;
            }

            if ((double)T < 0.5)
            {
                return Q;
            }

            if (T < 0f)
            {
                return P + (Q - P) * (0f - T) * 6f;
            }

            return P;
        }

        public static Color Invert(Color ToInvert)
        {
            return White - ToInvert;
        }

        public static Color Lerp(Color StartValue, Color EndValue, float Index)
        {
            Index = (float)Math.Clamp(Index, 0.0, 1.0);
            Color result = default(Color);
            result.A = StartValue.A + (EndValue.A - StartValue.A) * Index;
            result.R = StartValue.R + (EndValue.R - StartValue.R) * Index;
            result.G = StartValue.G + (EndValue.G - StartValue.G) * Index;
            result.B = StartValue.B + (EndValue.B - StartValue.B) * Index;
            return result;
        }

        public static float Max(Color Color)
        {
            return MathF.Max(Color.R, MathF.Max(Color.G, Color.B));
        }

        public static float Min(Color Color)
        {
            return MathF.Min(Color.R, MathF.Min(Color.G, Color.B));
        }

        public Color ToGrayscale()
        {
            return new Color(255f, Brightness, Brightness, Brightness);
        }
    }
}
