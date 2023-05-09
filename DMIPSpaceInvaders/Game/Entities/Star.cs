using System;
using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Engine.Graphics;
using DMIPSpaceInvaders.Engine.Utils;

namespace DMIPSpaceInvaders.Game.Entities
{
    public class Star : Projectile
    {
        public Star(Vector2D vel, Point2D loc) : base(Sprite.FromChar('*'), loc)
        {
            this._sprite.Dots[0, 0].Foreground = ConsoleApiWrap.ForegroundColor.White;
            this._velocity = vel;
            this._sprite.Layer = -1;
        }

        public override void Update(GameEngine engine)
        {
            this.Move(_velocity.X, _velocity.Y);
            if (_rectangle.Location.Y < 0)
                engine.DisposeObject(this);
            //engine.GetGraphics().DrawSprite(this._sprite, this._rectangle.Location);
        }
    }
}