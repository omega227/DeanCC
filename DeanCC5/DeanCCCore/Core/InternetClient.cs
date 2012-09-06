using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace DeanCCCore.Core
{
    public static class InternetClient
    {
        public static event EventHandler<InternetClientEventArgs> Downloaded;

        public const int DefaultTimeout = 15000;
        private const string UserAgentFormat =
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; ja) DeanCC {0}.{1}";

        private static string userAgent = GetUserAgent();

        private static string GetUserAgent()
        {
            string[] version = System.Windows.Forms.Application.ProductVersion.Split('.');
            return string.Format(UserAgentFormat, version);
        }

        public static string UserAgent
        {
            get
            {
                return userAgent;
            }
        }

        /// <summary>
        /// URIへのhttpプロトコルによる要求インスタンスを作成します
        /// </summary>
        /// <param name="url">要求先URI</param>
        /// <param name="lastModified">最終更新</param>
        /// <param name="referer">リファラー</param>
        /// <param name="cookie">クッキー</param>
        /// <param name="isgzip">gZip圧縮を要求すかどうか</param>
        /// <param name="range">要求する値の範囲(BYTE)</param>
        /// <param name="timeout">タイムアウト(ミリ秒)</param>
        /// <param name="eTag">E-TAG</param>
        /// <param name="allowRedirect">リダイレクトに要求するかどうか</param>
        /// <returns>作成した要求インスタンス</returns>
        public static HttpWebRequest Create(
            string url, DateTime lastModified, string referer = "", CookieContainer cookie = null,
            bool isgzip = true, int range = -1, int timeout = DefaultTimeout,
            string eTag = "", bool allowRedirect = true)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.UserAgent = userAgent; //配布する場合はこちらを有効にする(↓はコメントアウト)
            //req.UserAgent = url.StartsWith("http://beebee2see.appspot.com/") ?
            //    req.UserAgent = "BB2C/1.3.3 CFNetwork/342.1 Darwin/9.4.1" : userAgent;

            if (isgzip)
            {
                req.AutomaticDecompression = DecompressionMethods.GZip;
            }
            if (!string.IsNullOrEmpty(referer))
            {
                req.Referer = referer;
            }
            if (timeout > 0 || timeout == System.Threading.Timeout.Infinite)
            {
                req.Timeout = req.ReadWriteTimeout = timeout;
            }
            if (cookie != null)
            {
                req.CookieContainer = cookie;
            }
            if (range > 0)
            {
                req.AddRange(range);
            }
            if (lastModified > DateTime.MinValue)
            {
                req.IfModifiedSince = lastModified;
            }
            if (!string.IsNullOrEmpty(eTag))
            {
                req.Headers.Add("If-None-Match", eTag);
            }
            req.AllowAutoRedirect = allowRedirect;

            return req;
        }

        public static HttpWebRequest CreatePost(Uri uri, byte[] data)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.UserAgent = userAgent;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.LongLength;
            req.Expect = "";

            OnPosting(new InternetClientEventArgs(0, data.LongLength));
            using (System.IO.Stream st = req.GetRequestStream())
            {
                st.Write(data, 0, data.Length);
            }

            return req;
        }

        public static HttpWebResponse GetResponse(string url)
        {
            HttpWebRequest req = Create(url, DateTime.MinValue,
            timeout: Common.Options.InternetOptions.Timeout);
            return (HttpWebResponse)req.GetResponse();
        }

        public static HttpWebResponse GetResponse(string url, string referer, CookieContainer cookie)
        {
            HttpWebRequest req = Create(url, DateTime.MinValue,
            referer: referer, cookie: cookie, timeout: Common.Options.InternetOptions.Timeout);
            return (HttpWebResponse)req.GetResponse();
        }

        public static DownloadResult DownloadData(string url)
        {
            return DownloadData(url, DateTime.MinValue);
        }

        public static DownloadResult DownloadData(string url, DateTime lastModified, Encoding encoding = null)
        {
            HttpWebRequest req = Create(url: url, lastModified: lastModified,
            timeout: Common.Options.InternetOptions.Timeout);
            return DownloadData(req, encoding);
        }

        public static DownloadResult DownloadData(Uri uri, byte[] data, Encoding encoding = null)
        {
            HttpWebRequest req = CreatePost(uri, data);
            return DownloadData(req, encoding);
        }

        private static DownloadResult DownloadData(HttpWebRequest request, Encoding encoding = null)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream st = response.GetResponseStream())
                    using (StreamReader sr = new StreamReader(st, encoding ?? Common.Options.InternetOptions.CurrentEncoding))
                    {
                        //OnDownloaded(new InternetClientEventArgs(response.ContentLength, 0));
                        return new DownloadResult(true, sr.ReadToEnd(), response.LastModified);
                    }
                }
            }
            return DownloadResult.Empty;
        }

        //public static void PerformDownloaded(InternetClientEventArgs e)
        //{
        //    OnDownloaded(e);
        //}

        private static void OnDownloaded(InternetClientEventArgs e)
        {
            if (e.ReceiveBytes > 0)
            {
                Common.CurrentSettings.Information.CurrentDownloadByte += e.ReceiveBytes;
            }
            if (Downloaded != null)
            {
                Downloaded(null, e);
            }
        }

        private static void OnPosting(InternetClientEventArgs e)
        {
            if (e.SentBytes > 0)
            {
                Common.CurrentSettings.Information.CurrentUploadByte += e.SentBytes;
            }
        }

        public static void PerformResponseRecieved(InternetClientEventArgs e)
        {
            OnDownloaded(e);
            OnPosting(e);
        }

        public sealed class DownloadResult
        {
            /// <summary>
            /// 空の結果を表します。通常はダウンロードの失敗を意味します
            /// </summary>
            public static readonly DownloadResult Empty = new DownloadResult();

            public DownloadResult()
            {
            }

            public DownloadResult(bool success, string data, DateTime lastModified)
            {
                Success = success;
                Data = data;
                LastModified = lastModified;
            }

            public bool Success { get; private set; }
            public string Data { get; private set; }
            public DateTime LastModified { get; private set; }
        }
    }

    public sealed class InternetClientEventArgs : EventArgs
    {
        public InternetClientEventArgs()
        {
        }

        public InternetClientEventArgs(long receiveBytes, long sentBytes)
        {
            ReceiveBytes = receiveBytes;
            SentBytes = sentBytes;
        }

        public long ReceiveBytes { get; private set; }
        public long SentBytes { get; private set; }
    }
}
