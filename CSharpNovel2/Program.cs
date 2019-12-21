using CSharpNovel2.System;

namespace CSharpNovel2
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (SystemMain.Initialize())
            {
                SystemMain.MainLoop();
            }

            SystemMain.FinalizeGame();
        }
    }
}