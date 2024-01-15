using PatchOS.Files.Coroutines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Process
{
    public abstract class Process
    {
        internal Process(string name, Type type, Coroutine coroutine)
        {
            this.name = name;
            this.type = type;
            this.coroutine = coroutine;
        }
        internal int id;
        internal uint usageRAM;
        internal string name;
        internal enum Type 
        {
            User,
            Application,
            Service,
            System
        }
        internal Type type;
        internal Coroutine coroutine;
        internal abstract int Start(); //returns: 0 - ok, -1 - silent exit/proceess handles displaying message by itself, any other code - unexpected exit

        internal abstract void Stop();
    }
}
