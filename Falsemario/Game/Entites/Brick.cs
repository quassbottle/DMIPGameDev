using Falsemario.Engine;
using Falsemario.Engine.Basic;
using Falsemario.Engine.Graphics;

namespace Falsemario.Game.Entites
{
    public class Brick : GameObject, IDrawable, ICollidable
    {
        private Rectangle _rect;
        private Sprite _sprite;
        
        public Brick(Point2D loc)
        {
            _rect = new Rectangle(1, 1, loc);
            _sprite = Sprite.FromChar('#');
        }

        public Rectangle GetCollider() => _rect;
        public Sprite GetSprite() => _sprite;
    }
}