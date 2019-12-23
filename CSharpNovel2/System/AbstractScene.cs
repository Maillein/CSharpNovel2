using CSharpNovel2.Scene;
using SDL2;

namespace CSharpNovel2.System
{
    public abstract class AbstractScene
    {
        private IOnSceneChangeListener _implSceneChanged;

        protected AbstractScene(IOnSceneChangeListener impl, Parameter parameter) { _implSceneChanged = impl; }
        public abstract bool Update();
        public abstract void Render();
    }
}