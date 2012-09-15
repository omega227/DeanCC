using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 取得した画像URLリスト
    /// </summary>
    [Serializable]
    //ToDo: ローカルにキャッシュして、Dispose可能にする。
    public sealed class ImageHeaderCollection : Collection<IImageHeader>
    {
        //public event EventHandler Downloading;
        //public event EventHandler Downloaded;
        //public event EventHandler Saving;
        //public event EventHandler Saved;

        public ImageHeaderCollection()
        {
        }

        public bool DownloadCompleted
        {
            get
            {
                return this.All(image => image.DownloadCompleted || !image.Downloadable);
            }
        }

        protected override void InsertItem(int index, IImageHeader item)
        {
            base.InsertItem(index, item);
            if (item.IsZip)
            {
                ZipCount++;
            }
        }

        [NonSerialized]
        private int? zipCount;

        public int ZipCount
        {
            get
            {
                if (zipCount == null)
                {
                    zipCount = this.Count(image => image.IsZip);
                }
                return (int)zipCount;
            }
            private set
            {
                zipCount = value;
            }
        }

        public bool Contains(string url)
        {
            return this.Any(image => image.OriginalUrl.Equals(url));
        }
    }
}
