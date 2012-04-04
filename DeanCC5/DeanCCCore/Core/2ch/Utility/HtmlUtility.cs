using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using DeanCCCore.Core.Utility;

namespace DeanCCCore.Core._2ch.Utility
{
    public static class HtmlUtility
    {
        #region 定数メンバー
        private static readonly Encoding CurrentEncoding = Encoding.GetEncoding("shift_jis");
        private const string DefaultName = "名無しさん";
        private const string DefaultID = "???";
        private const string ResSpaceTag = "<br><br>";
        //private const string IgnoreLinkExtension = ".zip";

        private const string HtmlHeaderFormat =
@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=Shift_JIS"">
<title>{0}</title>
</head>
<body bgcolor=#efefef text=black link=blue alink=red vlink=#660099>
<div style=""margin-top:1em;""><span style='float:left;'>
<a href=""{1}"">■元のスレッド■</a>
</span>
<span style='float:right;'>
</span>&nbsp;</div>
<hr style=""background-color:#888;color:#888;border-width:0;height:1px;position:relative;top:-.4em;"">
<h1 style=""color:red;font-size:larger;font-weight:normal;margin:-.5em 0 0;"">{0}</h1>
<dl class=""thread"">
";

        private const string HtmlResZipFormat =
            @"<dt><a name=""R{0}"">{0}</a> ：<font color=green><b>{1}</b></font> [{2}] ：{3}<dd> {4} <br>";

        private const string HtmlResFormat =
            @"<dt><a name=""R{0}"">{0}</a> ：<font color=green><b>{1}</b></font>：{3}<dd> {4} ";

        private const string HtmlResMailtoFormat =
            @"<dt><a name=""R{0}"">{0}</a> ：<a href=""mailto:{2}""><b>{1}</b></a>：{3}<dd> {4} ";

        private const string HtmlResImageFormat =
            @"<a href =""{0}"" target=""_blank""><img src=""{0}"" height=""256"" alt=""image"" hspace=""4"" vspace=""8"" border=""1"" border-color=""blue"" target=""_blank""></a>";

        private static readonly Regex StockLinksPattern = new Regex(@"<a href=""http://2ch\.se/"">(\w+)</a>", RegexOptions.Compiled);
        private const string StockUrlReplacement = "$1";

        private static readonly Regex AnchorPattern = new Regex(@"<a href=""\.\./test/read\.cgi/.+/\d+/\d+"" target=""_blank"">&gt;&gt;(\d+)</a>", RegexOptions.Compiled);
        private const string FormatAnchorReplacement = @"<a href=""#R$1"">&gt;&gt;$1</a>";

        private static readonly Regex UrlPattern = new Regex(@"h?ttp://([-_.!~*'a-zA-Z0-9;/?:@&=+$,%#]+)", RegexOptions.Compiled);
        private const string LinkReplacement = @"<a href=""http://$1"" target=""_blank"">$0</a>";

        private static readonly Regex ResPattern = new Regex(@"^(?<Name>.*)<>(?<Mail>.*)<>(?<ID>.*)<>(?<Body>.*)<>.*$", RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex ImageTagPattern = new Regex(@"<a href=""(?<Url>http://[-_.!~*'a-zA-Z0-9;/?:@&=+$,%#]+)"" target=""_blank"">(?<OriginalUrl>h?ttp://[-_.!~*'a-zA-Z0-9;/?:@&=+$,%#]+)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private const string UrlLinkTagFormat = @"<a href=""{0}"" target=""_blank"">";

        private const string UrlLinkTagWithOriginalUrlFormat = @"<a href="".*{0}.*"" target=""_blank"">(?<OriginalUrl>h?ttp://[-_.!~*'a-zA-Z0-9;/?:@&=+$,%#]+)</a>";
        #endregion

        /// <summary>
        /// パス付き画像スレッドを表すHtmlを作成します
        /// </summary>
        /// <param name="dat">スレッドデータ</param>
        /// <param name="title">スレッドタイトル</param>
        /// <param name="imageHeaders">スレッド画像リスト</param>
        /// <returns>作成したhtmlソース</returns>
        public static string CreateSecureImageLogHtml(string dat, IThreadHeader threadHeader, IEnumerable<IImageHeader> imageHeaders)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat(HtmlHeaderFormat, threadHeader.Title, threadHeader.Url);
            html.Append(CreateSecureImageLogBody(dat, imageHeaders));

            return html.ToString();
        }

        /// <summary>
        /// スレッドを表すHtmlを作成します
        /// </summary>
        /// <param name="dat">スレッドデータ</param>
        /// <param name="title">スレッドタイトル</param>
        /// <param name="imageHeaders">スレッド画像リスト</param>
        /// <param name="imagesPath">画像の保存フォルダー</param>
        /// <returns>作成したhtmlソース</returns>
        public static string Create(string dat, IThreadHeader threadHeader, IEnumerable<IImageHeader> imageHeaders)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat(HtmlHeaderFormat, threadHeader.Title, threadHeader.Url);
            html.Append(CreateBody(dat, imageHeaders, threadHeader.ImageSaveFolder));

            return html.ToString();
        }

        /// <summary>
        /// パス付画像URLが含まれるレスのみを抽出した内容を作成します
        /// </summary>
        /// <param name="rawDat"></param>
        /// <param name="imageHeaders"></param>
        /// <returns></returns>
        private static string CreateSecureImageLogBody(string rawDat, IEnumerable<IImageHeader> imageHeaders)
        {
            string linkedDat = FormatDat(rawDat);
            StringBuilder body = new StringBuilder();
            MatchCollection reses = ResPattern.Matches(linkedDat);
            foreach (IGrouping<int, IImageHeader> resGroup in imageHeaders.GroupBy(image => image.SourceResIndex))
            {
                List<IImageHeader> resList = resGroup.ToList();
                if (resList.Exists(image => image.Secure))
                {
                    int resIndex = resGroup.Key;
                    Match res = reses[resIndex];
                    string resHtml = CreateSecureImageRes(resIndex, res.Groups["Name"].Value, res.Groups["Mail"].Value,
                        res.Groups["ID"].Value, res.Groups["Body"].Value, resList);
                    body.Append(resHtml);
                    body.AppendLine(ResSpaceTag);
                }
            }
            return body.ToString();
        }

        /// <summary>
        /// すべてのレスをまとめた内容を作成します
        /// </summary>
        /// <param name="rawDat"></param>
        /// <param name="imageHeaders"></param>
        /// <param name="imagesPath"></param>
        /// <returns></returns>
        private static string CreateBody(string rawDat, IEnumerable<IImageHeader> imageHeaders, string imagesPath)
        {
            string linkedDat = FormatDat(rawDat);
            StringBuilder body = new StringBuilder();
            MatchCollection reses = ResPattern.Matches(linkedDat);
            int index = 1;
            foreach (Match res in reses)
            {
                string resHtml = CreateRes(index++, res.Groups["Name"].Value, res.Groups["Mail"].Value,
                    res.Groups["ID"].Value, res.Groups["Body"].Value, imageHeaders, imagesPath);
                body.Append(resHtml);
                body.AppendLine(ResSpaceTag);
            }
            return body.ToString();
        }

        private static string CreateSecureImageRes(int index, string name, string mail, string id, string body,
            IEnumerable<IImageHeader> resImageHeaders)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = DefaultName;
            }
            if (string.IsNullOrEmpty(id))
            {
                id = DefaultID;
            }

            return string.Format(string.IsNullOrEmpty(mail) ?
            HtmlResFormat : HtmlResMailtoFormat, index, name, mail, id, body);
        }

        private static string CreateRes(int index, string name, string mail, string id, string body,
            IEnumerable<IImageHeader> imageHeaders, string imagesPath)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = DefaultName;
            }
            if (string.IsNullOrEmpty(id))
            {
                id = DefaultID;
            }

            string linkedBody = AddLinkUrl(body, imageHeaders, imagesPath);
            return string.Format(string.IsNullOrEmpty(mail) ?
            HtmlResFormat : HtmlResMailtoFormat, index, name, mail, id, linkedBody);
        }

        private static string AddLinkUrl(string body, IEnumerable<IImageHeader> imageHeaders, string imagesPath)
        {
            MatchCollection images = ImageTagPattern.Matches(body);
            StringBuilder imageLinksHtml = new StringBuilder();
            foreach (Match image in images)
            {
                string url = image.Groups["Url"].Value;
                IImageHeader header = imageHeaders.FirstOrDefault(h => h.OriginalUrl.Equals(url));
                if (header == null)
                {
                    continue;
                }
                if (header.IsZip)
                {
                    continue;
                }
                if (header.State == ImageState.NGFile)
                {
                    //リンク削除
                    string imagePattern = Regex.Escape(image.Value);
                    body = Regex.Replace(body, imagePattern, image.Groups["OriginalUrl"].Value);
                    continue;
                }

                if (File.Exists(header.SavedPath))
                {
                    string linkPath = null;
                    try
                    {
                        linkPath = PathUtility.MakeRelative(imagesPath, header.SavedPath);
                    }
                    catch (ArgumentException)
                    {
                        linkPath = header.SavedPath;
                    }
                    imageLinksHtml.AppendFormat(HtmlResImageFormat, linkPath);
                    string urlLinkPattern = Regex.Escape(string.Format(UrlLinkTagFormat, url));
                    body = Regex.Replace(body, urlLinkPattern, string.Format(UrlLinkTagFormat, linkPath));
                }
                else
                {
                    ImageHash imageHash = null;
                    lock (((System.Collections.ICollection)Common.DownloadedImageHashes).SyncRoot)
                    {
                        imageHash = Common.DownloadedImageHashes.FirstOrDefault(hash =>
                            hash.MD5Hash.Equals(header.MD5Hash, StringComparison.CurrentCultureIgnoreCase));
                    }
                    if (imageHash == null)
                    {
                        continue;
                    }
                    if (File.Exists(imageHash.SavePath))
                    {
                        string linkPath = null;
                        try
                        {
                            linkPath = PathUtility.MakeRelative(imagesPath, imageHash.SavePath);
                        }
                        catch (ArgumentException)
                        {
                            linkPath = imageHash.SavePath;
                        }
                        imageLinksHtml.AppendFormat(HtmlResImageFormat, linkPath);
                        string urlLinkPattern = Regex.Escape(string.Format(UrlLinkTagFormat, url));
                        body = Regex.Replace(body, urlLinkPattern, string.Format(UrlLinkTagFormat, linkPath));
                    }
                }
            }
            return (imageLinksHtml.Length == 0) ? (body) : (body + ResSpaceTag + imageLinksHtml.ToString());
        }

        static string FormatDat(string dat)
        {
            //株リンク削除
            string linkedDat = StockLinksPattern.Replace(dat, StockUrlReplacement);
            //安価を置換
            linkedDat = AnchorPattern.Replace(linkedDat, FormatAnchorReplacement);
            //リンクを付ける
            linkedDat = UrlPattern.Replace(linkedDat, LinkReplacement);
            //NGURLのリンクを削除
            linkedDat = RemoveNGUrlLink(linkedDat);
            return linkedDat;
        }

        static string RemoveNGUrlLink(string dat)
        {
            if (Common.Options.NGOptions.NGUrls.Count == 0)
            {
                return dat;
            }

            StringBuilder ngUrls = new StringBuilder();
            foreach (string ngurl in Common.Options.NGOptions.NGUrls)
            {
                ngUrls.AppendFormat("{0}|", ngurl);
            }
            string ngUrlsPattern =
                string.Format(UrlLinkTagWithOriginalUrlFormat,
                Regex.Escape(string.Format("({0})", Regex.Replace(ngUrls.ToString(), @"\|$", ""))));

            return Regex.Replace(dat, ngUrlsPattern, "${OriginalUrl}", RegexOptions.IgnoreCase);
        }
    }
}
