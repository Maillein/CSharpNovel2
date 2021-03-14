using System;
using System.Runtime.InteropServices;
using CSharpNovel2.GameSystem;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2
{
    public static class Text
    {
        public static void Print(SDL.SDL_Rect dist, string text, int size, SDL.SDL_Color color)
        {
            if (text == "") return;
            var font = GameCore.GetFont(size);
            var image = SDL_ttf.TTF_RenderUTF8_Blended(font, text, color);
            var texture = SDL.SDL_CreateTextureFromSurface(GameCore.Renderer, image);
            dist.x *= GameCore.ViewportRect.w / 1280;
            dist.y *= GameCore.ViewportRect.h / 720;
            dist.w *= GameCore.ViewportRect.w / 1280;
            dist.h *= GameCore.ViewportRect.h / 720;
            SDL.SDL_RenderCopy(GameCore.Renderer, texture, IntPtr.Zero, ref dist);
            SDL.SDL_FreeSurface(image);
            SDL.SDL_DestroyTexture(texture);
        }
        
        public static void Print(int x, int y, string text, int size, SDL.SDL_Color color)
        {
            if (text == "") return;
            var font = GameCore.GetFont(size);
            var image = SDL_ttf.TTF_RenderUTF8_Blended(font, text, color);
            var imageSurf = Marshal.PtrToStructure<SDL.SDL_Surface>(image);
            var texture = SDL.SDL_CreateTextureFromSurface(GameCore.Renderer, image);
            var dist = new SDL.SDL_Rect {x = x, y = y, w = imageSurf.clip_rect.w, h = imageSurf.clip_rect.h};
            dist.x *= GameCore.ViewportRect.w / 1280;
            dist.y *= GameCore.ViewportRect.h / 720;
            dist.w *= GameCore.ViewportRect.w / 1280;
            dist.h *= GameCore.ViewportRect.h / 720;
            SDL.SDL_RenderCopy(GameCore.Renderer, texture, IntPtr.Zero, ref dist);
            SDL.SDL_FreeSurface(image);
            SDL.SDL_DestroyTexture(texture);
        }

        public static void PrintClip(int x, int y, SDL.SDL_Rect clip, string text, int size, SDL.SDL_Color color)
        {
            if (text == "") return;
            var font = GameCore.GetFont(size);
            var image = SDL_ttf.TTF_RenderUTF8_Blended(font, text, color);
            var imageSurf = Marshal.PtrToStructure<SDL.SDL_Surface>(image);
            var texture = SDL.SDL_CreateTextureFromSurface(GameCore.Renderer, image);
            var dist = new SDL.SDL_Rect {x = x, y = y, w = imageSurf.clip_rect.w, h = imageSurf.clip_rect.h};
            dist.x *= GameCore.ViewportRect.w / 1280;
            dist.y *= GameCore.ViewportRect.h / 720;
            dist.w *= GameCore.ViewportRect.w / 1280;
            dist.h *= GameCore.ViewportRect.h / 720;
            SDL.SDL_RenderCopy(GameCore.Renderer, texture, ref clip, ref dist);
            SDL.SDL_FreeSurface(image);
            SDL.SDL_DestroyTexture(texture);
        }

        public static void PrintWrap(int x, int y, uint wrapLen, string text, int size, SDL.SDL_Color color)
        {
            if (text == "") return;
            var font = GameCore.GetFont(size);
            var image = SDL_ttf.TTF_RenderUTF8_Blended_Wrapped(font, text, color, wrapLen);
            var imageSurf = Marshal.PtrToStructure<SDL.SDL_Surface>(image);
            var texture = SDL.SDL_CreateTextureFromSurface(GameCore.Renderer, image);
            var dist = new SDL.SDL_Rect {x = x, y = y, w = imageSurf.clip_rect.w, h = imageSurf.clip_rect.h};
            dist.x *= GameCore.ViewportRect.w / 1280;
            dist.y *= GameCore.ViewportRect.h / 720;
            dist.w *= GameCore.ViewportRect.w / 1280;
            dist.h *= GameCore.ViewportRect.h / 720;
            SDL.SDL_RenderCopy(GameCore.Renderer, texture, IntPtr.Zero, ref dist);
            SDL.SDL_FreeSurface(image);
            SDL.SDL_DestroyTexture(texture);
        }
    }
}