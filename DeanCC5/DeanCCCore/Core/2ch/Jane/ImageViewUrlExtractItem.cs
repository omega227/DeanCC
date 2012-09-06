using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Jane
{
    public sealed class ImageViewUrlExtractItem : ImageViewUrlItem
    {
        public ImageViewUrlExtractItem(string key, string replacement, string referer, string optionReferer, string extractPattern)
            : base(key, replacement, referer)
        {
            this.OptionReferer = optionReferer;
            this.ExtractPattern = CorrectRegex(extractPattern);
        }

        public string ExtractPattern { get; private set; }
        public string OptionReferer { get; private set; }

        public override bool WithOption
        {
            get
            {
                return true;
            }
        }

        public override ImageViewURLReplaceItem Replace(string url)
        {
            string replacedReferer = Pattern.Replace(url, string.IsNullOrEmpty(OptionReferer) ? Referer : OptionReferer);
            string contents;
            CookieContainer cookieContainer = new CookieContainer();
            using (HttpWebResponse res = InternetClient.GetResponse(replacedReferer, url, null))//refererで内容取得，画像urlをリファラとして送信
            {
                cookieContainer.Add(res.Cookies);
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new WebException("cannot get webdata for extract url");
                }
                using (Stream st = res.GetResponseStream())
                using (StreamReader sr = new StreamReader(st, Common.Options.InternetOptions.CurrentEncoding))
                {
                    contents = sr.ReadToEnd();
                }
            }

            string replacement = Pattern.Replace(url, Replacement);
            string extractReplacement = CorrectExtractRegex(replacement);
            string replacedExtractPattern = Pattern.Replace(url, ExtractPattern);
            string matchString = Regex.Match(contents, replacedExtractPattern).Value;
            string imageUrl = Regex.Replace(matchString, replacedExtractPattern, extractReplacement);

            ImageViewURLReplaceItem result = Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute) ?
                new ImageViewURLReplaceItem(imageUrl, replacedReferer, cookieContainer) :
                ImageViewURLReplaceItem.Empty;

            return result;
        }

        private string CorrectExtractRegex(string pattern)
        {
            pattern = Regex.Replace(pattern, @"\$EXTRACT(\d)", @"$$${1}");
            pattern = Regex.Replace(pattern, @"\$EXTRACT", @"$$1");
            pattern = Regex.Replace(pattern, "$&", @"$\{0}");

            return pattern;
        }
    }
}
