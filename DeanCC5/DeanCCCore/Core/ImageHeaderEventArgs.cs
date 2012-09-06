using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public sealed class ImageHeaderEventArgs : System.ComponentModel.CancelEventArgs
    {
        public ImageHeaderEventArgs(string url)
        {
            Url = url;
        }

        public ImageDownloadResultStatus Status { get; set; }
        public bool Downloaded { get; set; }
        public byte[] DownloadedData { get; set; }
        public System.Net.HttpWebResponse ResponseHeader { get; set; }
        public string Url { get; set; }
        public bool Locked { get; set; }
        public bool TriedDownload { get; set; }
        private string host;
        public string Host
        {
            get
            {
                if (host == null)
                {
                    Uri uri = new Uri(Url);
                    host = uri.Host;
                }
                return host;
            }
        }
    }
}
