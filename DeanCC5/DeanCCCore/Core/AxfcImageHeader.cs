using System;
using System.Text;
using System.Text.RegularExpressions;
using DeanCCCore.Core._2ch;
using DeanCCCore.Core._2ch.Utility;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class AxfcImageHeader : ImageHeader
    {
        public AxfcImageHeader()
            : base()
        {
        }

        public AxfcImageHeader(int sourceResIndex, string url)
            : base(sourceResIndex, url)
        {
        }

        public override bool IsZip
        {
            get
            {
                return true;
            }
        }

        protected override void OnDownloading(ImageHeaderEventArgs e)
        {
            base.OnDownloading(e);
            if (e.Cancel)
            {
                return;
            }

            //ダウンロードページ取得
            InternetClient.DownloadResult cushionPage = InternetClient.DownloadData(e.Url, DateTime.MinValue, Encoding.UTF8);
            if (!cushionPage.Success)
            {
                //ToDo: ダウンロード失敗時処理
                e.Status = ImageDownloadResultStatus.Failed;
                e.Cancel = true;
                return;
            }

            //画像Url取得
            string downloadUrl = string.Empty;
            if (Regex.IsMatch(cushionPage.Data, @"<div class=""imgbox_\w"">"))
            {
                //画像認証あり
                Secure = true;
                Downloadable = false;
                e.Status = ImageDownloadResultStatus.BlockedImagePass;
                e.Cancel = true;
                return;
            }
            else
            {
                //画像認証なし
                if (!Regex.IsMatch(cushionPage.Data, @"name=""keyword"""))
                {
                    //パスなし
                    try
                    {
                        downloadUrl = GetDownloadUrl(cushionPage.Data, new Uri(e.Url));
                    }
                    catch (System.Net.ProtocolViolationException)
                    {
                        e.Status = ImageDownloadResultStatus.Failed;
                        e.Cancel = true;
                        return;
                    }
                }
                else
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
                            return;
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
            }

            if (!Uri.IsWellFormedUriString(downloadUrl, UriKind.Absolute))
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

        protected override string GetDownloadUrl(string cushionPageData, Uri pageUri, string keyword = "")
        {
            Match m = Regex.Match(cushionPageData, @"name=""sid"" value=""(?<sid>\d+)""><input type=""hidden"" name=""dqn"" value=""(?<dqn>\d+)");
            Match orig = Regex.Match(cushionPageData, @"name=""origfilename"" value=""1"" checked>.+name=""attachement"" value=""1"" checked>");
            string s = keyword != "" ?
                String.Format("keyword={0}&sid={1}&dqn={2}", keyword, m.Groups["sid"].Value, m.Groups["dqn"].Value) :
                String.Format("sid={0}&dqn={1}", m.Groups["sid"].Value, m.Groups["dqn"].Value);
            if (orig.Success)
            {
                s += "&attachement=1&origfilename=1";
            }

            //ダウンロードページ取得
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            pageUri = new Uri(pageUri, ".././dl.pl");
            InternetClient.DownloadResult downloadPage = InternetClient.DownloadData(pageUri, bytes, Encoding.UTF8);
            if (!downloadPage.Success)
            {
                //失敗
                throw new NotImplementedException();
            }

            //画像ページ取得
            Uri imageUri = new Uri(pageUri, Regex.Match(downloadPage.Data, @"\./link\.pl\?dr=\d+\&file=[^""]+").Value);
            InternetClient.DownloadResult imagePage = InternetClient.DownloadData(imageUri.OriginalString);
            if (!imagePage.Success)
            {
                //失敗
                throw new NotImplementedException();
            }

            //画像URL取得
            string downloadUrl = Regex.Match(imagePage.Data, @"URL=(?<url>[^""]+)").Groups["url"].Value;
            return downloadUrl;
        }
    }
}
