using SkiaSharp;

using System.IO;

using TextureJinn.Extentions.StreamHacks;


namespace TextureJinn.Rendering.Rasterisation.SVG
{
    public class SvgRenderer
    {
        protected string m_SvgData;

        /// <summary>
        /// Initializes the class with correct data
        /// </summary>
        /// <param name="path">The path to the svg data</param>
        public SvgRenderer(string path)
        {
            m_SvgData = File.ReadAllText(path);
        }

        /// <summary>
        /// Renders the svg to an SKBitmap
        /// </summary>
        /// <param name="size">The size of the bitmap. Accepts -1 for aspect-ratio preserving values</param>
        /// <returns></returns>
        public SKBitmap Render(Vector2Di size) {
            return Render(m_SvgData, size);
        }

        public static SKBitmap Render(string data, Vector2Di size)
        {
            using (var svg = new Svg.Skia.SKSvg())
            {
                svg.FromSvg(data);
                
                Vector2D refSize = new Vector2D(svg.Picture.CullRect.Size.Width, svg.Picture.CullRect.Size.Height);
                sm_CalculateSize(ref size, refSize);
                
                SKBitmap bitmap = new SKBitmap(size.X, size.Y, SKColorType.Rgba8888, SKAlphaType.Premul);
                
                using (SKCanvas canvas = new SKCanvas(bitmap))
                {
                    SKMatrix neo = SKMatrix.CreateScale(size.X / refSize.X, size.Y / refSize.Y);
                    canvas.DrawPicture(svg.Picture, ref neo);
                    canvas.Flush();

                    return bitmap;
                }
            }
        }

        /// <summary>
        /// Rasterizes an svg image to a bitmap of the given format
        /// </summary>
        /// <param name="data">The svg as text</param>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="format">The format to rasterize the image in</param>
        /// <returns>A stream of data containing the formatted bitmap</returns>
        public static FakeStream Render(string data, Vector2Di size, SKEncodedImageFormat format)
        {
            FakeStream output = new FakeStream();

            SKBitmap bitmap = Render(data, size);

            SKImage.FromBitmap(bitmap).Encode(format, 100).SaveTo(output);

            output.Position = 0;
            return output;
        }

        /// <summary>
        /// Rasterizes an svg image to a bitmap of the given format
        /// </summary>
        /// <param name="path">The path to save the image to</param>
        /// <param name="data">The svg as text</param>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="format">The format to rasterize the image in</param>
        public static void Render(string data, Vector2Di size, string path, SKEncodedImageFormat format = SKEncodedImageFormat.Png)
        {
            FakeStream output = Render(data, size, format);

            if (File.Exists(path)) File.Delete(path);

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
        public FakeStream Render(Vector2Di size, SKEncodedImageFormat format)
        {
            return Render(m_SvgData, size, format);
        }

        /// <summary>
        /// Rasterizes an svg using the already stored svg data to a path with the given format
        /// </summary>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="path">The path to render the svg to</param>
        /// <param name="format">The format in which to save the the image</param>
        public void Render(Vector2Di size, string path, SKEncodedImageFormat format = SKEncodedImageFormat.Png)
        {
            Render(m_SvgData, size, path, format);
        }

        protected static void sm_CalculateSize(ref Vector2Di size, Vector2D origin)
        {
            // Allow for aspect-ratio perserving sizes
            if (size.X == -1 && size.Y != -1)
            {
                float sFac = size.Y / origin.Y;
                size.X = (int)origin.X;
                size.Mul(new Vector2D(sFac, 1f));
            }
            else if (size.Y == -1 && size.X != -1)
            {
                float sFac = size.X / origin.X;
                size.Y = (int)origin.Y;
                size.Mul(new Vector2D(1f, sFac));
            }
            else if (size.X != -1 && size.Y != -1)
            {
                size.Mul(new Vector2D(size.X / origin.X, size.Y / origin.Y));
            }
        }
    }
}