using System;
using System.Collections.Generic;
using System.Text;

namespace Falsemario.Engine.Basic
{
    public class Point2D
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Point2D()
        {
        }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point2D operator +(Point2D l, Vector2D v)
        {
            return new Point2D
            { 
                X = l.X + v.X,
                Y = l.Y + v.Y
            };
        }

        public static Point2D operator -(Point2D l, Vector2D v)
        {
            return new Point2D
            {
                X = l.X - v.X,
                Y = l.Y - v.Y
            };
        }
    }
}
