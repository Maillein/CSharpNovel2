using System;
using System.Collections.Generic;
using System.IO;
using FlashScript;
using Sprache;

namespace CSharpNovel2.Game
{
    public class Parser
    {
        private readonly FileStream _fileStream;
        private readonly StreamReader _streamReader;
        private readonly Dictionary<string, long> _positions;

        public Parser(string fileName = "scenario.fbs")
        {
            _fileStream = File.Open($"./media/scripts/{fileName}", FileMode.Open);
            _streamReader = new StreamReader(_fileStream);
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
            while (line == "") line = _streamReader.ReadLine();
            line = line?.Replace("\\v", "\v");
            line = line?.Replace("\\n", "\n");
            var script = ParseScript.ParseProgram.Parse(line);
            foreach (var expr in script.exprs)
            {
                yield return expr;
            }
        }
    }
}