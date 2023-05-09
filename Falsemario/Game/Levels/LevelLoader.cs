using System;
using System.Collections.Generic;
using System.Drawing;
using Falsemario.Engine;
using Falsemario.Engine.Basic;

namespace Falsemario.Game.Levels
{
    public class LevelLoader
    {
        public LevelContext Context { get; }
        public List<LevelData> Levels { get; } = new List<LevelData>();
        
        public LevelLoader(LevelContext context)
        {
            this.Context = context;
        }
        
        public LevelData Load(string file)
        {
            var img = Bitmap.FromFile(file) as Bitmap;
            var lvl = new LevelData(new Tuple<Point2D, Type>[img.Width, img.Height]);
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    lvl.Objects[x, y] = Tuple.Create(new Point2D()
                    {
                        X = x, Y = y
                    }, ByColor(img.GetPixel(x, y)));
                }
            }

            return lvl;
        }

        private Type ByColor(Color clr)
        {
            return Context.Context.Find(tuple => tuple.Item1 == clr)?.Item2;
        }
    }
}