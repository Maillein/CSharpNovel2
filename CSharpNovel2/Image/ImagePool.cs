using System;
using System.Collections.Generic;
using SDL2;

namespace CSharpNovel2.Image
{
    public static class ImagePool
    {
        private static readonly Dictionary<string, int> HandleDictionary = new Dictionary<string, int>();
        private static readonly Dictionary<int, string> NameDictionary = new Dictionary<int, string>();
        private static readonly Dictionary<int, Tuple<int, int>> DivImgWh = new Dictionary<int, Tuple<int, int>>();
        private static readonly Dictionary<int, Image> Images = new Dictionary<int, Image>();
        private static readonly Dictionary<int, int> ImagesCount = new Dictionary<int, int>();
        private static readonly List<int> UsedHandle = new List<int>();

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
            return !Images.ContainsKey(handle) ? new SDL.SDL_Rect() : Images[handle].GetSrcRect();
        }

        public static int Load(string fileName)
        {
            if (HandleDictionary.ContainsKey(fileName))
            {
                ImagesCount[HandleDictionary[fileName]]++;
                return HandleDictionary[fileName];
            }

            var _nextHandle = FindHandle();
            Images[_nextHandle] = new Image(fileName);
            ImagesCount[_nextHandle] = 1;
            HandleDictionary[fileName] = _nextHandle;
            NameDictionary[_nextHandle] = fileName;
            return _nextHandle;
        }

        public static int LoadDiv(string fileName, int numOfW, int numOfH)
        {
            if (HandleDictionary.ContainsKey(fileName))
            {
                ImagesCount[HandleDictionary[fileName]]++;
                return HandleDictionary[fileName];
            }
            
            var _nextHandle = FindHandle();
            Images[_nextHandle] = new Image(fileName);
            ImagesCount[_nextHandle] = 1;
            DivImgWh[_nextHandle] = new Tuple<int, int>(numOfW, numOfH);
            HandleDictionary[fileName] = _nextHandle;
            NameDictionary[_nextHandle] = fileName;
            return _nextHandle;
        }

        public static bool Render(int handle, int x, int y)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].Render(x, y) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClip(int handle, int x, int y, SDL.SDL_Rect srcrect)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].RenderClip(x, y, srcrect) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClip(int handle, SDL.SDL_Rect srcrect, SDL.SDL_Rect dstrect)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].RenderClip(srcrect, dstrect) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderMag(int handle, int x, int y, double magnification)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].RenderMag(x, y, magnification) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderDiv(int handle, int index, int x, int y)
        {
            if (!Images.ContainsKey(handle)) return false;
            var num = DivImgWh[handle];
            var size = GetSrcRect(handle);
            var w = size.w / num.Item1;
            var h = size.h / num.Item2;
            if (Images[handle].RenderClip(x, y,
                    new SDL.SDL_Rect {x = w * (index % num.Item1), y = h * (index / num.Item2), w = w, h = h}) ==
                0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderRotate(int handle, double angle)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].RenderRotate(angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClipRotate(int handle, int x, int y, SDL.SDL_Rect srcrect, double angle)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].RenderClipRotate(x, y, srcrect, angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderClipRotate(int handle, SDL.SDL_Rect srcrect, SDL.SDL_Rect dstrect, double angle)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].RenderClipRotate(srcrect, dstrect, angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderMagRotate(int handle, int x, int y, double magnification, double angle)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (Images[handle].RenderMagRotate(x, y, magnification, angle) == 0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool RenderDivRotate(int handle, int index, int x, int y, double angle)
        {
            if (!Images.ContainsKey(handle)) return false;
            var num = DivImgWh[handle];
            var size = GetSrcRect(handle);
            var w = size.w / num.Item1;
            var h = size.h / num.Item2;
            if (Images[handle].RenderClipRotate(x, y,
                    new SDL.SDL_Rect {x = w * (index % num.Item1), y = h * (index / num.Item2), w = w, h = h}, angle) ==
                0) return true;
            Console.Error.WriteLine($"Unable to render {NameDictionary[handle]}. SDL Error: {SDL.SDL_GetError()}");
            return false;
        }

        public static bool Free(int handle)
        {
            if (!Images.ContainsKey(handle)) return false;
            if (ImagesCount[handle] > 1)
            {
                ImagesCount[handle]--;
                return true;
            }

            Images.Remove(handle);
            ImagesCount.Remove(handle);
            HandleDictionary.Remove(NameDictionary[handle]);
            NameDictionary.Remove(handle);
            DivImgWh.Remove(handle);
            UsedHandle.Remove(handle);
            
            return true;
        }
    }
}