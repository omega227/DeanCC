using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Jane
{
    public sealed class ReplaceStr : IReplaceStr
    {
        public ReplaceStr(string path)
        {
            this.path = path;
        }

        private string path;
        private bool loaded;
        public bool Loaded { get { return loaded; } }
        private List<ReplaceStrItem> items = new List<ReplaceStrItem>();
        private Regex urlPattern = new Regex(@"t\??tps?\??://");

        public string Replace(string dat)
        {
            string replacedDat = dat;
            foreach (ReplaceStrItem replacer in items)
            {
                replacedDat = replacer.Regex.Replace(replacedDat, replacer.Replacement);
            }

            return replacedDat;
        }

        public void Load()
        {
            Load(path);
        }

        public void Load(string path)
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

            string text = string.Empty;
            using (StreamReader sr =
                new StreamReader(path, Common.Options.InternetOptions.CurrentEncoding))
            {
                text = sr.ReadToEnd();
            }

            List<ReplaceStrItem> list = new List<ReplaceStrItem>();
            foreach (Match matchItem in Regex.Matches(
                text, @"^\<\w*(?<ignore>\d*)\>(?<key>.+)\t(?<replacement>.+)\t", RegexOptions.Multiline))
            {
                string key = matchItem.Groups["key"].Value;
                string replacement = matchItem.Groups["replacement"].Value;
                if (urlPattern.IsMatch(key) || urlPattern.IsMatch(replacement))//Urlに影響のあるものだけ追加
                {
                    bool ignore = !matchItem.Groups["ignore"].Value.Equals("2");
                    try
                    {
                        ReplaceStrItem item = new ReplaceStrItem(key, replacement, ignore);
                        list.Add(item);
                    }
                    catch (ArgumentException)
                    {
                        continue;
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
            Common.Logs.Add("ReplaceStr.txt読み込み完了",
                string.Format("{0:N0}パターン", items.Count), LogStatus.System);
        }

        public void Reload()
        {
            loaded = false;
            Load(path);
        }

        public sealed class ReplaceStrItem
        {
            public ReplaceStrItem(string key, string replacement, bool ignore)
            {
                this.regex = new Regex(key, ignore ? RegexOptions.IgnoreCase | RegexOptions.Compiled : RegexOptions.Compiled);
                this.replacement = replacement;
                RegexTest();
            }

            private void RegexTest()
            {
                regex.Replace("", replacement);
            }

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
        }
    }
}
