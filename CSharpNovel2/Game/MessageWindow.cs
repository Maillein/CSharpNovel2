using System;
using System.Runtime.InteropServices;
using CSharpNovel2.Components;
using CSharpNovel2.GameSystem;
using CSharpNovel2.Image;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2.Game
{
    public class MessageWindow : IComponents, IClickable
    {
        private readonly int _messageWindowImageHandle;
        private readonly WrappedText _message;
        
        public string Name { private get; set; }
        public bool IsShowing => _message.IsShowing;

        public MessageWindow()
        {
            _messageWindowImageHandle = ImagePool.Load("message_window.png");
            _message = new WrappedText(20, 150, 560, 980);
            Name = "";
        }

        public void SetMessage(string message)
        {
            _message.SetText(message);
        }
        
        public bool Update()
        {
            _message.Update();
            
            if (IsClicked())
            {
                if (_message.IsWaiting)
                {
                    _message.IsWaiting = false;
                    return true;
                }
                _message.ShowAllText();
            }
            
            return true;
        }

        public bool Render()
        {
            ImagePool.Render(_messageWindowImageHandle, 0, 530);
            Text.Print(100, 530, Name, 20, Define.White);
            _message.Render();
            return true;
        }

        public bool IsMouseOvered()
        {
            var mousePos = Mouse.GetPosition();
            return 0 <= mousePos.x && mousePos.x <= 1280 && 0 <= mousePos.y && mousePos.y <= 710;
            // return 0 <= mousePos.x && mousePos.x <= 1280 && 530 <= mousePos.y && mousePos.y <= 710;
        }

        public bool IsClicked()
        {
            return IsMouseOvered() && Mouse.GetPressingCount(SDL.SDL_BUTTON_LEFT) == 1 
                   || Keyboard.GetPressingCount(SDL.SDL_Scancode.SDL_SCANCODE_RETURN) == 1
                   || Keyboard.GetPressingCount(SDL.SDL_Scancode.SDL_SCANCODE_SPACE) == 1;
        }
    }
}