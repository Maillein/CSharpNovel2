using System;
using System.Collections.Generic;
using CSharpNovel2.Game;
using CSharpNovel2.Scene;
using CSharpNovel2.Title;

namespace CSharpNovel2.System
{
    public sealed class Looper : IOnSceneChangeListener
    {
        private readonly Stack<AbstractScene> _sceneStack = new Stack<AbstractScene>();
        private readonly Fps _fps = new Fps();

        public Looper()
        {
            var parameter = new Parameter();
            _sceneStack.Push(new TitleScene(this, parameter));
        }

        public bool Loop()
        {
            Keyboard.Update();
            Mouse.Update();
            var ret = _sceneStack.Peek().Update();
            _sceneStack.Peek().Render();
            _fps.Render();
            _fps.Wait();
            return ret;
        }

        public bool RenderOnly()
        {
            Keyboard.Update();
            Mouse.Update();
            _sceneStack.Peek().Render();
            _fps.Render();
            _fps.Wait();
            return true;
        }

        public void OnSceneChanged(EScene scene, Parameter parameter, bool stackClear)
        {
            if (stackClear)
            {
                _sceneStack.Clear();
            }

            switch (scene)
            {
                case EScene.Title:
                    _sceneStack.Push(new TitleScene(this, parameter));
                    break;
                case EScene.Game:
                     _sceneStack.Push(new GameScene(this, parameter));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, "シーン処理中に不明なエラーが発生しました。");
            }
        }

        public void PopScene()
        {
            if (_sceneStack.Count > 0)
            {
                _sceneStack.Pop();
            }
        }
    }
}