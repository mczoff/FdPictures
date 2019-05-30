using FdPictures.Core.Params;
using FdPictures.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FdPictures.Core
{
    public class FdPicturesContext
        : IFdPicturesContext
    {
        BitmapSplitService _bitmapSplitService;

        public FdPicturesContext()
        {
            _bitmapSplitService = new BitmapSplitService();
        }

        public BitmapSource Transform(BitmapSource bitmapSource, TransformParams transformParams)
        {
            var pictures = _bitmapSplitService.Split(bitmapSource, transformParams.HRatio, transformParams.WRatio);

            return null;
        }
    }
}
