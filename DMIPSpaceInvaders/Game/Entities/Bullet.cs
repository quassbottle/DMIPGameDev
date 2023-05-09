using System;
using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Engine.Graphics;

namespace DMIPSpaceInvaders.Game.Entities
{
    public class Bullet : Projectile
    {
        public GameObject Sender { get; private set; }

        public Bullet(Vector2D vel, GameObject sender, Point2D loc) : base(Sprite.FromChar('|'), loc)
        {
            this.Sender = sender;
            this._velocity = vel;
        }

        public Bullet(Vector2D vel, GameObject sender, int x, int y) : this(vel, sender, new Point2D() {X = x, Y = y})
        {
        }

        public override void Update(GameEngine engine)
        {
            _rectangle.Location += _velocity;
            if (_rectangle.Location.Y < 0 || _rectangle.Location.Y >= Console.BufferHeight)
            {
                if (Sender is Player)
                {
                    Program.PlayerBullets.Remove(this);
                }
                engine.DisposeObject(this);
            }
        }
    }
}