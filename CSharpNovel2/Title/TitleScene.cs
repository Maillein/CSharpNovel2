using System.Collections.Generic;
using CSharpNovel2.Components;
using CSharpNovel2.Image;
using CSharpNovel2.Scene;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2.Title
{
    public sealed class TitleScene : AbstractScene
    {
        private readonly int _menuMaskHandle;
        private readonly Dictionary<string, TextButton> _buttons = new Dictionary<string, TextButton>();

        private void Free()
        {
            ImagePool.Free(_menuMaskHandle);
        }
        
        private bool OnStartClick()
        {
            // Console.WriteLine("Start was clicked.");
            var parameter = new Parameter();
            parameter.Set("key", 123);
            implSceneChanged.OnSceneChanged(EScene.Game, parameter, false);
            Free();
            return true;
        }

        private bool OnQuitClick()
        {
            var flag = false;
            var yes = new TextButton(540, 390, "はい", () => true, Define.DeepSkyBlue, Define.WhiteTranslucent, 24);
            var no = new TextButton(720, 390, "いいえ", () => true, Define.DeepSkyBlue, Define.WhiteTranslucent, 24);
            var mask = new Image.Image("window_gray_mask.png");
            var window = new Image.Image("dialog_window.png");
            while (SDL.SDL_RenderClear(GameCore.Renderer) == 0)
            {
                SDL.SDL_PollEvent(out GameCore.GameEvent);
                Keyboard.Update();
                Mouse.Update();
                Render();
                var yf = yes.Update();
                var nf = no.Update();
                if (yf || nf)
                {
                    flag = nf;
                    break;
                }

                mask.Render(0, 0);
                window.Render(480, 258);
                Text.Print(530, 300, "ゲームを終了しますか？", 24, new SDL.SDL_Color{r = 255, g = 255, b = 255, a = 0});
                yes.Render();
                no.Render();
                SDL.SDL_RenderPresent(GameCore.Renderer);
            }

            return !flag;
        }
        public TitleScene(IOnSceneChangeListener impl, Parameter parameter) : base(impl, parameter)
        {
            _menuMaskHandle = ImagePool.Load("title_menu_mask.png");
            _buttons.Add("start", new TextButton(130, 100, "スタート", OnStartClick, Define.DeepSkyBlue, Define.White, 20));
            _buttons.Add("load", new TextButton(130, 130, "ロード", () => true, Define.DeepSkyBlue, Define.White, 20));
            _buttons.Add("setting", new TextButton(130, 160, "環境設定", () => true, Define.DeepSkyBlue, Define.White, 20));
            _buttons.Add("version", new TextButton(130, 190, "バージョン情報", () => true, Define.DeepSkyBlue, Define.White, 20));
            _buttons.Add("help", new TextButton(130, 220, "ヘルプ", () => true, Define.DeepSkyBlue, Define.White, 20));
            _buttons.Add("quit", new TextButton(130, 250, "終了", OnQuitClick, Define.DeepSkyBlue, Define.White, 20));
        }

        public override bool Update()
        {
            foreach (var (key, value) in _buttons)
            {
                if (key != "quit") value.Update();
            }
            return !_buttons["quit"].Update(); // <- なんでうまくいくの？
        }

        public override void Render()
        {
            ImagePool.Render(_menuMaskHandle, 0, 0);
            foreach (var (_, value) in _buttons) value.Render();
        }
    }
}