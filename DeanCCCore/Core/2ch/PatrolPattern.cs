using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DeanCCCore.Core._2ch.Utility;
using System.Runtime.Serialization;

namespace DeanCCCore.Core._2ch
{
    /// <summary>
    /// 巡回パターンを示します
    /// 巡回メソッドを実行するためにはInitialize()で初期化する必要があります
    /// </summary>
    [Serializable]
    public class PatrolPattern : IPatrolPattern
    {
        public const string DefaultNameFormat = "NoName{0}";
        [field: NonSerialized]
        public event EventHandler PatternChanged;
        [field: NonSerialized]
        public event EventHandler ExtentionChanged;
        [field: NonSerialized]
        public event EventHandler EnableChanged;

        /// <summary>
        /// 逆シリアル化の直後に発生します。このメソッドはvirtualにできません
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
            if (targetBoardInfo != null)
            {
                targetBoards = new BoardInfoCollection();
                targetBoards.Add(targetBoardInfo);
                targetBoardInfo = null;
            }
        }

        public PatrolPattern()
        {
        }

        public PatrolPattern(GenreFolder parentFolder)
            : this()
        {
            this.parentFolder = parentFolder;
        }

        public override string ToString()
        {
            return name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is IPatrolPattern && name.Equals(((IPatrolPattern)obj).Name);
        }

        void UpdatePatternRegex()
        {
            wordPattern = CreatePatternRegex(pattern, ngPattern, isIgnorePattern);
        }

        public static Regex CreatePatternRegex(string pattern, string ngPattern, bool isIgnorePattern)
        {
            Regex matchtmpBaseKeyword = new Regex(@"\|$");
            Regex tmpMatchBaseKeyword = new Regex(@"[^|｜]+");
            Regex matchBaseKeyword = new Regex(@"\S+");
            StringBuilder sb = new StringBuilder();
            StringBuilder tmpsb = new StringBuilder();
            CreateNGPattern(sb, ngPattern);
            foreach (Match Productkeyword in matchBaseKeyword.Matches(pattern))
            {
                foreach (Match Sumkeyword in tmpMatchBaseKeyword.Matches(Productkeyword.Value))
                {
                    tmpsb.AppendFormat("(?=.*{0})|", Sumkeyword.Value);
                }
                if (tmpsb.Length != 0)//スペースで区切って"|"だけキャプチャした場合null
                {
                    sb.AppendFormat("({0})", matchtmpBaseKeyword.Replace(tmpsb.ToString(), ""));
                    tmpsb = new StringBuilder();
                }
            }
            return CreatePatternRegex(sb.ToString(), isIgnorePattern);
        }

        public static void CreateNGPattern(StringBuilder sb, string ngPattern)
        {
            Regex matchBaseKeyword = new Regex(@"\S+");
            if (!string.IsNullOrWhiteSpace(ngPattern))
            {
                foreach (Match ngkeyword in matchBaseKeyword.Matches(ngPattern))
                {
                    sb.AppendFormat("(?!.*{0})", ngkeyword.Value);
                }
            }
        }

        public static Regex CreatePatternRegex(string pattern, bool isIgnorePattern)
        {
            string keyword = String.Format("^{0}.*$", pattern);
            return new Regex(keyword, isIgnorePattern ?
                (RegexOptions.Multiline | RegexOptions.IgnoreCase) : (RegexOptions.Multiline));
        }

        protected string CreateExtensionFormat()
        {
            return CreateExtensionFormat(enableJpg, enablePng, enableGif, enableBmp, enableZip);
        }

        private static string CreateExtensionFormat(bool jpg, bool png, bool gif, bool bmp, bool zip)
        {
            StringBuilder sb = new StringBuilder();
            if (jpg)
            {
                sb.Append("jpe?g|");
            }
            if (png)
            {
                sb.Append("png|");
            }
            if (gif)
            {
                sb.Append("gif|");
            }
            if (bmp)
            {
                sb.Append("bmp|");
            }
            if (zip)
            {
                sb.Append("zip|");
            }

            return string.Format("({0})", sb.ToString().TrimEnd('|'));
        }

        private bool initialized;
        public void Initialize(BoardInfoCollection targetBoards, string pattern, string ngPattern, //bool isIgnorePattern,
            bool jpg, bool png, bool gif, bool bmp, bool zip, string name, string subFolderFormat)
        {
            //if (initialized)編集のときプロパティを効率よく書き換えるのに使用される
            //{
            //    return;
            //}
            if (targetBoards == null)
            {
                throw new ArgumentNullException("targetBoard");
            }
            //if (targetBoard == BoardInfo.Empty)
            //{
            //    throw new ArgumentException("板情報を空にできません。");
            //}
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException("キーワードの項目を空にできません。");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("名前の項目を空にできません。");
            }
            if (!jpg && !png && !gif && !bmp && !zip)
            {
                throw new ArgumentException("ダウンロードする拡張子を選択してください。");
            }

            TargetBoards = targetBoards;

            Pattern = pattern;
            NGPattern = ngPattern;
            //IsIgnorePattern = isIgnorePattern;
            OnPatternChanged(EventArgs.Empty);

            enableJpg = jpg;
            enablePng = png;
            enableGif = gif;
            enableBmp = bmp;
            enableZip = zip;
            OnExtensionEnableChanged(EventArgs.Empty);

            this.name = name;
            SubFolderFormat = subFolderFormat;
            initialized = true;
        }
        /// <summary>
        /// キーワードとNGワードを合わせた正規表現
        /// </summary>
        private Regex wordPattern;

        private bool enable = true;
        public bool Enable
        {
            get
            {
                return enable && Initialized;
            }
            set
            {
                if (enable != value)
                {
                    bool beforeEnable = Enable;
                    enable = value;
                    if (beforeEnable != Enable)
                    {
                        EnableChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        private BoardInfo targetBoardInfo;
        private BoardInfoCollection targetBoards = new BoardInfoCollection();
        public virtual BoardInfoCollection TargetBoards
        {
            get { return targetBoards; }
            set { targetBoards = value; }
        }
        //public virtual BoardInfo TargetBoardInfo
        //{
        //    get { return targetBoardInfo; }
        //    set { targetBoardInfo = value; }
        //}

        private bool isIgnorePattern;
        public bool IsIgnorePattern
        {
            get { return isIgnorePattern; }
            set
            {
                if (isIgnorePattern != value)
                {
                    isIgnorePattern = value;
                    OnPatternChanged(EventArgs.Empty);
                }
            }
        }

        public string ExtensionFormat
        {
            get;
            set;
        }

        private string name = string.Format(DefaultNameFormat, 1);

        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string pattern;
        public string Pattern
        {
            get { return pattern; }
            set
            {
                pattern = value;
                OnPatternChanged(EventArgs.Empty);
            }
        }

        private void OnPatternChanged(EventArgs e)
        {
            if (PatternChanged != null)
            {
                PatternChanged(this, e);
            }
            //if (initialized)
            //{
            UpdatePatternRegex();
            //}
        }

        private string ngPattern;
        public string NGPattern
        {
            get { return ngPattern; }
            set
            {
                ngPattern = value;
                OnPatternChanged(EventArgs.Empty);
            }
        }

        private string subFolderFormat = ThreadHeader.TitleFormat;
        public string SubFolderFormat
        {
            get { return subFolderFormat; }
            set { subFolderFormat = value; }
        }


        //public virtual IThreadHeader[] Patrol()
        //{
        //    if (!initialized)
        //    {
        //        throw new InvalidOperationException("Not Initialized");
        //    }

        //    List<IThreadHeader> result = new List<IThreadHeader>();
        //    IThreadHeader[] allHeaders = ThreadParser.Parse(this, TargetBoardInfo, TargetBoardInfo.Read());
        //    foreach (IThreadHeader header in allHeaders)
        //    {
        //        if (IsMatch(header))
        //        {
        //            result.Add(header);
        //        }
        //    }

        //    return result.ToArray();
        //}

        public bool IsMatch(IThreadHeader input)
        {
            bool matchTitle = wordPattern.IsMatch(input.Title) && Common.GlobalNGPatternRegex.IsMatch(input.Title);//GlobalNGでなければIsMatchはtureを返す
            if (Common.Options.StartupOptions.RemoveExpirationThread)
            {
                bool spentLifeTime = (DateTime.Now - input.Since) > TimeSpan.FromDays(Common.Options.StartupOptions.ThreadLifeDate);
                return matchTitle && !spentLifeTime;
            }
            else
            {
                return matchTitle;
            }
        }

        protected virtual void OnExtensionEnableChanged(EventArgs e)
        {
            ExtensionFormat = CreateExtensionFormat();
            if (ExtentionChanged != null)
            {
                ExtentionChanged(this, e);
            }
        }

        private bool enableJpg = true;
        public bool EnableJpg
        {
            get { return enableJpg; }
            set
            {
                if (enableJpg != value)
                {
                    enableJpg = value;
                    OnExtensionEnableChanged(EventArgs.Empty);
                }
            }
        }

        private bool enablePng = true;
        public bool EnablePng
        {
            get { return enablePng; }
            set
            {
                if (enablePng != value)
                {
                    enablePng = value;
                    OnExtensionEnableChanged(EventArgs.Empty);
                }
            }
        }

        private bool enableGif = true;
        public bool EnableGif
        {
            get { return enableGif; }
            set
            {
                if (enableGif != value)
                {
                    enableGif = value;
                    OnExtensionEnableChanged(EventArgs.Empty);
                }
            }
        }

        private bool enableBmp = true;
        public bool EnableBmp
        {
            get { return enableBmp; }
            set
            {
                if (enableBmp != value)
                {
                    enableBmp = value;
                    OnExtensionEnableChanged(EventArgs.Empty);
                }
            }
        }

        private bool enableZip = false;
        public bool EnableZip
        {
            get { return enableZip; }
            set
            {
                if (enableZip != value)
                {
                    enableZip = value;
                    OnExtensionEnableChanged(EventArgs.Empty);
                }
            }
        }

        public bool CreatesSubFolder
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SubFolderFormat);
            }
        }

        public virtual bool Initialized
        {
            get { return initialized; }
        }

        private GenreFolder parentFolder;
        public GenreFolder ParentFolder
        {
            get { return parentFolder; }
            set
            {
                if (value != null && !value.Equals(parentFolder))
                {
                    parentFolder = value;
                    OnParentFolderChanged(EventArgs.Empty);
                }
            }
        }

        private void OnParentFolderChanged(EventArgs e)
        {
            //ToDo:フォルダー変更時の処理
        }

        //public string SaveFolderByFormat
        //{
        //    get
        //    {
        //        return CreatesSubFolder ?
        //            Path.Combine(parentFolder.LocalPath, SubFolderFormat) : parentFolder.LocalPath;
        //    }
        //}
    }
}
