using Svg.Skia;

using System.IO;
using System.Linq;
using System.Collections.Generic;

using TextureJinn.Config;


namespace TextureJinn.Rendering.Rasterisation.SVG
{
    public class TypefaceManager
    {
        /// <summary>
        /// A queue containing all of the typefaces to add. This queue can also contain directories
        /// </summary>
        public static Queue<string> s_TypefaceQue = new Queue<string>();
        /// <summary>
        /// Should the font asset path be prepended to all relative paths
        /// </summary>
        public static bool s_prependFontPath = true;
        public static string[] s_supportedFiletypes = new string[] { ".ttf" };

        /// <summary>
        /// Installs the font files listed in Typefaces list
        /// </summary>
        // /// <param name="reloadInstalled">Remove and re-install fonts already installed</param>
        public static void s_Install()
        {
            while (s_TypefaceQue.Count != 0)
            {
                string next = s_TypefaceQue.Dequeue();

                sm_ProcessPath(ref next);

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
            if (s_supportedFiletypes.Contains(Path.GetExtension(path)))
            {
                if (File.Exists(path))
                {
                    SKSvgSettings.s_typefaceProviders.Add(new CustomTypefaceProvider(path));
                }
            }
        }

        protected static void sm_ProcessPath(ref string path)
        {
            if (s_prependFontPath)
            {
                if (!Path.IsPathRooted(path))
                {
                    path = Path.Combine(TJCfg.FontPath, path);
                }
            }
        }
    }
}