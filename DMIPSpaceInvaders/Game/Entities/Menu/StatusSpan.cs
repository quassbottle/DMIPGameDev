using System;
using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Engine.Graphics;
using DMIPSpaceInvaders.Engine.Utils;
using DMIPSpaceInvaders.Game.Interfaces;

namespace DMIPSpaceInvaders.Game.Entities.Menu
{
    public class StatusSpan : GameObject, IDrawable, ICollidable
    {
        public string Text { get; set; } = "";
        
        private Sprite _sprite;
        private Rectangle _rect;
        private int _width;
        private ConsoleApiWrap.BackgroundColor _bgColor;
        private ConsoleApiWrap.ForegroundColor _fgColor;

        public Sprite GetSprite() => GetClearLine();

        public StatusSpan(int width, ConsoleApiWrap.BackgroundColor bg, ConsoleApiWrap.ForegroundColor fg, string text, int x, int y)
        {
            _width = width;
            _bgColor = bg;
            _fgColor = fg;
            _rect = new Rectangle(width, 1, x, y);
            Text = text;
        }

        private Sprite GetClearLine()
        {
            Dot[,] dots = new Dot[_width, 1];
            for (int i = 0; i < _width; i++)
            {
                dots[i, 0] = new Dot()
                {
                    Background = _bgColor,
                    Foreground = _fgColor
                };
                if (i < this.Text.Length)
                {
                    char? ch = this.Text[i];
                    dots[i, 0].Thing = ch.Value;
                }
                else
                {
                    dots[i, 0].Thing = ' ';
                }
            }

            var spr = new Sprite(dots);
            spr.Layer = 1000;
            return spr;
        }

        public Rectangle GetCollider() => _rect;
    }
}