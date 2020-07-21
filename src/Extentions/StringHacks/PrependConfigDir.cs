using System;
using System.IO;


namespace TextureJinn.Extentions.StringHacks
{
    public static class PrependConfigDir
    {
        private static string m_Local { get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TextureJinn/"); }

        public static string PrependCfgDir(this string str)
        {
            return Path.Combine(m_Local, str);
        }
    }
}