using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Utility
{
    public static class ThreadUtility
    {
        private static readonly Regex UrlPattern =
            new Regex(@"h?ttp://([-_.!~*'a-zA-Z0-9;/?:@&=+$,%#]+)", RegexOptions.Compiled);
        private static readonly Regex ThreadUrlPattern =
            new Regex(@"^h?ttp://(?<host>.+)/(?<path>[^/]+)/(index\d*\.html|\s*$)", RegexOptions.Compiled);
        private static readonly string[] IgnoreExtensionHosts = { "www1.axfc.net", "www.dotup.org" };
        private static readonly Regex MaybeImageUrlPattern =
            new Regex(@"h?ttp://([-_.!~*'a-zA-Z0-9;?:@&=+$,%#]+/[-_.!~*'a-zA-Z0-9;/?:@&=+$,%#]+)", RegexOptions.Compiled);

        public sealed class ParseHeaderResult
        {
            public ParseHeaderResult()
            {
            }
            public IEnumerable<ImageHeader> ImageHeaders { get; set; }
            public IEnumerable<MaybeImageHeader> MaybeImageHeaders { get; set; }
        }

        public static BoardInfo ParseBoardInfo(string url, string name)
        {
            Match match = ThreadUrlPattern.Match(url);
            if (match.Success)
            {
                BoardInfo info = new BoardInfo(match.Groups["host"].Value, match.Groups["path"].Value, name);
                return info;
            }
            return null;
        }

        public static ParseHeaderResult ParseHeader(int startResIndex, string text, string extensionFormat)
        {
            List<ImageHeader> imageUrls = new List<ImageHeader>();
            List<MaybeImageHeader> urls = new List<MaybeImageHeader>();
            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                ForEachUrls(lines[i], extensionFormat,
                    imageUrl =>
                    {
                        Uri uri;
                        if (Uri.TryCreate(imageUrl, UriKind.Absolute, out uri))
                        {
                            imageUrls.Add(CreateHeader(i + startResIndex, uri));
                        }
                    }, url =>
                        {
                            if (Common.Options.BrowsersOptions.JaneOptions.EnableImageViewURLReplacedatOption)
                            {
                                Uri uri;
                                if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                                {
                                    if (MaybeImageUrlPattern.IsMatch(uri.OriginalString))
                                    {
                                        urls.Add(new MaybeImageHeader(i + startResIndex, uri.OriginalString));
                                    }
                                }
                            }
                        });
            }

            ParseHeaderResult result = new ParseHeaderResult()
            {
                ImageHeaders = imageUrls,
                MaybeImageHeaders = urls
            };
            return result;
        }

        public static void ForEachUrls(string text, string extensionFormat, Action<string> imageUrlCallback, Action<string> urlCallback)
        {
            Regex extensionRegex = new Regex(extensionFormat, RegexOptions.IgnoreCase);
            MatchCollection matches = UrlPattern.Matches(text);
            foreach (Match urlMatch in matches)
            {
                string url = Uri.UriSchemeHttp + "://" + urlMatch.Groups[1].Value;
                if (IsImageUrl(url, extensionRegex))
                {
                    imageUrlCallback(url);
                }
                else
                {
                    urlCallback(url);
                }
            }
        }

        private static ImageHeader CreateHeader(int sourceResIndex, Uri uri)
        {
            if (uri.Host.Equals("www1.axfc.net"))
            {
                //Axfc
                return new AxfcImageHeader(sourceResIndex, uri.OriginalString);
            }
            else if (uri.AbsolutePath.Equals(".zip"))
            {
                //zip
                if (uri.Host.Equals("www.dotup.org"))
                {
                    //どっとうｐ
                    return new DotupZipFileHeader(sourceResIndex, uri.OriginalString);
                }
                else
                {
                    //その他
                    return new ZipFileHeader(sourceResIndex, uri.OriginalString);
                }
            }
            else
            {
                //通常の画像
                return new ImageHeader(sourceResIndex, uri.OriginalString);
            }
        }

        public static bool IsImageUrl(string url, Regex extensionRegex)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return false;
            }
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Host))
            {
                return false;
            }
            if (IgnoreExtensionHosts.Contains(uri.Host))
            {
                return true;
            }
            return extensionRegex.IsMatch(uri.AbsolutePath);
        }

        public static DateTime CalculateSinceTime(string key)
        {
            DateTime time = new DateTime(1970, 1, 1);
            int seconds;
            if (int.TryParse(key, out seconds))
            {
                time = time.AddSeconds((double)seconds);
            }
            return time;
        }

        public static float CalculateResSpeed(DateTime since, int resCount)
        {
            TimeSpan span = DateTime.Now - since.ToLocalTime();
            return (float)Math.Round(resCount / span.TotalDays, 1);
        }

        public static Thread GetThreadFromImageUrl(string imageUrl)
        {
            return Common.DownloadingThreads.First(thread =>
                thread.ImageHeaders != null && thread.ImageHeaders.Any(image => image.OriginalUrl.Equals(imageUrl)));
        }
    }
}
