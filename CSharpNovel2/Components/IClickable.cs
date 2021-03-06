using System;
using SDL2;

namespace CSharpNovel2.Components
{
    public interface IClickable
    {
        bool IsMouseOvered();
        bool IsClicked();
        delegate bool OnClick();
    }
}