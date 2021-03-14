using System.Collections.Generic;

namespace CSharpNovel2.Scene
{
    public class Parameter
    {
        public const int Error = -1;
        private readonly Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        public void Set(string key, int val) { _dictionary[key] = val; }

        public int Get(string key) { return _dictionary.ContainsKey(key) ? _dictionary[key] : Error; }
    }
}