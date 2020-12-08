using System;
using System.Collections.Generic;
using System.IO;
using FlashScript;
using Sprache;

namespace CSharpNovel2.Game
{
    public class Parser
    {
        private Parser<Expr> _parser;
        private Script _script;
        private StreamReader _streamReader;

        public Parser(string fileName = "scenario.fbs")
        {
            _streamReader = new StreamReader($"./media/scripts/{fileName}");
        }

        public Script ParseAll(string fileName)
        {
            var sr = new StreamReader($"./media/scripts/{fileName}");
            var scenario = sr.ReadToEnd();
            return ParseScript.ParseProgram.Parse(scenario);
        }

        public IEnumerable<Expr> Next()
        {
            var line = _streamReader.ReadLine();
            if (line == null) yield break;
            line = line.Replace("\\v", "\v");
            line = line.Replace("\\n", "\n");
            var script = ParseScript.ParseProgram.Parse(line);
            foreach (var expr in script.exprs)
            {
                yield return expr;
            }
        }
    }
}