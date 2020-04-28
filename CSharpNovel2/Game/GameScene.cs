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
                Name = "ナレーター",
                Message = "ノベルゲームの典型的なフォーマットです。\v\n"
                          +"画面上部3/4には背景や立ち絵、下部1/4にはメッセージウィンドウが表示されています。"
            };
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