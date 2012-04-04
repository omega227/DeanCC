using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public sealed class ImageDownloadEventArgs : System.ComponentModel.CancelEventArgs
    {
        public ImageDownloadEventArgs(IImageHeader image)
        {
            Image = image;
        }

        public IImageHeader Image { get; set; }
        public ImageDownloadResult Result { get; set; }
        //public int AllCount { get; set; }
        //public int CurrentIndex { get; set; }
    }
}
