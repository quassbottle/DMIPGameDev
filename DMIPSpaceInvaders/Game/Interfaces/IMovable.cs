using DMIPSpaceInvaders.Engine.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMIPSpaceInvaders.Game.Interfaces
{
    public interface IMovable : ICollidable
    {
        Vector2D GetVelocity();
        void Move(int x, int y);
    }
}
