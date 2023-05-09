using System;
using DMIPSpaceInvaders.Game;
using Falsemario.Engine;
using Falsemario.Game.Entites;

namespace Falsemario.Game
{
    public class TitleController : GameObject
    {
        public override void Update(GameEngine engine)
        {
            if (engine.IsRunning())
            {
                Console.Title =
                    $"Очки: {engine.Score} | Жизни: {((Player) engine.GetAllIf(obj => obj is Player)[0]).Health}";
            }
            else
            {
                Console.Title = $"Игра окончена (Вы заработали {engine.Score} очков!)";
            }
        }
    }
}