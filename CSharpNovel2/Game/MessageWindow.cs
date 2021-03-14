using System.Collections.Generic;
using System.Linq;
using CSharpNovel2.Components;
using CSharpNovel2.GameSystem;
using CSharpNovel2.Image;
using CSharpNovel2.System;
using SDL2;

namespace CSharpNovel2.Game
{
    public class MessageWindow : IComponents, IClickable
    {
        private static MessageWindow _messageWindow = new MessageWindow();

        public static MessageWindow getInstance = _messageWindow;
        
        private readonly int _messageWindowImageHandle;
        private readonly int _waitClickImageHandle;
        private int _waitClickImageIndex;
        private readonly WrappedText _message;

        public string Name { private get; set; }

        public string Message { set => _message.SetText(value); }
        public Queue<string> MessageQueue { get; set; }

        public bool IsShowing => _message.IsShowing;
        public bool IsWaiting => _message.IsWaiting;

        private MessageWindow()
        {
            _messageWindowImageHandle = ImagePool.Load("message_window.png");
            _waitClickImageHandle = ImagePool.LoadDiv("wait_click.png", 4,1);
            _message = new WrappedText(20, 150, 560, 980);
            Name = "";
            MessageQueue = new Queue<string>();
        }
        
        public bool Update()
        {
            _message.Update();

            if (GameCore.FrameCount % 10 == 0)
            {
                _waitClickImageIndex = (_waitClickImageIndex + 1) % 4;
            }
            
            if (IsClicked())
            {
                if (!IsShowing)
                {
                    Next();
                    return true;
                }
                if (IsWaiting)
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
            if (IsWaiting || !IsShowing)
            {
                    ImagePool.RenderDiv(_waitClickImageHandle, _waitClickImageIndex, 1200, 670);
            }
            Text.Print(100, 530, Name, 20, Define.White);
            _message.Render();
            return true;
        }

        public bool IsMouseOvered()
        {
            var mousePos = Mouse.GetPosition();
            // return 0 <= mousePos.x && mousePos.x <= 1280 && 0 <= mousePos.y && mousePos.y <= 710;
            return 0 <= mousePos.x && mousePos.x <= 1280 && 530 <= mousePos.y && mousePos.y <= 710;
        }

        public bool IsClicked()
        {
            return IsMouseOvered() && Mouse.GetPressingCount(SDL.SDL_BUTTON_LEFT) == 1 
                   || Keyboard.GetPressingCount(SDL.SDL_Scancode.SDL_SCANCODE_RETURN) == 1
                   || Keyboard.GetPressingCount(SDL.SDL_Scancode.SDL_SCANCODE_SPACE) == 1;
        }

        public bool Next()
        {
            if (MessageQueue.Count <= 0) return false;
            Message = MessageQueue.Dequeue();
            return true;
        }

        public bool AddMessage(string message)
        {
            MessageQueue.Enqueue(message);
            return true;
        }
    }
}