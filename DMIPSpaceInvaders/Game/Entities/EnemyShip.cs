using System;
using System.Threading.Tasks;
using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Engine.Graphics;
using DMIPSpaceInvaders.Game.Interfaces;
using DMIPSpaceInvaders.Game.Utils;

namespace DMIPSpaceInvaders.Game.Entities
{
    public class EnemyShip : GameObject, IMovable, IDrawable, IDamageable, IShootable
    {
        public int Health { get; set; } = 1;
        
        private Rectangle _rectangle;
        private Sprite _sprite;
        private Vector2D _velocity;
        private readonly Random _random = new Random();
        
        private int _buffX;
        private bool _collided;
        private int _moved;
        private int _delay;
        private int _initDelay;
        
        public EnemyShip(Point2D loc, Sprite spr)
        {
            this._rectangle = new Rectangle(spr.Width, spr.Height, loc);
            this._sprite = spr;
            this._velocity = new Vector2D();
            this._buffX = _rectangle.X;
            this._initDelay = 30 / ((int) Program.SelectedDifficulty / 100);
            this._delay = _initDelay;
        }

        public Rectangle GetCollider() => _rectangle;

        public Vector2D GetVelocity() => _velocity;
        public Sprite GetSprite() => _sprite;

        public Point2D GetLocation() => _rectangle.Location;

        public void Move(int x, int y)
        {
            _rectangle.X += x;
            _rectangle.Y += y;
        }

        public override void Update(GameEngine engine)
        {
            if (Program.IsGameOver)
                return;
            
            foreach (Bullet bullet in Program.PlayerBullets.Copy())
            {
                if (bullet != null && bullet.GetCollider().IntersectsWith(this.GetCollider()))
                {
                    engine.DisposeObject(this);
                    engine.DisposeObject(bullet);
                    Program.PlayerBullets.Remove(bullet);
                    Program.Score += (int)Program.SelectedDifficulty;
                }
            }

            _delay--;
            if (_delay == 0)
            {
                _delay = _initDelay;
                if (this.GetLocation().X - _buffX == (_collided ? -15 : 15))
                {
                    if (_moved == 4)
                    {
                        Program.IsGameOver = true;
                        return;
                    }
                    Move(0, 7);
                    _moved++;
                    _buffX = this.GetLocation().X;
                    _collided = !_collided;
                    return;
                }
                Move(_collided ? -1 : 1, 0);
            }
            
        }

        public Bullet Shoot(int offsetX, int offsetY, Vector2D velocity)
        {
            return new Bullet(velocity, this, GetLocation().X + offsetX, GetLocation().Y + offsetY);
        }
    }
}