using CSharpNovel2.Scene;

namespace CSharpNovel2.GameSystem
{
    public abstract class AbstractScene
    {
        protected readonly IOnSceneChangeListener implSceneChanged;

        protected AbstractScene(IOnSceneChangeListener impl, Parameter parameter) { implSceneChanged = impl; }
        public abstract bool Update();
        public abstract void Render();
    }
}