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
                    case Type.MString:
                        break;
                    case Type.MInt:
                        break;
                    case Type.MDecimal:
                        break;
                    case Type.Label:
                        break;
                    case Type.Lines:
                        Lines(expr as Lines);
                        break;
                    case Type.Variable:
                        break;
                    case Type.Tag:
                        break;
                    case Type.TagArg:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Lines(Lines lines)
        {
            _messageWindow.AddMessage(lines.Value);
        }
    }
}