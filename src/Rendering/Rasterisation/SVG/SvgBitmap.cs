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
        /// Initializes the class with correct data
        /// </summary>
        /// <param name="path">The path to the svg data</param>
        public RenderSvg(string path)
        {
            m_SvgData = File.ReadAllText(path);
        }

        /// <summary>
        /// Rasterizes an svg image to a bitmap of the given format
        /// </summary>
        /// <param name="data">The svg as text</param>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="format">The format to rasterize the image in</param>
        /// <returns>A stream of data containing the formatted bitmap</returns>
        public FakeStream Render(string data, Vector2Di size, SKEncodedImageFormat format = SKEncodedImageFormat.Bmp)
        {
            FakeUTF8Stream stream = new FakeUTF8Stream(data);

            using (var svg = new Svg.Skia.SKSvg())
            {
                svg.Load(stream);

                FakeStream output = new FakeStream();

                Vector2Di scale = new Vector2Di();
                Vector2D renderedSize = new Vector2Di(svg.Picture.CullRect.Size.Width, svg.Picture.CullRect.Size.Height);

                if (size.X == -1)
                {
                    float sFac = 
                }

                SKImage image = SKImage.FromBitmap(svg.Picture.ToBitmap(SKColor.Empty, 3f, 3f, SKColorType.Rgba8888, SKAlphaType.Premul));
                image.Encode(format, 100).SaveTo(output);
                System.Console.WriteLine(output.Length);

                // svg.Save(output, SKColor.Empty, format, 100, 1f, 1f);
                
                output.Position = 0;
                return output;
            }

        }

        /// <summary>
        /// Rasterizes an svg image to a bitmap of the given format
        /// </summary>
        /// <param name="path">The path to save the image to</param>
        /// <param name="data">The svg as text</param>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="format">The format to rasterize the image in</param>
        public void Render(string data, Vector2Di size, string path, SKEncodedImageFormat format = SKEncodedImageFormat.Bmp)
        {
            FakeStream output = Render(data, size, format);

            using (var file = File.OpenWrite(path))
            {
                byte[] out_data = new byte[output.iLength];
                output.Read(out_data);
                file.Write(out_data);
            }
        }

        /// <summary>
        /// Rasterizes an svg using the already stored svg data with the given format
        /// </summary>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="format">The format of the data</param>
        /// <returns>A stream containing the image</returns>
        public FakeStream Render(Vector2Di size, SKEncodedImageFormat format = SKEncodedImageFormat.Bmp)
        {
            return Render(m_SvgData, size, format);
        }

        /// <summary>
        /// Rasterizes an svg using the already stored svg data to a path with the given format
        /// </summary>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="path">The path to render the svg to</param>
        /// <param name="format">The format in which to save the the image</param>
        public void Render(Vector2Di size, string path, SKEncodedImageFormat format = SKEncodedImageFormat.Bmp)
        {
            Render(m_SvgData, size, path, format);
        }
    }
}