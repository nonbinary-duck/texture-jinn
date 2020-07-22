using Svg.Skia;
using SkiaSharp;

using System.IO;

using TextureJinn.Extentions.StreamHacks;


namespace TextureJinn.Rendering.Rasterisation.SVG
{
    public class SvgRenderer
    {
        protected SKPicture m_SvgPicture;

        /// <summary>
        /// Initializes the class with correct data
        /// </summary>
        /// <param name="data">Either the path to the svg data or the actual svg data (see isPath)</param>
        /// <param name="isPath">Defines if "data" is a path or raw svg data</param>
        public SvgRenderer(string data, bool isPath = true)
        {
            if (isPath)
            {
                m_Init(File.ReadAllText(data));
            }
            else
            {
                m_Init(data);
            }
        }

        /// <summary>
        /// Creates the SKPicture (drawing data) for the Render routine to use
        /// </summary>
        /// <param name="svgData"></param>
        protected void m_Init(string svgData)
        {
            SKSvg svg = new SKSvg();
            svg.FromSvg(svgData);

            m_SvgPicture = svg.Picture;

        }

        /// <summary>
        /// Renders the svg to an SKBitmap
        /// </summary>
        /// <param name="size">The size of the bitmap. Accepts -1 for aspect-ratio preserving values</param>
        /// <param name="clearColour">The colour to clear the canvas with. Has default value</param>
        /// <param name="colourType">The colour type of the bitmap. Has default value of no colour</param>
        /// <returns>A bitmap of the given size representing the svg</returns>
        public SKBitmap Render(Vector2Di size, SKColor clearColour = default(SKColor), SKColorType colourType = SKColorType.Rgba8888)
        {
            Vector2D refSize = new Vector2D(m_SvgPicture.CullRect.Size.Width, m_SvgPicture.CullRect.Size.Height);
            sm_CalculateSize(ref size, refSize);

            SKBitmap bitmap = new SKBitmap(size.X, size.Y, colourType, SKAlphaType.Premul);

            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                if (!clearColour.Equals(SKColor.Empty))
                {
                    canvas.Clear(clearColour);
                }

                SKMatrix neo = SKMatrix.CreateScale(size.X / refSize.X, size.Y / refSize.Y);
                canvas.DrawPicture(m_SvgPicture, ref neo);
                canvas.Flush();

                return bitmap;
            }
        }

        /// <summary>
        /// Rasterizes an svg image to a bitmap of the given format stored in a FakeStream
        /// </summary>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="format">The format to rasterize the image in</param>
        /// <returns>A stream of data containing the formatted bitmap</returns>
        public FakeStream Render(Vector2Di size, SKEncodedImageFormat format)
        {
            return EncodeBitmap(Render(size), format);
        }

        /// <summary>
        /// Stores a bitmap into a FakeStream
        /// </summary>
        /// <param name="format">The format to rasterize the image in</param>
        /// <param name="bitmap">The bitmap</param>
        /// <returns>A stream of data containing the formatted bitmap. Default is png</returns>
        public FakeStream EncodeBitmap(SKBitmap bitmap, SKEncodedImageFormat format = SKEncodedImageFormat.Png)
        {
            FakeStream output = new FakeStream();
            SKImage.FromBitmap(bitmap).Encode(format, 100).SaveTo(output);

            output.Position = 0;
            return output;
        }

        /// <summary>
        /// Renders then saves an svg into a file
        /// </summary>
        /// <param name="size">The size of the bitmap</param>
        /// <param name="path">The path to save the image to</param>
        /// <param name="format">The format to rasterize the image in. Default is png</param>
        public void Render(Vector2Di size, string path, SKEncodedImageFormat format = SKEncodedImageFormat.Png)
        {
            SaveStream(Render(size, format), path);
        }

        /// <summary>
        /// Saves a stream to a file
        /// </summary>
        /// <param name="output">The stream from which to take data</param>
        /// <param name="path">The path to save the image to</param>
        public void SaveStream(Stream output, string path)
        {
            if (File.Exists(path)) File.Delete(path);

            using (var file = File.OpenWrite(path))
            {
                byte[] out_data = new byte[output.Length];
                output.Read(out_data);
                file.Write(out_data);
            }
        }

        /// <summary>
        /// Encode then save a bitmap to a file
        /// </summary>
        /// <param name="bitmap">The bitmap in question</param>
        /// <param name="path">The path to save the bitmap to</param>
        /// <param name="format">The format to encode the bitmap in. Default is png</param>
        public void SaveBitmap(SKBitmap bitmap, string path, SKEncodedImageFormat format = SKEncodedImageFormat.Png)
        {
            SaveStream(EncodeBitmap(bitmap, format), path);
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