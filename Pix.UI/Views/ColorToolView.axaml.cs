using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Pix.UI.Views
{
    public partial class ColorToolView : UserControl
    {
        public ColorToolView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
