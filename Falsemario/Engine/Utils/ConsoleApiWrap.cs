using System;
using System.Runtime.InteropServices;
using Falsemario.Engine.Basic;

namespace Falsemario.Engine.Utils
{
    public class ConsoleApiWrap
    {
        #region Win Api // pinvoke.net

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleScreenBufferSize(
            IntPtr hConsoleOutput,
            COORD dwSize
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleTextAttribute(
            IntPtr hConsoleOutput,
            ushort wAttributes
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleTitle(
            string lpConsoleTitle
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleWindowInfo(
            IntPtr hConsoleOutput,
            bool bAbsolute,
            [In] ref SMALL_RECT lpConsoleWindow
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleCursorPosition(
            IntPtr hConsoleOutput,
            COORD dwCursorPosition
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleScreenBufferInfo(
            IntPtr hConsoleOutput,
            out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo
        );

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("Kernel32.dll")]
        private static extern IntPtr CreateConsoleScreenBuffer(
            int dwDesiredAccess, int dwShareMode,
            IntPtr secutiryAttributes,
            UInt32 flags,
            IntPtr screenBufferData);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutputAttribute(
            IntPtr hConsoleOutput,
            ushort[] lpAttribute,
            uint nLength,
            COORD dwWriteCoord,
            out uint lpNumberOfAttrsWritten
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutputCharacter(
            IntPtr hConsoleOutput,
            string lpCharacter,
            uint nLength,
            COORD dwWriteCoord,
            out uint lpNumberOfCharsWritten
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FillConsoleOutputAttribute(
            IntPtr hConsoleOutput,
            ushort wAttribute,
            uint nLength,
            COORD dwWriteCoord,
            out uint lpNumberOfAttrsWritten
        );

        private enum WinApiCharAttributes : ushort
        {
            /// <summary>
            /// None.
            /// </summary>
            None = 0x0000,

            /// <summary>
            /// Text color contains blue.
            /// </summary>
            FOREGROUND_BLUE = 0x0001,

            /// <summary>
            /// Text color contains green.
            /// </summary>
            FOREGROUND_GREEN = 0x0002,

            /// <summary>
            /// Text color contains red.
            /// </summary>
            FOREGROUND_RED = 0x0004,

            /// <summary>
            /// Text color is intensified.
            /// </summary>
            FOREGROUND_INTENSITY = 0x0008,

            /// <summary>
            /// Background color contains blue.
            /// </summary>
            BACKGROUND_BLUE = 0x0010,

            /// <summary>
            /// Background color contains green.
            /// </summary>
            BACKGROUND_GREEN = 0x0020,

            /// <summary>
            /// Background color contains red.
            /// </summary>
            BACKGROUND_RED = 0x0040,

            /// <summary>
            /// Background color is intensified.
            /// </summary>
            BACKGROUND_INTENSITY = 0x0080,

            /// <summary>
            /// Leading byte.
            /// </summary>
            COMMON_LVB_LEADING_BYTE = 0x0100,

            /// <summary>
            /// Trailing byte.
            /// </summary>
            COMMON_LVB_TRAILING_BYTE = 0x0200,

            /// <summary>
            /// Top horizontal
            /// </summary>
            COMMON_LVB_GRID_HORIZONTAL = 0x0400,

            /// <summary>
            /// Left vertical.
            /// </summary>
            COMMON_LVB_GRID_LVERTICAL = 0x0800,

            /// <summary>
            /// Right vertical.
            /// </summary>
            COMMON_LVB_GRID_RVERTICAL = 0x1000,

            /// <summary>
            /// Reverse foreground and background attribute.
            /// </summary>
            COMMON_LVB_REVERSE_VIDEO = 0x4000,

            /// <summary>
            /// Underscore.
            /// </summary>
            COMMON_LVB_UNDERSCORE = 0x8000,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }

        private struct SMALL_RECT
        {

            public short Left;
            public short Top;
            public short Right;
            public short Bottom;

        }

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private struct CONSOLE_SCREEN_BUFFER_INFO
        {

            public COORD dwSize;
            public COORD dwCursorPosition;
            public ushort wAttributes;
            public SMALL_RECT srWindow;
            public COORD dwMaximumWindowSize;

        }

        #endregion

        public enum DesiredAccess : int
        {
            READ = unchecked((int) 0x80000000),
            WRITE = 0x40000000,
            BOTH = READ | WRITE
        };
        public enum ForegroundColor : ushort
        {
            Black = 0,
            DarkGray = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY,

            Gray = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED |
                        ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN |
                        ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE,

            White = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED |
                    ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN |
                    ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE |
                    ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY,
            DarkBlue = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE,
            DarkGreen = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN,

            DarkCyan = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN |
                   ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE,
            DarkRed = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED,

            DarkMagenta = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED |
                     ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE,

            Blue = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE |
                        ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY,

            Green = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN |
                         ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY,

            Cyan = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN |
                        ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE |
                        ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY,

            Red = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED |
                       ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY,

            Magenta = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED |
                          ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_BLUE |
                          ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY,

            DarkYellow = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED |
                     ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN,

            Yellow = ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_RED |
                     ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_GREEN |
                     ConsoleApiWrap.WinApiCharAttributes.FOREGROUND_INTENSITY
        }
        public enum BackgroundColor : ushort
        {
            Black = 0,
            DarkGray = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,

            Gray = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED |
                        ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN |
                        ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE,

            White = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED |
                    ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN |
                    ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE |
                    ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,
            DarkBlue = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE,
            DarkGreen = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN,

            DarkCyan = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN |
                   ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE,
            DarkRed = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED,

            DarkMagenta = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED |
                     ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE,

            Blue = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE |
                        ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,

            Green = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN |
                         ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,

            Cyan = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN |
                        ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE |
                        ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,

            Red = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED |
                       ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,

            Magenta = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED |
                          ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_BLUE |
                          ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,

            DarkYellow = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED |
                     ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN,

            Yellow = ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_RED |
                     ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_GREEN |
                     ConsoleApiWrap.WinApiCharAttributes.BACKGROUND_INTENSITY,
        }

        public enum ShareMode : int
        {
            MAGIC_VALUE = 0x03
        }

        public IntPtr CreateBuffer(DesiredAccess desiredAccess, ShareMode shareMode)
        {
            return CreateBuffer(desiredAccess, shareMode, IntPtr.Zero, IntPtr.Zero);
        }

        public IntPtr CreateBuffer(DesiredAccess desiredAccess, ShareMode shareMode, IntPtr consoleBuffer)
        {
            return CreateBuffer(desiredAccess, shareMode, consoleBuffer, IntPtr.Zero);
        }

        public IntPtr CreateBuffer(DesiredAccess desiredAccess, ShareMode shareMode, IntPtr consoleBuffer,
            IntPtr securityAttributes)
        {
            return CreateConsoleScreenBuffer((int) desiredAccess, (int) shareMode, securityAttributes, 1,
                consoleBuffer); // Единственным поддерживаемым тип экранного буфера является CONSOLE_TEXTMODE_BUFFER (равный 1)
        }

        public bool SetBufferSize(IntPtr buffer, short x, short y) =>
            SetConsoleScreenBufferSize(buffer, new COORD() {X = x, Y = y});

        public bool SetCursorPosition(IntPtr buffer, short x, short y) =>
            SetConsoleCursorPosition(buffer, new COORD() {X = x, Y = y});

        public bool SetWindow(IntPtr buffer, bool absolute, Rectangle rect)
        {
            var sR = new SMALL_RECT()
            {
                Top = (short) rect.Top,
                Left = (short) rect.Left,
                Right = (short) rect.Right,
                Bottom = (short) rect.Bottom
            };
            return SetConsoleWindowInfo(buffer, absolute, ref sR);
        }

        public void SetBuffer(IntPtr newBuffer) => SetConsoleActiveScreenBuffer(newBuffer);

        public void SetBufferInfo(IntPtr buffer, ScreenBufferInfo screenBufferInfo)
        {
            if (screenBufferInfo.MaximumWindowSize != null)
                SetBufferSize(buffer, (short) screenBufferInfo.MaximumWindowSize.X,
                    (short) screenBufferInfo.MaximumWindowSize.Y);
            if (screenBufferInfo.Window != null)
                SetWindow(buffer, true, screenBufferInfo.Window);
            if (screenBufferInfo.CursorPosition != null)
                SetCursorPosition(buffer, (short) screenBufferInfo.CursorPosition.X,
                    (short) screenBufferInfo.CursorPosition.Y);
            if (screenBufferInfo.Attributes.HasValue)
                SetConsoleTextAttribute(buffer, (ushort) screenBufferInfo.Attributes);
        }

        public ScreenBufferInfo GetBufferInfo(IntPtr buffer)
        {
            CONSOLE_SCREEN_BUFFER_INFO winApiInfo;
            GetConsoleScreenBufferInfo(buffer, out winApiInfo);
            if (!GetConsoleScreenBufferInfo(buffer, out winApiInfo))
                return null;
            return new ScreenBufferInfo()
            {
                Attributes = (short) winApiInfo.wAttributes,
                CursorPosition = new Point2D()
                {
                    X = winApiInfo.dwCursorPosition.X,
                    Y = winApiInfo.dwCursorPosition.Y
                },
                MaximumWindowSize = new Point2D()
                {
                    X = winApiInfo.dwMaximumWindowSize.X,
                    Y = winApiInfo.dwMaximumWindowSize.Y
                },
                Window = new Rectangle(winApiInfo.dwSize.X, winApiInfo.dwSize.Y, winApiInfo.srWindow.Top,
                    winApiInfo.srWindow.Left)
            };
        }

        public void SetWindowSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
        }

        public void WriteAttributeAt(IntPtr buffer, ushort[] attribute, Point2D point)
        {
            WriteAttributeAt(buffer, attribute, 1, point.X, point.Y);
        }

        public void WriteAttributeAt(IntPtr buffer, ushort[] attribute, int x, int y)
        {
            WriteAttributeAt(buffer, attribute, attribute.Length, x, y);
        }
        
        public void WriteAttributeAt(IntPtr buffer, ushort[] attribute, int length, int x, int y)
        {
            uint written;
            WriteConsoleOutputAttribute(buffer, attribute, (uint) length, new COORD() {X = (short) x, Y = (short) y}, out written);
        }

        public void WriteAt(IntPtr buffer, string text, Point2D point)
        {
            WriteAt(buffer, text, point.X, point.Y);
        }

        public void WriteAt(IntPtr buffer, string text, int x, int y)
        {
            uint written;
            WriteConsoleOutputCharacter(buffer, text, (uint) text.Length, new COORD() {X = (short) x, Y = (short) y},
                out written);
        }

        public IntPtr GetConsoleWindowPtr() => GetConsoleWindow();
    }
}