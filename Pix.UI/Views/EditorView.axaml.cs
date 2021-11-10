using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Pix.UI.Views
{
    public partial class EditorView : UserControl
    {
        public EditorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
