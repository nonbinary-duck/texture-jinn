using System;
using System.IO;

using SkiaSharp;

using TextureJinn.Config;

using TextureJinn.Rendering.Windowing;
using TextureJinn.Rendering.Rasterisation;
using TextureJinn.Rendering.Rasterisation.SVG;

using TextureJinn.Extentions.StringHacks;
using TextureJinn.Extentions.StreamHacks;


namespace TextureJinn
{
    class Program
    {
        static void Main(string[] args)
        {
            TypefaceManager.s_TypefaceQue.Enqueue("Quantico");
            TypefaceManager.s_Install();
            
            Root root = new Root();
        }
    }
}
