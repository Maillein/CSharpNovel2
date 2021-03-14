using System;
using System.Runtime.InteropServices;
using CSharpNovel2.GameSystem;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2.Image
{
    public class Image
    {
        public string FilePath { get; }
        public string FileName { get; }

        private readonly int _width = 0;
        private readonly int _height = 0;
        private readonly IntPtr _texture;

        public Image(string fileName)
        {
            FileName = fileName;
            FilePath = "./media/images/" + fileName;
            _texture = SDL_image.IMG_LoadTexture(GameCore.Renderer, FilePath);
            if (_texture == IntPtr.Zero)
            {
                Console.Error.WriteLine($"Unable to load file: {fileName}. SDL_image error: {SDL.SDL_GetError()}");
            }
            else
            {
                SDL.SDL_QueryTexture(_texture, out _, out _, out _width, out _height);
            }
        }

        public SDL.SDL_Rect GetSrcRect()
        {
            return new SDL.SDL_Rect {x = 0, y = 0, w = _width, h = _height};
        }

        public int Render(int x, int y)
        {
            var srcrect = new SDL.SDL_Rect {x = 0, y = 0, w = _width, h = _height};
            var dstrect = new SDL.SDL_Rect
            {
                x = x * GameCore.ViewportRect.w / 1280, y = y * GameCore.ViewportRect.h / 720,
                w = _width * GameCore.ViewportRect.w / 1280, h = _height * GameCore.ViewportRect.h / 720
            };
            return SDL.SDL_RenderCopy(GameCore.Renderer, _texture, ref srcrect, ref dstrect);
        }

        public int RenderClip(int x, int y, SDL.SDL_Rect srcrect)
        {
            var dstrect = new SDL.SDL_Rect
            {
                x = x * GameCore.ViewportRect.w / 1280, y = y * GameCore.ViewportRect.h / 720,
                w = srcrect.w * GameCore.ViewportRect.w / 1280, h = srcrect.h * GameCore.ViewportRect.h / 720
            };
            return SDL.SDL_RenderCopy(GameCore.Renderer, _texture, ref srcrect, ref dstrect);
        }

        public int RenderClip(SDL.SDL_Rect srcrect, SDL.SDL_Rect dstrect)
        {
            return SDL.SDL_RenderCopy(GameCore.Renderer, _texture, ref srcrect, ref dstrect);
        }

        public int RenderMag(int x, int y, double magnification)
        {
            var srcrect = GetSrcRect();
            var dstrect = new SDL.SDL_Rect
                {x = x, y = y, w = (int) (srcrect.w * magnification), h = (int) (srcrect.h * magnification)};
            return SDL.SDL_RenderCopy(GameCore.Renderer, _texture, ref srcrect, ref dstrect);
        }

        public int RenderRotate(double angle)
        {
            var srcrect = GetSrcRect();
            var dstrect = GetSrcRect();
            return SDL.SDL_RenderCopyEx(GameCore.Renderer, _texture, ref srcrect, ref dstrect, angle * 180.0 / 3.1415,
                IntPtr.Zero, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public int RenderClipRotate(int x, int y, SDL.SDL_Rect srcrect, double angle)
        {
            var dstrect = new SDL.SDL_Rect {x = x, y = y, w = srcrect.w, h = srcrect.h};
            return SDL.SDL_RenderCopyEx(GameCore.Renderer, _texture, ref srcrect, ref dstrect, angle * 180.0 / 3.1415,
                IntPtr.Zero, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public int RenderClipRotate(SDL.SDL_Rect srcrect, SDL.SDL_Rect dstrect, double angle)
        {
            return SDL.SDL_RenderCopyEx(GameCore.Renderer, _texture, ref srcrect, ref dstrect, angle * 180.0 / 3.1415,
                IntPtr.Zero, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public int RenderMagRotate(int x, int y, double magnification, double angle)
        {
            var srcrect = GetSrcRect();
            var dstrect = new SDL.SDL_Rect
                {x = x, y = y, w = (int) (srcrect.w * magnification), h = (int) (srcrect.h * magnification)};
            return SDL.SDL_RenderCopyEx(GameCore.Renderer, _texture, ref srcrect, ref dstrect, angle * 180.0 / 3.1415,
                IntPtr.Zero, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}