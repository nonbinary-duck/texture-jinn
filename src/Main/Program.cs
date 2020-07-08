using System;

using TextureJinn.Rendering.Windowing;
using TextureJinn.Extentions.StringHacks;


namespace TextureJinn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n".AppendPst2PadSep() + "Loading window...");
            
            Root rootRenderer = new Root();

            rootRenderer.Start();

            Console.WriteLine("Window closed".Append2PadSep());
        }
    }
}
