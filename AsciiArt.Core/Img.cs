using System;

namespace AsciiArt.Core
{
    public class Img
    {
        private Byte[] Grid;

        public int Width { get; }

        public int Height { get; }

        public Img(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new Byte[width * height];
        }
        
        public byte this[int x, int y]
        {
            get => Grid[y * Width + x];
            set => Grid[y * Width + x] = value;
        }
    }
}