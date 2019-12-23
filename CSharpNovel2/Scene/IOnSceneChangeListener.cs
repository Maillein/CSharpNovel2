namespace CSharpNovel2.Scene
{
    public interface IOnSceneChangeListener
    {
        void OnSceneChanged(EScene scene, Parameter parameter, bool stackClear);
        void PopScene();
    }
}