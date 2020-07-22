using System;
using System.IO;

using TextureJinn.Extentions.FunFacts;
using TextureJinn.Rendering.Rasterisation.SVG;


namespace TextureJinn.Config
{
    public static class TJCfg
    {
        public static string AppName { get => "TextureJinn"; }
        public static string LocalDir { get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); }
        public static string ConfigPath { get => Path.Combine(LocalDir, AppName + "/"); }
        public static string AssetPath { get => Path.Combine(ConfigPath, "Assets/"); }
        public static string FontPath { get => Path.Combine(AssetPath, "Fonts/"); }
        public static string ImagePath { get => Path.Combine(AssetPath, "Images/"); }
        public static string VectorPath { get => Path.Combine(ImagePath, "Vector Images/"); }

        public static SvgManager Svgs = new SvgManager();
        public static FunFacts FunFacts = new FunFacts();
        public static string FunFact { get => FunFacts.FunFact; }

        public static readonly VersionInfo TJVersion = new VersionInfo()
        {
            VerNum = "vN/A",
            GitBranch = "master"
        };
    }
}