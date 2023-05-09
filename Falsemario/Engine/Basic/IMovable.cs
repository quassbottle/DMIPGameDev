using System;
using System.Collections.Generic;
using System.Text;
 using Falsemario.Engine.Basic;

 namespace Falsemario.Engine.Basic
{
    public interface IMovable : ICollidable
    {
        Vector2D GetVelocity();
        void Move(int x, int y);
    }
}
