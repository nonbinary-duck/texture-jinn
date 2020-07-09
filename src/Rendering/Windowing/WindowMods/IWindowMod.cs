using Avalonia.Controls;


namespace TextureJinn.Rendering.Windowing.WindowMods
{
    public interface IWindowMod
    {
        /// <summary> Where in the order should this mod be executed (higher runs first) </summary>
        public int Priority { get; }

        public void ApplyToWindow(ref Window window);
    }
}