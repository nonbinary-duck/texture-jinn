namespace TextureJinn.Config
{
    public struct VersionInfo
    {
        public string VerNum { get; set; }
        public string GitTag { get => VerNum; }
        public string GitBranch { get; set; }
    }
}