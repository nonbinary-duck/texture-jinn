using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;

using TextureJinn.Rendering.Windowing.xaml.pages; 


namespace TextureJinn.Rendering.Windowing.xaml
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }
        }
    }
}