using System.IO;
using System.Collections.Generic;

using TextureJinn.Config;


namespace TextureJinn.Rendering.Rasterisation.SVG
{
    public class SvgManager
    {
        public Dictionary<string, SvgRenderer> Renderers;
        public bool PrependVectorPath;

        /// <summary>
        /// Initializes the SvgManager
        /// </summary>
        /// <param name="prependVectorPath">Should relative paths have the vector asset path prepended to them</param>
        public SvgManager(bool prependVectorPath = true)
        {
            Renderers = new Dictionary<string, SvgRenderer>();
            PrependVectorPath = prependVectorPath;
        }

        protected void m_ProcessPath(ref string path)
        {
            // Having two individual if statements means the app doesn't run Path.IsPa... every time
            if (PrependVectorPath)
            {
                if (!Path.IsPathRooted(path))
                {
                    path = Path.Combine(TJCfg.VectorPath, path);
                }
            }
        }

        public SvgRenderer GetSvg(string path)
        {
            m_ProcessPath(ref path);

            if (Renderers.ContainsKey(path))
            {
                return Renderers.GetValueOrDefault(path);
            }
            
            SvgRenderer newRenderer = new SvgRenderer(path);
            Renderers.Add(path, newRenderer);
            
            return newRenderer;
        }
    }
}