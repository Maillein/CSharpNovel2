using SDL2;

namespace CSharpNovel2.Components
{
    public interface IComponents
    {
        bool Update();
        bool Render();
    }
}