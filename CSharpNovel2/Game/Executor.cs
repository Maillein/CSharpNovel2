using System;
using FlashScript;
using Type = FlashScript.Type;

namespace CSharpNovel2.Game
{
    public class Executor
    {
        private Parser _parser;
        private MessageWindow _messageWindow = MessageWindow.getInstance;

        public Executor(Parser parser)
        {
            _parser = parser;
        }
        
        public void Next()
        {
            var exprs = _parser.Next();
            foreach (var expr in exprs)
            {
                switch (expr.Type)
                {
                    case Type.M_STRING:
                        break;
                    case Type.M_INT:
                        break;
                    case Type.M_DECIMAL:
                        break;
                    case Type.LABEL:
                        Label(expr as Label);
                        break;
                    case Type.LINES:
                        Lines(expr as Lines);
                        break;
                    case Type.VARIABLE:
                        break;
                    case Type.TAG:
                        Tag(expr as Tag);
                        break;
                    case Type.TAG_ARG:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Label(Label label)
        {
            
        }
        
        private void Lines(Lines lines)
        {
            _messageWindow.AddMessage(lines.Value);
        }

        private void Tag(Tag tag)
        {
            switch (tag.Name)
            {
                case "sn":
                    var name = (tag.Args["name"] as MString)?.Value;
                    _messageWindow.Name = name;
                    break;
                case "cm":
                    _messageWindow.Message = "";
                    Next();
                    break;
            }
        }
    }
}