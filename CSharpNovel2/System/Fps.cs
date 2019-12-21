using System;
using System.Collections.Generic;
using SDL2;

namespace CSharpNovel2.System
{
    public class Fps
    {
        private const int ListLenMax = 120;
        private const int FPS = 60;
        private const int UpIntvl = 60;

        private readonly List<uint> _list = new List<uint>();
        private double _fps = 0.0;
        private uint _counter = 0;

        private void UpdateAverage()
        {
            var len = _list.Count;
            if (len < ListLenMax)
            {
                return;
            }

            var tookTime = _list[len - 1] - _list[0];
            var average = (double) tookTime / (len - 1);
            if (Math.Abs(average) < 0.1) return;
            _fps = 1000 / average;
        }

        private void Regist()
        {
            _list.Add(SDL.SDL_GetTicks());
            if (_list.Count > ListLenMax)
            {
                _list.RemoveAt(0);
            }
        }

        private uint GetWaitTime()
        {
            var len = _list.Count;
            if (len == 0)
            {
                return 0;
            }

            const int shouldTookTime = (int) (1000.0 / 60.0 * FPS);
            var actuallyTookTime = (int)(SDL.SDL_GetTicks() - _list[0]);
            var waitTime = shouldTookTime - actuallyTookTime;
            waitTime = waitTime > 0 ? waitTime : 0;
            return (uint) waitTime;
        }

        public void Wait()
        {
            _counter++;
            SDL.SDL_Delay(GetWaitTime());
            Regist();
            if (_counter != UpIntvl) return;
            UpdateAverage();
            _counter = 0;
        }

        public void Render()
        {
            if (Math.Abs(_fps) < 0.1)
            {
                return;
            }

            var sfps = _fps.ToString("F2");
            Text.Print( 1233, 698, sfps, 16, new SDL.SDL_Color {r = 200, g = 200, b = 200, a = 0});
        }
    }
}