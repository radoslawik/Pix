using ReactiveUI;
using ReactiveUI.Fody.Helpers;
namespace Pix.Core.Models
{
    public class ColorModel : ReactiveObject
    {
        public ColorModel(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public ColorModel(System.Drawing.Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }

        [Reactive]
        public int R { get; set; }
        [Reactive]
        public int G { get; set; }
        [Reactive]
        public int B { get; set; }
    }
}
