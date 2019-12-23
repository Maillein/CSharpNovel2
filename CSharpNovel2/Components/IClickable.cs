using System;
using SDL2;

namespace CSharpNovel2.Components
{
    public interface IClickable
    {
        bool IsClicked();
        bool IsMouseOvered();
        delegate bool OnClick();
    }
}