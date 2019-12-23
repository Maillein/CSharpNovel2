using CSharpNovel2.Scene;
using SDL2;

namespace CSharpNovel2.System
{
    public abstract class AbstractScene
    {
        protected readonly IOnSceneChangeListener ImplSceneChanged;

        protected AbstractScene(IOnSceneChangeListener impl, Parameter parameter) { ImplSceneChanged = impl; }
        public abstract bool Update();
        public abstract void Render();
    }
}