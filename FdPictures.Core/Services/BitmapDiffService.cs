using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FdPictures.Core.Services
{
    public class BitmapDiffService
    {
        public BitmapSource Process(BitmapSource bitmapSource1, BitmapSource bitmapSource2)
        {
            //BitmapSource bitmapSource = BitmapSource.Create(
            //    bitmapSource1.Height > bitmapSource2.Height ? bitmapSource1.Height : bitmapSource2.Height)

            var width = bitmapSource1.Width > bitmapSource2.Width ? bitmapSource2.Width : bitmapSource1.Width;
            var height = bitmapSource1.Height > bitmapSource2.Height ? bitmapSource2.Height : bitmapSource1.Height;

            //WriteableBitmap wb = new WriteableBitmap(
            //        (int)width,
            //        (int)height,
            //        96, 96,
            //        bitmapSource1.Format, null);

            //wb.WritePixels();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var color1 = GetPixelColor(bitmapSource1, i, j);
                    var color2 = GetPixelColor(bitmapSource2, i, j);

                    if (color1 == color2)
                    {
                        
                    }
                }
            }

            return null;
        }

        public static Color GetPixelColor(BitmapSource bitmap, int x, int y)
        {
            Color color;
            var bytesPerPixel = (bitmap.Format.BitsPerPixel + 7) / 8;
            var bytes = new byte[bytesPerPixel];
            var rect = new Int32Rect(x, y, 1, 1);

            bitmap.CopyPixels(rect, bytes, bytesPerPixel, 0);

            if (bitmap.Format == PixelFormats.Bgra32)
            {
                color = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
            }
            else if (bitmap.Format == PixelFormats.Bgr32)
            {
                color = Color.FromRgb(bytes[2], bytes[1], bytes[0]);
            }
            // handle other required formats
            else
            {
                color = Colors.Black;
            }

            return color;
        }
    }
}
