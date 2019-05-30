using FdPictures.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FdPictures.Core
{
    public interface IFdPicturesContext
    {
        BitmapSource Transform(BitmapSource bitmapSource, TransformParams transformParams);
    }
}
