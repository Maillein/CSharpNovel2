namespace CSharpNovel2.Scene
{
    public interface IOnSceneChangeListener
    {
        void OnSceneChanged(eScene scene, Parameter parameter, bool stackClear);
        void PopScene();
    }
}