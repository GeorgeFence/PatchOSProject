using Cosmos.System;
using PatchOS.Files.Coroutines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PatchOS.Files.Drivers
{
    internal class Key : Process.Process
    {
        public static Coroutine process = new Coroutine(key());
        public static bool Yield = false;
        public static KeyEvent keyevent = null; 
        public Key() : base("Key", Type.Service, process)
        {

        }
        static IEnumerator<CoroutineControlPoint> key()
        {
            while (!Yield)
            {
                keyevent = null;
                KeyboardManager.TryReadKey(out keyevent);
            }
            yield return null;
        }
        internal override int Start()
        {
            return 0;
        }

        internal override void Stop()
        {
            Yield = true;
        }
    }
}
