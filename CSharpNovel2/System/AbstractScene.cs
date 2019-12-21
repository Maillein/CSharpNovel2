using CSharpNovel2.Scene;

namespace CSharpNovel2.System
{
    public abstract class AbstractScene
    {
        private IOnSceneChangeListener _implSceneChanged;
        protected Keyboard Keyboard = new Keyboard();

        protected AbstractScene(IOnSceneChangeListener impl, Parameter parameter) { _implSceneChanged = impl; }
        public abstract bool Update();
        public abstract void Render();
    }
}