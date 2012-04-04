using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using DeanCCCore.Core._2ch.Utility;
using DeanCCCore.Core.Utility;
using Microsoft.VisualBasic.FileIO;

namespace DeanCCCore.Core._2ch
{
    /// <summary>
    /// 2chスレッドの主要情報を提供します
    /// </summary>
    [Serializable]
    public class ThreadHeader : IThreadHeader
    {
        public static string[] FormatNamePairs = 
        {   TitleFormat, "スレッドタイトル",
            CategoryFormat, "カテゴリー", 
            BoardFormat, "板",
            SinceFormat, "スレッド作成日", 
            KeyFormat, "スレッド固有の番号",
            FolderFormat,"画像フォルダー"};

        public const int ResLimit = 1000;
        public const string TitleFormat = "%title%";
        public const string CategoryFormat = "%category%";
        public const string BoardFormat = "%board%";
        public const string KeyFormat = "%key%";
        public const string SinceFormat = "%since%";
        public const string FolderFormat = "%folder%";
        public const string SinceTextFormat = "yyyy/MM/dd";
        public const string HtmlExtension = ".html";
        public const string DatExtension = ".dat";
        public const string DefaultLogsFolderName = "Logs";
        private static readonly HttpStatusCode[] UpdateStatus = { HttpStatusCode.OK, HttpStatusCode.PartialContent };
        private static readonly HttpStatusCode[] LostLogStatus = { HttpStatusCode.Found };
        private static readonly HttpStatusCode[] AboneStatus = { HttpStatusCode.RequestedRangeNotSatisfiable };
        private static readonly HttpStatusCode[] IgnoreStatus = { HttpStatusCode.NotModified };

        public ThreadHeader()
        {
        }

        //[OnDeserialized]
        //void OnDeserialized(StreamingContext sc)
        //{
        //    if (State != ThreadState.Pastlog ||
        //        State != ThreadState.None)
        //    {
        //        State = ThreadState.Normal;
        //    }
        //}
        [field: NonSerialized]
        public event EventHandler<ThreadUpdateEventArgs> Updating;
        [field: NonSerialized]
        public event EventHandler<ThreadUpdateEventArgs> Updated;
        [field: NonSerialized]
        public event EventHandler Lost;
        [field: NonSerialized]
        public event EventHandler ABoned;
        [NonSerialized]
        private bool repaired;
        public bool Repaired
        {
            get { return repaired; }
        }

        public ThreadHeader(IBoardInfo source, string key, string title)
            : this()
        {
            //Parent = parent;
            SourceBoard = source;
            url = ThreadUrlFormatter.FormatUrl(source.Server, source.Path, key);
            Title = title;
            Key = key;
            Since = ThreadUtility.CalculateSinceTime(key);
        }

        public override bool Equals(object obj)
        {
            return obj is ThreadHeader && this.url.Equals(((ThreadHeader)obj).Url);
        }
        public override int GetHashCode()
        {
            return url.GetHashCode();
        }

        public IBoardInfo SourceBoard
        {
            get;
            private set;
        }
        public DateTime Since
        {
            get;
            set;
        }
        public DateTime LastModified
        {
            get;
            set;
        }
        public string DatUrl
        {
            get { return ThreadUrlFormatter.FormatDatUrl(url); }
        }
        public string Bg20DatUrl
        {
            get { return ThreadUrlFormatter.FormatBg20ServerDatUrl(url); }
        }
        public string ETag
        {
            get;
            set;
        }
        public int GotByteCount
        {
            get;
            set;
        }
        public int GotResCount
        {
            get;
            set;
        }
        public bool IsLimitOverThread
        {
            get
            {
                return GotResCount > ResLimit;
            }
        }
        public string Key
        {
            get;
            private set;
        }
        public string Title
        {
            get;
            private set;
        }
        public int NewResCount
        {
            get;
            set;
        }
        private bool isPastlog;
        public bool IsPastlog
        {
            get { return isPastlog; }
            set
            {
                if (isPastlog != value)
                {
                    isPastlog = value;
                    if (isPastlog)
                    {
                        OnLost(EventArgs.Empty);
                    }
                }
            }
        }
        public bool IsIgnored
        {
            get;
            set;
        }

        public ThreadState State
        {
            get;
            set;
        }

        private string url;
        public string Url
        {
            get { return url; }
        }

        public float ResSpeed
        {
            get;
            private set;
        }

        public bool IsMark
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public static bool ContainsFormat(string target)
        {
            return target.Contains(TitleFormat) ||
                  target.Contains(CategoryFormat) ||
                  target.Contains(BoardFormat) ||
                  target.Contains(KeyFormat) ||
                  target.Contains(SinceFormat);
        }

        /// <summary>
        /// 新着レスを取得します
        /// </summary>
        /// <exception cref="System.Net.WebException">データの取得に失敗しました</exception>
        /// <exception cref="System.IO.IOExeception">DATの保存に失敗しました</exception>
        /// <returns>レスを表す文字列</returns>
        public string Update()
        {
            if (Parent == null)
            {
                throw new InvalidOperationException("巡回設定が指定されていません");
            }

            ThreadUpdateEventArgs e = new ThreadUpdateEventArgs();
            OnUpdating(e);
            if (e.Cancel)
            {
                OnUpdated(e);
                return string.Empty;
            }

            string newReses = string.Empty;
            bool gotData = GotByteCount > 0;
            HttpWebRequest request = InternetClient.Create(
                (!gotData && Common.Options.DatOptions.GetatableBg20Server) ? Bg20DatUrl : DatUrl,
                gotData ? LastModified : DateTime.MinValue, isgzip: !gotData, range: GotByteCount,
                timeout: Common.Options.InternetOptions.Timeout, eTag: ETag, allowRedirect: false);
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (UpdateStatus.Contains(response.StatusCode))
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(responseStream,
                            Common.Options.InternetOptions.CurrentEncoding))
                        {
                            newReses = reader.ReadToEnd();
                        }
                        e.Updated = true;
                        e.Response = response;
                        e.NewReses = newReses;
                        OnUpdated(e);
                        return newReses;
                    }
                    else if (LostLogStatus.Contains(response.StatusCode))
                    {
                        OnLost(EventArgs.Empty);
                        return string.Empty;
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
                    if (AboneStatus.Contains(errorResponse.StatusCode))
                    {
                        OnABoned(EventArgs.Empty);
                    }
                    else if (!IgnoreStatus.Contains(errorResponse.StatusCode))
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }

            OnUpdated(e);
            return newReses;
        }

        protected virtual void OnUpdating(ThreadUpdateEventArgs e)
        {
            if (Updating != null)
            {
                Updating(this, e);
            }
            State = e.Cancel ? ThreadState.NotUpdate : ThreadState.Updating;
        }

        protected virtual void OnUpdated(ThreadUpdateEventArgs e)
        {
            if (Updated != null)
            {
                Updated(this, e);
            }

            if (e.Updated)
            {
                int newResesByteCount = System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.NewReses);
                GotByteCount += newResesByteCount;
                Common.CurrentSettings.Information.CurrentDownloadByte += newResesByteCount;
                NewResCount = e.NewReses.Count(c => c.Equals('\n'));
                GotResCount += NewResCount;
                LastModified = e.Response.LastModified;
                ETag = e.Response.Headers["ETag"];
                ResSpeed = ThreadUtility.CalculateResSpeed(Since, GotResCount);
                if (Common.Options.DatOptions.LogSaveMode == Options.LogSaveModes.DatOnly ||
                    Common.Options.DatOptions.LogSaveMode == Options.LogSaveModes.BothDatAndHtml)
                {
                    SaveLog(e.NewReses);
                }
                if (!repaired)
                {
                    repaired = true;
                }
                State = ThreadState.Updated;
                InternetClient.PerformResponseRecieved(new InternetClientEventArgs(newResesByteCount, 0));
                Common.ConnectionLimiter.Wait(SourceBoard.Server);
            }
            else
            {
                State = ThreadState.NotUpdate;
            }
        }

        protected virtual void OnLost(EventArgs e)
        {
            if (Lost != null)
            {
                Lost(this, e);
            }
            isPastlog = true;
            State = ThreadState.Pastlog;
        }

        protected virtual void OnABoned(EventArgs e)
        {
            if (ABoned != null)
            {
                ABoned(this, e);
            }
            ClearStatus();
            State = ThreadState.ABone;
        }

        public void ClearStatus()
        {
            GotByteCount = 0;
            GotResCount = 0;
            if (Directory.Exists(LogSaveFolder))
            {
                File.Delete(Path.Combine(LogSaveFolder, LogFileName));
            }
            State = ThreadState.None;
        }

        public bool CreativeDirectory { get; set; }

        public void SaveLog(string dat)
        {
            if (!Directory.Exists(LogSaveFolder))
            {
                Directory.CreateDirectory(LogSaveFolder);
                CreativeDirectory = true;
            }
            string filePath = Path.Combine(LogSaveFolder, LogFileName);
            using (StreamWriter writer =
                new StreamWriter(filePath, true, Common.Options.InternetOptions.CurrentEncoding))
            {
                writer.Write(dat);
            }
        }

        public string Format(string text)
        {
            string replacedText = text.Replace(TitleFormat, Title);
            replacedText = replacedText.Replace(CategoryFormat,
                Common.CurrentSettings.Boards.FindFromBoardInfo(SourceBoard).Name);
            replacedText = replacedText.Replace(BoardFormat, SourceBoard.Name);
            replacedText = replacedText.Replace(KeyFormat, Key);
            replacedText = replacedText.Replace(SinceFormat, Since.ToString(SinceTextFormat));
            replacedText = replacedText.Replace(FolderFormat, imageSaveFolder != null ? imageSaveFolder : string.Empty);

            return replacedText;
        }

        private string logSaveFolder;
        public string LogSaveFolder
        {
            get
            {
                if (string.IsNullOrEmpty(logSaveFolder))
                {
                    logSaveFolder = Common.Options.DatOptions.SavesSameImagesFolder ?
                        ImageSaveFolder :
                        Path.Combine(Common.Options.DatOptions.DefaultSaveFolder, DefaultLogsFolderName,
                        Common.CurrentSettings.Boards.FindFromBoardInfo(SourceBoard).Name, SourceBoard.Name);
                }
                return logSaveFolder;
            }
        }

        public string SubFolderName
        {
            get
            {
                if (!Parent.CreatesSubFolder)
                {
                    throw new InvalidOperationException("サブフォルダーは作成されません");
                }
                return Path.GetFileName(ImageSaveFolder);//getを呼び出して初期化
            }
        }

        private string imageSaveFolder;
        public string ImageSaveFolder
        {
            get
            {
                if (string.IsNullOrEmpty(imageSaveFolder))
                {
                    imageSaveFolder = Parent.CreatesSubFolder ?
                        Path.Combine(Parent.ParentFolder.LocalPath, FileNameFormat.EscapeFileName(Format(Parent.SubFolderFormat))) :
                        Parent.ParentFolder.LocalPath;
                }
                return imageSaveFolder;
            }
            set
            {
                if (imageSaveFolder != value)
                {
                    string oldSaveFolder = imageSaveFolder;
                    imageSaveFolder = value;
                    OnImageSaveFolderChanged(oldSaveFolder);
                }
            }
        }

        private void OnImageSaveFolderChanged(string oldImageSaveFolder)
        {
            if (Common.Options.DatOptions.SavesSameImagesFolder)
            {
                logSaveFolder = imageSaveFolder;
            }
            Directory.CreateDirectory(imageSaveFolder);
            FileSystem.MoveDirectory(oldImageSaveFolder, imageSaveFolder, true);
            Common.Logs.Add("画像フォルダー移動", imageSaveFolder, LogStatus.Normal);
        }

        private string logFileName;
        public string LogFileName
        {
            get
            {
                if (string.IsNullOrEmpty(logFileName))
                {
                    logFileName = FileNameFormat.EscapeFileName(Format(Common.Options.DatOptions.FileNameFormat)) + DatExtension;
                }
                return logFileName;
            }
        }

        private string htmlLogFileName;
        public string HtmlLogFileName
        {
            get
            {
                if (string.IsNullOrEmpty(htmlLogFileName))
                {
                    htmlLogFileName = FileNameFormat.EscapeFileName(Format(Common.Options.DatOptions.FileNameFormat)) + HtmlExtension;
                }
                return htmlLogFileName;
            }
        }

        public int DownloadedCount
        {
            get;
            set;
        }
        public IPatrolPattern Parent
        {
            get;
            set;
        }
    }
}
