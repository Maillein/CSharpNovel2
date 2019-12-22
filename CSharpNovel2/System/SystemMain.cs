using SDL2;

namespace CSharpNovel2.System
{
    public sealed class SystemMain
    {
        public static bool Initialize() { return GameCore.Initialize(); }
        
        public static bool FinalizeGame() { return GameCore.FinalizeGame(); }

        public static void MainLoop()
        {
            var looper = new Looper();

            while (SDL.SDL_RenderClear(GameCore.Renderer) == 0)
            {
                SDL.SDL_PollEvent(out GameCore.GameEvent);
                if (GameCore.GameEvent.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    break;
                }

                if (!looper.Loop())
                {
                    break;
                }
                SDL.SDL_RenderPresent(GameCore.Renderer);
            }
        }
    }
}