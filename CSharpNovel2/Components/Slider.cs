using CSharpNovel2.GameSystem;
using CSharpNovel2.System;
using SDL2;
using NotImplementedException = System.NotImplementedException;

namespace CSharpNovel2.Components
{
    public delegate bool CallBack();
    
    public class Slider : IComponents, IClickable
    {
        private string _title;
        private (double min, double max) _range;
        private double _value;
        private SDL.SDL_Rect _clipRect = new SDL.SDL_Rect();
        private SDL.SDL_Rect _leftRect = new SDL.SDL_Rect();
        private SDL.SDL_Rect _rightRect = new SDL.SDL_Rect();
        private SDL.SDL_Rect _sliderRect = new SDL.SDL_Rect();
        private CallBack _callBack;

        private bool _changing = false;
        
        public double Value
        {
            get => _value;
            set
            {
                if (value < _range.min) _value = _range.min;
                else if (value > _range.max) _value = _range.max;
                else _value = value;
            }
        }

        public Slider(string title, double min, double max, double value, SDL.SDL_Rect clipRect, CallBack callBack)
        {
            _title = title;
            _range = (min, max);
            Value = value;
            _clipRect = clipRect;
            _leftRect = new SDL.SDL_Rect{x = clipRect.x, y = clipRect.y + clipRect.h / 3, w = (int)(value / (max - min) * clipRect.w), h = clipRect.h / 3};
            _rightRect = new SDL.SDL_Rect{x = clipRect.x + _leftRect.w, y = clipRect.y + clipRect.h / 3, w = clipRect.w - _leftRect.w, h = clipRect.h / 3};
            _sliderRect = new SDL.SDL_Rect { x = clipRect.x + _leftRect.w - 4, y = clipRect.y, w = 8, h = clipRect.h};
            _callBack = callBack;
        }

        public bool Update()
        {
            if (IsClicked()) Value = (Mouse.GetPosition().x - _clipRect.x) / (double)_clipRect.w * (_range.max - _range.min);
            _leftRect.w = (int) (Value / (_range.max - _range.min) * _clipRect.w);
            _rightRect.x = _clipRect.x + _leftRect.w;
            _rightRect.w = _clipRect.w - _leftRect.w;
            _sliderRect.x = _clipRect.x + _leftRect.w - 4;
            return _callBack();
        }

        public bool Render()
        {
            SDL.SDL_SetRenderDrawColor(GameCore.Renderer, Define.DeepSkyBlue.r,Define.DeepSkyBlue.g,Define.DeepSkyBlue.b,Define.DeepSkyBlue.a);
            var ret = SDL.SDL_RenderFillRect(GameCore.Renderer, ref _leftRect) == 0;
            SDL.SDL_SetRenderDrawColor(GameCore.Renderer, Define.DarkGray.r,Define.DarkGray.g,Define.DarkGray.b,Define.DarkGray.a);
            if (SDL.SDL_RenderFillRect(GameCore.Renderer, ref _rightRect) != 0) ret = false;
            SDL.SDL_SetRenderDrawColor(GameCore.Renderer, Define.DeepSkyBlue.r,Define.DeepSkyBlue.g,Define.DeepSkyBlue.b,Define.DeepSkyBlue.a);
            if (SDL.SDL_RenderFillRect(GameCore.Renderer, ref _sliderRect) != 0) ret = false;
            SDL.SDL_SetRenderDrawColor(GameCore.Renderer, 0, 0, 0, 0);
            return ret;
        }

        public bool IsMouseOvered()
        {
            var mousePos = Mouse.GetPosition();
            return    _clipRect.x <= mousePos.x && mousePos.x <= _clipRect.x + _clipRect.w 
                   && _clipRect.y <= mousePos.y && mousePos.y <= _clipRect.y + _clipRect.h;
        }

        public bool IsClicked()
        {
            if (!_changing) return _changing = IsMouseOvered() && Mouse.GetPressingCount(SDL.SDL_BUTTON_LEFT) > 0;
            if (Mouse.GetReleasingCount(SDL.SDL_BUTTON_LEFT) <= 0) return true;
            _changing = false;
            return _changing;
        }
    }
}