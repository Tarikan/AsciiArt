using System;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace AsciiArt.Core
{
    public static class ImageUtils
    {
        public delegate int rgbaToGreyscale(Color color, bool invert);
        
        public static int[] ImageToGreyscale(Bitmap img, rgbaToGreyscale del, bool invert)
        {
            int[] res = new int[img.Height * img.Width];

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    res[i * img.Height + j] = del(img.GetPixel(i, j), invert);
                }
            }

            var values = res.Distinct();
            var unique_values = values.Select(v => v/255f);
            var min = unique_values.Min();
            var max = unique_values.Max();
            var avg = (min+max)/2;
            var new_values = unique_values.Select(v => v - avg);
            max = new_values.Max();
            var coef = 0.5/max;
            var result_values = new_values.Select(v => Convert.ToInt32((v * coef + 0.5) * 255));
            var ret = result_values.Zip(values, (v1, v2) => (v2, v1)).ToDictionary(k => k.v2, v => v.v1);

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    res[i * img.Height + j] = ret[res[i * img.Height + j]];
                }
            }

            return res;
        }

        public static string GrayscaleToAscii(int[] greyscaleArr, int height)
        {
            var str = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
            var sb = new StringBuilder(greyscaleArr.Length + height);
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < greyscaleArr.Length / height; j++)
                {
                    //var test = Convert.ToInt32(Math.Round((double) (greyscaleArr[i, j] / str.Length)));
                    sb.Append(str[Convert.ToInt32(Math.Round((double)(greyscaleArr[j * height + i] / str.Length)))]);
                }

                sb.Append("\n");
            }
            return sb.ToString();
        }

        public static int RgbaToGreyscale(Color color, bool invert)
        {
            if (!invert)
            {
                return (int) ((color.R * 0.2126) +
                              (color.G * 0.7152) +
                              (color.B * 0.0722));
            }
            
            return (int) (((255 - color.R) * 0.2126) +
                          ((255 - color.G) * 0.7152) +
                          ((255 - color.B) * 0.0722));
        }
    }
}