using System;
using System.Runtime.InteropServices;

namespace DMIPSpaceInvaders.Game.Utils.Interop
{
    public static class WinApi // pinvoke.net
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8
                ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
                : new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleOutputCP(
            uint wCodePageID
        );
        
        public static bool IsKeyPressed(Keys key)
        {
            return GetAsyncKeyState((int) key) != 0;
        }

        public enum Keys
        {
            /// <summary>The bitmask to extract a key code from a key value.</summary>
            // Token: 0x0400100E RID: 4110
            KeyCode = 65535,

            /// <summary>The bitmask to extract modifiers from a key value.</summary>
            // Token: 0x0400100F RID: 4111
            Modifiers = -65536,

            /// <summary>No key pressed.</summary>
            // Token: 0x04001010 RID: 4112
            None = 0,

            /// <summary>The left mouse button.</summary>
            // Token: 0x04001011 RID: 4113
            LButton = 1,

            /// <summary>The right mouse button.</summary>
            // Token: 0x04001012 RID: 4114
            RButton = 2,

            /// <summary>The CANCEL key.</summary>
            // Token: 0x04001013 RID: 4115
            Cancel = 3,

            /// <summary>The middle mouse button (three-button mouse).</summary>
            // Token: 0x04001014 RID: 4116
            MButton = 4,

            /// <summary>The first x mouse button (five-button mouse).</summary>
            // Token: 0x04001015 RID: 4117
            XButton1 = 5,

            /// <summary>The second x mouse button (five-button mouse).</summary>
            // Token: 0x04001016 RID: 4118
            XButton2 = 6,

            /// <summary>The BACKSPACE key.</summary>
            // Token: 0x04001017 RID: 4119
            Back = 8,

            /// <summary>The TAB key.</summary>
            // Token: 0x04001018 RID: 4120
            Tab = 9,

            /// <summary>The LINEFEED key.</summary>
            // Token: 0x04001019 RID: 4121
            LineFeed = 10,

            /// <summary>The CLEAR key.</summary>
            // Token: 0x0400101A RID: 4122
            Clear = 12,

            /// <summary>The RETURN key.</summary>
            // Token: 0x0400101B RID: 4123
            Return = 13,

            /// <summary>The ENTER key.</summary>
            // Token: 0x0400101C RID: 4124
            Enter = 13,

            /// <summary>The SHIFT key.</summary>
            // Token: 0x0400101D RID: 4125
            ShiftKey = 16,

            /// <summary>The CTRL key.</summary>
            // Token: 0x0400101E RID: 4126
            ControlKey = 17,

            /// <summary>The ALT key.</summary>
            // Token: 0x0400101F RID: 4127
            Menu = 18,

            /// <summary>The PAUSE key.</summary>
            // Token: 0x04001020 RID: 4128
            Pause = 19,

            /// <summary>The CAPS LOCK key.</summary>
            // Token: 0x04001021 RID: 4129
            Capital = 20,

            /// <summary>The CAPS LOCK key.</summary>
            // Token: 0x04001022 RID: 4130
            CapsLock = 20,

            /// <summary>The IME Kana mode key.</summary>
            // Token: 0x04001023 RID: 4131
            KanaMode = 21,

            /// <summary>The IME Hanguel mode key. (maintained for compatibility; use <see langword="HangulMode" />)</summary>
            // Token: 0x04001024 RID: 4132
            HanguelMode = 21,

            /// <summary>The IME Hangul mode key.</summary>
            // Token: 0x04001025 RID: 4133
            HangulMode = 21,

            /// <summary>The IME Junja mode key.</summary>
            // Token: 0x04001026 RID: 4134
            JunjaMode = 23,

            /// <summary>The IME final mode key.</summary>
            // Token: 0x04001027 RID: 4135
            FinalMode = 24,

            /// <summary>The IME Hanja mode key.</summary>
            // Token: 0x04001028 RID: 4136
            HanjaMode = 25,

            /// <summary>The IME Kanji mode key.</summary>
            // Token: 0x04001029 RID: 4137
            KanjiMode = 25,

            /// <summary>The ESC key.</summary>
            // Token: 0x0400102A RID: 4138
            Escape = 27,

            /// <summary>The IME convert key.</summary>
            // Token: 0x0400102B RID: 4139
            IMEConvert = 28,

            /// <summary>The IME nonconvert key.</summary>
            // Token: 0x0400102C RID: 4140
            IMENonconvert = 29,

            /// <summary>The IME accept key, replaces <see cref="F:System.Windows.Forms.Keys.IMEAceept" />.</summary>
            // Token: 0x0400102D RID: 4141
            IMEAccept = 30,

            /// <summary>The IME accept key. Obsolete, use <see cref="F:System.Windows.Forms.Keys.IMEAccept" /> instead.</summary>
            // Token: 0x0400102E RID: 4142
            IMEAceept = 30,

            /// <summary>The IME mode change key.</summary>
            // Token: 0x0400102F RID: 4143
            IMEModeChange = 31,

            /// <summary>The SPACEBAR key.</summary>
            // Token: 0x04001030 RID: 4144
            Space = 32,

            /// <summary>The PAGE UP key.</summary>
            // Token: 0x04001031 RID: 4145
            Prior = 33,

            /// <summary>The PAGE UP key.</summary>
            // Token: 0x04001032 RID: 4146
            PageUp = 33,

            /// <summary>The PAGE DOWN key.</summary>
            // Token: 0x04001033 RID: 4147
            Next = 34,

            /// <summary>The PAGE DOWN key.</summary>
            // Token: 0x04001034 RID: 4148
            PageDown = 34,

            /// <summary>The END key.</summary>
            // Token: 0x04001035 RID: 4149
            End = 35,

            /// <summary>The HOME key.</summary>
            // Token: 0x04001036 RID: 4150
            Home = 36,

            /// <summary>The LEFT ARROW key.</summary>
            // Token: 0x04001037 RID: 4151
            Left = 37,

            /// <summary>The UP ARROW key.</summary>
            // Token: 0x04001038 RID: 4152
            Up = 38,

            /// <summary>The RIGHT ARROW key.</summary>
            // Token: 0x04001039 RID: 4153
            Right = 39,

            /// <summary>The DOWN ARROW key.</summary>
            // Token: 0x0400103A RID: 4154
            Down = 40,

            /// <summary>The SELECT key.</summary>
            // Token: 0x0400103B RID: 4155
            Select = 41,

            /// <summary>The PRINT key.</summary>
            // Token: 0x0400103C RID: 4156
            Print = 42,

            /// <summary>The EXECUTE key.</summary>
            // Token: 0x0400103D RID: 4157
            Execute = 43,

            /// <summary>The PRINT SCREEN key.</summary>
            // Token: 0x0400103E RID: 4158
            Snapshot = 44,

            /// <summary>The PRINT SCREEN key.</summary>
            // Token: 0x0400103F RID: 4159
            PrintScreen = 44,

            /// <summary>The INS key.</summary>
            // Token: 0x04001040 RID: 4160
            Insert = 45,

            /// <summary>The DEL key.</summary>
            // Token: 0x04001041 RID: 4161
            Delete = 46,

            /// <summary>The HELP key.</summary>
            // Token: 0x04001042 RID: 4162
            Help = 47,

            /// <summary>The 0 key.</summary>
            // Token: 0x04001043 RID: 4163
            D0 = 48,

            /// <summary>The 1 key.</summary>
            // Token: 0x04001044 RID: 4164
            D1 = 49,

            /// <summary>The 2 key.</summary>
            // Token: 0x04001045 RID: 4165
            D2 = 50,

            /// <summary>The 3 key.</summary>
            // Token: 0x04001046 RID: 4166
            D3 = 51,

            /// <summary>The 4 key.</summary>
            // Token: 0x04001047 RID: 4167
            D4 = 52,

            /// <summary>The 5 key.</summary>
            // Token: 0x04001048 RID: 4168
            D5 = 53,

            /// <summary>The 6 key.</summary>
            // Token: 0x04001049 RID: 4169
            D6 = 54,

            /// <summary>The 7 key.</summary>
            // Token: 0x0400104A RID: 4170
            D7 = 55,

            /// <summary>The 8 key.</summary>
            // Token: 0x0400104B RID: 4171
            D8 = 56,

            /// <summary>The 9 key.</summary>
            // Token: 0x0400104C RID: 4172
            D9 = 57,

            /// <summary>The A key.</summary>
            // Token: 0x0400104D RID: 4173
            A = 65,

            /// <summary>The B key.</summary>
            // Token: 0x0400104E RID: 4174
            B = 66,

            /// <summary>The C key.</summary>
            // Token: 0x0400104F RID: 4175
            C = 67,

            /// <summary>The D key.</summary>
            // Token: 0x04001050 RID: 4176
            D = 68,

            /// <summary>The E key.</summary>
            // Token: 0x04001051 RID: 4177
            E = 69,

            /// <summary>The F key.</summary>
            // Token: 0x04001052 RID: 4178
            F = 70,

            /// <summary>The G key.</summary>
            // Token: 0x04001053 RID: 4179
            G = 71,

            /// <summary>The H key.</summary>
            // Token: 0x04001054 RID: 4180
            H = 72,

            /// <summary>The I key.</summary>
            // Token: 0x04001055 RID: 4181
            I = 73,

            /// <summary>The J key.</summary>
            // Token: 0x04001056 RID: 4182
            J = 74,

            /// <summary>The K key.</summary>
            // Token: 0x04001057 RID: 4183
            K = 75,

            /// <summary>The L key.</summary>
            // Token: 0x04001058 RID: 4184
            L = 76,

            /// <summary>The M key.</summary>
            // Token: 0x04001059 RID: 4185
            M = 77,

            /// <summary>The N key.</summary>
            // Token: 0x0400105A RID: 4186
            N = 78,

            /// <summary>The O key.</summary>
            // Token: 0x0400105B RID: 4187
            O = 79,

            /// <summary>The P key.</summary>
            // Token: 0x0400105C RID: 4188
            P = 80,

            /// <summary>The Q key.</summary>
            // Token: 0x0400105D RID: 4189
            Q = 81,

            /// <summary>The R key.</summary>
            // Token: 0x0400105E RID: 4190
            R = 82,

            /// <summary>The S key.</summary>
            // Token: 0x0400105F RID: 4191
            S = 83,

            /// <summary>The T key.</summary>
            // Token: 0x04001060 RID: 4192
            T = 84,

            /// <summary>The U key.</summary>
            // Token: 0x04001061 RID: 4193
            U = 85,

            /// <summary>The V key.</summary>
            // Token: 0x04001062 RID: 4194
            V = 86,

            /// <summary>The W key.</summary>
            // Token: 0x04001063 RID: 4195
            W = 87,

            /// <summary>The X key.</summary>
            // Token: 0x04001064 RID: 4196
            X = 88,

            /// <summary>The Y key.</summary>
            // Token: 0x04001065 RID: 4197
            Y = 89,

            /// <summary>The Z key.</summary>
            // Token: 0x04001066 RID: 4198
            Z = 90,

            /// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
            // Token: 0x04001067 RID: 4199
            LWin = 91,

            /// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
            // Token: 0x04001068 RID: 4200
            RWin = 92,

            /// <summary>The application key (Microsoft Natural Keyboard).</summary>
            // Token: 0x04001069 RID: 4201
            Apps = 93,

            /// <summary>The computer sleep key.</summary>
            // Token: 0x0400106A RID: 4202
            Sleep = 95,

            /// <summary>The 0 key on the numeric keypad.</summary>
            // Token: 0x0400106B RID: 4203
            NumPad0 = 96,

            /// <summary>The 1 key on the numeric keypad.</summary>
            // Token: 0x0400106C RID: 4204
            NumPad1 = 97,

            /// <summary>The 2 key on the numeric keypad.</summary>
            // Token: 0x0400106D RID: 4205
            NumPad2 = 98,

            /// <summary>The 3 key on the numeric keypad.</summary>
            // Token: 0x0400106E RID: 4206
            NumPad3 = 99,

            /// <summary>The 4 key on the numeric keypad.</summary>
            // Token: 0x0400106F RID: 4207
            NumPad4 = 100,

            /// <summary>The 5 key on the numeric keypad.</summary>
            // Token: 0x04001070 RID: 4208
            NumPad5 = 101,

            /// <summary>The 6 key on the numeric keypad.</summary>
            // Token: 0x04001071 RID: 4209
            NumPad6 = 102,

            /// <summary>The 7 key on the numeric keypad.</summary>
            // Token: 0x04001072 RID: 4210
            NumPad7 = 103,

            /// <summary>The 8 key on the numeric keypad.</summary>
            // Token: 0x04001073 RID: 4211
            NumPad8 = 104,

            /// <summary>The 9 key on the numeric keypad.</summary>
            // Token: 0x04001074 RID: 4212
            NumPad9 = 105,

            /// <summary>The multiply key.</summary>
            // Token: 0x04001075 RID: 4213
            Multiply = 106,

            /// <summary>The add key.</summary>
            // Token: 0x04001076 RID: 4214
            Add = 107,

            /// <summary>The separator key.</summary>
            // Token: 0x04001077 RID: 4215
            Separator = 108,

            /// <summary>The subtract key.</summary>
            // Token: 0x04001078 RID: 4216
            Subtract = 109,

            /// <summary>The decimal key.</summary>
            // Token: 0x04001079 RID: 4217
            Decimal = 110,

            /// <summary>The divide key.</summary>
            // Token: 0x0400107A RID: 4218
            Divide = 111,

            /// <summary>The F1 key.</summary>
            // Token: 0x0400107B RID: 4219
            F1 = 112,

            /// <summary>The F2 key.</summary>
            // Token: 0x0400107C RID: 4220
            F2 = 113,

            /// <summary>The F3 key.</summary>
            // Token: 0x0400107D RID: 4221
            F3 = 114,

            /// <summary>The F4 key.</summary>
            // Token: 0x0400107E RID: 4222
            F4 = 115,

            /// <summary>The F5 key.</summary>
            // Token: 0x0400107F RID: 4223
            F5 = 116,

            /// <summary>The F6 key.</summary>
            // Token: 0x04001080 RID: 4224
            F6 = 117,

            /// <summary>The F7 key.</summary>
            // Token: 0x04001081 RID: 4225
            F7 = 118,

            /// <summary>The F8 key.</summary>
            // Token: 0x04001082 RID: 4226
            F8 = 119,

            /// <summary>The F9 key.</summary>
            // Token: 0x04001083 RID: 4227
            F9 = 120,

            /// <summary>The F10 key.</summary>
            // Token: 0x04001084 RID: 4228
            F10 = 121,

            /// <summary>The F11 key.</summary>
            // Token: 0x04001085 RID: 4229
            F11 = 122,

            /// <summary>The F12 key.</summary>
            // Token: 0x04001086 RID: 4230
            F12 = 123,

            /// <summary>The F13 key.</summary>
            // Token: 0x04001087 RID: 4231
            F13 = 124,

            /// <summary>The F14 key.</summary>
            // Token: 0x04001088 RID: 4232
            F14 = 125,

            /// <summary>The F15 key.</summary>
            // Token: 0x04001089 RID: 4233
            F15 = 126,

            /// <summary>The F16 key.</summary>
            // Token: 0x0400108A RID: 4234
            F16 = 127,

            /// <summary>The F17 key.</summary>
            // Token: 0x0400108B RID: 4235
            F17 = 128,

            /// <summary>The F18 key.</summary>
            // Token: 0x0400108C RID: 4236
            F18 = 129,

            /// <summary>The F19 key.</summary>
            // Token: 0x0400108D RID: 4237
            F19 = 130,

            /// <summary>The F20 key.</summary>
            // Token: 0x0400108E RID: 4238
            F20 = 131,

            /// <summary>The F21 key.</summary>
            // Token: 0x0400108F RID: 4239
            F21 = 132,

            /// <summary>The F22 key.</summary>
            // Token: 0x04001090 RID: 4240
            F22 = 133,

            /// <summary>The F23 key.</summary>
            // Token: 0x04001091 RID: 4241
            F23 = 134,

            /// <summary>The F24 key.</summary>
            // Token: 0x04001092 RID: 4242
            F24 = 135,

            /// <summary>The NUM LOCK key.</summary>
            // Token: 0x04001093 RID: 4243
            NumLock = 144,

            /// <summary>The SCROLL LOCK key.</summary>
            // Token: 0x04001094 RID: 4244
            Scroll = 145,

            /// <summary>The left SHIFT key.</summary>
            // Token: 0x04001095 RID: 4245
            LShiftKey = 160,

            /// <summary>The right SHIFT key.</summary>
            // Token: 0x04001096 RID: 4246
            RShiftKey = 161,

            /// <summary>The left CTRL key.</summary>
            // Token: 0x04001097 RID: 4247
            LControlKey = 162,

            /// <summary>The right CTRL key.</summary>
            // Token: 0x04001098 RID: 4248
            RControlKey = 163,

            /// <summary>The left ALT key.</summary>
            // Token: 0x04001099 RID: 4249
            LMenu = 164,

            /// <summary>The right ALT key.</summary>
            // Token: 0x0400109A RID: 4250
            RMenu = 165,

            /// <summary>The browser back key (Windows 2000 or later).</summary>
            // Token: 0x0400109B RID: 4251
            BrowserBack = 166,

            /// <summary>The browser forward key (Windows 2000 or later).</summary>
            // Token: 0x0400109C RID: 4252
            BrowserForward = 167,

            /// <summary>The browser refresh key (Windows 2000 or later).</summary>
            // Token: 0x0400109D RID: 4253
            BrowserRefresh = 168,

            /// <summary>The browser stop key (Windows 2000 or later).</summary>
            // Token: 0x0400109E RID: 4254
            BrowserStop = 169,

            /// <summary>The browser search key (Windows 2000 or later).</summary>
            // Token: 0x0400109F RID: 4255
            BrowserSearch = 170,

            /// <summary>The browser favorites key (Windows 2000 or later).</summary>
            // Token: 0x040010A0 RID: 4256
            BrowserFavorites = 171,

            /// <summary>The browser home key (Windows 2000 or later).</summary>
            // Token: 0x040010A1 RID: 4257
            BrowserHome = 172,

            /// <summary>The volume mute key (Windows 2000 or later).</summary>
            // Token: 0x040010A2 RID: 4258
            VolumeMute = 173,

            /// <summary>The volume down key (Windows 2000 or later).</summary>
            // Token: 0x040010A3 RID: 4259
            VolumeDown = 174,

            /// <summary>The volume up key (Windows 2000 or later).</summary>
            // Token: 0x040010A4 RID: 4260
            VolumeUp = 175,

            /// <summary>The media next track key (Windows 2000 or later).</summary>
            // Token: 0x040010A5 RID: 4261
            MediaNextTrack = 176,

            /// <summary>The media previous track key (Windows 2000 or later).</summary>
            // Token: 0x040010A6 RID: 4262
            MediaPreviousTrack = 177,

            /// <summary>The media Stop key (Windows 2000 or later).</summary>
            // Token: 0x040010A7 RID: 4263
            MediaStop = 178,

            /// <summary>The media play pause key (Windows 2000 or later).</summary>
            // Token: 0x040010A8 RID: 4264
            MediaPlayPause = 179,

            /// <summary>The launch mail key (Windows 2000 or later).</summary>
            // Token: 0x040010A9 RID: 4265
            LaunchMail = 180,

            /// <summary>The select media key (Windows 2000 or later).</summary>
            // Token: 0x040010AA RID: 4266
            SelectMedia = 181,

            /// <summary>The start application one key (Windows 2000 or later).</summary>
            // Token: 0x040010AB RID: 4267
            LaunchApplication1 = 182,

            /// <summary>The start application two key (Windows 2000 or later).</summary>
            // Token: 0x040010AC RID: 4268
            LaunchApplication2 = 183,

            /// <summary>The OEM Semicolon key on a US standard keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010AD RID: 4269
            OemSemicolon = 186,

            /// <summary>The OEM 1 key.</summary>
            // Token: 0x040010AE RID: 4270
            Oem1 = 186,

            /// <summary>The OEM plus key on any country/region keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010AF RID: 4271
            Oemplus = 187,

            /// <summary>The OEM comma key on any country/region keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010B0 RID: 4272
            Oemcomma = 188,

            /// <summary>The OEM minus key on any country/region keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010B1 RID: 4273
            OemMinus = 189,

            /// <summary>The OEM period key on any country/region keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010B2 RID: 4274
            OemPeriod = 190,

            /// <summary>The OEM question mark key on a US standard keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010B3 RID: 4275
            OemQuestion = 191,

            /// <summary>The OEM 2 key.</summary>
            // Token: 0x040010B4 RID: 4276
            Oem2 = 191,

            /// <summary>The OEM tilde key on a US standard keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010B5 RID: 4277
            Oemtilde = 192,

            /// <summary>The OEM 3 key.</summary>
            // Token: 0x040010B6 RID: 4278
            Oem3 = 192,

            /// <summary>The OEM open bracket key on a US standard keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010B7 RID: 4279
            OemOpenBrackets = 219,

            /// <summary>The OEM 4 key.</summary>
            // Token: 0x040010B8 RID: 4280
            Oem4 = 219,

            /// <summary>The OEM pipe key on a US standard keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010B9 RID: 4281
            OemPipe = 220,

            /// <summary>The OEM 5 key.</summary>
            // Token: 0x040010BA RID: 4282
            Oem5 = 220,

            /// <summary>The OEM close bracket key on a US standard keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010BB RID: 4283
            OemCloseBrackets = 221,

            /// <summary>The OEM 6 key.</summary>
            // Token: 0x040010BC RID: 4284
            Oem6 = 221,

            /// <summary>The OEM singled/double quote key on a US standard keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010BD RID: 4285
            OemQuotes = 222,

            /// <summary>The OEM 7 key.</summary>
            // Token: 0x040010BE RID: 4286
            Oem7 = 222,

            /// <summary>The OEM 8 key.</summary>
            // Token: 0x040010BF RID: 4287
            Oem8 = 223,

            /// <summary>The OEM angle bracket or backslash key on the RT 102 key keyboard (Windows 2000 or later).</summary>
            // Token: 0x040010C0 RID: 4288
            OemBackslash = 226,

            /// <summary>The OEM 102 key.</summary>
            // Token: 0x040010C1 RID: 4289
            Oem102 = 226,

            /// <summary>The PROCESS KEY key.</summary>
            // Token: 0x040010C2 RID: 4290
            ProcessKey = 229,

            /// <summary>Used to pass Unicode characters as if they were keystrokes. The Packet key value is the low word of a 32-bit virtual-key value used for non-keyboard input methods.</summary>
            // Token: 0x040010C3 RID: 4291
            Packet = 231,

            /// <summary>The ATTN key.</summary>
            // Token: 0x040010C4 RID: 4292
            Attn = 246,

            /// <summary>The CRSEL key.</summary>
            // Token: 0x040010C5 RID: 4293
            Crsel = 247,

            /// <summary>The EXSEL key.</summary>
            // Token: 0x040010C6 RID: 4294
            Exsel = 248,

            /// <summary>The ERASE EOF key.</summary>
            // Token: 0x040010C7 RID: 4295
            EraseEof = 249,

            /// <summary>The PLAY key.</summary>
            // Token: 0x040010C8 RID: 4296
            Play = 250,

            /// <summary>The ZOOM key.</summary>
            // Token: 0x040010C9 RID: 4297
            Zoom = 251,

            /// <summary>A constant reserved for future use.</summary>
            // Token: 0x040010CA RID: 4298
            NoName = 252,

            /// <summary>The PA1 key.</summary>
            // Token: 0x040010CB RID: 4299
            Pa1 = 253,

            /// <summary>The CLEAR key.</summary>
            // Token: 0x040010CC RID: 4300
            OemClear = 254,

            /// <summary>The SHIFT modifier key.</summary>
            // Token: 0x040010CD RID: 4301
            Shift = 65536,

            /// <summary>The CTRL modifier key.</summary>
            // Token: 0x040010CE RID: 4302
            Control = 131072,

            /// <summary>The ALT modifier key.</summary>
            // Token: 0x040010CF RID: 4303
            Alt = 262144
        } // System.Windows.Forms
    }
}