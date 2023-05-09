using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Game.Entities;

namespace DMIPSpaceInvaders.Game.Interfaces
{
    public interface IShootable
    {
        Bullet Shoot(int offsetX, int offsetY, Vector2D velocity);
    }
}