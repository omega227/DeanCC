using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DeanCCCore.Core.Utility;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class BrowsersOptionsItem
    {
        public BrowsersOptionsItem()
        {
            webBroserOptions = new WebBrowserOptionsItem();
            janeOptions = new JaneOptionsItem();
        }

        private WebBrowserOptionsItem webBroserOptions;
        /// <summary>
        /// ウェブブラウザーに関する設定
        /// </summary>
        public WebBrowserOptionsItem WebBroserOptions
        {
            get
            {
                return webBroserOptions;
            }
            set
            {
                webBroserOptions = value;
            }
        }
        private JaneOptionsItem janeOptions;
        /// <summary>
        /// 2chブラウザーに関する設定
        /// </summary>
        public JaneOptionsItem JaneOptions
        {
            get
            {
                return janeOptions;
            }
            set
            {
                janeOptions = value;
            }
        }

        [Serializable]
        public sealed class WebBrowserOptionsItem
        {
            public WebBrowserOptionsItem()
            {
                WebBrowserPath = string.Empty;
            }
            /// <summary>
            /// ウェブブラウザーの実行パス
            /// </summary>
            public string WebBrowserPath { get; set; }
            /// <summary>
            /// OS既定のウェブブラウザーを使用する
            /// </summary>
            public bool UsesDefaultWebBrowser
            {
                get
                {
                    return !File.Exists(WebBrowserPath);
                }
            }
        }

        [Serializable]
        public sealed class JaneOptionsItem
        {
            public JaneOptionsItem()
            {
                JanePath = string.Empty;
                ImageSaveMode = Options.ImageSaveMode.OnlyImage;
            }
            /// <summary>
            /// Jane2ch.exeのパス
            /// </summary>
            public string JanePath { get; set; }

            /// <summary>
            /// 画像（キャッシュ）を保存するパターン
            /// </summary>
            public ImageSaveMode ImageSaveMode { get; set; }

            /// <summary>
            /// Jane2ch.exeのあるフォルダー
            /// </summary>
            public string JaneFolder
            {
                get
                {
                    if (string.IsNullOrEmpty(JanePath))
                    {
                        return string.Empty;
                    }
                    return Path.GetDirectoryName(JanePath);
                }
                set
                {
                    JanePath = Path.Combine(value, "Jane2ch.exe");
                }
            }

            /// <summary>
            /// 画像を保存するかどうかを示す値
            /// </summary>
            public bool SavableImage
            {
                get
                {
                    return ImageSaveMode == Options.ImageSaveMode.OnlyImage ||
                        ImageSaveMode == Options.ImageSaveMode.BothImageandCache;
                }
            }
            /// <summary>
            /// キャッシュを保存するかどうかを示す値
            /// </summary>
            public bool SavableCache
            {
                get
                {
                    return ImageSaveMode == Options.ImageSaveMode.OnlyCache ||
                        ImageSaveMode == Options.ImageSaveMode.BothImageandCache;
                }
            }

            /// <summary>
            /// ImageViewUrlReplace.datのオプション($EXTRACT等)を有効にするかどうかを示す値
            /// </summary>
            public bool EnableImageViewURLReplacedatOption { get; set; }

            /// <summary>
            /// JaneStyleのImageViewURLReplace.datのパス
            /// </summary>
            public string ImageViewURLRepalcedatPath
            {
                get
                {
                    if (!Directory.Exists(JaneFolder))
                    {
                        return string.Empty;
                    }
                    return Path.Combine(JaneFolder, "ImageViewURLReplace.dat");
                }
            }
            /// <summary>
            /// キャッシュの保存先を取得します
            /// </summary>
            /// <returns></returns>
            public string GetViewCacheFolderPath()
            {
                if (Directory.Exists(JaneFolder))
                {
                    string inipath = Path.Combine(JaneFolder, "ImageView.ini");
                    if (File.Exists(inipath))
                    {
                        // 文字列を読み出す
                        StringBuilder sb = new StringBuilder(1024);
                        string defaultPath = Path.Combine(JaneFolder, "VwCache");
                        NativeMethod.GetPrivateProfileString("Cache", "CachePath",
                            defaultPath, sb, (uint)sb.Capacity, inipath);
                        string cachePath = sb.ToString();

                        return cachePath != string.Empty ? cachePath : defaultPath;
                    }
                }
                return string.Empty;
            }

            /// <summary>
            /// ログの保存先を取得します
            /// </summary>
            /// <returns>通常、Logs\2ch</returns>
            public string GetLogPath()
            {
                if (Directory.Exists(JaneFolder))
                {
                    string inipath = Path.Combine(JaneFolder, "Jane2ch.ini");
                    if (File.Exists(inipath))
                    {
                        // 文字列を読み出す
                        StringBuilder sb = new StringBuilder(1024);
                        string defaultPath = Path.Combine(JaneFolder, @"Logs\2ch");
                        NativeMethod.GetPrivateProfileString("PATH", "LogBasePath",
                            defaultPath, sb, (uint)sb.Capacity, inipath);//lpDefaultが無視される
                        string logPath = sb.ToString();

                        return logPath != string.Empty ? logPath : defaultPath;
                    }
                }
                return string.Empty;
            }

            /// <summary>
            /// JaneSytleのReplaceStr.txtのパス
            /// </summary>
            public string ReplaceStrtxtPath
            {
                get
                {
                    if (!Directory.Exists(JaneFolder))
                    {
                        return string.Empty;
                    }
                    return Path.Combine(JaneFolder, "ReplaceStr.txt");
                }
            }
        }
    }

    /// <summary>
    /// 画像・JaneStyleのキャッシュを保存する方法を指定します
    /// </summary>
    public enum ImageSaveMode
    {
        /// <summary>
        /// 画像のみ
        /// </summary>
        OnlyImage,
        /// <summary>
        /// キャッシュのみ
        /// </summary>
        OnlyCache,
        /// <summary>
        /// 画像とキャッシュ両方
        /// </summary>
        BothImageandCache
    }
}
