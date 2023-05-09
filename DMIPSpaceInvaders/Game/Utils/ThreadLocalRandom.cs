using System;
using System.Threading;

namespace DMIPSpaceInvaders.Game.Utils
{
    public static class ThreadLocalRandom
    {
        private static int _seed = Environment.TickCount;
        
        private static readonly ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        public static Random Get() => randomWrapper.Value;
    }
}