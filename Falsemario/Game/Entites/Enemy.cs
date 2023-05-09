using System.Threading.Tasks;
using DMIPSpaceInvaders.Game;
using DMIPSpaceInvaders.Game.Utils;
using Falsemario.Engine;
using Falsemario.Engine.Basic;
using Falsemario.Engine.Graphics;

namespace Falsemario.Game.Entites
{
    public class Enemy : GameObject, IMovable, ICollidable, IDrawable
    {
        private Vector2D _velocity = new Vector2D();
        private Rectangle _rect;
        private Sprite _sprite;
        
        public Enemy(Point2D loc)
        {
            _rect = new Rectangle(1, 1, loc);
            _sprite = Sprite.FromChar('o');
            _velocity.X = 1;
        }

        public Rectangle GetCollider() => _rect;

        public Vector2D GetVelocity() => _velocity;

        public void Move(int x, int y)
        {
            _rect.Location += _velocity;
        }

        public Sprite GetSprite() => _sprite;

        private int delay = 0;
        public override async void Update(GameEngine engine)
        {
            var objNext = engine.GetByLocation(this, new Point2D()
            {
                X = _rect.X + _velocity.X,
                Y = _rect.Y
            });
            var objNextBottom = engine.GetByLocation(this, new Point2D()
            {
                X = _rect.X + _velocity.X,
                Y = _rect.Y + 1
            });

            if (objNextBottom == null || (objNext != null && !(objNext is Enemy) && !(objNext is Player))) 
            {
                _velocity.X *= -1;
            }

            delay++;
            if (delay == 5)
            {
                _rect.Location += _velocity;
                delay = 0;
            }
        }
    }
}