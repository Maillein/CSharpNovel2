using System;
using CSharpNovel2.GameSystem;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2.Components
{
    public delegate bool OnClick();

    public class TextButton : IButton
    {
        private SDL.SDL_Rect _clipRect;
        private readonly IntPtr _mouseOverTexture;
        private readonly IntPtr _notMouseOverTexture;
        private readonly OnClick _onClick;

        public TextButton(int x, int y, string text, OnClick onClick, SDL.SDL_Color mouseOverColor,
            SDL.SDL_Color notMouseOverColor, int size)
        {
            var font = GameCore.GetFont(size);
            _onClick = onClick;
            var mouseOverImg = SDL_ttf.TTF_RenderUTF8_Blended(font, text, mouseOverColor);
            var notMouseOverImg = SDL_ttf.TTF_RenderUTF8_Blended(font, text, notMouseOverColor);
            _mouseOverTexture = SDL.SDL_CreateTextureFromSurface(GameCore.Renderer, mouseOverImg);
            _notMouseOverTexture = SDL.SDL_CreateTextureFromSurface(GameCore.Renderer, notMouseOverImg);
            _clipRect.x = x;
            _clipRect.y = y;
            SDL.SDL_QueryTexture(_mouseOverTexture, out _, out _, out _clipRect.w, out _clipRect.h);
            SDL.SDL_FreeSurface(mouseOverImg);
            SDL.SDL_FreeSurface(notMouseOverImg);
        }

        public bool Update() { return IsClicked() ? _onClick() : IsClicked(); }

        public bool Render()
        {
            var sdlRenderCopy = IsMouseOvered()
                ? SDL.SDL_RenderCopy(GameCore.Renderer, _mouseOverTexture, IntPtr.Zero, ref _clipRect)
                : SDL.SDL_RenderCopy(GameCore.Renderer, _notMouseOverTexture, IntPtr.Zero, ref _clipRect);
            return sdlRenderCopy == 0;
        }

        public bool IsMouseOvered()
        {
            var mousePos = Mouse.GetPosition();
            return    _clipRect.x <= mousePos.x && mousePos.x <= _clipRect.x + _clipRect.w 
                   && _clipRect.y <= mousePos.y && mousePos.y <= _clipRect.y + _clipRect.h;
        }

        public bool IsClicked() { return IsMouseOvered() && Mouse.GetPressingCount(SDL.SDL_BUTTON_LEFT) == 1; }
    }
}