using Gtk;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TextureJinn.Rendering.Windowing.WindowMods;


namespace TextureJinn.Rendering.Windowing
{
    class Root
    {
        protected Window m_root;

        public Root()
        {
            Application.Init();

            m_root = new Window("Texture Jinn");

            m_root.DeleteEvent += DeleteEvent;
        }

        public void Start()
        {
            WindowCompiler mods = new WindowCompiler();
            mods.Add(new RemoveBorders());

            mods.ModifyWindow(ref m_root);

            m_root.ShowAll();

            Application.Run();
        }

        private void DeleteEvent(object obj, DeleteEventArgs args)
        {
            Application.Quit();
        }

        private void Hello(object obj, EventArgs args)
        {
            Console.WriteLine("Hello World");
        }
    }
}