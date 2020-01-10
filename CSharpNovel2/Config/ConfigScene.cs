using CSharpNovel2.GameSystem;
using CSharpNovel2.Scene;
using CSharpNovel2.System;
using NotImplementedException = System.NotImplementedException;

namespace CSharpNovel2.Config
{
    public class ConfigScene : AbstractScene
    {
        public ConfigScene(IOnSceneChangeListener impl, Parameter parameter) : base(impl, parameter)
        {
        }

        public override bool Update() { throw new NotImplementedException(); }

        public override void Render() { throw new NotImplementedException(); }
    }
}