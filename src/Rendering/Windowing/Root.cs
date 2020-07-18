using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TextureJinn.Rendering.Windowing.xaml;
using TextureJinn.Rendering.Windowing.xaml.pages;
using TextureJinn.Rendering.Windowing.WindowMods;


namespace TextureJinn.Rendering.Windowing
{
    class Root
    {
        public Root()
        {
            BuildApp().StartWithClassicDesktopLifetime(new string[0]);
        }

        protected AppBuilder BuildApp() => AppBuilder.Configure<App>().UseSkia().UsePlatformDetect();
    }
}