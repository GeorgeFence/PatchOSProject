using Cosmos.System;
using PatchOS.Files.Coroutines;
using PatchOS.Files.Drivers.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Drivers
{
    internal class Key : Process.Process
    {
        public static Coroutine cor = new Coroutine(key());
        public static bool Yield = false;
        public static KeyEvent keyevent = null;
        public static bool KeyPressed = false;
        public Key() : base("Key", Type.System, cor)
        {

        }
        static IEnumerator<CoroutineControlPoint> key() 
        {
            void make()
            {
                //Kernel.DelayCode(500);
                make();
            }
            make();
            yield return null;
        }
        public static void TryReadKey()
        {
            if (KeyboardManager.TryReadKey(out keyevent))
            {
                KeyPressed = true;
            }
            else
            {
                KeyPressed = false;
            }
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
