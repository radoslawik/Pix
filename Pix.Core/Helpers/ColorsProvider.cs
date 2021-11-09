using Pix.Core.Enums;
using Pix.Core.Models;
using System;

namespace Pix.Core.Helpers
{
    public static class ColorsProvider
    {
        public static ColorModel[] GetColors(object obj)
        {
            return obj switch
            {
                Palette palette => GetColors(palette),
                _ => Array.Empty<ColorModel>(),
            };
        }

        private static ColorModel[] GetColors(Palette palette)
        {
            return palette switch
            {
                Palette.Ice => new[] {
                    new ColorModel(128, 128, 255),
                    new ColorModel(64, 64, 64),
                    new ColorModel(164, 164, 164),
                    new ColorModel(180, 50, 80),
                },
                Palette.Desert => new[] {
                    new ColorModel(124, 63, 88),
                    new ColorModel(235, 107, 111),
                    new ColorModel(249, 168, 117),
                    new ColorModel(255, 246, 211),
                },
                Palette.Soft => new[] {
                    new ColorModel(44, 33, 55),
                    new ColorModel(118, 68, 98),
                    new ColorModel(237, 180, 161),
                    new ColorModel(169, 104, 104),
                },
                Palette.Sharp => new[] {
                    new ColorModel(48, 0, 48),
                    new ColorModel(96, 40, 120),
                    new ColorModel(248, 144, 32),
                    new ColorModel(248, 240, 136),
                },
                Palette.Forest => new[] {
                    new ColorModel(51, 44, 80),
                    new ColorModel(70, 135, 143),
                    new ColorModel(148, 227, 68),
                    new ColorModel(226, 243, 228),
                },
                Palette.Pastel => new[] {
                    new ColorModel(116, 86, 155),
                    new ColorModel(150, 251, 199),
                    new ColorModel(247, 255, 174),
                    new ColorModel(255, 179, 203),
                    new ColorModel(216, 191, 216),
                },
                Palette.Bronze => new[] {
                    new ColorModel(10, 10, 10),
                    new ColorModel(34, 39, 40),
                    new ColorModel(63, 85, 70),
                    new ColorModel(126, 90, 100),
                    new ColorModel(190, 149, 133),
                    new ColorModel(218, 211, 190),
                },
                _ => Array.Empty<ColorModel>(),
            };
        }
    }
}
