using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pix
{
    public class Palettes
    {
        public Color[] IcePalette = new[] {
                Color.FromArgb(128, 128, 255),
                Color.FromArgb(64, 64, 64),
                Color.FromArgb(164, 164, 164),
                Color.FromArgb(180, 50, 80),
            };

        public Color[] DesertPalette = new[] {
                Color.FromArgb(124, 63, 88),
                Color.FromArgb(235, 107, 111),
                Color.FromArgb(249, 168, 117),
                Color.FromArgb(255, 246, 211),
            };

        public Color[] SoftPalette = new[] {
                Color.FromArgb(44, 33, 55),
                Color.FromArgb(118, 68, 98),
                Color.FromArgb(237, 180, 161),
                Color.FromArgb(169, 104, 104),
            };

        public Color[] SharpPalette = new[] {
                Color.FromArgb(48, 0, 48),
                Color.FromArgb(96, 40, 120),
                Color.FromArgb(248, 144, 32),
                Color.FromArgb(248, 240, 136),
            };

        public Color[] GreenPalette = new[] {
                Color.FromArgb(51, 44, 80),
                Color.FromArgb(70, 135, 143),
                Color.FromArgb(148, 227, 68),
                Color.FromArgb(226, 243, 228),
            };

        public Color[] PastelPalette = new[] {
                Color.FromArgb(116, 86, 155),
                Color.FromArgb(150, 251, 199),
                Color.FromArgb(247, 255, 174),
                Color.FromArgb(255, 179, 203),
                Color.FromArgb(216, 191, 216),
            };

        public Color[] BronzePalette = new[] {
                Color.FromArgb(10, 10, 10),
                Color.FromArgb(34, 39, 40),
                Color.FromArgb(63, 85, 70),
                Color.FromArgb(126, 90, 100),
                Color.FromArgb(190, 149, 133),
                Color.FromArgb(218, 211, 190),
            };

        public Color[][] GetPalettes()
        {
            return new[] { IcePalette, DesertPalette, SoftPalette, SharpPalette, GreenPalette, PastelPalette, BronzePalette };
        }
    }
}
