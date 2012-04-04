using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace DeanCCCore.Core.VersionUp
{
    /// <summary>
    /// バージョンアップ機能を提供します
    /// </summary>
    public static class VersionUpClient
    {
        public static readonly string NewVersionFolder = Path.Combine(Application.StartupPath, "New");
        public static readonly string NewVersionExeFilePath = Path.Combine(NewVersionFolder, "DeanCC.exe");
        public static readonly string NewVersionCoreFilePath = Path.Combine(NewVersionFolder, "DeanCC.Core.dll");
        public static readonly string[] NewVersionFiles = { NewVersionExeFilePath, NewVersionCoreFilePath };
        //public static readonly string CurrentVersionExeFilePath = Path.Combine(Application.StartupPath, "DeanCC.exe");
        //public static readonly string CurrentVersionCoreFilePath = Path.Combine(Application.StartupPath, "DeanCC.Core.dll");
        //public static readonly string[] CurrentVersionFiles = { CurrentVersionExeFilePath, CurrentVersionCoreFilePath };
        private static readonly string UpdaterPath = Path.Combine(Application.StartupPath, "DeanCCUpdater.exe");
        private const string VersionInfoUrl = "http://sites.google.com/site/deanomega/Version5.xml";
        private static readonly string[] UpdateFileUrls = { "https://sites.google.com/site/deanomega/home/DeanCC.exe.gz",
                                                        "https://sites.google.com/site/deanomega/home/DeanCC.Core.dll.gz" };

        public static event EventHandler<VersionUpEventArgs> CheckedNewVersion;
        public static event EventHandler Downloaded;

        ///// <summary>
        ///// 現在のアセンブリをバックアップします
        ///// </summary>
        //public static void BackUp()
        //{
        //    foreach (string sourceFilePath in CurrentVersionFiles)
        //    {
        //        File.Copy(sourceFilePath, sourceFilePath + Settings.BackUpSuffix, true);
        //    }
        //}

        ///// <summary>
        ///// 新しいアセンブリを適用します
        ///// </summary>
        //public static void Update()
        //{
        //    for (int i = 0; i < NewVersionFiles.Length; i++)
        //    {
        //        MessageBox.Show(NewVersionFiles[i] + "\n" + CurrentVersionFiles[i]);
        //        File.Copy(NewVersionFiles[i], CurrentVersionFiles[i], true);
        //    }
        //}

        /// <summary>
        /// 新しいバージョンがあるかどうかを確認します
        /// </summary>
        /// <returns></returns>
        public static bool CheckNewVersion()
        {
            XmlDocument versionDocument = new XmlDocument();
            VersionUpEventArgs e = new VersionUpEventArgs();
            try
            {
                versionDocument.Load(VersionInfoUrl);
            }
            catch (System.Net.WebException)
            {
                OnCheckedNewVersion(e);
                return false;
            }

            string versionText = versionDocument.DocumentElement.SelectSingleNode("version").InnerText;
            e.Version = versionText;
            bool newVersion = versionText.CompareTo(Application.ProductVersion) > 0;
            if (newVersion)
            {
                e.ExistsNewVersion = true;
                e.ReleseText = versionDocument.DocumentElement.SelectSingleNode("releasetext").InnerText;
            }

            OnCheckedNewVersion(e);
            return newVersion;
        }

        private static void OnCheckedNewVersion(VersionUpEventArgs e)
        {
            if (CheckedNewVersion != null)
            {
                CheckedNewVersion(null, e);
            }
        }

        /// <summary>
        /// 新しいバージョンをダウンロードします
        /// </summary>
        public static void DownloadNewVersion()
        {
            Directory.CreateDirectory(NewVersionFolder);
            int readCount;
            byte[] buffer = new byte[1024];
            for (int i = 0; i < UpdateFileUrls.Length; i++)
            {
                string fileUrl = UpdateFileUrls[i];
                string filePath = NewVersionFiles[i];
                using (HttpWebResponse response = InternetClient.GetResponse(fileUrl))
                using (Stream result = response.GetResponseStream())
                using (GZipStream gzip = new GZipStream(result, CompressionMode.Decompress))
                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    while ((readCount = gzip.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        file.Write(buffer, 0, readCount);
                    }
                }
            }
            OnDownloaded();
        }

        private static void OnDownloaded()
        {
            if (Downloaded != null)
            {
                Downloaded(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// アップデーターを起動します
        /// </summary>
        public static void RunUpdater()
        {
            Process.Start(UpdaterPath);
        }
    }
}
