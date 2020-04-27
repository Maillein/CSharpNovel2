using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CSharpNovel2.GameSystem;
using CSharpNovel2.System;

namespace CSharpNovel2
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Console.WriteLine("Initialize");
            if (SystemMain.Initialize())
            {
                // Console.WriteLine("Initialized");
                SystemMain.MainLoop();
            }

            SystemMain.FinalizeGame();
        }
    }
}