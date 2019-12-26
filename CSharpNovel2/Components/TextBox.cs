using System;
using CSharpNovel2.System;
using SDL2;
using NotImplementedException = System.NotImplementedException;

namespace CSharpNovel2.Components
{
    public class TextBox : IComponents, IClickable
    {
        private SDL.SDL_Rect _clipRect = new SDL.SDL_Rect();
        private bool _editing = false;
        private string _value;

        public string Value { get; set; }

        public TextBox(SDL.SDL_Rect clipRect, string value)
        {
            _clipRect = clipRect;
            Value = value;
        }

        public bool Update()
        {
            if (!_editing)
            {
                if (IsClicked())
                {
                    _editing = true;
                    SDL.SDL_StartTextInput();
                }
                else return true;
            }

            if (_editing)
            {
                if (IsMouseOvered())
                {
                    _editing = false;
                    SDL.SDL_StopTextInput();
                }
                else
                {
                    switch (GameCore.GameEvent.type)
                    {
                        case SDL.SDL_EventType.SDL_TEXTINPUT:
                            unsafe
                            {
                                fixed (byte* ch = GameCore.GameEvent.text.text)
                                {
                                    // Value += (string) ch;
                                }
                                break;
                            }

                        case SDL.SDL_EventType.SDL_TEXTEDITING:
                            break;
                    }
                }
            }
            return true;
        }

        public bool Render()
        {
            throw new NotImplementedException();
        }

        public bool IsMouseOvered()
        {
            var mousePos = Mouse.GetPosition();
            return    _clipRect.x <= mousePos.x && mousePos.x <= _clipRect.x + _clipRect.w 
                   && _clipRect.y <= mousePos.y && mousePos.y <= _clipRect.y + _clipRect.h;
        }

        public bool IsClicked()
        {
            return IsMouseOvered() && Mouse.GetPressingCount(SDL.SDL_BUTTON_LEFT) == 1;
        }

        public bool IsOuterClicked()
        {
            return !IsMouseOvered() && Mouse.GetPressingCount(SDL.SDL_BUTTON_LEFT) == 1 ||
                   Keyboard.GetPressingCount((int) SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE) == 1;
        }
    }
}