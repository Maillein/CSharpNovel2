using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SDL2;

namespace CSharpNovel2.GameSystem
{
    public static class GameCore
    {
        private static IntPtr Window { get; set; } = IntPtr.Zero;
        public static IntPtr Renderer { get; private set; } = IntPtr.Zero;

        public static SDL.SDL_Event GameEvent = new SDL.SDL_Event();

        public static ulong FrameCount { get; set; } = 0;

        private static readonly Dictionary<int, IntPtr> Fonts = new Dictionary<int, IntPtr>();
        private static readonly Dictionary<int, int> FontHeights = new Dictionary<int, int>();

        public static IntPtr GetFont(int size) { return Fonts[size]; }
        public static int GetFontHeight(int size) { return FontHeights[size]; }

        public static IntPtr GetFontFromHeight(int height)
        {
            var size = 0;
            foreach (var (key, value) in FontHeights)
            {
                if (value < height) size = Utils.Max(size, value);
            }

            return Fonts[size];
        }

        public static int GetFontSizeFromHeight(int height)
        {
            var size = 0;
            foreach (var (key, value) in FontHeights)
            {
                if (value < height) size = Utils.Max(size, value);
            }

            return size;
        }

        public static bool Initialize()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
            {
                Console.Error.WriteLine($"Unable to initialize. Error: {SDL.SDL_GetError()}");
                return false;
            }
            
            // Console.WriteLine("SDL was initialized.");

            Window = SDL.SDL_CreateWindow("Title", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED,
                Define.WindowWidth, Define.WindowHeight, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (Window == IntPtr.Zero)
            {
                Console.Error.WriteLine($"Unable to create window. Error: {SDL.SDL_GetError()}");
                return false;
            }
            
            // Console.WriteLine("Window was created.");

            Renderer = SDL.SDL_CreateRenderer(Window, -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (Renderer == IntPtr.Zero)
            {
                Console.Error.WriteLine($"Unable to create renderer. Error: {SDL.SDL_GetError()}");
                return false;
            }
            
            SDL.SDL_SetRenderDrawColor(Renderer, 0, 0, 0, 0);
            
            // Console.WriteLine("Renderer was created.");

            if ((SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) &
                 (int) SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Console.Error.WriteLine($"Unable to initialize SDL_image. Error: {SDL.SDL_GetError()}");
                return false;
            }
            
            if (SDL_ttf.TTF_Init() < 0)
            {
                Console.Error.WriteLine($"Unable to initialize SDL_ttf. Error: {SDL.SDL_GetError()}");
                return false;
            }

            for (var i = 1; i <= 40; i++)
            {
                Fonts.Add(i, SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", i));
                if (Fonts[i] == IntPtr.Zero)
                {
                    Console.WriteLine("Unable to load Font.");
                    return false;
                }

                FontHeights.Add(i, SDL_ttf.TTF_FontHeight(Fonts[i]));
            }
            
            return true;
        }

        public static void FinalizeGame()
        {
            SDL.SDL_DestroyRenderer(Renderer);
            SDL.SDL_DestroyWindow(Window);
            SDL.SDL_Quit();
        }
    }
}