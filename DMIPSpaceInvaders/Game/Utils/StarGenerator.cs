using System;
using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Game.Entities;

namespace DMIPSpaceInvaders.Game.Utils
{
    public static class StarGenerator
    {
        private static Random _rnd => ThreadLocalRandom.Get();

        public static Projectile GenerateAt(Point2D loc)
        {
            return new Star(new Vector2D() {Y = _rnd.Next(-5, -1)}, loc);
        }
    }
}