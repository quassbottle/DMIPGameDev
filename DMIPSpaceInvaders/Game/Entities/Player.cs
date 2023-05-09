using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Engine.Graphics;
using DMIPSpaceInvaders.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DMIPSpaceInvaders.Engine.Utils;
using DMIPSpaceInvaders.Game.Utils;

namespace DMIPSpaceInvaders.Game.Entities
{
    public class Player : GameObject, IDrawable, IMovable, ICollidable, IDamageable, IShootable
    {
        public int Health { get; set; } = 3;
        public bool CanMove { get; set; } = true;

        private Rectangle _rectangle;
        private Vector2D _velocity;
        private readonly Sprite _initSprite;
        private Sprite _sprite;

        public Player(Point2D loc, Sprite spr)
        {
            this._rectangle = new Rectangle(spr.Width, spr.Height, loc);
            spr.Layer = 5;
            _initSprite = spr;
            this._sprite = _initSprite;
            this._velocity = new Vector2D();
        }

        public Point2D GetLocation() => _rectangle.Location;

        public Sprite GetSprite() => _sprite;

        public Vector2D GetVelocity() => _velocity;

        public Rectangle GetCollider() => _rectangle;
        public override void Update(GameEngine engine)
        {
            foreach (Bullet bullet in engine.GetAllIf(obj => obj is Bullet bul && bul.Sender is EnemyShip))
            {
                if (bullet != null && bullet.GetCollider().IntersectsWith(this.GetCollider()))
                {
                    engine.DisposeObject(bullet);
                    if (Health == 0)
                    {
                        Program.IsGameOver = true;
                        engine.DisposeObject(this);
                    }
                    Health--;
                }
            }
            
            if (_rectangle.Location.X + _velocity.X > 0 && _rectangle.Location.X + _rectangle.Width + _velocity.X <= Console.BufferWidth) //todo
                _rectangle.Location += _velocity;
        }

        public void Move(int x, int y)
        {
            _rectangle.Location.X += x;
            _rectangle.Location.Y += y;
        }

        public Bullet Shoot(int offsetX, int offsetY, Vector2D velocity)
        {
            return new Bullet(velocity, this, GetLocation().X + offsetX, GetLocation().Y + offsetY);
        }
    }
}
