using System;
using System.IO;

using SkiaSharp;

using TextureJinn.Config;

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
            // Console.WriteLine("Hello World!\n".AppendPst2PadSep() + "Loading window...");

            // Root root = new Root();

            // Console.WriteLine("Window closed".Append2PadSep());

            TypefaceManager.s_TypefaceQue.Enqueue("Quantico");
            TypefaceManager.s_Install();

            SvgRenderer svg = TJCfg.Svgs.GetSvg("SplashBase.svg");

            svg.SaveBitmap(svg.Render(new Vector2Di(-1, 2048)), "test.png");

            string data = File.ReadAllText("SplashBase.svg".PrependVectorDir());
            
            data = data.Replace("tj_text_s:2", TJCfg.TJVersion.VerNum);
            data = data.Replace("tj_text_s:1", TJCfg.FunFact);

            for (int i = 0; i < 850; i++)
            {
                Console.WriteLine(TJCfg.FunFact);
            }

            svg = new SvgRenderer(data, false);
            svg.Render(new Vector2Di(-1, 2048), "test2.png");
        }
    }
}
