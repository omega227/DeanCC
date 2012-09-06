using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using DeanCCCore.Core._2ch.Utility;
//using System.Windows.Forms;

namespace DeanCCCore.Core._2ch
{
    /// <summary>
    /// 2chスレッドを提供します
    /// </summary>
    [Serializable]
    public class Thread : IThread
    {
        //
        //ToDo:DownloadCompletedThreadクラスを作成
        //ダウンロード完了したら、別のクラスに変えるようにする
        private const int WaitInterval = 1000;
        private const double WaitTimeout = 15 * 1000;
        private static readonly string SecureImageLogSaveFolder = Path.Combine(Settings.SaveFolder, "Htmls");
        private const string SecureImageLogFileNameFormat = "{0}.image.html";
        private static readonly HttpStatusCode[] ImpossibleDownloadStatuses = { HttpStatusCode.ServiceUnavailable };
        private const int DownloadInterval = 1500;

        public static event EventHandler<ImageDownloadEventArgs> Downloading;
        public static event EventHandler<ImageDownloadEventArgs> Downloaded;
        public static event EventHandler<ImagePassEventArgs> ImagePassRequired;

        [field: NonSerialized]
        public event EventHandler<ThreadEventArgs> Running;
        [field: NonSerialized]
        public event EventHandler<ThreadEventArgs> Ran;
        [field: NonSerialized]
        public event EventHandler<ImageSaveEventArgs> ImageSaving;
        [field: NonSerialized]
        public event EventHandler<ImageSaveEventArgs> ImageSaved;
        [field: NonSerialized]
        public event EventHandler<ThreadEventArgs> Updating;
        [field: NonSerialized]
        public event EventHandler<ThreadEventArgs> Updated;

        /// <summary>
        /// 逆シリアル化の直後に発生します。このメソッドはvirtualにできません
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
            //デシリアライズされた直後はプログレスを100%に設定
            totalDownloadedImageCount = totalDownloadingImageCount = 1;
            //if (maybeImageHeaders == null)
            //{
            //    maybeImageHeaders = new ImageHeaderCollection();
            //}
        }

        public Thread()
        {
        }

        public Thread(IThreadHeader header)
            : this()
        {
            this.header = header;
        }

        public override bool Equals(object obj)
        {
            return obj is IThread && header.Url.Equals(((IThread)obj).Header.Url);
        }

        public override int GetHashCode()
        {
            return header.Url.GetHashCode();
        }

        private IThreadHeader header;
        [Browsable(false)]
        public IThreadHeader Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        /// <summary>
        /// ダウンロードが完了しているか
        /// </summary>
        [Browsable(false)]
        public bool DownloadCompleted
        {
            get
            {
                return imageHeaders == null;
            }
        }
        /// <summary>
        /// ダウンロード完了後に初期化される総画像数
        /// </summary>
        private int allImageCount;
        /// <summary>
        /// ダウンロード完了後に初期化される総ZIP数
        /// </summary>
        private int allZipCount;

        [Browsable(false)]
        public bool IsMark
        {
            get { return header.IsMark; }
        }

        /// <summary>
        /// 一時停止中かどうかを示します
        /// </summary>
        [Browsable(false)]
        public bool DownloadPaused
        {
            get
            {
                if (imageHeaders == null)
                {
                    return false;
                }
                return imageHeaders.Count < Common.Options.ImageSaveOptions.Threshold;
            }
        }

        /// <summary>
        /// ダウンロード可能かどうかを示します
        /// </summary>
        [Browsable(false)]
        public bool Downloadable
        {
            get
            {
                return !DownloadCompleted && !header.IsIgnored && !DownloadPaused;
            }
        }

        private ImageHeaderCollection imageHeaders = new ImageHeaderCollection();
        [Browsable(false)]
        public ImageHeaderCollection ImageHeaders
        {
            get { return imageHeaders; }
        }

        //private ImageHeaderCollection maybeImageHeaders = new ImageHeaderCollection();
        //[Browsable(false)]
        //public ImageHeaderCollection MaybeImageHeaders
        //{
        //    get { return maybeImageHeaders; }
        //}

        private DateTime lastImageModified;

        /// <summary>
        /// ダウンロード対象の累計
        /// </summary>
        [NonSerialized]
        private int totalDownloadingImageCount;
        /// <summary>
        /// ダウンロード実行済みの累計
        /// ダウンロード成功とは限らない
        /// </summary>
        [NonSerialized]
        private int totalDownloadedImageCount;

        [NonSerialized]
        private bool cancellationDownload;

        [NonSerialized]
        private bool running;

        private static readonly object syncSaving = new object();

        private IEnumerable<IImageHeader> GetDownloadImages()
        {
            //if (Common.Options.BrowsersOptions.JaneOptions.EnableImageViewURLReplacedatOption)
            //{
            //    List<IImageHeader> allImageHeader = imageHeaders.ToList();
            //    allImageHeader.AddRange(maybeImageHeaders);
            //    return allImageHeader.Where(image => !image.DownloadCompleted && image.Downloadable);
            //}
            //else
            //{
                return imageHeaders.Where(image => !image.DownloadCompleted && image.Downloadable);
            //}
        }

        /// <summary>
        /// メインプロセスを開始します
        /// </summary>
        /// <exception cref="System.IO.IOException">画像の保存中にエラーが発生しました</exception>
        /// <exception cref="System.Net.WebException">アップローダーへのアクセス中にエラーが発生しました</exception>
        /// <exception cref="System.InvalidOperationException">ダウンロード中にエラーが発生しました</exception>
        public void Run()
        {
            ThreadEventArgs e = new ThreadEventArgs();
            OnRunning(e);
            if (e.Cancel)
            {
                OnRan(e);
                return;
            }

            IEnumerable<IImageHeader> downloadImages = GetDownloadImages();
            totalDownloadingImageCount = downloadImages.Count();
            try
            {
                System.Threading.Tasks.Parallel.ForEach(Uploader.Split(downloadImages), (uploader, loopState) =>
                {
                    foreach (IImageHeader image in uploader.ImageHeaders)
                    {
                        if (cancellationDownload)
                        {
                            loopState.Break();
                            return;
                        }
                        try
                        {
                            ImageDownloadResult result = null;
                            lock (uploader.SyncRoot)
                            {
                                result = Download(image);
                            }
                            Save(result, image);
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
                                    return;
                                }
                            }
                        }
                        totalDownloadedImageCount++;
                    }
                });
            }
            catch (AggregateException ex)
            {
                OnRan(e);
                ex.Handle((innerException) =>
                {
                    if (innerException is IOException)
                    {
                        Common.Logs.Add("保存エラー", innerException.Message, LogStatus.Error);
                        //System.Windows.Forms.MessageBox.Show(ex.Message + "\nダウンロードを中断しました。", "保存失敗",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                    else if (innerException is WebException)
                    {
                        Common.Logs.Add("通信エラー", innerException.Message, LogStatus.Error);
                        //MessageBox.Show(ex.Message + "\nダウンロードを中断しました。", "ダウンロード失敗",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                    return false;
                });
                //if (ex.InnerException is IOException || ex.InnerException is WebException)
                //{
                //    throw ex.InnerException;
                //}
                //else
                //{
                //    throw new InvalidOperationException("ダウンロード中にエラーが発生しました。\r\n" + header.Url, ex.InnerException);
                //}
            }
            OnRan(e);
        }

        protected virtual void OnRunning(ThreadEventArgs e)
        {
            if (Running != null)
            {
                Running(this, e);
            }
            running = true;
            header.State = ThreadState.Downloading;
            if (header.IsIgnored)
            {
                e.Cancel = true;
            }
        }

        protected virtual void OnRan(ThreadEventArgs e)
        {
            if (running)
            {
                cancellationDownload = false;
                header.State = ThreadState.DownloadComplete;
                if ((header.IsPastlog || header.IsLimitOverThread) && imageHeaders.DownloadCompleted)
                {
                    OnDownloadCompleted();
                }
                running = false;
                //lock (Settings.SyncRoot)
                //{
                //    Common.CurrentSettings.Save();
                //}
                //lock (((System.Collections.ICollection)Common.DownloadedImageHashes).SyncRoot)
                //{
                //    Common.DownloadedImageHashes.Save();
                //}

                if (Ran != null)
                {
                    Ran(this, e);
                }
            }
        }

        protected virtual void OnDownloadCompleted()
        {
            if (Common.Options.DatOptions.LogSaveMode == Options.LogSaveModes.BothDatAndHtml &&
                File.Exists(LogFilePath))
            {
                SaveLogHtml();
            }

            bool ignoreThread = DownloadedCount <= 0 &&
                Common.Options.DatOptions.SavesSameImagesFolder && header.CreativeDirectory;
            if (ignoreThread && Directory.Exists(header.ImageSaveFolder))
            {
                Directory.Delete(header.ImageSaveFolder, true);
            }
            else
            {
                Common.CurrentSettings.Information.TotalDownloadCompletedThreadCount++;
                bool moveImageSaveFolder = Common.Options.ImageSaveOptions.MovesSaveFolder &&
                    Path.IsPathRooted(Common.Options.ImageSaveOptions.MovedDestinationFolder) &&
                    header.Parent.CreatesSubFolder &&
                    Directory.Exists(header.ImageSaveFolder);
                if (moveImageSaveFolder)
                {
                    string destinationSubFolderPath =
                        Path.Combine(Common.Options.ImageSaveOptions.MovedDestinationFolder, header.SubFolderName);
                    header.ImageSaveFolder = destinationSubFolderPath;
                }
            }

            allImageCount = ImageCount;
            allZipCount = ZipCount;
            imageHeaders = null;

            IEnumerable<Command> commands =
                Common.Options.CommandOptions.CommandList.Where(command => command.CommandMode == CommandMode.DownloadCompleted);
            foreach (Command command in commands)
            {
                command.Execute(this);
                System.Threading.Thread.Sleep(500);
            }
        }

        private ImageDownloadResult Download(IImageHeader destination)
        {
            ImageDownloadEventArgs e = new ImageDownloadEventArgs(destination);
            OnDownloading(e);
            if (e.Cancel)
            {
                return new ImageDownloadResult(ImageDownloadResultStatus.None);
            }

            ImageDownloadResult result = destination.Download();
            e.Result = result;

            OnDownloaded(e);
            return result;
        }

        private void Save(ImageDownloadResult result, IImageHeader destination)
        {
            ImageSaveEventArgs e = new ImageSaveEventArgs(result, destination);
            OnImageSaving(e);
            if (e.Cancel)
            {
                return;
            }

            if (Common.Options.BrowsersOptions.JaneOptions.SavableImage)
            {
                string saveFolder = destination.IsZip && !Common.Options.ZipOptions.SavesSameImagesFolder ?
                    Common.Options.ZipOptions.DefaultSaveFolder : header.ImageSaveFolder;
                string fileNameFormat = Common.Options.ImageSaveOptions.FileNameFormat;
                fileNameFormat = header.Format(fileNameFormat);
                destination.Save(result.Data, saveFolder, fileNameFormat);
            }
            if (Common.Options.BrowsersOptions.JaneOptions.SavableCache && Common.ViewCacher != null)
            {
                Common.ViewCacher.Save(
                    result.Data, result.ContentType, result.LastModified, result.Url, result.Referer, header.Url);
            }
            OnImageSaved(e);
        }

        /// <summary>
        /// 画像を保存する直前に呼び出されます
        /// </summary>
        protected virtual void OnImageSaving(ImageSaveEventArgs e)
        {
            if (e.DownloadResult.Status != ImageDownloadResultStatus.Success)
            {
                e.Cancel = true;
                return;
            }
            lock (((System.Collections.ICollection)Common.DownloadedImageHashes).SyncRoot)
            {
                if (Common.Options.ImageSaveOptions.BlockDownloadedImage &&
                    Common.DownloadedImageHashes.Any(hash =>
                        hash.MD5Hash.Equals(e.ImageHeader.MD5Hash, StringComparison.CurrentCultureIgnoreCase)))
                {
                    e.ImageHeader.State = ImageState.RepeatedDownload;
                    e.Cancel = true;
                    return;
                }
            }
            if (Common.NGFiles != null && Common.NGFiles.Exists(e.ImageHeader.MD5Hash))
            {
                e.ImageHeader.State = ImageState.NGFile;
                e.Cancel = true;
                return;
            }

            if (ImageSaving != null)
            {
                ImageSaving(this, e);
            }
        }

        /// <summary>
        /// 画像を保存した直後に呼び出されます
        /// 保存しなかった場合は呼び出されません
        /// </summary>
        protected virtual void OnImageSaved(ImageSaveEventArgs e)
        {
            lock (((System.Collections.ICollection)Common.DownloadedImageHashes).SyncRoot)
            {
                Common.DownloadedImageHashes.Add(new ImageHash(e.ImageHeader.MD5Hash, e.ImageHeader.SavedPath));
            }
            header.DownloadedCount++;
            lastImageModified = DateTime.Now;
            Common.CurrentSettings.Information.TotalSavedImageCount++;
            if (Common.Options.ImageSaveOptions.ApplyOriginalTimestamp)
            {
                File.SetLastAccessTime(e.ImageHeader.SavedPath, e.DownloadResult.LastModified);
            }
            if (ImageSaved != null)
            {
                ImageSaved(this, e);
            }
        }

        protected virtual void OnDownloading(ImageDownloadEventArgs e)
        {
            if (!e.Image.Downloadable || e.Image.DownloadCompleted)
            {
                e.Cancel = true;
            }
            if (Downloading != null)
            {
                Downloading(null, e);
            }
        }

        protected virtual void OnDownloaded(ImageDownloadEventArgs e)
        {
            if (Downloaded != null)
            {
                Downloaded(null, e);
            }
            if (e.Result.Data != null)
            {
                InternetClient.PerformResponseRecieved(new InternetClientEventArgs(e.Result.Data.LongLength, 0));
            }
            switch (e.Result.Status)
            {
                case ImageDownloadResultStatus.BlockedImagePass:
                    OnImagePassRequired(new ImagePassEventArgs(this, (ImageHeader)e.Image));
                    break;
            }
            System.Threading.Thread.Sleep(DownloadInterval);
        }

        protected virtual void OnImagePassRequired(ImagePassEventArgs e)
        {
            if (ImagePassRequired != null)
            {
                ImagePassRequired(null, e);
            }
        }

        public bool Update()
        {
            ThreadEventArgs e = new ThreadEventArgs();
            OnUpdating(e);
            if (e.Cancel)
            {
                //OnUpdated(e);
                return false;
            }

            string newReses = header.Update();
            bool updated = !string.IsNullOrEmpty(newReses);
            if (updated)
            {
                string replacedNewReses = Common.ReplaceStr != null ? Common.ReplaceStr.Replace(newReses) : newReses;
                int startResIndex = header.GotResCount - header.NewResCount;
                ThreadUtility.ParseHeaderResult result = ThreadUtility.ParseHeader(startResIndex, replacedNewReses, header.Parent.ExtensionFormat);
                foreach (ImageHeader newImage in result.ImageHeaders)
                {
                    if (!imageHeaders.Contains(newImage) &&
                        Common.Options.NGOptions.NGUrls.All(ngUrl => !newImage.OriginalUrl.Contains(ngUrl)))
                    {
                        imageHeaders.Add(newImage);
                    }
                }
                if (Common.Options.BrowsersOptions.JaneOptions.EnableImageViewURLReplacedatOption)
                {
                    foreach (MaybeImageHeader newUrl in result.MaybeImageHeaders)
                    {
                        if (!imageHeaders.Contains(newUrl))
                        {
                            imageHeaders.Add(newUrl);
                        }
                        //if (!maybeImageHeaders.Contains(newUrl))
                        //{
                        //    maybeImageHeaders.Add(newUrl);
                        //}
                    }
                }
            }

            OnUpdated(e);
            return updated;
        }

        protected virtual void OnUpdating(ThreadEventArgs e)
        {
            if (Updating != null)
            {
                Updating(this, e);
            }
            if (header.IsPastlog || header.IsLimitOverThread || header.IsIgnored)
            {
                e.Cancel = true;
            }
            if (Settings.Crushed && !header.Repaired)
            {
                header.ClearStatus();
            }
        }

        protected virtual void OnUpdated(ThreadEventArgs e)
        {
            if (Updated != null)
            {
                Updated(this, e);
            }
            totalDownloadedImageCount = 0;//プログレスを0%に設定
        }

        public string LogFilePath
        {
            get
            {
                return Path.Combine(header.LogSaveFolder, header.LogFileName);
            }
        }

        public string HtmlFilePath
        {
            get
            {
                return Path.Combine(header.LogSaveFolder, header.HtmlLogFileName);
            }
        }

        /// <summary>
        /// 指定したフォルダーにDATファイルをコピーします。
        /// 既にDATファイルが存在する場合は、サイズが大きいほうを適用します。
        /// </summary>
        /// <param name="logFolder">コピー先のフォルダー</param>
        public void CopyLogFile(string logFolder)
        {
            string saveFolder = Path.Combine(logFolder,
                Common.CurrentSettings.Boards.FindFromBoardInfo(header.SourceBoard).Name);
            string savePath = Path.Combine(saveFolder,
                header.SourceBoard.Name, header.Key + ThreadHeader.DatExtension);
            long destinationSize = 0;
            if (File.Exists(savePath))
            {
                destinationSize = new FileInfo(savePath).Length;
            }

            if (header.GotByteCount > destinationSize)
            {
                Directory.CreateDirectory(saveFolder);
                string sourceLogPath = LogFilePath;
                File.Copy(sourceLogPath, savePath, true);
            }
        }

        /// <summary>
        /// パス付画像のレスHTMLファイルを保存します
        /// </summary>
        /// <returns>Htmlファイルパス</returns>
        public string SaveSecureImageLogHtml()
        {
            if (DownloadCompleted)
            {
                throw new InvalidOperationException("ダウンロード完了後はHTMLを保存できません");
            }
            if (imageHeaders.All(image => !image.Secure))
            {
                throw new InvalidOperationException("パス付き画像を含んでいません");
            }

            string dat = LoadReplacedLog();
            string html = HtmlUtility.CreateSecureImageLogHtml(dat, header, imageHeaders);
            string htmlFileName = string.Format(SecureImageLogFileNameFormat, header.Key);
            string htmlFilePath = Path.Combine(SecureImageLogSaveFolder, htmlFileName);
            Directory.CreateDirectory(SecureImageLogSaveFolder);
            using (StreamWriter writer = new StreamWriter(htmlFilePath, false,
                Common.Options.InternetOptions.CurrentEncoding))
            {
                writer.Write(html);
            }

            return htmlFilePath;
        }

        /// <summary>
        /// ローカルに保存されているDatファイルを利用してHtmlファイルを保存します
        /// 既にHtmlファイルが存在する場合は上書きします
        /// </summary>
        /// <returns>Htmlファイルパス</returns>
        public string SaveLogHtml()
        {
            if (DownloadCompleted)
            {
                throw new InvalidOperationException("ダウンロード完了後はHTMLを保存できません");
            }

            string dat = LoadReplacedLog();
            string html = HtmlUtility.Create(dat, header, imageHeaders);
            string htmlFilePath = Path.Combine(header.LogSaveFolder, header.HtmlLogFileName);
            Directory.CreateDirectory(header.LogSaveFolder);
            using (StreamWriter writer = new StreamWriter(htmlFilePath, false,
                Common.Options.InternetOptions.CurrentEncoding))
            {
                writer.Write(html);
            }

            return htmlFilePath;
        }

        private string LoadReplacedLog()
        {
            string dat = string.Empty;
            using (StreamReader reader = new StreamReader(LogFilePath,
                Common.Options.InternetOptions.CurrentEncoding))
            {
                dat = reader.ReadToEnd();
            }
            int datSize = System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(dat);
            if (datSize < header.GotByteCount)
            {
                throw new InvalidOperationException(LogFilePath + " ログデータが欠けています");
            }
            string replacedDat = Common.ReplaceStr != null ? Common.ReplaceStr.Replace(dat) : dat;
            return replacedDat;
        }

        public string LoadReplacedLog(int index)
        {
            string[] logLines = LoadReplacedLog().Split('\n');
            if (logLines.Length < index)
            {
                throw new InvalidOperationException("ログデータが欠けています");
            }

            return logLines[index];
        }

        /// <summary>
        /// 現在実行中のダウンロードを強制中止します
        /// </summary>
        public void StopDownload()
        {
            if (running)
            {
                CancelDownload();
                OnRan(new ThreadEventArgs());
            }
        }

        /// <summary>
        /// 現在実行中のダウンロードを中断します
        /// </summary>
        public void CancelDownload()
        {
            if (running)
            {
                cancellationDownload = true;
                header.State = ThreadState.CancelDownload;
            }
        }

        /// <summary>
        /// 保存フォルダをゴミ箱へ送ります
        /// </summary>
        public void DeleteThreadFolder()
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(
                header.ImageSaveFolder,
                Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
        }

        public void WaitCnacellation()
        {
            WaitCnacellation(WaitTimeout);
        }

        public void WaitCnacellation(double timeout)
        {
            DateTime limit = DateTime.Now.Add(TimeSpan.FromMilliseconds(timeout));
            while (running)
            {
                if (DateTime.Now > limit)
                {
                    throw new TimeoutException(timeout + "ミリ秒以内にダウンロードスレッドが終了しませんでした。");
                }
                System.Threading.Thread.Sleep(WaitInterval);
            }
        }

        #region リスト表示用プロパティ群

        [DisplayName("設定")]
        public string ParentName
        {
            get { return header.Parent.Name; }
        }
        [DisplayName("タイトル")]
        public string Title
        {
            get { return header.Title; }
        }
        [DisplayName("状態")]
        public string StateText
        {
            get { return ThreadStateString.GetText(header.State); }
        }
        [DisplayName("レス")]
        public int GotResCount
        {
            get { return header.GotResCount; }
        }
        [DisplayName("完了")]
        [DisplayDataGridViewColumnType(DataGridViewColumnType.ProgressBar)]
        public float ProgressDownload
        {
            get
            {
                if (header.State == ThreadState.None || header.State == ThreadState.Updated || DownloadPaused)
                {
                    return 0;
                }
                else if (totalDownloadingImageCount <= 0 || header.State == ThreadState.DownloadComplete)
                {
                    return 100;
                }
                else
                {
                    float progress = (float)Math.Round((double)totalDownloadedImageCount / totalDownloadingImageCount * 100, 1);
                    if (running && progress >= 100)
                    {
                        return 99;
                    }
                    else
                    {
                        return progress;
                    }
                }
            }
        }
        [DisplayName("画像")]
        public int ImageCount
        {
            get
            {
                return DownloadCompleted ? allImageCount : imageHeaders.Count;
            }
        }
        [DisplayName("DL")]
        public int DownloadedCount
        {
            get { return header.DownloadedCount; }
        }
        [DisplayName("ZIP")]
        public int ZipCount
        {
            get { return DownloadCompleted ? allZipCount : imageHeaders.ZipCount; }
        }
        [DisplayName("勢い")]
        public float ResSpeed
        {
            get { return header.ResSpeed; }
        }
        [DisplayName("Since")]
        public DateTime Since
        {
            get { return header.Since; }
        }
        [DisplayName("最終更新")]
        public DateTime LastModified
        {
            get { return header.LastModified; }
        }
        [DisplayName("画像更新")]
        public DateTime LastImageModified
        {
            get { return lastImageModified; }
        }
        [DisplayName("板")]
        public string SourceBoardName
        {
            get { return header.SourceBoard.Name; }
        }
        [DisplayName("URL")]
        public string Url
        {
            get { return header.Url; }
        }

        #endregion
    }
}
