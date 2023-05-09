using DMIPSpaceInvaders.Engine.Basic;
using System;
using System.Collections.Generic;
using System.Text;
using DMIPSpaceInvaders.Engine.Utils;

namespace DMIPSpaceInvaders.Engine.Graphics
{
    public class Blackboard
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        //private char?[,] _board;

        private Dot[,] _dots;

        private IntPtr _consoleBuffer;
        private ConsoleApiWrap _consoleApiWrap = new ConsoleApiWrap();

        public Blackboard(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            //_board = new char?[width, height];
            _dots = new Dot[width, height];
            _consoleBuffer = _consoleApiWrap.CreateBuffer(ConsoleApiWrap.DesiredAccess.BOTH,
                ConsoleApiWrap.ShareMode.MAGIC_VALUE);
            _consoleApiWrap.SetBufferSize(_consoleBuffer, (short)width, (short)height);

        }

        /*private void ClearBuffer()
        {
            Array.Clear(_board, 0, _board.Length);
        }

        public void EraseAll()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (_board[x, y] != null)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(" ");
                    }
                }
            }
            ClearBuffer();
        }

        public void Erase(Point2D loc)
        {
            Draw(' ', loc.X, loc.Y);
        }

        public void Erase(int x, int y)
        {
            _board[x, y] = ' ';
        }

        public void Draw(char thing, Point2D loc)
        {
            Draw(thing, loc.X, loc.Y);
        }

        public void Draw(char thing, int x, int y)
        {
            _board[x, y] = thing;
        }

        public void DrawSprite(Sprite spr, Point2D loc)
        {
            DrawSprite(spr, loc.X, loc.Y);
        }

        public void DrawSprite(Sprite spr, int x, int y)
        {
            for (int width = 0; width < spr.CharArray.GetLength(0); width++)
            {
                for (int height = 0; height < spr.CharArray.GetLength(1); height++)
                {
                    if ((x + width < _board.GetLength(0) && y + height < _board.GetLength(1)) &&
                        (x + width >= 0 && y + height >= 0))
                        _board[x + width, y + height] = spr.CharArray[width, height];
                }
            }
        }*/
        
        /*public void DrawAll()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (_board[x, y] != null)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(_board[x, y]);
                    }
                }
            }
        }*/

        public void Draw(Dot dot, int x, int y)
        {
            Draw(dot.Thing, dot.Foreground, dot.Background, x, y);
        }
        
        public void Draw(Dot dot, Point2D loc)
        {
            Draw(dot.Thing, dot.Foreground, dot.Background, loc.X, loc.Y);
        }

        public void Draw(char thing, ConsoleApiWrap.ForegroundColor foregroundColor, ConsoleApiWrap.BackgroundColor backgroundColor, int x, int y)
        {
            _dots[x, y] = new Dot() {Thing = thing, Foreground = foregroundColor, Background = backgroundColor};
        }

        public void DrawSprite(Sprite spr, Point2D loc)
        {
            DrawSprite(spr, loc.X, loc.Y);
        }
        public void DrawSprite(Sprite spr, int x, int y)
        {
            for (int width = 0; width < spr.Dots.GetLength(0); width++)
            {
                for (int height = 0; height < spr.Dots.GetLength(1); height++)
                {
                    if ((x + width < _dots.GetLength(0) && y + height < _dots.GetLength(1)) &&
                        (x + width >= 0 && y + height >= 0))
                    {
                        _dots[x + width, y + height] = spr.Dots[width, height];
                    }
                }
            }
        }
        
        public void DrawAll()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (_dots[x, y] != null)
                    {
                        if (_dots[x, y] == null)
                            continue;
                        var dot = _dots[x, y];
                        _consoleApiWrap.WriteAt(_consoleBuffer, dot.Thing.ToString(), x, y);
                        _consoleApiWrap.WriteAttributeAt(_consoleBuffer, new[] { (ushort)((int)dot.Foreground | (int)dot.Background) }, x, y);
                    }
                }
            }
            _consoleApiWrap.SetBuffer(_consoleBuffer);
        }
        
        public void EraseAll()
        {
            Array.Clear(_dots, 0, _dots.Length);
            _consoleBuffer = _consoleApiWrap.CreateBuffer(ConsoleApiWrap.DesiredAccess.BOTH, ConsoleApiWrap.ShareMode.MAGIC_VALUE);
        }
    }
}
