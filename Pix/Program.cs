using System;
using System.Drawing;
using System.Linq;

namespace Pix
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine($"Usage:" +
                    $"{Environment.NewLine}  arg1 - file name (with extension)" +
                    $"{Environment.NewLine}  arg2 - palette number (0-2)" +
                    $"{Environment.NewLine}  arg3 - pixel block size (1-..)");
                return;
            }

            var palettes = new Palettes().GetPalettes();
            var paletteIndex = Math.Max(Math.Min(int.Parse(args[1]), palettes.Length - 1), 0);
            var selectedPalette = palettes[paletteIndex];
            selectedPalette = selectedPalette.OrderBy(x => CalculateLuminance(x)).ToArray();
            var divisor = 256 / selectedPalette.Length;
            var bitmap = new Bitmap(Image.FromFile(args[0]));
            var limit = Math.Min(bitmap.Width, bitmap.Height);
            var pixelSize = int.Parse(args[2]);
            for (var i = 0; i < limit; i += pixelSize)
            {
                for (var j = 0; j < limit; j += pixelSize)
                {
                    var acc = 0d;
                    for (var k = 0; k < pixelSize; k++)
                    {
                        for (var l = 0; l < pixelSize; l++)
                        {
                            var color = bitmap.GetPixel(i + k, j + l);
                            acc += CalculateLuminance(color);
                        }
                    }

                    var meanLuminance = acc / (pixelSize * pixelSize);
                    var newColor = selectedPalette[(int)(meanLuminance / divisor)];

                    for (var k = 0; k < pixelSize; k++)
                    {
                        for (var l = 0; l < pixelSize; l++)
                        {
                            bitmap.SetPixel(i + k, j + l, newColor);
                        }
                    }
                }
            }
            bitmap.Save($"out_{args[0]}");
            Console.WriteLine($"Done! Saved as out_{args[0]}");
        }
        private static double CalculateLuminance(Color c)
        {
            return 0.2126 * c.R + 0.7152 * c.G + 0.0722 * c.B;
        }

    }
}