using System;
using System.Runtime.InteropServices;
using System.Text;
using CSharpNovel2.GameSystem;
using CSharpNovel2.System;
using SDL2;
using NotImplementedException = System.NotImplementedException;

namespace CSharpNovel2.Components
{
    public class TextBox : IComponents, IClickable
    {
        private SDL.SDL_Rect _clipRect = new SDL.SDL_Rect();
        private bool _editing = false;
        private int _fontsize;
        private string _composition = "";
        private int _cursor;
        private int _selectionLen;

        public string Value { get; set; }

        public TextBox(SDL.SDL_Rect clipRect, string value)
        {
            _clipRect = clipRect;
            _fontsize = GameCore.GetFontSizeFromHeight(_clipRect.h);
            Value = " " + value;
            SDL.SDL_SetTextInputRect(ref _clipRect);
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
                if (IsOuterClicked())
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
                                    var len = 0;
                                    for (var i = 0; i < SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE; i++)
                                    {
                                        if (ch[i] != 0) continue;
                                        len = i;
                                        break;
                                    }
                                    var arr = new byte[len];
                                    Marshal.Copy((IntPtr)ch, arr, 0, len);
                                    Value += Encoding.UTF8.GetString(arr);
                                }
                            }
                            break;

                        case SDL.SDL_EventType.SDL_TEXTEDITING:
                            unsafe
                            {
                                fixed (byte* ch = GameCore.GameEvent.edit.text)
                                {
                                    var len = 0;
                                    for (var i = 0; i < SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE; i++)
                                    {
                                        if (ch[i] != 0) continue;
                                        len = i;
                                        break;
                                    }
                                    var arr = new byte[len];
                                    Marshal.Copy((IntPtr)ch, arr, 0, len);
                                    _composition = Encoding.UTF8.GetString(arr);
                                    _cursor = GameCore.GameEvent.edit.start;
                                    _selectionLen = GameCore.GameEvent.edit.length;
                                }
                            }
                            break;
                    }
                }
            }
            if (Keyboard.GetPressingCount(SDL.SDL_Scancode.SDL_SCANCODE_BACKSPACE) == 1
                || Keyboard.GetPressingCount(SDL.SDL_Scancode.SDL_SCANCODE_BACKSPACE) > 40)
            {
                if (_composition.Length <= 1 && Value.Length > 1)
                {
                    Value = Value.Remove(Value.Length - 1);
                }
            }
            return true;
        }

        public bool Render()
        {
            SDL.SDL_SetRenderDrawColor(GameCore.Renderer, 200, 200, 200, 200);
            SDL.SDL_RenderDrawRect(GameCore.Renderer, ref _clipRect);
            SDL.SDL_SetRenderDrawColor(GameCore.Renderer, 0, 0, 0, 0);
            Text.Print(_clipRect.x - 5, _clipRect.y - 5, Value + _composition, _fontsize, Define.White);
            return true;
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

        private bool IsOuterClicked()
        {
            return !IsMouseOvered() && Mouse.GetPressingCount(SDL.SDL_BUTTON_LEFT) == 1 ||
                   Keyboard.GetPressingCount( SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE) == 1;
        }
    }
}