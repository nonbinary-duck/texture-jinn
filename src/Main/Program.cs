using System;
using System.IO;

using SkiaSharp;

using TextureJinn.Rendering.Windowing;
using TextureJinn.Rendering.Rasterisation.SVG;

using TextureJinn.Extentions.StringHacks;
using TextureJinn.Extentions.StreamHacks;


namespace TextureJinn
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!\n".AppendPst2PadSep() + "Loading window...");

            // Root root = new Root();

            // Console.WriteLine("Window closed".Append2PadSep());

            RenderSvg svg = new RenderSvg("Assets/Images/Vector Images/SplashBase.svg");

            DateTime start = DateTime.Now;

            for (int i = 0; i < 1; i++)
            {
                svg.Render(new Rendering.Rasterisation.Vector2Di(1000, 1000), "test.png", SKEncodedImageFormat.Png);
            }

            DateTime end = DateTime.Now;

            Console.WriteLine((end - start).TotalSeconds);
        }
    }
}
