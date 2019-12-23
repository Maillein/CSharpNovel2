using System;
using System.Runtime.CompilerServices;
using SDL2;

namespace CSharpNovel2.System
{
    public static class GameCore
    {
        public static IntPtr Window { get; private set; } = IntPtr.Zero;
        public static IntPtr Renderer { get; private set; } = IntPtr.Zero;

        public static SDL.SDL_Event GameEvent = new SDL.SDL_Event();

        public static IntPtr RoundedMgenplus4 { get; private set; } = IntPtr.Zero;
        public static IntPtr RoundedMgenplus8 { get; private set; }= IntPtr.Zero;
        public static IntPtr RoundedMgenplus12 { get; private set; }= IntPtr.Zero;
        public static IntPtr RoundedMgenplus16 { get; private set; }= IntPtr.Zero;
        public static IntPtr RoundedMgenplus18 { get; private set; }= IntPtr.Zero;
        public static IntPtr RoundedMgenplus20 { get; private set; }= IntPtr.Zero;
        public static IntPtr RoundedMgenplus24 { get; private set; } = IntPtr.Zero;
        public static IntPtr RoundedMgenplus36 { get; private set; }= IntPtr.Zero;

        public static IntPtr GetFont(int size)
        {
            return size switch
            {
                4 => RoundedMgenplus4,
                8 => RoundedMgenplus8,
                12 => RoundedMgenplus12,
                16 => RoundedMgenplus16,
                18 => RoundedMgenplus18,
                20 => RoundedMgenplus20,
                24 => RoundedMgenplus24,
                36 => RoundedMgenplus36,
                _ => RoundedMgenplus20,
            };
        }

        public static int FontHeight4 { get; private set; }
        public static int FontHeight8 { get; private set; }
        public static int FontHeight12 { get; private set; }
        public static int FontHeight16 { get; private set; }
        public static int FontHeight18 { get; private set; }
        public static int FontHeight20 { get; private set; }
        public static int FontHeight24 { get; private set; }
        public static int FontHeight36 { get; private set; }
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

            RoundedMgenplus4  = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 4);
            RoundedMgenplus8  = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 8);
            RoundedMgenplus12 = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 12);
            RoundedMgenplus16 = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 16);
            RoundedMgenplus18 = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 18);
            RoundedMgenplus20 = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 20);
            RoundedMgenplus24 = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 24);
            RoundedMgenplus36 = SDL_ttf.TTF_OpenFont("./media/fonts/rounded-mgenplus-1c-regular.ttf", 36);

            FontHeight4  = SDL_ttf.TTF_FontHeight(RoundedMgenplus4);
            FontHeight8  = SDL_ttf.TTF_FontHeight(RoundedMgenplus8);
            FontHeight12 = SDL_ttf.TTF_FontHeight(RoundedMgenplus12);
            FontHeight16 = SDL_ttf.TTF_FontHeight(RoundedMgenplus16);
            FontHeight18 = SDL_ttf.TTF_FontHeight(RoundedMgenplus18);
            FontHeight20 = SDL_ttf.TTF_FontHeight(RoundedMgenplus20);
            FontHeight24 = SDL_ttf.TTF_FontHeight(RoundedMgenplus24);
            FontHeight36 = SDL_ttf.TTF_FontHeight(RoundedMgenplus36);

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