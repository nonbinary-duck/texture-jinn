using Cairo;
using Gtk;


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
            window.Resizable = true;
            window.AppPaintable = true;

            // window.ScreenChanged += ScreenChanged;
        }

        protected void ScreenChanged(object o, ScreenChangedArgs args) {
            
        }
    }
}