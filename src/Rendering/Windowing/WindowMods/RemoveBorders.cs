using Avalonia;
using Avalonia.Controls;


namespace TextureJinn.Rendering.Windowing.WindowMods
{
    public class RemoveBorders : IWindowMod
    {
        public int Priority { get; protected set; }

        public RemoveBorders() {
            Priority = 0;
        }

        public void ApplyToWindow(ref Window window)
        {
            window.BorderThickness = new Thickness(0);

            // window.ScreenChanged += ScreenChanged;
        }
    }
}