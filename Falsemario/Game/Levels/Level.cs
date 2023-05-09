using System;
using Falsemario.Engine;

namespace Falsemario.Game.Levels
{
    public class Level
    {
        public LevelData Data { get; }
        public GameObject[,] Objects { get; }
        
        public Level(LevelData data)
        {
            this.Data = data;
            this.Objects = new GameObject[data.Objects.GetLength(0), data.Objects.GetLength(1)];
            foreach (var obj in data.Objects)
            {
                var point = obj.Item1;
                var type = obj.Item2;
                if (type != null && type.BaseType == typeof(GameObject))
                {
                    Objects[point.X, point.Y] = Activator.CreateInstance(type, point) as GameObject;
                }
            }
        }
    }
}