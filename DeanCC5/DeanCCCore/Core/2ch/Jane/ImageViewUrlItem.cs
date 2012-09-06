using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Jane
{
    public class ImageViewUrlItem
    {
        public ImageViewUrlItem()
        {
        }

        public ImageViewUrlItem(string key, string replacement, string referer)
        {
            this.Pattern = new Regex(CorrectRegex(key), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            this.Replacement = CorrectRegex(replacement);
            this.Referer = CorrectRegex(referer);

            RegexTest();
        }

        /// <summary>
        /// ImageViewURLReplace.dat用正規表現を.NETの正規表現に直します
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        protected string CorrectRegex(string pattern)
        {
            pattern = Regex.Replace(pattern, @"$\d", @"$\{${1}}");
            pattern = Regex.Replace(pattern, "$&", @"$\{0}");

            return pattern;
        }

        protected virtual void RegexTest()
        {
            Pattern.Replace("", Replacement);
            Pattern.Replace("", Referer);
        }

        /// <summary>
        /// このインスタンスが表す正規表現
        /// </summary>
        public Regex Pattern { get; protected set; }

        /// <summary>
        /// 置換文字
        /// </summary>
        public string Replacement { get; protected set; }

        /// <summary>
        /// リファラ
        /// </summary>
        public string Referer { get; protected set; }

        /// <summary>
        /// オプション($EXTRACT等)が有効なアイテムかどうかを表します
        /// </summary>
        public virtual bool WithOption { get { return false; } }

        public virtual ImageViewURLReplaceItem Replace(string url)
        {
            ImageViewURLReplaceItem result = new ImageViewURLReplaceItem();
            result.Referer = Pattern.Replace(url, Referer);
            result.ReplacedUrl = Pattern.Replace(url, Replacement);

            return result;
        }
    }
}
