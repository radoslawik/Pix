using Pix.Core.Models;

namespace Pix.UI.Helpers
{
    public static class ColorConversion
    {
        public static System.Drawing.Color ToColor(this ColorModel model)
        {
            return System.Drawing.Color.FromArgb(model.R, model.G, model.B);
        }

        public static Avalonia.Media.Color ToAvaloniaColor(this ColorModel model)
        {
            return new Avalonia.Media.Color(255, (byte)model.R, (byte)model.G, (byte)model.B);
        }
    }
}
