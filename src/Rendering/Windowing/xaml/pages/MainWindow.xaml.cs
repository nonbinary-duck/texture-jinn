using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;


namespace TextureJinn.Rendering.Windowing.xaml.pages
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            // Icon = new WindowIcon();
        }
    }
}
