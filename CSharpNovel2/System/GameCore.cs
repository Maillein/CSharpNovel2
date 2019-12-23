using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDL2;

namespace CSharpNovel2.System
{
    public static class GameCore
    {
        private static IntPtr Window { get; set; } = IntPtr.Zero;
        public static IntPtr Renderer { get; private set; } = IntPtr.Zero;

        public static SDL.SDL_Event GameEvent = new SDL.SDL_Event();
        
        private static readonly Dictionary<int, IntPtr> Fonts = new Dictionary<int, IntPtr>();
        private static readonly Dictionary<int, int> FontHeights = new Dictionary<int, int>();

        public static IntPtr GetFont(int size) { return Fonts[size]; }
        public static int GetFontHeight(int size) { return FontHeights[size]; }
        public static bool Initialize()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
            {
                Console.Error.WriteLine($"Unable to initialize. Error: {SDL.SDL_GetError()}");
                return false;
            }
            
            Window = SDL.SDL_CreateWindow(
                "Title", 
                SDL.SDL_WINDOWPOS_CENTERED, 
                SDL.SDL_WINDOWPOS_CENTERED, 
                Define.WindowWidth, 
                Define.WindowHeight,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (Window == IntPtr.Zero)
            {
                Console.Error.WriteLine($"Unable to create window. Error: {SDL.SDL_GetError()}");
                return false;
            }
            
            Renderer = SDL.SDL_CreateRenderer(Window, -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (Renderer == IntPtr.Zero)
            {
                Console.Error.WriteLine($"Unable to create renderer. Error: {SDL.SDL_GetError()}");
                return false;
            }

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

            for (var i = 1; i < 40; i++)
            {
                Fonts.Add(i, SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", i));
                FontHeights.Add(i, SDL_ttf.TTF_FontHeight(Fonts[i]));
            }
            
            SDL.SDL_SetRenderDrawColor(Renderer, 0, 0, 0, 0);
            
            return true;
        }
        public static bool FinalizeGame()
        {
            SDL.SDL_DestroyRenderer(Renderer);
            SDL.SDL_DestroyWindow(Window);
            SDL.SDL_Quit();
            return true;
        }
    }
}