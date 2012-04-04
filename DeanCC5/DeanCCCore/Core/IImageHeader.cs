using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DeanCCCore.Core
{
    public interface IImageHeader
    {
        bool DownloadCompleted { get; }
        bool Downloadable { get; }
        int TriedCount { get; }
        bool Secure { get; set; }
        DateTime FirstDownloadTime { get; }
        string OriginalUrl { get; }
        int SourceResIndex { get; }
        bool IsZip { get; }
        //string FileName { get; }
        string MD5Hash { get; }
        ImageState State { get; set; }
        ImageDownloadResult Download();
        void Save(byte[] data, string saveFolder);
        void ResetState();
        string SavedPath { get; }
    }
}
