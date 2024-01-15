using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.Coroutines
{
    public abstract class CoroutineControlPoint
    {
        public abstract bool CanContinue { get; }
    }
}
