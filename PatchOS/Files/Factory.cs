using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files
{
    public static class Factory
    {
        #region Factory Code
        private static String A = "A1";
        private static String B = "A2";
        private static String C = "A3";
        private static String D = "A4";
        private static String E = "A5";
        private static String F = "A6";
        private static String G = "A7";
        private static String H = "A8";
        private static String I = "A9";
        private static String J = "10";
        private static String K = "11";
        private static String L = "12";
        private static String M = "13";
        private static String N = "14";
        private static String O = "15";
        private static String P = "16";
        private static String Q = "17";
        private static String R = "18";
        private static String S = "19";
        private static String T = "B1";
        private static String U = "B2";
        private static String V = "B3";
        private static String W = "B4";
        private static String X = "B5";
        private static String Y = "B6";
        private static String Z = "B7";

        private static String a = "B8";
        private static String b = "B9";
        private static String c = "20";
        private static String d = "21";
        private static String e = "22";
        private static String f = "23";
        private static String g = "24";
        private static String h = "25";
        private static String i = "26";
        private static String j = "27";
        private static String k = "28";
        private static String l = "29";
        private static String m = "C1";
        private static String n = "C2";
        private static String o = "C3";
        private static String p = "C4";
        private static String q = "C5";
        private static String r = "C6";
        private static String s = "C7";
        private static String t = "C8";
        private static String u = "C9";
        private static String v = "30";
        private static String w = "31";
        private static String x = "32";
        private static String y = "33";
        private static String z = "34";

        private static String AND = "33";
        private static String STR = "22";
        private static String DOT = "11";
        private static String CON = "00";
        private static String END = "99";

        private static String EMP = "ff";

        #endregion


        private static List<String> FEncode = new List<string>();
        private static List<String> FDecode = new List<string>();

        public static void Init()
        {
            try
            {
                #region Encode
                FEncode.Add(A); FEncode.Add(B); FEncode.Add(C); FEncode.Add(D); FEncode.Add(E); FEncode.Add(F); FEncode.Add(G); FEncode.Add(H); FEncode.Add(I); FEncode.Add(J); FEncode.Add(K); FEncode.Add(L); FEncode.Add(M); FEncode.Add(N); FEncode.Add(O); FEncode.Add(P); FEncode.Add(Q); FEncode.Add(R); FEncode.Add(S); FEncode.Add(T); FEncode.Add(U); FEncode.Add(V); FEncode.Add(W); FEncode.Add(X); FEncode.Add(Y); FEncode.Add(Z);
                FEncode.Add(a); FEncode.Add(b); FEncode.Add(c); FEncode.Add(d); FEncode.Add(e); FEncode.Add(f); FEncode.Add(g); FEncode.Add(h); FEncode.Add(i); FEncode.Add(j); FEncode.Add(k); FEncode.Add(l); FEncode.Add(m); FEncode.Add(n); FEncode.Add(o); FEncode.Add(p); FEncode.Add(q); FEncode.Add(r); FEncode.Add(s); FEncode.Add(t); FEncode.Add(u); FEncode.Add(v); FEncode.Add(w); FEncode.Add(x); FEncode.Add(y); FEncode.Add(z); FEncode.Add(EMP); FEncode.Add(CON); FEncode.Add(END); FEncode.Add(DOT); FEncode.Add(STR); FEncode.Add(AND);
                #endregion
                #region Decode
                FDecode.Add("A"); FDecode.Add("B"); FDecode.Add("C"); FDecode.Add("D"); FDecode.Add("E"); FDecode.Add("F"); FDecode.Add("G"); FDecode.Add("H"); FDecode.Add("I"); FDecode.Add("J"); FDecode.Add("K"); FDecode.Add("L"); FDecode.Add("M"); FDecode.Add("N"); FDecode.Add("O"); FDecode.Add("P"); FDecode.Add("Q"); FDecode.Add("R"); FDecode.Add("S"); FDecode.Add("T"); FDecode.Add("U"); FDecode.Add("V"); FDecode.Add("W"); FDecode.Add("X"); FDecode.Add("Y"); FDecode.Add("Z");
                FDecode.Add("a"); FDecode.Add("b"); FDecode.Add("c"); FDecode.Add("d"); FDecode.Add("e"); FDecode.Add("f"); FDecode.Add("g"); FDecode.Add("h"); FDecode.Add("i"); FDecode.Add("j"); FDecode.Add("k"); FDecode.Add("l"); FDecode.Add("m"); FDecode.Add("n"); FDecode.Add("o"); FDecode.Add("p"); FDecode.Add("q"); FDecode.Add("r"); FDecode.Add("s"); FDecode.Add("t"); FDecode.Add("u"); FDecode.Add("v"); FDecode.Add("w"); FDecode.Add("x"); FDecode.Add("y"); FDecode.Add("z"); FDecode.Add(" "); FDecode.Add("-"); FDecode.Add(";"); FDecode.Add("."); FDecode.Add("'"); FDecode.Add(",");
                   
                #endregion
            }
            catch(Exception ex)
            {
                SYS32.KernelPanic(ex, "error while Init");
            }
            
        }
        public static void Write(string Text, string file)
        {
            try{
                string encoded = Encode(Text);
                if (PMFAT.FileExists(file) == false)
                {
                    PMFAT.CreateFile(file);
                }
                PMFAT.WriteAllText(file, encoded);
            }
            catch (Exception ex) {
                SYS32.KernelPanic(ex, "error while Write");
            }
        }

        public static string Encode(string Text)
        {
            string Out = "";
            string[] tempOut = Text.Split("");
            try
            {
                for (int i = 0; i < tempOut.Count(); i++)
                {
                    for (int j = 0; j < FDecode.Count(); j++)
                    {
                        if (tempOut[i] == FDecode[j])
                        {
                            Out = Out + FEncode[j];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SYS32.KernelPanic(ex, "error while Decode");
            }
            return Out;
        }

        public static string Decode(string Text)
        {
            string Out = "";
            string[] tempOut = Text.Split(" ");
            try
            {
                for (int i = 0; i < tempOut.Count(); i++)
                {
                    for (int j = 0; j < FEncode.Count(); j++)
                    {
                        if (tempOut[i] == FEncode[j])
                        {
                            Out = Out + FDecode[j];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SYS32.KernelPanic(ex, "error while Decode");
            }
            return Out;
        }

        public static string Read(string file)
        {
            string Out = Decode(PMFAT.ReadText(@file.ToString()));
            return Out;
        }

        public static void Execute(string file)
        {
            string DecodedText = Decode(PMFAT.ReadText(file));
            string[] Line = DecodedText.Split(";");

            for (int i = 0; i < Line.Count(); i++)
            {
                string[] code = Line[i].Split(".");
                int j = code.Count();
                string[] content = code[i].Split('-');
                switch (code[0])
                {
                    case "Console":
                        {
                            #region Console
                            if (code[1].StartsWith("WriteLine"))
                            {
                                if (content[1].Contains("'"))
                                {
                                    string[] string_ = content[1].Split("'");
                                    Console.WriteLine(string_[1]);
                                    return;
                                }
                            }
                            if (code[1].StartsWith("Write"))
                            {
                                if (content[1].Contains("'"))
                                {
                                    string[] string_ = content[1].Split("'");
                                    Console.Write(string_[1]);
                                    return;
                                }
                            }
                            if (code[1].StartsWith("SetPos"))
                            {
                                if (content[1].Contains(","))
                                {
                                    string[] string_ = content[1].Split(",");
                                    Console.SetCursorPosition(Int32.Parse(string_[0]), Int32.Parse(string_[1]));
                                    return;
                                }
                            }

                            #endregion
                            return;
                        }
                }
            }
        }
    }
}
