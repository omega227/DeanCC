using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DeanCCCore.Core;
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
        private static readonly Regex OptionRefererPattern = new Regex(@"([^=]+)(.+)?");
        //private const string InvalidReferer = "$EXTRACT";

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
            foreach (ImageViewUrlItem item in items)
            {
                if (item.Pattern.IsMatch(url))
                {
                    return item.Replace(url);//どれかにマッチしたら置換を終了
                }
            }

            return new ImageViewURLReplaceItem(url);
        }

        public bool IsMatch(string url)
        {
            foreach (ImageViewUrlItem item in items)
            {
                if (item.Pattern.IsMatch(url))
                {
                    return true;
                }
            }
            return false;
        }

        private bool loaded;
        public bool Loaded { get { return loaded; } }
        public bool EnableOption
        {
            get;
            private set;
        }

        private string path;
        public string Path
        {
            get
            {
                return path;
            }
        }

        private List<ImageViewUrlItem> items = new List<ImageViewUrlItem>();

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
                        string key = elements[0];
                        string repl = elements[1];
                        string refe = elements.Length >= 3 ? elements[2] : string.Empty;
                        try
                        {
                            string option = elements.Length >= 4 ? elements[3] : string.Empty;
                            Match optionMatch = OptionRefererPattern.Match(option);
                            string mode = optionMatch.Groups[1].Value;
                            ImageViewUrlItem item = null;
                            if (mode == "$EXTRACT" &&
                                Common.Options.BrowsersOptions.JaneOptions.EnableImageViewURLReplacedatOption)
                            {
                                string extractPattern = elements[4];
                                string optionRefefer = optionMatch.Groups[2].Value;
                                item = new ImageViewUrlExtractItem(key, repl, refe, optionRefefer, extractPattern);
                            }
                            else if (mode == "$COOKIE" &&
                                Common.Options.BrowsersOptions.JaneOptions.EnableImageViewURLReplacedatOption)
                            {
                                string optionRefefer = optionMatch.Groups[2].Value;
                                item = new ImageViewUrlCookieItem(key, repl, refe, optionRefefer);
                            }
                            else
                            {
                                item = new ImageViewUrlItem(key, repl, refe);
                            }
                            list.Add(item);
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
            EnableOption = items.Any(item => item.WithOption);
            Common.Logs.Add("ImageViewURLReplace.dat読み込み完了",
                string.Format("{0:N0}パターン", items.Count), LogStatus.System);
        }

        public void Reload()
        {
            loaded = false;
            Load(path);
        }
    }
}
