using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using System.Runtime.InteropServices;

namespace Pix.UI.Controls
{
    public partial class LiveImage : UserControl
    {
        private WriteableBitmap? _writeable;
        private int? _destHeight;
        private int? _destWidth;

        public int[]? Data
        {
            get => (int[]?)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static readonly AvaloniaProperty DataProperty =
            AvaloniaProperty.Register<LiveImage, int[]?>(nameof(Data), null, notifying: BitmapChanged);

        public int? OriginalWidth
        {
            get => (int?)GetValue(OriginalWidthProperty);
            set => SetValue(OriginalWidthProperty, value);
        }

        public static readonly AvaloniaProperty OriginalWidthProperty =
            AvaloniaProperty.Register<LiveImage, int?>(nameof(OriginalWidth), 200, notifying: BitmapSizeChanged);

        public int? OriginalHeight
        {
            get => (int?)GetValue(OriginalHeightProperty);
            set => SetValue(OriginalHeightProperty, value);
        }

        public static readonly AvaloniaProperty OriginalHeightProperty =
            AvaloniaProperty.Register<LiveImage, int?>(nameof(OriginalHeight), 200, notifying: BitmapSizeChanged);

        private static void BitmapChanged(IAvaloniaObject arg, bool updated)
        {
            if (updated)
            {
                Dispatcher.UIThread.InvokeAsync((arg as LiveImage)!.UpdateBitmap);
            }
        }

        private static void BitmapSizeChanged(IAvaloniaObject arg, bool updated)
        {
            if (updated)
            {
                Dispatcher.UIThread.InvokeAsync((arg as LiveImage)!.ReinitializeBitmap);
            }
        }

        private void ReinitializeBitmap()
        {
            if (OriginalHeight is int height && OriginalWidth is int width)
            {
                _writeable = new WriteableBitmap(new PixelSize(width, height), new Vector(width, height), PixelFormat.Bgra8888, AlphaFormat.Unpremul);
                (_destWidth, _destHeight) = CalculateSize(width, height);
                Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
            }
        }

        private void UpdateBitmap()
        {
            if (Data is not null && OriginalHeight is not null && OriginalWidth is not null)
            {
                Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
            }
        }

        public LiveImage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (_writeable is not null && Data is not null)
            {
                using var buffer = _writeable.Lock();
                Marshal.Copy(Data, 0, buffer.Address, Data.Length);
                context.DrawImage(_writeable,
                    new Rect(0, 0, OriginalWidth ?? Bounds.Width, OriginalHeight ?? Bounds.Height),
                    Bounds.CenterRect(new Rect(0, 0, _destWidth ?? Bounds.Width, _destHeight ?? Bounds.Height)));

            }
        }

        private (int, int) CalculateSize(int x, int y)
        {
            var imageRatio = 1.0 * x / y;
            var gridRatio = 1.0 * Bounds.Width / Bounds.Height;
            if (imageRatio > gridRatio)
            {
                if (x > Bounds.Width)
                {
                    return ((int)Bounds.Width, (int)(Bounds.Width / imageRatio));
                }
                else
                {
                    return (x, y);
                }
            }
            else
            {
                return (x, (int)(x / imageRatio));
            }
        }
    }
}
