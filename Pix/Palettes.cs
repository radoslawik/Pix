using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pix
{
    public class Palettes
    {
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
            return new[] { SoftPalette, SharpPalette, BronzePalette };
        }
    }
}
