using System;
using System.Drawing;
using System.Threading.Tasks;
using DMIPSpaceInvaders.Game;
using Falsemario.Engine;
using Falsemario.Engine.Basic;
using Falsemario.Game;
using Falsemario.Game.Entites;
using Falsemario.Game.Levels;

namespace Falsemario
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ShowMenu();
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public static Point2D Spawn { get; private set; }
        public static Level Level { get; private set; }
        private async Task MainAsync()
        {
            var lvlLoader = new LevelLoader(new LevelContext(Tuple.Create(Color.FromArgb(255, 0, 0, 0), typeof(Brick)),
                Tuple.Create(Color.FromArgb(255, 255, 0, 0), typeof(Enemy)),
                Tuple.Create(Color.FromArgb(255, 0, 255, 0), typeof(Coin)),
                Tuple.Create(Color.FromArgb(255, 0, 0, 255), typeof(Player))));
            var test = lvlLoader.Load(@"level.bmp");
            var lvl = new Level(test);
            Console.SetWindowSize(lvl.Objects.GetLength(0), lvl.Objects.GetLength(1));
            var gameEngine = new GameEngine(lvl.Objects.GetLength(0), lvl.Objects.GetLength(1));
            foreach (var obj in lvl.Objects)
            {
                if (obj is Player pl)
                {
                    Spawn = pl.GetCollider().Location;
                }
                if (obj != null)
                    gameEngine.CreateInstance(obj);
            }
            Level = lvl;
            gameEngine.CreateInstance(new TitleController());
            gameEngine.Start();
            await Task.Delay(-1);
        }

        private static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
███████╗ █████╗ ██╗     ███████╗███████╗███╗   ███╗ █████╗ ██████╗ ██╗ ██████╗ 
██╔════╝██╔══██╗██║     ██╔════╝██╔════╝████╗ ████║██╔══██╗██╔══██╗██║██╔═══██╗
█████╗  ███████║██║     ███████╗█████╗  ██╔████╔██║███████║██████╔╝██║██║   ██║
██╔══╝  ██╔══██║██║     ╚════██║██╔══╝  ██║╚██╔╝██║██╔══██║██╔══██╗██║██║   ██║
██║     ██║  ██║███████╗███████║███████╗██║ ╚═╝ ██║██║  ██║██║  ██║██║╚██████╔╝
╚═╝     ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝ ╚═════╝ ");
            Console.WriteLine("Игра разработана за 3 часа для олимпиады ДМИП-IT");
            Console.WriteLine("Управление:\n- Стрелочки влево/вправо или A/D для перемещения влево и вправо соответственно\n- Пробел для прыжка");
            Console.WriteLine("8 - персонаж\no - враг\n$ - монеты\n# - стена");
            Console.WriteLine("Основная информация игры будет написана в названии приложения. Чтобы победить, нужно выйти за правую часть карты. Приятной игры!");
            Console.WriteLine("Игра загружает уровень из файла level.bmp, так что проверьте, есть ли он в директории испольняемого файла. \nПри желании уровень можно отредактировать");
            Console.WriteLine("Нажмите любую клавишу, чтобы начать игру");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}