using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Globalization;
using System.IO;

namespace Pix.UI.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is string path)
            {
                if (!File.Exists(path))
                {
                    return null;
                }
                using var imageStream = File.OpenRead(path);
                return Bitmap.DecodeToWidth(imageStream, 200);
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
