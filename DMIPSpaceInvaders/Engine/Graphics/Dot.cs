using System;
using DMIPSpaceInvaders.Engine.Utils;

namespace DMIPSpaceInvaders.Engine.Graphics
{
    public class Dot
    {
        //public ConsoleColor Color { get; set; } = ConsoleColor.Cyan;
        public ConsoleApiWrap.ForegroundColor Foreground { get; set; } = ConsoleApiWrap.ForegroundColor.Gray;
        public ConsoleApiWrap.BackgroundColor Background { get; set; } = ConsoleApiWrap.BackgroundColor.Black;
        public char Thing { get; set; }

        public static explicit operator char(Dot pix)
        {
            return pix.Thing;
        }

        public static explicit operator Dot(char c)
        {
            return new Dot() {Thing = c};
        }
        
        public static Dot[,] FromCharArray(char?[,] chars)
        {
            int width = chars.GetLength(0);
            int height = chars.GetLength(1);
            Dot[,] pixels = new Dot[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    var value = chars[i, k];
                    if (value.HasValue)
                        pixels[i, k] = new Dot() { Thing = value.Value };
                }
            }

            return pixels;
        }
    }
}