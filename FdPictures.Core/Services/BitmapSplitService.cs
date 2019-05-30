using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FdPictures.Core.Services
{
    public class BitmapSplitService
    {
        public CroppedBitmap[,] Split(BitmapSource bitmapSource, int hratio, int wratio)
        {

            var croppedBitmaps = GetCroopedBitmap(bitmapSource, hratio, wratio);

            return null;
        }

        private CroppedBitmap[,] GetCroopedBitmap(BitmapSource bitmapSource, int hratio, int wratio)
        {
            CroppedBitmap[,] croppedBitmaps = new CroppedBitmap[hratio, wratio];

            int hsizepart = (int)bitmapSource.Height / hratio;
            int wsizepart = (int)bitmapSource.Width / wratio;


            for (int heightIndex = 0; heightIndex < hratio; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < wratio; widthIndex++)
                {
                    croppedBitmaps[heightIndex, widthIndex] = new CroppedBitmap(bitmapSource, new Int32Rect(widthIndex * wsizepart, heightIndex * hsizepart, wsizepart, hsizepart));
                }
            }

            for (int heightIndex = 0; heightIndex < hratio; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < wratio; widthIndex++)
                {
                    FileStream mStream = new FileStream($@"E:\\tempimages\{widthIndex}{heightIndex}tmp.jpg", FileMode.Create);

                    JpegBitmapEncoder jEncoder = new JpegBitmapEncoder();
                    jEncoder.Frames.Add(BitmapFrame.Create(croppedBitmaps[heightIndex, widthIndex]));
                    jEncoder.Save(mStream);
                }
            }

            return croppedBitmaps;
        }
    }
}
