using SDL2;

namespace CSharpNovel2
{
    public static class Define
    {
        public const int WindowWidth = 1280;
        public const int WindowHeight = 720;

        public static readonly SDL.SDL_Color White = new SDL.SDL_Color {r = 255, g = 255, b = 255, a = 0};
        public static readonly SDL.SDL_Color WhiteTranslucent = new SDL.SDL_Color {r = 255, g = 255, b = 255, a = 128};
        public static readonly SDL.SDL_Color DeepSkyBlue = new SDL.SDL_Color {r = 0, g = 0xbf, b = 0xff, a = 0};
    }
}