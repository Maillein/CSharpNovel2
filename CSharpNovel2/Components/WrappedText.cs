using System;
using System.Collections.Generic;
using CSharpNovel2.GameSystem;
using SDL2;

namespace CSharpNovel2.Components
{
    public class WrappedText : IComponents
    {
        private string _text;
        private readonly List<string> _lines = new List<string>();
        private int _currentLineNumber;
        private int _currentCharNumber;
        private readonly int _fontSize;
        private readonly int _offsetX;
        private readonly int _offsetY;
        private readonly int _widthLimit;
        private readonly int _lineHeight;
        
        public bool IsShowing { get; private set; }
        public bool IsWaiting { get; set; }
        public WrappedText(int fontSize, int offsetX, int offsetY, int widthLimit)
        {
            _fontSize = fontSize;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _widthLimit = widthLimit;
            _lineHeight = GameCore.GetFontHeight(fontSize);
            IsShowing = true;
            IsWaiting = false;
            Clear();
        }

        private void Clear()
        {
            _text = "";
            _lines.Clear();
            _lines.Add("");
            _currentLineNumber = 0;
            _currentCharNumber = 0;
        }

        public void SetText(string text)
        {
            Clear();
            _text = text;
        }

        public bool UpdateText()
        {
            var nextChar = "";
            char ch;
            if (_currentCharNumber < _text?.Length)
            {
                ch = _text[_currentCharNumber++];
            }
            else
            {
                IsShowing = false;
                return true;
            }
            if (ch == '\v')
            {
                IsWaiting = true;
                return true;
            }
            if (ch == '\n')
            {
                ch = _text[_currentCharNumber++];
                nextChar += ch;
                if (0xD800 <= ch && ch <= 0xDFFF)
                {
                    nextChar += _text[_currentCharNumber++];
                }
                _lines.Add(nextChar);
                _currentLineNumber++;
                return true;
            }
            nextChar += ch;
            if (0xD800 <= ch && ch <= 0xDFFF) nextChar += _text[_currentCharNumber++];
            SDL_ttf.TTF_SizeUTF8(GameCore.GetFont(_fontSize), _lines[_currentLineNumber] + nextChar, out var w, out var _);
            if (w > _widthLimit)
            {
                _lines.Add(nextChar);
                _currentLineNumber++;
            }
            else
            {
                _lines[_currentLineNumber] += nextChar;
            }

            return true;
        }

        public void ShowAllText()
        {
            var oldCharNumber = _currentCharNumber;
            UpdateText();
            while (oldCharNumber != _currentCharNumber)
            {
                oldCharNumber = _currentCharNumber;
                UpdateText();
                if (IsWaiting) return;
            }

            IsShowing = false;
        }

        public bool Update()
        {
            if (IsWaiting) return true;
            if (GameCore.FrameCount % (ulong) (Define.Fps / Define.TextSpeed) != 0) return true;
            return UpdateText();
        }

        public bool Render()
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                if (!_lines[i].Equals(""))
                    Text.Print(_offsetX, _offsetY + _lineHeight * i, _lines[i], _fontSize,Define.White);
            }

            return true;
        }
    }
}