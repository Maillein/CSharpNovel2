using CSharpNovel2.Scene;
using CSharpNovel2.System;
using NotImplementedException = System.NotImplementedException;

namespace CSharpNovel2.Game
{
    public class GameScene : AbstractScene
    {
        public GameScene(IOnSceneChangeListener impl, Parameter parameter) : base(impl, parameter)
        {
        }

        public override bool Update()
        {
            return true;
        }

        public override void Render()
        {
        }
    }
}