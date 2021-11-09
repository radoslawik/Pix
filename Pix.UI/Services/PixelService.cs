using Pix.Core.Models;
using Pix.Core.Services.Interfaces;
using Pix.UI.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Pix.UI.Services
{
    public class PixelService : IPixelService
    {
        private Bitmap? _bitmap;
        private int[]? _currentData;
        private bool _sync;

        private static double CalculateLuminance(ColorModel c)
        {
            return 0.2126 * c.R + 0.7152 * c.G + 0.0722 * c.B;
        }

        public async Task<int[]> GetPixelizedStream(int blocksSize, bool forceAllColors, bool orderByLuminance, ColorModel[] palette)
        {
            if (_bitmap is null)
            {
                throw new Exception();
            }

            return await Task.Run(() =>
            {
                if (_sync && _currentData is not null)
                {
                    return _currentData;
                }

                _sync = true;
                var selectedPalette = orderByLuminance ? palette.OrderBy(x => CalculateLuminance(x)).ToArray() : palette;
                var data = new int[_bitmap.Width * _bitmap.Height];
                var limit = Math.Min(_bitmap.Width, _bitmap.Height);
                var minLum = 255.0;
                var maxLum = 0.0;

                if (forceAllColors)
                {
                    for (var i = 0; i < _bitmap.Width; i++)
                    {
                        for (var j = 0; j < _bitmap.Height; j++)
                        {
                            var lum = CalculateLuminance(new ColorModel(_bitmap.GetPixel(i, j)));
                            if (lum < minLum)
                            {
                                minLum = lum;
                            }
                            if (lum > maxLum)
                            {
                                maxLum = lum;
                            }
                        }
                    }
                }

                var divisor = forceAllColors ? (maxLum - minLum) / selectedPalette.Length : 256 / selectedPalette.Length;

                for (var i = 0; i < limit; i += blocksSize)
                {
                    for (var j = 0; j < limit; j += blocksSize)
                    {
                        var acc = 0d;
                        for (var k = 0; k < blocksSize; k++)
                        {
                            for (var l = 0; l < blocksSize; l++)
                            {
                                var color = _bitmap.GetPixel(Math.Min(_bitmap.Width - 1, i + k), Math.Min(_bitmap.Height - 1, j + l));
                                acc += CalculateLuminance(new ColorModel(color));
                            }
                        }

                        var meanLuminance = acc / (blocksSize * blocksSize);
                        var modifiedLum = forceAllColors ? (meanLuminance - minLum) : meanLuminance;
                        var newColor = selectedPalette[Math.Min((int)(modifiedLum / divisor), selectedPalette.Length - 1)];

                        for (var k = 0; k < blocksSize; k++)
                        {
                            for (var l = 0; l < blocksSize; l++)
                            {
                                var row = Math.Min(_bitmap.Width - 1, i + k);
                                var col = Math.Min(_bitmap.Height - 1, j + l);
                                data[col * limit + row] = (int)newColor.ToAvaloniaColor().ToUint32();
                            }
                        }
                    }
                }

                _currentData = data;
                _sync = false;
                return data;
            });

        }

        public (int, int) Initialize(string file)
        {
            _bitmap = new Bitmap(Image.FromFile(file));
            _currentData = Array.Empty<int>();
            return (_bitmap.Width, _bitmap.Height);
        }
    }
}
