using Avalonia.Data.Converters;
using Pix.Core.Models;
using Pix.UI.Helpers;
using System;
using System.Globalization;

namespace Pix.UI.Converters
{
    public class BrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color sysColor)
            {
                return new Avalonia.Media.SolidColorBrush(new Avalonia.Media.Color(sysColor.A, sysColor.R, sysColor.G, sysColor.B));
            }
            else if (value is Avalonia.Media.Color color)
            {
                return new Avalonia.Media.SolidColorBrush(color);
            }
            else if (value is ColorModel model)
            {
                return new Avalonia.Media.SolidColorBrush(model.ToAvaloniaColor());
            }

            return Avalonia.Media.Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
