using PatchOS.Commands;
using PatchOS.Commands.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PatchOS.Files
{
    public static class USL
    {
#pragma warning disable CS0649 // Field 'USL.help' is never assigned to, and will always have its default value null
        private static Help help;
#pragma warning restore CS0649 // Field 'USL.help' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'USL.cd' is never assigned to, and will always have its default value null
        private static cd cd;
#pragma warning restore CS0649 // Field 'USL.cd' is never assigned to, and will always have its default value null

        //Capital 
        public static string A = "(([]{{}]]]";
        public static string B = "{][{{()))(";
        public static string C = "(){][]]]{*";
        public static string D = "{[][{(()))";
        public static string E = "{{]]{(()))";
        public static string F = "(({][{)))(";
        public static string G = @"]*///**\";
        public static string H = "]*//*///*";
        public static string I = "]*//*//**";
        public static string J = "]*//*/*/*";
        public static string K = "]*//*/*(){}]";
        public static string L = "]*//*(){}]]/*";
        public static string M = "]*//*(){}]]**";
        public static string N = @"]*//**\]*";
        public static string O = "]*//*())[{";
        public static string P = "]*/*////*";
        public static string Q = "]*/*///**";
        public static string R = "]*/*//*/*";
        public static string S = "]*/*//*(){}]";
        public static string T = "]*/*/*//*";
        public static string U = "]*/*/*/**";
        public static string V = "]*/*/*(){}]]*";
        public static string W = @"]*/*/**\";
        public static string X = "]*/*(){}]]//*";
        public static string Y = "]*/*(){}]]/**";
        public static string Z = "]*/*(){}]]*/*";

        //Lowercase
        public static string a = "]*(){}]]///**";
        public static string b = "]*(){}]]//*/*";
        public static string c = "]*(){}]]//*(){}]";
        public static string d = "]*(){}]]/*//*";
        public static string e = "]*(){}]]/*/**";
        public static string f = "]*(){}]]/*(){}]]*";
        public static string g = @"]*(){}]]/**\";
        public static string h = "]*(){}]]*///*";
        public static string i = "]*(){}]]*//**";
        public static string j = "]*(){}]]*/*/*";
        public static string k = "]*(){}]]*/*(){}]";
        public static string l = "]*(){}]]*(){}]]/*";
        public static string m = "]*(){}]]*(){}]]**";
        public static string n = @"]*(){}]]**\]*";
        public static string o = "]*(){}]]*())[{";
        public static string p = @"]**\]///*";
        public static string q = @"]**\]//**";
        public static string r = @"]**\]/*/*";
        public static string s = @"]**\]/*(){}]";
        public static string t = @"]**\]*//*";
        public static string u = @"]**\]*/**";
        public static string v = @"]**\]*(){}]]*";
        public static string w = @"]**\]**\";
        public static string x = "]*())[{]//*";
        public static string y = "]*())[{]/**";
        public static string z = "]*())[{]*/*";

        //numbers
        public static string nmb1 = "0001";
        public static string nmb2 = "0010";
        public static string nmb3 = "0011";
        public static string nmb4 = "0100";
        public static string nmb5 = "0101";
        public static string nmb6 = "0110";
        public static string nmb7 = "0111";
        public static string nmb8 = "1000";
        public static string nmb9 = "1001";
        public static string nmb0 = "0000";

        //other
        public static string Space = "*/****{{{]]][";
        public static string End = @"***\***]]]";
        public static string plus = @"/\*]]]]******";
        public static string True = "[****((";
        public static string False = "]****((";

        //voids
        public static string USLtoTEXT(string Input)
        {
            string Out = Input;
            try
            {


                Out = Input.Replace(Space,  " ");
                Out = Out.Replace(End,  ";");
                Out = Out.Replace(plus,  "+");
                Out = Out.Replace(True,  "true");
                Out = Out.Replace(False,  "false");
                Out = Out.Replace(A,  "A");
                Out = Out.Replace(B,  "B");
                Out = Out.Replace(C,  "C");
                Out = Out.Replace(D,  "D");
                Out = Out.Replace(E,  "e");
                Out = Out.Replace(F,  "F");
                Out = Out.Replace(G,  "G");
                Out = Out.Replace(H,  "H");
                Out = Out.Replace(I,  "I");
                Out = Out.Replace(J,  "J");
                Out = Out.Replace(K,  "K");
                Out = Out.Replace(L,  "L");
                Out = Out.Replace(M,  "M");
                Out = Out.Replace(N,  "N");
                Out = Out.Replace(O,  "O");
                Out = Out.Replace(P,  "P");
                Out = Out.Replace(Q,  "Q");
                Out = Out.Replace(R,  "R");
                Out = Out.Replace(S,  "S");
                Out = Out.Replace(T,  "T");
                Out = Out.Replace(U,  "U");
                Out = Out.Replace(V,  "V");
                Out = Out.Replace(W,  "W");
                Out = Out.Replace(X,  "X");
                Out = Out.Replace(Y,  "Y");
                Out = Out.Replace(Z,  "Z");
                Out = Out.Replace(a,  "a");
                Out = Out.Replace(b,  "b");
                Out = Out.Replace(c,  "c");
                Out = Out.Replace(d,  "d");
                Out = Out.Replace(e,  "e");
                Out = Out.Replace(f,  "f");
                Out = Out.Replace(g,  "g");
                Out = Out.Replace(h,  "h");
                Out = Out.Replace(i,  "i");
                Out = Out.Replace(j,  "j");
                Out = Out.Replace(k,  "k");
                Out = Out.Replace(l,  "l");
                Out = Out.Replace(m,  "m");
                Out = Out.Replace(n,  "n");
                Out = Out.Replace(o,  "o");
                Out = Out.Replace(p,  "p");
                Out = Out.Replace(q,  "q");
                Out = Out.Replace(r,  "r");
                Out = Out.Replace(s,  "s");
                Out = Out.Replace(t,  "t");
                Out = Out.Replace(u,  "u");
                Out = Out.Replace(v,  "v");
                Out = Out.Replace(w,  "w");
                Out = Out.Replace(x,  "x");
                Out = Out.Replace(y,  "y");
                Out = Out.Replace(z,  "z");

                CLI.WriteLine(Out);

            }
            catch (Exception ex)
            {
                SYS32.KernelPanic(ex, "error while processing USLtoTEXT");
            }

            return Out;
        }

        public static string TEXTtoUSL(string Input)
        {
            string Out = Input;
            try
            {


                Out = Input.Replace(" ",  Space);
                Out = Out.Replace(";",  End);
                Out = Out.Replace("+",  plus);
                Out = Out.Replace("true",  True);
                Out = Out.Replace("false",  False);
                Out = Out.Replace("A",  A);
                Out = Out.Replace("B",  B);
                Out = Out.Replace("C",  C);
                Out = Out.Replace("D",  D);
                Out = Out.Replace("E",  E);
                Out = Out.Replace("F",  F);
                Out = Out.Replace("G",  G);
                Out = Out.Replace("H",  H);
                Out = Out.Replace("I",  I);
                Out = Out.Replace("J",  J);
                Out = Out.Replace("K",  K);
                Out = Out.Replace("L",  L);
                Out = Out.Replace("M",  M);
                Out = Out.Replace("N",  N);
                Out = Out.Replace("O",  O);
                Out = Out.Replace("P",  P);
                Out = Out.Replace("Q",  Q);
                Out = Out.Replace("R",  R);
                Out = Out.Replace("S",  S);
                Out = Out.Replace("T",  T);
                Out = Out.Replace("U",  U);
                Out = Out.Replace("V",  V);
                Out = Out.Replace("W",  W);
                Out = Out.Replace("X",  X);
                Out = Out.Replace("Y",  Y);
                Out = Out.Replace("Z",  Z);
                Out = Out.Replace("a",  a);
                Out = Out.Replace("b",  b);
                Out = Out.Replace("c",  c);
                Out = Out.Replace("d",  d);
                Out = Out.Replace("e",  e);
                Out = Out.Replace("f",  f);
                Out = Out.Replace("g",  g);
                Out = Out.Replace("h",  h);
                Out = Out.Replace("i",  i);
                Out = Out.Replace("j",  j);
                Out = Out.Replace("k",  k);
                Out = Out.Replace("l",  l);
                Out = Out.Replace("m",  m);
                Out = Out.Replace("n",  n);
                Out = Out.Replace("o",  o);
                Out = Out.Replace("p",  p);
                Out = Out.Replace("q",  q);
                Out = Out.Replace("r",  r);
                Out = Out.Replace("s",  s);
                Out = Out.Replace("t",  t);
                Out = Out.Replace("u",  u);
                Out = Out.Replace("v",  v);
                Out = Out.Replace("w",  w);
                Out = Out.Replace("x",  x);
                Out = Out.Replace("y",  y);
                Out = Out.Replace("z",  z);

                CLI.WriteLine(Out);

            }
            catch(Exception ex)
            {
                SYS32.KernelPanic(ex, "error while processing TEXTtoUSL");
            }

            return Out;
        }

        public static void executeCMDs(string text)
        {
            try
            {
                string[] line = text.Split(';');
                for (int i = 0; i < line.Count(); i++)
                {
                    string str = line[i];
                    string[] linesplit = str.Split(" ");
                    CLI.WriteLine(line[i]);
                    if (linesplit[0] == "cli")
                    {
                        if (linesplit[1] == "write")
                        {
                            CLI.Write(line[i].Replace("cli write ", ""));
                        }
                        else if (linesplit[1] == "writeline")
                        {
                            CLI.WriteLine(line[i].Replace("cli writeline ", ""));
                        }
                    }
                    if (linesplit[0] == "help")
                    {
                        help.drawCMDs();
                    }
                    if (linesplit[0] == "cd")
                    {
                        cd.Execute(text, linesplit);
                    }
                    if (linesplit[0] == "wait")
                    {
                        Kernel.DelayCode(UInt32.Parse(linesplit[1]));
                    }
                }

            }
            catch(Exception ex)
            {
                SYS32.KernelPanic(ex, "error while executing TEXTtoUSL");
            }
        }
    }
}
