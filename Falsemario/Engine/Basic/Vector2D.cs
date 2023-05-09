using System;
using System.Collections.Generic;
using System.Text;

namespace Falsemario.Engine.Basic
{
    public class Vector2D
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2D()
        {
        }
    }
}
