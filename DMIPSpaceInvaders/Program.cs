using DMIPSpaceInvaders.Engine.Basic;
using DMIPSpaceInvaders.Engine.Graphics;
using DMIPSpaceInvaders.Game.Entities;
using DMIPSpaceInvaders.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DMIPSpaceInvaders.Engine.Utils;
using DMIPSpaceInvaders.Game.Entities.Menu;
using DMIPSpaceInvaders.Game.Utils;
using DMIPSpaceInvaders.Game.Utils.Interop;

namespace DMIPSpaceInvaders
{
    public static class GameDifficultyExtensions
    {
        public static string GetName(this GameDifficulty self)
        {
            switch (self)
            {
                case GameDifficulty.EASY:
                    return "Легкая";
                case GameDifficulty.NORMAL:
                    return "Нормальная";
                default:
                    return "Сложная";
            }
        }
    }
    public enum GameDifficulty
    {
        EASY = 100,
        NORMAL = 150,
        HARD = 200
    }

    class Program
    {
        static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        private static char?[,] _enemyShipTexture = new char?[,]
        {
            {'}', null},
            {'#', null},
            {'#', '0'},
            {'#', null},
            {'{', null}
        };

        private static char?[,] ship = new char?[,]
        {
            {null, '}'},
            {null, '#'},
            {'0', '#'},
            {null, '#'},
            {null, '{'}
        };

        public static readonly List<Bullet> PlayerBullets = new List<Bullet>();
        public static int Score { get; set; }
        public static bool IsGameOver { get; set; } = false;
        public static GameDifficulty SelectedDifficulty { get; private set; } = GameDifficulty.EASY;
        
        private int EnemiesExist => _gameEngine.GetAllIf(obj => obj is EnemyShip).Count;
        private string ScorePath => $"{Environment.CurrentDirectory}/score.txt";
        
        private int _shootDelay;
        private int _initShootDelay;
        private int _enemyShootDelayBuff;
        private int _enemyShootDelay;
        private int _enterPressedCount;
        private GameEngine _gameEngine;
        private ConsoleApiWrap _consoleApiWrap = new ConsoleApiWrap();
        private Random _rnd = new Random();
        private Player _player;
        private StatusSpan _statusSpan;
        private string _statusSpanText;

        private async Task MainAsync(string[] args)
        {
           Console.OutputEncoding = Encoding.GetEncoding("windows-1251");
            WinApi.SetConsoleOutputCP(1251);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(191, 50);
            Console.SetBufferSize(191, 50);
            Console.CursorVisible = false;
            Console.Title = "Space Invaders";
            DisableResize();
            
            if (!File.Exists(ScorePath))
            {
                File.Create(ScorePath);
            }
            
            SetupGame();

            _initShootDelay = 15 * ((int) SelectedDifficulty / 100);
            _enemyShootDelay = 20 / ((int) SelectedDifficulty / 100);
            
            StartGame();
            await BeginUpdating();
        }

        private void SetupGame()
        {
            Console.Clear();
            Console.WriteLine(@"  ____  __  __ ___ ____    ____                         ___                     _               
 |  _ \|  \/  |_ _|  _ \  / ___| _ __   __ _  ___ ___  |_ _|_ ____   ____ _  __| | ___ _ __ ___ 
 | | | | |\/| || || |_) | \___ \| '_ \ / _` |/ __/ _ \  | || '_ \ \ / / _` |/ _` |/ _ \ '__/ __|
 | |_| | |  | || ||  __/   ___) | |_) | (_| | (_|  __/  | || | | \ V / (_| | (_| |  __/ |  \__ \
 |____/|_|  |_|___|_|     |____/| .__/ \__,_|\___\___| |___|_| |_|\_/ \__,_|\__,_|\___|_|  |___/
                                |_|                                                             
                                                                                                          ");
            Console.WriteLine("Управление:\n" +
                              "Стрелочки влево/вправо; Кнопки A и D - движение влево и право\n" +
                              "Пробел - выстрел\n");
            var difs = Enum.GetValues(typeof(GameDifficulty)) as GameDifficulty[];
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{difs[i].GetName()} - {i + 1}");
            }
            
            Console.Write("Введите номер сложности: ");
            int.TryParse(Console.ReadKey().KeyChar.ToString(), out int key);
            if (key > 3 || key <= 0)
            {
                SetupGame();
            }
            Console.WriteLine();
            SelectedDifficulty = difs[key == 0 ? 2 : Math.Min(key, 3) - 1];
        }

        private void WriteScore(int score)
        {
            File.AppendAllText(ScorePath, $"{DateTime.Now.ToString()} - {score} очков\n");
        }

        private List<string> GetSortedScores()
        {
            return File.ReadAllLines(ScorePath).ToList()
                .OrderByDescending(str => int.Parse(str.Trim().Replace("очков", "").Split('-')[1])).ToList();
        }
        
        private void RespawnEnemies()
        {
            for (int offsetY = 4; offsetY <= 14; offsetY += 4)
            {
                for (int offsetX = 1; offsetX < 189; offsetX += 21)
                {
                    var enemyShip = new EnemyShip(new Point2D() {X = offsetX, Y = offsetY},
                        new Sprite(_enemyShipTexture).FillWith(ConsoleApiWrap.ForegroundColor.Red));
                    _gameEngine.CreateInstance(enemyShip);
                }
            }
        }
        private void DisableResize()
        {
            var currentWindowHandle = WinApi.GetConsoleWindow();
            var dwNewLong = new IntPtr(~(0x00020000 | 0x00010000 | 0x00040000) &
                                       WinApi.GetWindowLongPtr(currentWindowHandle, -16).ToInt32());
            WinApi.SetWindowLongPtr(new HandleRef(this, currentWindowHandle), -16,
                dwNewLong); // отключает возможность изменения размера окна консоли
        }
        private void StartGame()
        {
            var sprite = new Sprite(ship);
            _player = new Player(new Point2D() {X = 1, Y = 47}, sprite.FillWith(ConsoleApiWrap.ForegroundColor.Blue));

            _statusSpan = new StatusSpan(191, ConsoleApiWrap.BackgroundColor.Green,
                ConsoleApiWrap.ForegroundColor.Black, "привет мир", 0, 1);
            _gameEngine = new GameEngine(Console.BufferWidth, Console.BufferHeight);

            _gameEngine.CreateInstance(_player);
            _gameEngine.CreateInstance(_statusSpan);
            _gameEngine.CreateInstance(new StatusSpan(191, ConsoleApiWrap.BackgroundColor.Green,
                ConsoleApiWrap.ForegroundColor.Black, '='.Repeat(Console.BufferWidth), 0, 0));
            _gameEngine.CreateInstance(new StatusSpan(191, ConsoleApiWrap.BackgroundColor.Green,
                ConsoleApiWrap.ForegroundColor.Black, '='.Repeat(Console.BufferWidth), 0, 2));

            RespawnEnemies();

            _gameEngine.Start();
        }
        
        private async Task EnemyShoot()
        {
            if (_enemyShootDelayBuff < _enemyShootDelay)
            {
                _enemyShootDelayBuff++;
                return;
            }

            var everyShip = _gameEngine.GetAllIf(s => s is EnemyShip);
            for (int x = 0; x < Console.BufferWidth; x++)
            {
                foreach (var gameObject in everyShip)
                {
                    var enemyShip = gameObject as EnemyShip;

                    int offsetY = enemyShip.GetLocation().Y + 4;
                    var shipsHere = _gameEngine.GetAllIf(s => s is EnemyShip eS && eS.GetLocation().X == x);
                    if (shipsHere.Count == 0)
                        break;

                    if (!shipsHere.Exists(obj => (obj as EnemyShip).GetCollider().Y == offsetY))
                    {
                        if (ThreadLocalRandom.Get().Next(100) == 1)
                            _gameEngine.CreateInstance(enemyShip.Shoot(2, 1, new Vector2D() {Y = 1}));
                    }

                }
            }

            _enemyShootDelayBuff = 0;
            await Task.CompletedTask;
        }
        private async Task BeginUpdating()
        {
            while (true)
            {
                _statusSpanText = !IsGameOver
                    ? $"Осталось жизней: {_player.Health} |" +
                      $" Набрано очков: {Score} |" +
                      $" Уничтожено вражеских кораблей: {Score / 100} (ост. {EnemiesExist}) |" +
                      $" Сложность: {SelectedDifficulty.GetName()}"
                    : "Игра окончена! Нажмите ENTER чтобы продолжить.";
                
                _gameEngine.CreateInstance(StarGenerator.GenerateAt(new Point2D() { X =  ThreadLocalRandom.Get().Next(0, 210), Y = 50 }));

                if (!IsGameOver)
                {
                    if (WinApi.IsKeyPressed(WinApi.Keys.A) || WinApi.IsKeyPressed(WinApi.Keys.Left))
                    {
                        _player.GetVelocity().X = -1;
                    }
                    else if (WinApi.IsKeyPressed(WinApi.Keys.D) || WinApi.IsKeyPressed(WinApi.Keys.Right))
                    {
                        _player.GetVelocity().X = 1;
                    }
                    else
                    {
                        _player.GetVelocity().X = 0;
                    }

                    if (WinApi.IsKeyPressed(WinApi.Keys.Space))
                    {
                        if (_shootDelay <= 0)
                        {
                            var playerBullet = _player.Shoot(2, -1, new Vector2D() {Y = -1});
                            PlayerBullets.Add(playerBullet);
                            _gameEngine.CreateInstance(playerBullet);
                            _shootDelay = _initShootDelay;
                        }
                    }

                    await EnemyShoot();
                    
                    if (EnemiesExist <= 0)
                    {
                        RespawnEnemies();
                    }
                }
                else
                {
                    if (_enterPressedCount < 1)
                    {
                        _gameEngine.Stop();
                        _gameEngine.UpdateManually(_statusSpan);
                        _gameEngine.DrawManually(_statusSpan);
                        if (WinApi.IsKeyPressed(WinApi.Keys.Enter))
                        {
                            WriteScore(Score);
                            var buffer = _consoleApiWrap.CreateBuffer(ConsoleApiWrap.DesiredAccess.BOTH,
                                ConsoleApiWrap.ShareMode.MAGIC_VALUE);
                            _consoleApiWrap.SetBuffer(buffer);
                            _consoleApiWrap.WriteAt(buffer, "Таблица рекордов", 0, 0);

                            var sorted = GetSortedScores();
                            for (int i = 0; i < (sorted.Count >= 10 ? 10 : sorted.Count); i++)
                            {
                                _consoleApiWrap.WriteAt(buffer, sorted[i], 0, i + 1);
                            }

                            _enterPressedCount++;
                        }
                    }
                }

                _statusSpan.Text = _statusSpanText;
                await Task.Delay(50);
                _shootDelay--;
            }
        }
    }
}
