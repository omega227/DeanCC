using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Jane
{
    /// <summary>
    /// JaneのImageViewURLReplace.datに対応するクラス
    /// </summary>
    /// <remarks>
    /// cookieには未対応 Twintailのソースを参考にした
    /// http://www.geocities.jp/nullpo0/
    /// </remarks>     
    public sealed class ImageViewURLReplace : IImageViewURLReplace
    {
        private static readonly Regex CommentPattern = new Regex(@"^(;|'|//)");
        private const string InvalidReferer = "$EXTRACT";

        public ImageViewURLReplace()
        {
        }

        public ImageViewURLReplace(string path)
            : this()
        {
            this.path = path;
        }

        public ImageViewURLReplaceItem Replace(string url)
        {
            ImageViewURLReplaceItem result = new ImageViewURLReplaceItem(url);
            foreach (ImageViewUrlItem item in items)
            {
                if (item.Regex.IsMatch(url))
                {
                    result.Referer = item.Regex.Replace(url, item.Referer);
                    result.ReplacedUrl = item.Regex.Replace(url, item.Replacement);
                    //cookieには未対応

                    break;//どれかにマッチしたら置換を終了
                }
            }

            return result;
        }

        public bool IsMatch(string url)
        {
            foreach (ImageViewUrlItem item in items)
            {
                if (item.Regex.IsMatch(url))
                {
                    return true;
                }
            }
            return false;
        }

        private bool loaded;
        public bool Loaded { get { return loaded; } }

        private string path;
        public string Path
        {
            get
            {
                return path;
            }
        }

        private List<ImageViewUrlItem> items = new List<ImageViewUrlItem>();

        /// <summary>
        /// ImageViewURLReplace.dat を .NET の Regex用正規表現に直す
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private string CorrectRegex(string pattern)
        {
            pattern = Regex.Replace(pattern, @"$\d", @"$\{${1}}");
            pattern = Regex.Replace(pattern, "$&", @"$\{0}");

            return pattern;
        }

        public void Load()
        {
            Load(path);
        }

        private void Load(string path)
        {
            if (loaded)
            {
                throw new InvalidOperationException("This file is already loaded");
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("path");
            }

            this.path = path;
            // sample
            //  元のURL(正規表現) タブ文字(\t) 置換先URL(正規表現) タブ文字(\t) 置換先URLに渡すリファラー
            // "http://www.sage.com/\thttp://www.age.com/\thttp://www.age.com/index.html"

            string text = string.Empty;
            using (StreamReader sr = new StreamReader(path, Common.Options.InternetOptions.CurrentEncoding))
            {
                text = sr.ReadToEnd();
            }

            List<ImageViewUrlItem> list = new List<ImageViewUrlItem>();
            foreach (string line in Regex.Split(text, "\r\n|\r|\n"))
            {
                if (!CommentPattern.IsMatch(line))
                {
                    string[] elements = line.Split('\t');
                    if (elements.Length >= 2)
                    {
                        string key = CorrectRegex(elements[0]);
                        string repl = CorrectRegex(elements[1]);
                        string refe = elements.Length >= 3 ? CorrectRegex(elements[2]) : string.Empty;
                        try
                        {
                            if (!refe.Contains(InvalidReferer))
                            {
                                list.Add(new ImageViewUrlItem(key, repl, refe));
                            }
                        }
                        catch (ArgumentException)
                        {
                            continue;
                        }
                    }
                }
            }

            items.Clear();
            items.AddRange(list);

            OnLoaded();
        }

        private void OnLoaded()
        {
            loaded = true;
            Common.Logs.Add("ImageViewURLReplace.dat読み込み完了",
                string.Format("{0:N0}パターン", items.Count), LogStatus.System);
        }

        public void Reload()
        {
            loaded = false;
            Load(path);
        }
    }

    public sealed class ImageViewUrlItem
    {
        private Regex regex;
        /// <summary>
        /// このインスタンスが表す正規表現
        /// </summary>
        public Regex Regex
        {
            get
            {
                return regex;
            }
        }

        private string replacement;
        /// <summary>
        /// 置換文字
        /// </summary>
        public string Replacement
        {
            get
            {
                return replacement;
            }
        }

        private string referer;
        /// <summary>
        /// リファラ
        /// </summary>
        public string Referer
        {
            get
            {
                return referer;
            }
        }

        public ImageViewUrlItem(string key, string replacement, string referer)
        {
            this.regex = new Regex(key, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            this.replacement = replacement;
            this.referer = referer;
            RegexTest();
        }

        private void RegexTest()
        {
            regex.Replace("", replacement);
            regex.Replace("", referer);
        }
    }
}
