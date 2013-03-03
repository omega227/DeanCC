using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using DeanCCCore.Core._2ch;
using DeanCCCore.Core._2ch.Jane;
using DeanCCCore.Core._2ch.Utility;
using DeanCCCore.Core.Utility;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 画像情報を表します
    /// </summary>
    [Serializable]
    public class ImageHeader : IImageHeader
    {
        private const int MaximumRenameableLength = 5;
        private static readonly HttpStatusCode[] ImpossibleDownloadStatuses = { HttpStatusCode.Forbidden, HttpStatusCode.NotFound };        

        [field: NonSerialized]
        public event EventHandler<ImageHeaderEventArgs> Downloading;
        [field: NonSerialized]
        public event EventHandler<ImageHeaderEventArgs> Downloaded;
        [field: NonSerialized]
        public event EventHandler<ImageHeaderEventArgs> ResponseHeaderReseived;

        public const string ResNumberFormat = "%res%";
        public const string FileNameFormat = "%file%";
        public static string[] FormatNamePairs = 
        {   ResNumberFormat, "レス番号",
            FileNameFormat, "ファイル名"};

        private const string DefaultFileName = "image";
        private static readonly string[] AllowContentTypeTexts = { "image", "application/zip" };

        public ImageHeader()
        {
        }

        public ImageHeader(int sourceResIndex, string url)
            : this()
        {
            SourceResIndex = sourceResIndex;
            originalUrl = url;
        }


        [DisplayName("状態")]
        public string StateText
        {
            get { return ImageHeaderStateString.GetString(state); }
        }
        [DisplayName("結果")]
        public string ResultText
        {
            get { return ImageHeaderStateString.GetString(DownloadResult); }
        }
        private string originalUrl;
        [DisplayName("URL")]
        public string OriginalUrl
        {
            get
            {
                return originalUrl;
            }
            protected set
            {
                originalUrl = value;
            }
        }

        private int triedCount;
        [DisplayName("ダウンロード試行回数")]
        public int TriedCount
        {
            get
            {
                return triedCount;
            }
        }

        private DateTime firstDownloadedTime;
        [DisplayName("初回ダウンロード日時")]
        public DateTime FirstDownloadTime
        {
            get
            {
                return firstDownloadedTime;
            }
        }

        private string savedPath;
        [DisplayName("保存場所")]
        public string SavedPath
        {
            get
            {
                return savedPath;
            }
        }

        [NonSerialized]
        private string sourceThreadTitle;
        [DisplayName("スレッド")]
        public string SourceThreadTitle
        {
            get { return sourceThreadTitle; }
            set { sourceThreadTitle = value; }
        }

        private bool downloadCompleted;
        [Browsable(false)]
        public bool DownloadCompleted
        {
            get
            {
                return downloadCompleted;
            }
        }

        private bool downloadable = true;
        [Browsable(false)]
        public bool Downloadable
        {
            get
            {
                return downloadable;
            }
            protected set
            {
                downloadable = value;
            }
        }

        [Browsable(false)]
        public virtual bool IsZip
        {
            get { return false; }
        }

        [Browsable(false)]
        public int SourceResIndex
        {
            get;
            protected set;
        }

        private string fileName;

        private string md5Hash;
        [Browsable(false)]
        public string MD5Hash
        {
            get
            {
                return md5Hash;
            }
        }

        private ImageState state;
        [Browsable(false)]
        public ImageState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        [Browsable(false)]
        public ImageDownloadResultStatus DownloadResult
        {
            get;
            set;
        }

        [Browsable(false)]
        public bool Secure
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is ImageHeader && this.originalUrl.Equals(((ImageHeader)obj).OriginalUrl);
        }

        public override int GetHashCode()
        {
            return originalUrl.GetHashCode();
        }

        public virtual ImageDownloadResult Download()
        {
            ImageDownloadResult result = null;
            if (Common.ImageViewURLReplacer != null)
            {
                try
                {
                    ImageViewURLReplaceItem item = Common.ImageViewURLReplacer.Replace(originalUrl);
                    result = Download(item.ReplacedUrl, item.Referer, item.Cookie);
                }
                catch (UriFormatException)
                {
                    result = Download(originalUrl);
                }
            }
            else
            {
                result = Download(originalUrl);
            }

            DownloadResult = result.Status;
            return result;
        }

        protected virtual ImageDownloadResult Download(string url, string referer = "", CookieContainer cookie = null)
        {
            if (downloadCompleted)
            {
                throw new InvalidOperationException("This file is already downloaded");
            }
            if (!downloadable)
            {
                throw new InvalidOperationException("This file can not download");
            }

            ImageHeaderEventArgs e = new ImageHeaderEventArgs(url);
            try
            {
                OnDownloading(e);
                if (e.Cancel)
                {
                    OnDownloaded(e);
                    return new ImageDownloadResult(e.Status);
                }

                e.TriedDownload = true;
                using (HttpWebResponse response = InternetClient.GetResponse(e.Url, referer, cookie))
                {
                    e.ResponseHeader = response;
                    OnResponseHeaderReceived(e);
                    if (e.Cancel)
                    {
                        OnDownloaded(e);
                        return new ImageDownloadResult(e.Status);
                    }

                    using (Stream result = response.GetResponseStream())
                    {
                        byte[] data = StreamUtility.ReadBytes(result);
                        e.DownloadedData = data;
                        OnResponseStreamReceived(e);
                        if (e.Cancel)
                        {
                            OnDownloaded(e);
                            return new ImageDownloadResult(e.Status);
                        }

                        e.Downloaded = true;
                        OnDownloaded(e);
                        return new ImageDownloadResult(data, e.Url, referer, ResponseUtility.TryLastModified(response), response.ContentType);
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response == null)
                    {
                        throw;
                    }
                    HttpWebResponse errorResponse = (HttpWebResponse)ex.Response;
                    if (ImpossibleDownloadStatuses.Contains(errorResponse.StatusCode))
                    {
                        downloadable = false;
                        e.Downloaded = false;
                    }
                    else
                    {
                        throw;
                    }
                }
                else//プロトコルエラー以外の例外
                {
                    //ダウンロード可能（リトライ）
                    downloadable = true;
                    e.Downloaded = false;
                }
            }

            OnDownloaded(e);
            return new ImageDownloadResult(e.Status);
        }

        protected virtual void OnDownloading(ImageHeaderEventArgs e)
        {
            if (triedCount > Common.Options.ImageSaveOptions.MaximumRetryCount)
            {
                e.Status = ImageDownloadResultStatus.OverTriedCount;
                e.Cancel = true;
                downloadable = false;
                return;
            }
            else if (triedCount > 0 &&
                DateTime.Now - firstDownloadedTime > TimeSpan.FromDays(Common.Options.ImageSaveOptions.RetryImageLifeDate))
            {
                e.Status = ImageDownloadResultStatus.OverLimitDate;
                e.Cancel = true;
                downloadable = false;
                return;
            }

            if (!Uri.IsWellFormedUriString(e.Url, UriKind.Absolute))
            {
                e.Status = ImageDownloadResultStatus.InvalidUriFormat;
                e.Cancel = true;
                downloadable = false;
                return;
            }

            if (Downloading != null)
            {
                Downloading(this, e);
            }

            //if (!e.Cancel)
            //{
            //    Common.UploaderLimiter.Wait(e.Host);
            //    e.Locked = true;
            //}
        }

        protected virtual void OnDownloaded(ImageHeaderEventArgs e)
        {
            //if (e.Locked)
            //{
            //    Common.UploaderLimiter.Release(e.Host);
            //    e.Locked = false;
            //}
            if (e.TriedDownload)
            {
                triedCount++;
            }

            if (e.Downloaded)
            {
                firstDownloadedTime = DateTime.Now;
                // ハッシュ値を計算
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                md5Hash = BitConverter.ToString(md5.ComputeHash(e.DownloadedData)).Replace("-", "").ToLower();
                downloadCompleted = true;
                downloadable = false;
                state = ImageState.DownloadComplete;
            }
            else
            {
                state = downloadable ? ImageState.DownloadPause : ImageState.DownloadFailed;
            }

            if (Downloaded != null)
            {
                Downloaded(this, e);
            }
        }

        protected virtual void OnResponseStreamReceived(ImageHeaderEventArgs e)
        {
            if (e.ResponseHeader.ContentLength > 0)
            {
                Common.CurrentSettings.Information.CurrentDownloadByte += e.ResponseHeader.ContentLength;
                if (e.DownloadedData.LongLength != e.ResponseHeader.ContentLength)
                {
                    e.Status = ImageDownloadResultStatus.ContentLengthMissMatch;
                    e.Cancel = true;
                    return;
                }
            }
        }

        protected virtual void OnResponseHeaderReceived(ImageHeaderEventArgs e)
        {
            if (AllowContentTypeTexts.All(type => !e.ResponseHeader.ContentType.Contains(type)))
            {
                e.Status = ImageDownloadResultStatus.ContentTypeMissMatch;
                e.Cancel = true;
                return;
            }

            try
            {
                string defaultFileName = ResponseUtility.GetDefaultFileName(e.ResponseHeader);
                string invalidableFileName = string.IsNullOrEmpty(defaultFileName) ?
                    Path.GetFileName(e.ResponseHeader.ResponseUri.AbsolutePath) : defaultFileName;
                string safeFileName = Utility.FileNameFormat.EscapeFileName(invalidableFileName);
                string extension = Utility.FileNameFormat.GetImageExteinsion(safeFileName);
                if (extension == string.Empty)
                {
                    extension = Path.GetExtension(e.ResponseHeader.ResponseUri.AbsolutePath);
                    fileName = safeFileName + extension;
                }
                else
                {
                    fileName = safeFileName;
                }
            }
            catch (ArgumentException)
            {
                fileName = DefaultFileName;
            }

            if (ResponseHeaderReseived != null)
            {
                ResponseHeaderReseived(this, e);
            }
        }

        protected virtual string GetDownloadUrl(string cushionPage, Uri uri, string keyword = "")
        {
            throw new NotImplementedException();
        }

        protected virtual string GetSecureDownloadUrl(string cushionPageData, Uri pageUri)
        {
            Res res = null;
            Thread thread = null;
            bool containsResFormat = false;
            bool containsThreadFormat = false;
            foreach (string keyword in Common.Options.ZipOptions.Keywords)
            {
                string formatedKeyword = null;
                if (!containsResFormat)
                {
                    containsResFormat = Res.ContainsFormat(keyword);
                }
                if (!containsThreadFormat)
                {
                    containsThreadFormat = ThreadHeader.ContainsFormat(keyword);
                }
                if (containsResFormat)
                {
                    try
                    {
                        if (thread == null)
                        {
                            thread = ThreadUtility.GetThreadFromImageUrl(OriginalUrl);
                        }
                        if (res == null)
                        {
                            res = new Res(thread.LoadReplacedLog(SourceResIndex));
                        }
                        formatedKeyword = res.Format(keyword);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Common.Logs.Add("パスワード適用エラー",
                            "パスワード:" + keyword + "を適用できませんでした。" + ex.Message, LogStatus.Error);
                        continue;
                    }
                }
                if (containsThreadFormat)
                {
                    if (thread == null)
                    {
                        thread = ThreadUtility.GetThreadFromImageUrl(OriginalUrl);
                    }
                    formatedKeyword = thread.Header.Format(formatedKeyword ?? keyword);
                }

                string downloadUrl = GetDownloadUrl(cushionPageData, pageUri, formatedKeyword ?? keyword);
                if (!string.IsNullOrEmpty(downloadUrl))
                {
                    return downloadUrl;
                }
            }
            return string.Empty;
        }

        public virtual void Save(byte[] data, string saveFolder , string fileNameFormat)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new InvalidOperationException("fileName is not initialized");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            Directory.CreateDirectory(saveFolder);
            string formatedFileName = Format(fileNameFormat);
            string safeFileName = Utility.FileNameFormat.EscapeFileName(formatedFileName);
            string savePath = Path.Combine(saveFolder, safeFileName);
            if (File.Exists(savePath))
            {
                if (safeFileName.Length > MaximumRenameableLength)
                {
                    return;
                }
                savePath = Utility.FileNameFormat.Rename(savePath);
            }
            using (FileStream fs = new FileStream(savePath, FileMode.CreateNew))
            {
                fs.Write(data, 0, data.Length);                
            }
            savedPath = savePath;
        }

        public void ResetState()
        {
            downloadCompleted = false;
            downloadable = true;
            state = ImageState.Non;
            triedCount = 0;
            firstDownloadedTime = DateTime.MinValue;
        }


        public string Format(string text)
        {
            if (text == FileNameFormat)
            {
                return fileName;
            }

            string extension = Utility.FileNameFormat.GetImageExteinsion(fileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            string replacedText = text.Replace(ResNumberFormat, (SourceResIndex+1).ToString());
            replacedText = replacedText.Replace(FileNameFormat, fileNameWithoutExtension);

            string formatedFileName = replacedText + extension;

            return formatedFileName;
        }
    }
}
