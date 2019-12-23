using System.Runtime.InteropServices;
using SDL2;

namespace CSharpNovel2.System
{
    public static class Keyboard
    {
        private const int KeyNum = 1024;
        private static readonly int[] PressingCount = new int[KeyNum];
        private static readonly int[] ReleasingCount = new int[KeyNum];

        private static bool IsAvailableCode(int keyCode) { return 0 <= keyCode && keyCode <= KeyNum; }

        public static void Update()
        {
            var nowKeyStatesPtr = SDL.SDL_GetKeyboardState(out var size);
            var nowKeyStates = new byte[size];
            Marshal.Copy(nowKeyStatesPtr, nowKeyStates, 0, nowKeyStates.Length);
            for (var i = 0; i < size; i++)
            {
                if (nowKeyStates[i] != 0)
                {
                    if (ReleasingCount[i] > 0)
                    {
                        ReleasingCount[i] = 0;
                    }

                    PressingCount[i]++;
                }
                else
                {
                    if (PressingCount[i] > 0)
                    {
                        PressingCount[i] = 0;
                    }

                    ReleasingCount[i]++;
                }
            }
        }

        public static int GetPressingCount(int keyCode)
        {
            if (!IsAvailableCode(keyCode))
            {
                return -1;
            }

            return PressingCount[keyCode];
        }

        public static int GetReleasingCount(int keyCode)
        {
            if (!IsAvailableCode(keyCode))
            {
                return -1;
            }

            return ReleasingCount[keyCode];
        }
    }
}