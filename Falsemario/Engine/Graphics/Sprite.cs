using Falsemario.Engine.Basic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Falsemario.Engine.Utils;

namespace Falsemario.Engine.Graphics
{
    public class Sprite
    {
        //public char?[,] CharArray { get; private set; }
        public Dot[,] Dots { get; private set; }
        public int Layer { get; set; } = 0;
        public int Width => Dots.GetLength(0);
        public int Height => Dots.GetLength(1);
        public Sprite(char?[,] charArray)
        {
            //this.CharArray = charArray;
            this.Dots = Dot.FromCharArray(charArray);
        }

        public Sprite(Dot[,] dots)
        {
            this.Dots = dots;
        }

        public static Sprite FromChar(char ch)
        {
            return new Sprite(new char?[,] { { ch } });
        }

        public Sprite FillWith(ConsoleApiWrap.ForegroundColor color)
        {
            var newSprite = this;
            foreach (var dot in newSprite.Dots)
            {
                if (dot != null)
                    dot.Foreground = color;
            }

            return newSprite;
        }
        
        public Sprite FillWith(ConsoleApiWrap.BackgroundColor color)
        {
            var newSprite = this;
            foreach (var dot in newSprite.Dots)
            {
                if (dot != null)
                    dot.Background = color;
            }

            return newSprite;
        }
    }
}
