using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core.Utility
{
    public static class ProcessUtility
    {
        public const char ArgumentSperator = ' ';

        /// <summary>
        /// 指定したブラウザーでUrlを開きます
        /// </summary>
        /// <param name="url">対象のUrl</param>
        /// <param name="browser">ブラウザーの種類</param>
        public static void OpenUrl(string url, UrlOpenBrowser browser)
        {
            switch (browser)
            {
                case UrlOpenBrowser.Auto:
                    OpenUrl(url, Common.Options.BrowsersOptions.WebBroserOptions.UsesDefaultWebBrowser ?
                        UrlOpenBrowser.DefaultWebBrowser : UrlOpenBrowser.SelectedWebBrowser);
                    break;

                case UrlOpenBrowser.DefaultWebBrowser:
                    Process.Start(url);
                    break;

                case UrlOpenBrowser.SelectedWebBrowser:
                    Process.Start(Common.Options.BrowsersOptions.WebBroserOptions.WebBrowserPath, url);
                    break;

                case UrlOpenBrowser.JaneStyle:
                    Process.Start(Common.Options.BrowsersOptions.JaneOptions.JanePath, url);
                    break;
            }
        }

        /// <summary>
        /// 指定したスレッドを2ch専用ブラウザーで開きます
        /// </summary>
        /// <param name="threads">対象のスレッド</param>
        public static void OpenThreads(Thread[] threads)
        {
            string logFolderPath =
                Common.Options.BrowsersOptions.JaneOptions.GetLogPath();
            bool copyLog = logFolderPath != string.Empty;

            StringBuilder urls = new StringBuilder();
            for (int i = 0; i < threads.Length; i++)
            {
                if (i > 0)
                {
                    urls.Append(ArgumentSperator);
                }
                urls.Append(threads[i].Url);
                if (copyLog)
                {
                    threads[i].CopyLogFile(logFolderPath);
                }
            }
            OpenUrl(urls.ToString(), UrlOpenBrowser.JaneStyle);
        }

        /// <summary>
        /// Urlを開きます
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            OpenUrl(url, Common.Options != null ? UrlOpenBrowser.Auto : UrlOpenBrowser.DefaultWebBrowser);
        }

        /// <summary>
        /// フォルダーを開きます
        /// </summary>
        /// <param name="path">対象のフォルダーパス</param>
        public static void OpenFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageBox.Show(path + "\nフォルダーは存在しません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private const char FileNameSign = '"';
        private const char SeparateSign = ' ';
        private static int FindFileNamePosition(string commandLine)
        {
            //一番初めにみつかった区切り文字の位置を取得（""内はのぞく）
            bool innerFileNameSing = false;
            for (int i = 0; i < commandLine.Length; i++)
            {
                char currentChar = commandLine[i];
                switch (currentChar)
                {
                    case FileNameSign:
                        innerFileNameSing = !innerFileNameSing;
                        break;

                    case SeparateSign:
                        if (!innerFileNameSing)
                        {
                            return i;
                        }
                        break;
                }
            }
            return -1;
        }
        /// <summary>
        /// コマンドラインを指定して，プロセスを起動するのに必要なインスタンスを取得します
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns></returns>
        public static ProcessStartInfo CreateProcessStartInfo(string commandLine)
        {
            int fileNamePosition = FindFileNamePosition(commandLine);
            if (fileNamePosition > 0)
            {
                string fileName = commandLine.Substring(0, fileNamePosition);
                int argumentsStartPosition = fileNamePosition + 1;
                string arguments = commandLine.Substring(argumentsStartPosition, commandLine.Length - argumentsStartPosition);
                return new ProcessStartInfo(fileName, arguments);
            }
            else
            {
                return new ProcessStartInfo(commandLine);
            }
        }
    }

    /// <summary>
    /// Urlを開くのに使用するブラウザーを指定します
    /// </summary>
    public enum UrlOpenBrowser
    {
        /// <summary>
        /// 自動で判別します
        /// </summary>
        Auto,
        /// <summary>
        /// オプションで指定したブラウザーを使用します
        /// </summary>
        SelectedWebBrowser,
        /// <summary>
        /// OS既定のブラウザーを使用します
        /// </summary>
        DefaultWebBrowser,
        /// <summary>
        /// 2ch専用ブラウザー（JaneStyle）を使用します
        /// </summary>
        JaneStyle
    }
}
