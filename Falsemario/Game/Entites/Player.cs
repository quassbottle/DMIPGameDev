using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DMIPSpaceInvaders.Game;
using DMIPSpaceInvaders.Game.Utils.Interop;
using Falsemario.Engine;
using Falsemario.Engine.Basic;
using Falsemario.Engine.Graphics;

namespace Falsemario.Game.Entites
{
    public class Player : GameObject, ICollidable, IMovable, IDamageable, IDrawable
    {
        public int Health { get; set; } = 3;
        
        private Vector2D _velocity = new Vector2D();
        private Rectangle _rect;
        private Sprite _sprite;
        
        public Player(Point2D loc)
        {
            _rect = new Rectangle(1, 1, loc);
            _sprite = Sprite.FromChar('8');
        }

        public Rectangle GetCollider() => _rect;

        public Vector2D GetVelocity() => _velocity;

        public void Move(int x, int y)
        {
            _rect.Location += new Vector2D()
            {
                X = x, Y = y
            };
        }

        public Sprite GetSprite() => _sprite;

        private int jT = 0;
        /*public override void Update(GameEngine engine)
        {
            var objBelow = engine.GetByLocation(this, new Point2D()
            {
                X = _rect.X,
                Y = _rect.Y + 1
            });

            if (WinApi.IsKeyPressed(WinApi.Keys.A) || WinApi.IsKeyPressed(WinApi.Keys.Left))
            {
                _velocity.X = -1;
            }
            else if (WinApi.IsKeyPressed(WinApi.Keys.D) || WinApi.IsKeyPressed(WinApi.Keys.Right))
            {
                _velocity.X = 1;
            }
            else
            {
                _velocity.X = 0;
            }
            
            
            var objNext = engine.GetByLocation(this, new Point2D()
            {
                X = _rect.X + _velocity.X,
                Y = _rect.Y
            });
            if (objNext is Brick)
            {
                _velocity.X = 0;
            }
            _velocity.Y = 1;
            if (objBelow is Brick)
            {
                _velocity.Y = 0;
                jT = 0;
            }
            if (WinApi.IsKeyPressed(WinApi.Keys.Space) && _velocity.Y == 0 && jT == 0)
            {
                _velocity.Y = -4;
                jT++;
            }

            if (objBelow is Enemy)
            {
                Console.Title += "a";
            }

            int dy = _velocity.Y != 0 ? _velocity.Y / Math.Abs(_velocity.Y) : 0;
            int sourceY = _rect.Location.Y;
            int destY = _rect.Location.Y + _velocity.Y;
            _rect.Location.X += _velocity.X;
            _rect.Location.Y = destY;
            for (int y = sourceY + dy; dy != 0 && y != destY; y += dy) {
                var obj = engine.GetByLocation(this, new Point2D()
                {
                    X =  _rect.Location.X,
                    Y = y
                });
                if (obj is Brick) {
                    _rect.Location.Y = y - dy;
                    break;
                }
                if (obj is Coin)
                {
                    engine.DisposeObject(obj);   
                }
                if (obj is Enemy)
                {
                    engine.DisposeObject(obj);
                    Console.Title = "1";
                }
            }
            
            //_rect.Location += _velocity;
        }*/
        public override void Update(GameEngine engine)
        {
            var bottomPoints = new Point2D(_rect.Location.X, _rect.Location.Y + 1);
            
            if (WinApi.IsKeyPressed(WinApi.Keys.Space) && _velocity.Y == 0 && CheckBricks(engine, bottomPoints))
            {
                _velocity.Y = -2;
            } else if (!CheckBricks(engine, bottomPoints)) {
                _velocity.Y++;
            }
            
            if (WinApi.IsKeyPressed(WinApi.Keys.A) || WinApi.IsKeyPressed(WinApi.Keys.Left))
            {
                _velocity.X = -1;
            }
            else if (WinApi.IsKeyPressed(WinApi.Keys.D) || WinApi.IsKeyPressed(WinApi.Keys.Right))
            {
                _velocity.X = 1;
            }
            else
            {
                _velocity.X = 0;
            }
            
            var playerPoints = new List<Point2D>() {
                _rect.Location
                //new Point2D(_rect.Location.X, _rect.Location.Y + 1)
            }; 
            
            
            
            var velX = new Vector2D() { X = _velocity.X, Y = 0 };
            var nextPoints = playerPoints.Select(point => point + velX).ToList();
            if (CheckBricks(engine, nextPoints[0])) {
                _velocity.X = 0;
                velX.X = 0;
            }
            
            _rect.Location += velX;
            
            int startY = _rect.Location.Y;
            int destY = _rect.Location.Y + _velocity.Y;
            _rect.Location.Y = destY;
            int dY = _velocity.Y != 0 ? (_velocity.Y / Math.Abs(_velocity.Y)) : 0;
            for (int y = startY; dY != 0 && y != destY; y += dY) {
                var diffY = new Vector2D(0, y - startY);
                var newPlayerPoints = playerPoints.Select(point => point + velX + diffY).ToList();
                if (CheckBricks(engine, newPlayerPoints)) {
                    _rect.Location.Y = y - dY;
                    _velocity.Y = 0;      
                    break;
                }
                foreach (Point2D point in newPlayerPoints) {
                    var obj = engine.GetByLocation(this, point);
                    if (obj is Coin)
                    {
                        engine.DisposeObject(obj);   
                        engine.Score += 1;
                    }
                    if (obj is Enemy)
                    {
                        engine.DisposeObject(obj);
                        engine.Score += 100;
                    }
                }
            }

            if (_rect.Y > Program.Level.Objects.GetLength(1))
            {
                this.Health--;
                _rect.Location = Program.Spawn;
            }
            if (Health < 0 || _rect.X > Program.Level.Objects.GetLength(0))
            {
                engine.Stop();
                engine.DisposeObject(this);
            }
            
            if (CheckEnemies(engine, nextPoints[0]))
            {
                this.Health--;
                _rect.Location = Program.Spawn;
            }
        }
        
        public bool CheckBricks(GameEngine engine, List<Point2D> points) {
            foreach (Point2D point in points) {
                var obj = engine.GetByLocation(this, point);
                if (obj is Brick || point.X < 0) {
                    return true;
                }
            }
            return false;
        }

        public bool CheckEnemies(GameEngine engine, Point2D point)
        {
            var obj = engine.GetByLocation(this, point);
            return obj is Enemy || point.X < 0;
        }

        public bool CheckBricks(GameEngine engine, Point2D point)
        {
            var obj = engine.GetByLocation(this, point);
            return obj is Brick || point.X < 0;
        }
    }
}