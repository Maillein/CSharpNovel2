using System.IO;
using FlashScript;
using Sprache;

namespace CSharpNovel2.Game
{
    public class Parser
    {
        private Parser<Expr> _parser;
        private Script _script;

        public Parser()
        {
            ParseAll("scenario.fbs");
        }

        public bool ParseAll(string fileName)
        {
            var sr = new StreamReader($"./media/scripts/{fileName}");
            var scenario = sr.ReadToEnd();
            _script = ParseScript.ParseProgram.Parse(scenario);
            return _script != null;
        }
    }
}