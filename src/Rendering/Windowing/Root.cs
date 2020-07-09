using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TextureJinn.Rendering.Windowing.WindowMods;


namespace TextureJinn.Rendering.Windowing
{
    class Root : Application
    {
        protected Window m_window;

        public Root()
        {
            m_window = new Window();
        }

        public void Start()
        {
            // WindowCompiler mods = new WindowCompiler();
            // mods.Add(new RemoveBorders());

            // mods.ModifyWindow(ref m_window);
        }
    }
}