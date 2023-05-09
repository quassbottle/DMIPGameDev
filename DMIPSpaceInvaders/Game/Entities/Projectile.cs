using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Engine.Graphics;
using DMIPSpaceInvaders.Game.Interfaces;

namespace DMIPSpaceInvaders.Game.Entities
{
    public class Projectile : GameObject, IDrawable, IMovable
    {
        protected Sprite _sprite;
        protected Rectangle _rectangle;
        protected Vector2D _velocity;
        
        public Projectile(Sprite spr, Point2D loc)
        {
            _sprite = spr;
            _rectangle = new Rectangle(spr.Width, spr.Height, loc);
        }

        public Projectile(Sprite spr, int x, int y) : this(spr, new Point2D() { X = x, Y = y })
        {
        }

        public Rectangle GetCollider() => _rectangle;

        public Sprite GetSprite() => _sprite;

        public Point2D GetLocation() => _rectangle.Location;

        public Vector2D GetVelocity() => _velocity;

        public void Move(int x, int y)
        {
            _rectangle.Location.X += x;
            _rectangle.Location.Y += y;
        }
    }
}