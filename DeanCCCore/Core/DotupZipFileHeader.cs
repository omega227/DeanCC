using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class DotupZipFileHeader : ZipFileHeader
    {
        public DotupZipFileHeader()
        {
        }

        public DotupZipFileHeader(int sourceResIndex, string url)
        {
            SourceResIndex = sourceResIndex;
            OriginalUrl = url;
        }

        protected override void OnDownloading(ImageHeaderEventArgs e)
        {
            base.OnDownloading(e);
            if (e.Cancel)
            {
                return;
            }

            //ダウンロードページ取得
            string cushionPageUrl = e.Url + ".html";
            InternetClient.DownloadResult cushionPage =
                InternetClient.DownloadData(cushionPageUrl, DateTime.MinValue, Encoding.GetEncoding("shift_jis"));
            if (!cushionPage.Success)
            {
                //ToDo: ダウンロード失敗時処理
                e.Status = ImageDownloadResultStatus.Failed;
                e.Cancel = true;
                return;
            }

            //画像URL取得
            string downloadUrl = string.Empty;
            if (Regex.IsMatch(cushionPage.Data,
                @" <form action=""http://www.dotup.org/upload.cgi"" method=""post"" name=""DL"" id=""DL"">"))
            {
                try
                {
                    downloadUrl = GetSecureDownloadUrl(cushionPage.Data, new Uri(e.Url));
                    if (downloadUrl == string.Empty)
                    {
                        //パスが不一致
                        Secure = true;
                        Downloadable = false;
                        e.Status = ImageDownloadResultStatus.PasswordMissMatch;
                        e.Cancel = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    Secure = true;
                    Downloadable = false;
                    e.Status = ImageDownloadResultStatus.Failed;
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                //パスなし
                downloadUrl = e.Url;
            }

            if (!e.Cancel && !Uri.IsWellFormedUriString(downloadUrl, UriKind.Absolute))
            {
                e.Status = ImageDownloadResultStatus.InvalidUriFormat;
                e.Cancel = true;
                Downloadable = false;
            }
            else
            {
                e.Url = downloadUrl;
            }
        }

        protected override string GetDownloadUrl(string cushionPage, Uri uri, string keyword = "")
        {
            string s = string.Format("file={0}&jcode=漢字&mode=dl&dlkey={1}",
                Regex.Match(uri.OriginalString, @"www.dotup.org(\d+)").Groups[1].Value, keyword);

            byte[] bytes = Encoding.ASCII.GetBytes(s);
            uri = new Uri("http://www.dotup.org/upload.cgi");
            InternetClient.DownloadResult downloadPage =
                InternetClient.DownloadData(uri, bytes, Encoding.GetEncoding("shift_jis"));
            if (!downloadPage.Success)
            {
                //失敗
                throw new NotImplementedException();
            }

            string downloadUrl = Regex.Match(downloadPage.Data, @"<a href=""([^""]+)"">ダウンロード</a>").Groups[1].Value;
            return downloadUrl;
        }
    }
}
