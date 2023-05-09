using System;
using System.Collections.Generic;
using System.Text;
using DMIPSpaceInvaders.Engine.Basic;

namespace DMIPSpaceInvaders.Game.Interfaces
{
    public interface ICollidable
    {
        Rectangle GetCollider();
    }
}
