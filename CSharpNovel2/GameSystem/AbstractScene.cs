using CSharpNovel2.Scene;

namespace CSharpNovel2.GameSystem
{
    public abstract class AbstractScene
    {
        protected readonly IOnSceneChangeListener ImplSceneChanged;

        protected AbstractScene(IOnSceneChangeListener impl, Parameter parameter) { ImplSceneChanged = impl; }
        public abstract bool Update();
        public abstract void Render();
    }
}