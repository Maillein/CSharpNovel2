using System;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2.Image
{
    public class Image
    {
        private string _filePath;
        private string _fileName;
        // private int _width = 0;
        // private int _height = 0;
        private IntPtr _texture = IntPtr.Zero;

        Image(string fileName)
        {
            _fileName = fileName;
            _filePath = "./media/images/" + fileName;
            _texture = SDL_image.IMG_LoadTexture(GameCore.Renderer, _filePath);
        }
    }
}