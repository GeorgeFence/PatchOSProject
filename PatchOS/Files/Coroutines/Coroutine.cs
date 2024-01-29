using PatchOS.Files.Coroutines;
using System;
using System.Collections.Generic;

namespace PatchOS.Files.Coroutines
{
    public class Coroutine
    {
        private readonly IEnumerator<CoroutineControlPoint?> executor;

        public CoroutineControlPoint? CurrentControlPoint => executor.Current;

        public bool Running { get; internal set; }

        public bool Halted { get; set; }

        public CoroutinePool? Pool { get; private set; }

        public bool ExecutionEnded { get; private set; }

        public Coroutine(IEnumerator<CoroutineControlPoint?> executor)
        {
            this.executor = executor;
        }

        internal void Join(CoroutinePool pool)
        {
            Pool = pool;
        }

        internal void Exit()
        {
            Pool = null;
        }

        internal void Step()
        {
            try
            {
                ExecutionEnded = !executor.MoveNext();
            }
            catch(Exception ex)
            {
                SYS32.KernelPanic(ex, "Coroutine Step");
            }
        }

        public void Start()
        {
            if (Running)
            {
                throw new InvalidOperationException("Cannot start an already running Coroutine.");
            }

            CoroutinePool.Main.AddCoroutine(this);
        }

        public void Stop()
        {
            Pool?.RemoveCoroutine(this);
            Exit();
        }
    }
}
