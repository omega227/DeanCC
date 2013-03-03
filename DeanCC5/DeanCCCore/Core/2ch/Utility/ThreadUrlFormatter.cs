using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Utility
{
    public static class ThreadUrlFormatter
    {
        public const string UrlFormat = "http://{0}/test/read.cgi/{1}/{2}/";
        public const string DatUrlFormat = "http://{0}/{1}/dat/{2}.dat";
        public const string Bg20DatUrlFormat = "http://bg20.2ch.net/test/r.so/{0}/{1}/{2}/";
        public const string BoardUrlFormat = "http://{0}/{1}/";
        public static readonly Regex UrlPattern =
            new Regex(@"h?ttp://(?<host>.+)/test/read\.cgi/(?<path>\w+)/(?<number>\d+)/", RegexOptions.Compiled);
        public static readonly Regex DatUrlPattern =
            new Regex(@"h?ttp://(?<host>.+)/(?<path>.+)/dat/(?<number>\d+)\.dat", RegexOptions.Compiled);
        public static readonly Regex BoardUrlPattern =
            new Regex(@"h?ttp://(?<host>.+)/(?<path>.+)/", RegexOptions.Compiled);

        public static string FormatDatUrl(string url)
        {
            Match m = MatchUrl(url);

            return FormatDatUrl(m.Groups["host"].Value, m.Groups["path"].Value, m.Groups["number"].Value);
        }

        public static string FormatBg20ServerDatUrl(string url)
        {
            Match m = MatchUrl(url);

            return string.Format(Bg20DatUrlFormat, m.Groups["host"].Value, m.Groups["path"].Value, m.Groups["number"].Value);
        }

        public static string FormatUrl(string unkownFormatUrl)
        {
            Match m = MatchUrl(unkownFormatUrl);
            return string.Format(UrlFormat, m.Groups["host"].Value, m.Groups["path"].Value, m.Groups["number"].Value);
        }

        public static string FormatUrl(string host, string path, string key)
        {
            return string.Format(UrlFormat, host, path, key);
        }

        public static string FormatDatUrl(string host, string path, string key)
        {
            return string.Format(DatUrlFormat, host, path, key);
        }

        public static string FormatBoardUrl(string url)
        {
            Match m = MatchUrl(url);
            return string.Format(BoardUrlFormat, m.Groups["host"].Value, m.Groups["path"].Value);
        }

        private static Match MatchUrl(string url)
        {
            Match m = null;
            m = UrlPattern.Match(url);
            if (m.Success)
            {
                return m;
            }
            m = DatUrlPattern.Match(url);
            if (m.Success)
            {
                return m;
            }
            m = BoardUrlPattern.Match(url);
            if (m.Success)
            {
                return m;
            }

            //2chのURLでない
            throw new ArgumentException("2ちゃんねるのURL以外の文字列が入力されました。");
        }
    }
}
