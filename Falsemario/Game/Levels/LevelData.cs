using System;
using Falsemario.Engine;
using Falsemario.Engine.Basic;

namespace Falsemario.Game.Levels
{
    public class LevelData
    {
        public Tuple<Point2D, Type>[,] Objects { get; }
        
        public LevelData(Tuple<Point2D, Type>[,] level)
        {
            Objects = level;
        }
    }
}