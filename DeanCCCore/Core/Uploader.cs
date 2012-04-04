using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public sealed class Uploader
    {
        public Uploader(IEnumerable<IImageHeader> imageHeaders)
        {
            Uri uri = new Uri(imageHeaders.First().OriginalUrl);
            Host = uri.Host;
            //ToDo: 同一ホスト（アップローダー）であることを確認
            foreach (IImageHeader image in imageHeaders)
            {
                this.imageHeaders.Add(image);
            }
        }

        public object SyncRoot { get { return Host; } }
        private ImageHeaderCollection imageHeaders = new ImageHeaderCollection();
        public ImageHeaderCollection ImageHeaders
        {
            get { return imageHeaders; }
            set { imageHeaders = value; }
        }

        public string Host { get; private set; }

        /// <summary>
        /// アップローダーごとにコレクションを分割します
        /// </summary>
        public static IEnumerable<Uploader> Split(IEnumerable<IImageHeader> headers)
        {
            List<string> hosts = new List<string>();
            List<Uploader> result = new List<Uploader>();
            foreach (IImageHeader currentHeader in headers)
            {
                string currentHost = new Uri(currentHeader.OriginalUrl).Host;
                if (!hosts.Contains(currentHost))
                {
                    hosts.Add(currentHost);
                    IEnumerable<IImageHeader> images = headers.Where(image => new Uri(image.OriginalUrl).Host.Equals(currentHost));
                    result.Add(new Uploader(images));
                }
            }
            return result;
        }
    }
}
