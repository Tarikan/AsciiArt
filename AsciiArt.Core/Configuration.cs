using System;

namespace AsciiArt.Core
{
    public class Configuration
    {
        public RgbaColor BackgroundColor { get; }
    }

    public readonly struct RgbaColor
    {
        public short R { get; }
        public short G { get; }
        public short B { get; }
        public short A { get; }

        public RgbaColor(short r = 0,
            short g = 0,
            short b = 0,
            short a = 0)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}