using Svg.Skia;
using SkiaSharp;

using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace TextureJinn.Rendering.Rasterisation.SVG
{
    public class InstallTypefaces
    {
        /// <summary>
        /// A queue containing all of the typefaces to add. This queue can also contain directories
        /// </summary>
        /// <typeparam name="string"></typeparam>
        public static Queue<string> s_TypefaceQue = new Queue<string>();
        public static string[] supportedFiletypes = new string[] { ".ttf" };

        /// <summary>
        /// Installs the font files listed in Typefaces list
        /// </summary>
        // /// <param name="reloadInstalled">Remove and re-install fonts already installed</param>
        public static void Install()
        {
            while (s_TypefaceQue.Count != 0)
            {
                string next = s_TypefaceQue.Dequeue();

                if (Path.GetExtension(next) == "")
                {
                    string[] dir = Directory.GetFiles(next);

                    for (int i = 0; i < dir.Length; i++)
                    {
                        sm_AddFont(dir[i]);
                    }
                }
                else
                {
                    sm_AddFont(next);
                }
            }
        }

        protected static void sm_AddFont(string path)
        {
            if (supportedFiletypes.Contains(Path.GetExtension(path)))
            {
                if (File.Exists(path))
                {
                    SKSvgSettings.s_typefaceProviders.Add(new CustomTypefaceProvider(path));
                }
            }
        }
    }
}