using System;
using DeanCCCore.Core._2ch.Jane;

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

        public override ImageDownloadResult Download()
        {
            ImageDownloadResult result = null;
            if (Common.ImageViewURLReplacer != null)
            {
                try
                {
                    ImageViewURLReplaceItem item = Common.ImageViewURLReplacer.Replace(OriginalUrl);
                    result = Download(item.ReplacedUrl, item.Referer, item.Cookie);
                }
                catch (UriFormatException)
                {
                    //imageview.datのuriの置換に失敗した時点でダウンロードしない
                    result.Status = ImageDownloadResultStatus.InvalidUriFormat;
                }
            }
            else
            {
                result = Download(OriginalUrl);
            }

            DownloadResult = result.Status;
            return result;
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
