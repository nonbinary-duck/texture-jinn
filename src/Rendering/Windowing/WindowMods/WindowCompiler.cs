using Avalonia.Controls;

using System.Collections.Generic;


namespace TextureJinn.Rendering.Windowing.WindowMods
{
    public class WindowCompiler : List<IWindowMod>
    {
        public void ModifyWindow(ref Window window)
        {
            SortByPriority();

            for (int i = 0; i < Count; i++)
            {
                System.Console.WriteLine(this[i].Priority);
                this[i].ApplyToWindow(ref window);
            }
        }

        protected void SortByPriority() {
            this.Sort((x, y) => { return x.Priority.CompareTo(y.Priority); });
        }
    }
}