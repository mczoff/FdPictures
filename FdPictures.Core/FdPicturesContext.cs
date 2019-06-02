using FdPictures.Core.Params;
using FdPictures.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FdPictures.Core
{
    public class FdPicturesContext
        : IFdPicturesContext
    {
        readonly BitmapDiffService _bitmapDiffService;

        public FdPicturesContext()
        {
            _bitmapDiffService = new BitmapDiffService();
        }

        public BitmapSource Transform(BitmapSource bitmapSource, TransformParams transformParams)
        {
            var firstBitmap = new CroppedBitmap(bitmapSource, transformParams.FirstRectangle);
            var secondBitmap = new CroppedBitmap(bitmapSource, transformParams.SecondRectangle);


            var ss = _bitmapDiffService.Process(firstBitmap, secondBitmap);


            //using (FileStream mStream = new FileStream($@"E:\\tmp1.jpg", FileMode.Create))
            //{
            //    JpegBitmapEncoder jEncoder = new JpegBitmapEncoder();
            //    jEncoder.Frames.Add(BitmapFrame.Create(firstBitmap));
            //    jEncoder.Save(mStream);
            //}


            //using (FileStream mStream1 = new FileStream($@"E:\\tmp2.jpg", FileMode.Create))
            //{
            //    JpegBitmapEncoder jEncoder1 = new JpegBitmapEncoder();
            //    jEncoder1.Frames.Add(BitmapFrame.Create(secondBitmap));
            //    jEncoder1.Save(mStream1);
            //}

            return null;
        }
    }
}
