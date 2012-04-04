using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DeanCCCore.Core._2ch
{
    public sealed class ThreadEventArgs : System.ComponentModel.CancelEventArgs
    {
        public ThreadEventArgs()
        {
        }

        public int DownloadedCount
        {
            get;
            set;
        }
    }

    public sealed class ThreadUpdateEventArgs : System.ComponentModel.CancelEventArgs
    {
        public ThreadUpdateEventArgs()
        {
        }

        public HttpWebResponse Response { get; set; }
        public string NewReses { get; set; }
        public bool Updated { get; set; }
    }

    public sealed class ImageSaveEventArgs : System.ComponentModel.CancelEventArgs
    {
        public ImageSaveEventArgs(ImageDownloadResult result , IImageHeader imageHeader)
        {
            DownloadResult = result;
            ImageHeader = imageHeader;
        }

        public ImageDownloadResult DownloadResult { get; set; }
        public IImageHeader ImageHeader { get; set; }
    }
}
