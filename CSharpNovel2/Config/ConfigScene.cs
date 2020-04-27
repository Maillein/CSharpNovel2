using CSharpNovel2.Components;
using CSharpNovel2.GameSystem;
using CSharpNovel2.Scene;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2.Config
{
    public class ConfigScene : AbstractScene
    {
        private readonly Slider _textSpeedSlider;
        private readonly TextButton _backButton;

        private bool ChangeTextSpeed()
        {
            Define.TextSpeed = (int) _textSpeedSlider.Value;
            return true;
        }

        private bool OnBackClick()
        {
            implSceneChanged.PopScene();
            return true;
        }
        
        public ConfigScene(IOnSceneChangeListener impl, Parameter parameter) : base(impl, parameter)
        {
            _textSpeedSlider = new Slider("TextSpeed", 1, 60, Define.TextSpeed, new SDL.SDL_Rect{x=300, y=112, w=200, h=12}, ChangeTextSpeed);
            _backButton = new TextButton(1180, 670, "戻る",  OnBackClick, Define.DeepSkyBlue, Define.DarkGray, 30 );
        }

        public override bool Update()
        {
            _textSpeedSlider.Update();
            _backButton.Update();
            return true;
        }

        public override void Render()
        {
            Text.Print(100, 100, "テキストの速さ", 24, Define.White);
            _textSpeedSlider.Render();
            _backButton.Render();
        }
    }
}