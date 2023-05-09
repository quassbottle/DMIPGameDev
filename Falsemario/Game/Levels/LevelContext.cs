using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Falsemario.Engine;

namespace Falsemario.Game.Levels
{
    public class LevelContext
    {
        public List<Tuple<Color, Type>> Context { get; } = new List<Tuple<Color, Type>>();

        public LevelContext(){}

        public LevelContext(params Tuple<Color, Type>[] objects)
        {
            foreach (var pair in objects.ToArray())
            {
                Context.Add(pair);
            }
        }
        
        public void Add(Color c, Type g)
        {
            Context.Add(Tuple.Create(c, g));
        }
    }
}