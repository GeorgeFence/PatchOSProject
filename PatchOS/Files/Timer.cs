using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files
{
    public class Timer
    {
        private PIT.PITTimer timer;
        public Timer(Action act, object value, int MS, bool repeat)
        {
            timer = new PIT.PITTimer(act, (uint)MS, repeat);
        }
    }
}
