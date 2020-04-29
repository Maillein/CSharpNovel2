using System.Collections.Generic;
using CSharpNovel2.GameSystem;
using CSharpNovel2.Image;
using CSharpNovel2.Scene;

namespace CSharpNovel2.Game
{
    public sealed class GameScene : AbstractScene
    {
        private readonly int _backgroundImageHandel;
        private readonly MessageWindow _messageWindow;
        
        public GameScene(IOnSceneChangeListener impl, Parameter parameter) : base(impl, parameter)
        {
            _backgroundImageHandel = ImagePool.Load("default_background.png");
            _messageWindow = new MessageWindow
            {
                Name = "ナレーター", Message = "", MessageQueue = new Queue<string>()
            };
            
            _messageWindow.MessageQueue.Enqueue("ノベルゲームの典型的なフォーマットです。\v\n"
                                                +"画面上部3/4には背景や立ち絵、下部1/4にはメッセージウィンドウが表示されています。");
            _messageWindow.MessageQueue.Enqueue("次のメッセージに切り替えることもできます。");
            _messageWindow.MessageQueue.Enqueue("このように非常に長いメッセージもメッセージウィンドウの端で自動的に折り返して"
                                                + "表示することができます。ただし、禁則処理はまだ実装していないため、句読点や「」などを"
                                                + "使用する際は気をつけましょう。");
            _messageWindow.MessageQueue.Enqueue("将来的には文字に色をつけたいと思っていますが、\v\n"
                                                + "技術的に難しいので当分の間は白一色です。");
            _messageWindow.MessageQueue.Enqueue("現在\v\nテキストボックスは\v\n5行まで\v\n表示\v\nできます。");
            _messageWindow.Next();
        }
        
        public override bool Update()
        {
            _messageWindow.Update();
            return true;
        }

        public override void Render()
        {
            ImagePool.Render(_backgroundImageHandel, 0, 0);
            _messageWindow.Render();
        }
    }
}