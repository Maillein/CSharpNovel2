using System.Threading.Tasks;
using CSharpNovel2.Components;
using SDL2;

namespace CSharpNovel2.GameSystem
{
    public static class SystemMain
    {
        public static bool Initialize() { return GameCore.Initialize(); }

        public static void FinalizeGame() { GameCore.FinalizeGame(); }

        public static void MainLoop()
        {
            var looper = new Looper();

            while (SDL.SDL_RenderClear(GameCore.Renderer) == 0)
            {
                GameCore.FrameCount++;
                SDL.SDL_PollEvent(out GameCore.GameEvent);
                if (GameCore.GameEvent.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    var flag = false;
                    var yes = new TextButton(540, 390, "はい", () => true, Define.DeepSkyBlue, Define.WhiteTranslucent, 24);
                    var no = new TextButton(720, 390, "いいえ", () => true, Define.DeepSkyBlue, Define.WhiteTranslucent, 24);
                    var mask = new Image.Image("window_gray_mask.png");
                    var window = new Image.Image("dialog_window.png");
                    while (SDL.SDL_RenderClear(GameCore.Renderer) == 0)
                    {
                        SDL.SDL_PollEvent(out GameCore.GameEvent);
                        looper.RenderOnly();
                        var yf = yes.Update();
                        var nf = no.Update();
                        if (yf || nf)
                        {
                            flag = yf;
                            break;
                        }

                        mask.Render(0, 0);
                        window.Render(480, 258);
                        Text.Print(530, 300, "ゲームを終了しますか？", 24, new SDL.SDL_Color {r = 255, g = 255, b = 255, a = 0});
                        yes.Render();
                        no.Render();
                        SDL.SDL_RenderPresent(GameCore.Renderer);
                    }

                    if (flag)
                    {
                        break;
                    }

                    continue;
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