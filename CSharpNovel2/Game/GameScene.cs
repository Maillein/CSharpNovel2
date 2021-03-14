using System.Collections.Generic;
using CSharpNovel2.GameSystem;
using CSharpNovel2.Image;
using CSharpNovel2.Scene;
using SDL2;

namespace CSharpNovel2.Game
{
    public sealed class GameScene : AbstractScene
    {
        private readonly int _backgroundImageHandel;
        private readonly MessageWindow _messageWindow;
        private readonly Parser _parser;
        private readonly Executor _executor;
        
        public GameScene(IOnSceneChangeListener impl, Parameter parameter) : base(impl, parameter)
        {
            _backgroundImageHandel = ImagePool.Load("default_background.png");
            _messageWindow = MessageWindow.getInstance;
            _parser = new Parser();
            _executor = new Executor(_parser);
            
            // AddMessage("ノベルゲームの典型的なフォーマットです。\v\n"
            //                                     +"画面上部3/4には背景や立ち絵、下部1/4にはメッセージウィンドウが表示されています。");
            // AddMessage("次のメッセージに切り替えることもできます。");
            // AddMessage("このように非常に長いメッセージもメッセージウィンドウの端で自動的に折り返して"
            //                                     + "表示することができます。ただし、禁則処理はまだ実装していないため、句読点や「」などを"
            //                                     + "使用する際は気をつけましょう。");
            // AddMessage("将来的には文字に色をつけたいと思っていますが、\v\n"
            //                                     + "技術的に難しいので当分の間は白一色です。");
            // AddMessage("現在\v\nテキストボックスは\v\n5行まで\v\n表示\v\nできます。");
            // Next();
        }
        
        public override bool Update()
        {
            if (_messageWindow.IsClicked() && !_messageWindow.IsShowing)
            {
                Next();
            }
            _messageWindow.Update();
            return true;
        }

        public override void Render()
        {
            ImagePool.Render(_backgroundImageHandel, 0, 0);
            _messageWindow.Render();
        }

        public void Next()
        {
            _executor.Next();
        }
    }
}