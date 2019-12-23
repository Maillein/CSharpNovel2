using System;
using System.Collections.Generic;
using System.Numerics;
using SDL2;

namespace CSharpNovel2.Image
{
    public static class ImagePool
    {
        private static Dictionary<string, int> _handleDictionary = new Dictionary<string, int>();
        private static Dictionary<int, string> _nameDictionary = new Dictionary<int, string>();
        private static Dictionary<int, Tuple<int, int>> _divImgWh = new Dictionary<int, Tuple<int, int>>();
        private static Dictionary<int, Image> _images = new Dictionary<int, Image>();
        private static Dictionary<int, int> _imagesCount = new Dictionary<int, int>();
        private static List<int> UsedHandle = new List<int>();

        private static int FindHandle()
        {
            if (UsedHandle.Count == 0)
            {
                UsedHandle.Add(0);
                return 0;
            }

            UsedHandle.Sort();
            for (var i = 0; i < UsedHandle.Count; i++)
            {
                if (UsedHandle[i] == i) continue;
                UsedHandle.Add(i);
                return i;
            }

            var ret = UsedHandle.Count;
            UsedHandle.Add(ret);
            return ret;
        }
        
        private static SDL.SDL_Rect GetSrcRect(int handle)
        {
            return !_images.ContainsKey(handle) ? new SDL.SDL_Rect() : _images[handle].GetSrcRect();
        }

        public static int Load(string fileName)
        {
            if (_handleDictionary.ContainsKey(fileName))
            {
                _imagesCount[_handleDictionary[fileName]]++;
                return _handleDictionary[fileName];
            }

            var _nextHandle = FindHandle();
            _images[_nextHandle] = new Image(fileName);
            _imagesCount[_nextHandle] = 1;
            _handleDictionary[fileName] = _nextHandle;
            _nameDictionary[_nextHandle] = fileName;
            return _nextHandle;
        }

        public static int LoadDiv(string fileName, int numOfW, int numOfH)
        {
            if (_handleDictionary.ContainsKey(fileName))
            {
                _imagesCount[_handleDictionary[fileName]]++;
                return _handleDictionary[fileName];
            }
            
            var _nextHandle = FindHandle();
            _images[_nextHandle] = new Image(fileName);
            _imagesCount[_nextHandle] = 1;
            _divImgWh[_nextHandle] = new Tuple<int, int>(numOfW, numOfH);
            _handleDictionary[fileName] = _nextHandle;
            _nameDictionary[_nextHandle] = fileName;
            return _nextHandle;
        }

        public static bool Render(int handle, int x, int y)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].Render(x, y) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClip(int handle, int x, int y, SDL.SDL_Rect srcrect)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].RenderClip(x, y, srcrect) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClip(int handle, SDL.SDL_Rect srcrect, SDL.SDL_Rect dstrect)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].RenderClip(srcrect, dstrect) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderMag(int handle, int x, int y, double magnification)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].RenderMag(x, y, magnification) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderDiv(int handle, int index, int x, int y)
        {
            if (!_images.ContainsKey(handle)) return false;
            var num = _divImgWh[handle];
            var size = GetSrcRect(handle);
            var w = size.w / num.Item1;
            var h = size.h / num.Item2;
            if (_images[handle].RenderClip(x, y,
                    new SDL.SDL_Rect {x = w * (index % num.Item1), y = h * (index / num.Item2), w = w, h = h}) ==
                0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderRotate(int handle, double angle)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].RenderRotate(angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClipRotate(int handle, int x, int y, SDL.SDL_Rect srcrect, double angle)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].RenderClipRotate(x, y, srcrect, angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClipRotate(int handle, SDL.SDL_Rect srcrect, SDL.SDL_Rect dstrect, double angle)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].RenderClipRotate(srcrect, dstrect, angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderMagRotate(int handle, int x, int y, double magnification, double angle)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_images[handle].RenderMagRotate(x, y, magnification, angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderDivRotate(int handle, int index, int x, int y, double angle)
        {
            if (!_images.ContainsKey(handle)) return false;
            var num = _divImgWh[handle];
            var size = GetSrcRect(handle);
            var w = size.w / num.Item1;
            var h = size.h / num.Item2;
            if (_images[handle].RenderClipRotate(x, y,
                    new SDL.SDL_Rect {x = w * (index % num.Item1), y = h * (index / num.Item2), w = w, h = h}, angle) ==
                0) return true;
            Console.Error.WriteLine($"Unable to render {_nameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool Free(int handle)
        {
            if (!_images.ContainsKey(handle)) return false;
            if (_imagesCount[handle] > 1)
            {
                _imagesCount[handle]--;
                return true;
            }

            _images.Remove(handle);
            _imagesCount.Remove(handle);
            _handleDictionary.Remove(_nameDictionary[handle]);
            _nameDictionary.Remove(handle);
            _divImgWh.Remove(handle);
            UsedHandle.Remove(handle);
            
            return true;
        }
    }
}