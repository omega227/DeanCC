using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 取得時には画像URLと判断できないURL情報を表します
    /// </summary>
    /// <remarks>
    /// JaneStyle用ImageViewURLRepalce.datの$EXTRACTオプション等によって画像URLに置き換わる可能性のあるURLを格納するためのクラスです
    /// </remarks>
    [Serializable]
    public sealed class MaybeImageHeader : ImageHeader
    {
        public MaybeImageHeader()
        {
        }

        public MaybeImageHeader(int sourceResIndex, string url)
            : base(sourceResIndex, url)
        {
        }

        protected override void OnDownloading(ImageHeaderEventArgs e)
        {
            base.OnDownloading(e);
            if (string.IsNullOrEmpty(e.Url))
            {
                e.Status = ImageDownloadResultStatus.FailedReplaceExtractUrl;
            }
        }
    }
}
