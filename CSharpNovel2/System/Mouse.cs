using SDL2;

namespace CSharpNovel2.System
{
    public static class Mouse
    {
        private const int ButtonNum = 8;
        
        private static int _x, _y;
        private static readonly int[] PressingCount = new int[ButtonNum];
        private static readonly int[] ReleasingCount = new int[ButtonNum];

        private static bool IsAvailableCode(int buttonCode) { return 1 <= buttonCode && buttonCode <= 3; }

        public static void Update()
        {
            var state = SDL.SDL_GetMouseState(out _x, out _y);
            
            // マウスが左クリックされているかどうかチェック
            if ((state & SDL.SDL_BUTTON_LMASK) != 0)
            {
                if (ReleasingCount[SDL.SDL_BUTTON_LEFT] > 0)
                {
                    ReleasingCount[SDL.SDL_BUTTON_LEFT] = 0;
                }

                PressingCount[SDL.SDL_BUTTON_LEFT]++;
            }
            else
            {
                if (PressingCount[SDL.SDL_BUTTON_LEFT] > 0)
                {
                    PressingCount[SDL.SDL_BUTTON_LEFT] = 0;
                }

                ReleasingCount[SDL.SDL_BUTTON_LEFT]++;
            }
            
            // マウスが真ん中クリックされているかどうかチェック
            if ((state & SDL.SDL_BUTTON_MMASK) != 0)
            {
                if (ReleasingCount[SDL.SDL_BUTTON_MIDDLE] > 0)
                {
                    ReleasingCount[SDL.SDL_BUTTON_MIDDLE] = 0;
                }

                PressingCount[SDL.SDL_BUTTON_MIDDLE]++;
            }
            else
            {
                if (PressingCount[SDL.SDL_BUTTON_MIDDLE] > 0)
                {
                    PressingCount[SDL.SDL_BUTTON_MIDDLE] = 0;
                }

                ReleasingCount[SDL.SDL_BUTTON_MIDDLE]++;
            }
            
            // マウスが右クリックされているかどうかチェック
            if ((state & SDL.SDL_BUTTON_RMASK) != 0)
            {
                if (ReleasingCount[SDL.SDL_BUTTON_RIGHT] > 0)
                {
                    ReleasingCount[SDL.SDL_BUTTON_RIGHT] = 0;
                }

                PressingCount[SDL.SDL_BUTTON_RIGHT]++;
            }
            else
            {
                if (PressingCount[SDL.SDL_BUTTON_RIGHT] > 0)
                {
                    PressingCount[SDL.SDL_BUTTON_RIGHT] = 0;
                }

                ReleasingCount[SDL.SDL_BUTTON_RIGHT]++;
            }
        }

        public static int GetPressingCount(int buttonCode)
        {
            if (!IsAvailableCode(buttonCode))
            {
                return -1;
            }

            return PressingCount[buttonCode];
        }

        public static int GetReleasingCount(int buttonCode)
        {
            if (!IsAvailableCode(buttonCode))
            {
                return -1;
            }

            return ReleasingCount[buttonCode];
        }

        public static SDL.SDL_Point GetPosition()
        {
            return new SDL.SDL_Point {x = _x, y = _y};
        }
    }
}