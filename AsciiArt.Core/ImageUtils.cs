using System;
using System.Linq;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

//using Color = System.Drawing.Color;

namespace AsciiArt.Core
{
    public static class ImageUtils
    {
        //public delegate int rgbaoGreyscale(Color color, bool invert);

        public static Img ReadAndGreyscale(Byte[] file, double compressRatio, bool invert)
        {
            var img = Image.Load(file);
            
            int width = img.Width;
            int height = img.Height;
            
            //Console.WriteLine($"image w = {width} h = {height}");

            if (compressRatio != 1)
            {
                width = Convert.ToInt32(width * compressRatio);
                height = Convert.ToInt32(height * compressRatio);
            }

            Img res = new Img(width,
                height);
            //img.Mutate(i => i.Contrast(0.5f));
            if (invert)
            {
                img.Mutate(i => i.Invert());
            }

            if (compressRatio != 1)
            {
                img.Mutate(i =>
                    i.Resize(new Size(width, height)));
            }
            
            img.Mutate(i => i.GaussianSharpen());

            img.Mutate(i => i.Dither());
            
            img.Mutate(i => i.Contrast(0.7f));
            
            img.Mutate(i => i.Grayscale());
            

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //Console.WriteLine($"i = {i} j = {j} res w/h = {res.Width}/{res.Height} size = {res.Width * res.Height}");
                    res[j, i] = Convert.ToByte(img[j, i].R);
                }
            }
            
            img.Dispose();

            return res;
        }

        public static string GrayscaleToAscii(Img img)
        {
            var str = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
            var sb = new StringBuilder(img.Width * img.Height + img.Height);
            for (var i = 0; i < img.Height; i++)
            {
                for (var j = 0; j < img.Width; j++)
                {
                    sb.Append(str[Convert.ToInt32(Math.Round((double) (img[j, i] / str.Length)))]);
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}