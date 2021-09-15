using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Pix
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            var src = Path.Combine(path, "original");
            var files = Directory.GetFiles(src, "*.png");

            foreach(var file in files)
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                var outputDir = Path.Combine(path, "out", filename);
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
                for(int p=0;p<7;p++)
                {
                    for(int s=1;s<5;s++)
                    {
                        var palettes = new Palettes().GetPalettes();
                        var paletteIndex = Math.Max(Math.Min(p, palettes.Length - 1), 0);
                        var selectedPalette = palettes[paletteIndex];
                        selectedPalette = selectedPalette.OrderBy(x => CalculateLuminance(x)).ToArray();
                        var divisor = 256 / selectedPalette.Length;
                        using var bitmap = new Bitmap(Image.FromFile(file));
                        var limit = Math.Min(bitmap.Width, bitmap.Height);
                        var pixelSize = s;
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
                                var newColor = selectedPalette[Math.Min((int)(meanLuminance / divisor), selectedPalette.Length - 1)];

                                for (var k = 0; k < pixelSize; k++)
                                {
                                    for (var l = 0; l < pixelSize; l++)
                                    {
                                        bitmap.SetPixel(i + k, j + l, newColor);
                                    }
                                }
                            }
                        }
                        bitmap.Save(Path.Combine(outputDir, $"out_{p}_{s}_{filename}.png"));
                        Console.WriteLine($"Done! Saved as out_{p}_{s}_{filename}.png");
                    }
                }
            }

            
        }
        private static double CalculateLuminance(Color c)
        {
            return 0.2126 * c.R + 0.7152 * c.G + 0.0722 * c.B;
        }

    }
}