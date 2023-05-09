using System;

namespace DMIPSpaceInvaders.Game.Utils
{
    public static class RNGColor
    {
        private static ConsoleColor[] All => Enum.GetValues(typeof(ConsoleColor)) as ConsoleColor[];
        
        public static ConsoleColor Get() => All[ThreadLocalRandom.Get().Next(0, All.Length)];
    }
}