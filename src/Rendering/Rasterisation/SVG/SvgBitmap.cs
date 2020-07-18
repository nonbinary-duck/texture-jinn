using Svg.Skia;
using SkiaSharp;

using System.IO;

using TextureJinn.Extentions.StreamHacks;


namespace TextureJinn.Rendering.Rasterisation.SVG
{
    public class RenderSvg
    {
        protected string m_SvgData;

        /// <summary>
        /// Generates the IBitmap class
        /// </summary>
        /// <param name="path">The path to the svg image</param>
        public RenderSvg(string path)
        {
            m_SvgData = File.ReadAllText(path);
        }

        public void Render(Vector2Di size)
        {
            Render(m_SvgData, size);
        }

        /// <summary>
        /// Renders the svg
        /// </summary>
        /// <param name="data">The svg data to render</param>
        public void Render(string data, Vector2Di size)
        {
            FakeUTF8Stream stream = new FakeUTF8Stream(data);

            using (var svg = new Svg.Skia.SKSvg())
            {
                svg.Load(stream);

                using (var output = new FakeStream())
                {
                    // svg.Picture.ToImage(output, SKColors.Empty, SKEncodedImageFormat.Png, 100, 1f, 1f, SKColorType.Argb4444, SKAlphaType.Opaque);
                    // svg.Save(output, SKColor.Empty, SKEncodedImageFormat.Dng, 100, 1f, 1f);
                    SKCanvas canvas = new SKCanvas(new SKBitmap(size.X, size.Y));
                    canvas.DrawPicture(svg.Picture);
                    canvas.

                    using (var file = File.OpenWrite("test.png"))
                    {
                        int count = output.iLength;
                        byte[] png = new byte[count];
                        output.Position = 0;
                        output.Read(png);
                        file.Write(png);
                    }
                }
            }

        }
    }
}