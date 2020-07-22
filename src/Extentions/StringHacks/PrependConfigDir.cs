using System;
using System.IO;

using TextureJinn.Config;


namespace TextureJinn.Extentions.StringHacks
{
    public static class PrependConfigDir
    {
        public static string PrependCfgDir(this string str)
        {
            return Path.Combine(TJCfg.ConfigPath, str);
        }

        public static string PrependAssetDir(this string str)
        {
            return Path.Combine(TJCfg.AssetPath, str);
        }

        public static string PrependVectorDir(this string str)
        {
            return Path.Combine(TJCfg.VectorPath, str);
        }
    }
}