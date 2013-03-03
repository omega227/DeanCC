using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DeanCCCore.Core._2ch;
using DeanCCCore.Core._2ch.Jane;
using DeanCCCore.Core.Options;

namespace DeanCCCore.Core
{
    /// <summary>
    /// このアプリケーションの主要なプロパティやメソッド等を提供します
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 初期設定が完了したときに発生します
        /// </summary>
        public static event EventHandler Initialized;

        /// <summary>
        /// 初期設定の直前に発生します
        /// </summary>
        public static event EventHandler Initializing;

        ///// <summary>
        ///// 同時ダウンロード数
        ///// </summary>
        //public const int DownloadCapacity = 5;

        private const string simpleTitleFormat = "Dean CC {0}";
        //private const string titleFormat = "送信：{0,4:N0} KB/s 受信：{1,4:N0} KB/s - Dean CC {2}";
        private const string TrimedTailVersionText = ".0";

        ///// <summary>
        ///// ベータ版かどうかを示す値を取得します
        ///// </summary>
        //public const bool IsBeta = false;

        /// <summary>
        /// 公式ページのURL
        /// </summary>
        public const string OfficialHPDocumentPageUrl = "https://sites.google.com/site/deanomega/docs";

        private const string MutexName = "DeanCCMutex";
        /// <summary>
        /// DeanCCの同期オブジェクトを表します。
        /// </summary>
        public static readonly System.Threading.Mutex Mutex = new System.Threading.Mutex(false, MutexName);

        ///// <summary>
        ///// フル タイトルテキストを取得します
        ///// </summary>
        ///// <returns></returns>
        //public static string GetTitle()
        //{
        //    return string.Format(titleFormat,
        //        networkCounter.SentKiloBytePerSecond, networkCounter.ReceiveKiloBytePerSecond, Version);
        //}
        /// <summary>
        /// バージョンのみのタイトルテキストを取得します
        /// </summary>
        /// <returns></returns>
        public static string GetSimpleTitle()
        {
            return string.Format(simpleTitleFormat, VersionText);
        }

        private static bool initialized;

        /// <summary>
        /// 巡回パターンの共通NGワードにマッチする正規表現
        /// </summary>
        public static Regex GlobalNGPatternRegex { get; set; }

        #region スレッドリスト
        //
        //ToDo: ここにリストを追加したら、UpdateThreads()にも処理を加える
        //
        private static BindingThreadCollection enableThreads = new BindingThreadCollection();
        /// <summary>
        /// 除外されていないすべてのスレッド
        /// </summary>
        public static BindingThreadCollection EnableThreads
        {
            get
            {
                return enableThreads;
            }
        }

        private static QuickDownloadingThreadCollection quickDownloadingThreads = new QuickDownloadingThreadCollection();
        /// <summary>
        /// 高頻度ダウンロード中スレッドリスト
        /// </summary>
        public static QuickDownloadingThreadCollection QuickDownloadingThreads
        {
            get
            {
                return quickDownloadingThreads;
            }
        }

        private static DownloadingThreadCollection downloadingThreads = new DownloadingThreadCollection();
        /// <summary>
        /// ダウンロード中スレッドリスト
        /// </summary>
        public static DownloadingThreadCollection DownloadingThreads
        {
            get
            {
                return downloadingThreads;
            }
        }

        private static DownloadPausedThreadCollection downloadPausedThreads = new DownloadPausedThreadCollection();
        /// <summary>
        /// ダウンロード一時停止中のスレッドリスト
        /// </summary>
        public static DownloadPausedThreadCollection DownloadPausedThreads
        {
            get
            {
                return downloadPausedThreads;
            }
        }

        private static DownloadedThreadCollection downloadedThreads = new DownloadedThreadCollection();
        /// <summary>
        /// ダウンロード完了(dat落ち)スレッドリスト
        /// </summary>
        public static DownloadedThreadCollection DownloadedThreads
        {
            get
            {
                return downloadedThreads;
            }
        }

        private static ExcludedThreadCollection excludedThreads = new ExcludedThreadCollection();
        /// <summary>
        /// 除外されたスレッドリスト
        /// </summary>
        public static ExcludedThreadCollection ExcludedThreads
        {
            get
            {
                return excludedThreads;
            }
        }

        private static SecureThreadCollection secureThreads = new SecureThreadCollection();
        /// <summary>
        /// パス付き画像スレッドリスト
        /// </summary>
        public static SecureThreadCollection SecureThreads
        {
            get
            {
                return secureThreads;
            }
        }

        #endregion

        private static ImageViewURLReplace imageViewURLRepalcer;
        /// <summary>
        /// JaneのImageViewURLReplace.datに対応するインスタンス
        /// </summary>
        public static IImageViewURLReplace ImageViewURLReplacer
        {
            get
            {
                return imageViewURLRepalcer;
            }
        }

        private static NGFiles ngFiles;
        /// <summary>
        /// JaneのNGFiles.txtに対応するインスタンス
        /// </summary>
        public static INGFiles NGFiles
        {
            get
            {
                return ngFiles;
            }
        }

        private static ViewCache viewCacher;
        /// <summary>
        /// JaneのVwCacheに対応するインスタンス
        /// </summary>
        public static ViewCache ViewCacher
        {
            get
            {
                return viewCacher;
            }
        }

        public static ReplaceStr replaceStr;
        /// <summary>
        /// JaneのReplaceStr.txtに対応するインスタンス
        /// </summary>
        public static IReplaceStr ReplaceStr
        {
            get
            {
                return replaceStr;
            }
        }

        /// <summary>
        /// 初回起動時巡回設定の説明を表示したかどうか
        /// </summary>
        public static bool ShowsFirstPatrolPatternEditDescription
        {
            get;
            set;
        }

        /// <summary>
        /// 初回起動時オプションの説明を表示したかどうか
        /// </summary>
        public static bool ShowsFirstOptionDescription
        {
            get;
            set;
        }

        private static ConnectionLimited connectionLimiter;
        /// <summary>
        /// 2chへの接続制限機能を提供します
        /// </summary>
        public static ConnectionLimited ConnectionLimiter
        {
            get
            {
                return connectionLimiter;
            }
        }

        //private static UploaderConnectionLimited uploaderLimiter = new UploaderConnectionLimited();
        ///// <summary>
        ///// 画像アップローダーへの接続制限機能を提供します
        ///// </summary>
        //public static UploaderConnectionLimited UploaderLimiter
        //{
        //    get
        //    {
        //        return uploaderLimiter;
        //    }
        //}

        private static LogItemCollection logs;
        /// <summary>
        /// このアプリケーションの動作ログを保持するコレクション
        /// </summary>
        public static LogItemCollection Logs
        {
            get
            {
                if (logs == null)
                {
                    logs = new LogItemCollection();
                }
                return logs;
            }
        }

        //private static NetworkPerformanceCounter networkCounter = new NetworkPerformanceCounter();
        ///// <summary>
        ///// アプリケーションの通信速度を提供します
        ///// </summary>
        //public static NetworkPerformanceCounter NetworkCounter
        //{
        //    get
        //    {
        //        return networkCounter;
        //    }
        //}

        //private static InternetClientCounter networkCounter = new InternetClientCounter();
        ///// <summary>
        ///// アプリケーションの通信速度を提供します
        ///// </summary>
        //public static InternetClientCounter NetworkCounter
        //{
        //    get
        //    {
        //        return networkCounter;
        //    }
        //}

        //private static bool loadBackup;
        //private static bool crush;
        //public static bool Crush
        //{
        //    get { return crush; }
        //}

        /// <summary>
        /// CoreおよびCurrentSettings等を初期化します
        /// アプリケーションのエントリポイントで呼び出す必要があります
        /// </summary>
        public static void Initialize()
        {
            //crush = Settings.Crushed && File.Exists(Settings.BackUpSavePath);
            //if (crush)
            //{                
            //    if (MessageBox.Show("異常終了を検出しました。\n設定ファイルのバックアップを適用しますか？",
            //        "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    {
            //        loadBackup = true;
            //    }
            //}

            OptionItems options = CreateOptions();
            Settings settings = CreateSettings();
            PatrolTable patterns = CreatePatrolPatterns();
            Initialize(settings, options, patterns);
        }

        private static void Initialize(Settings settings, OptionItems options, PatrolTable patterns)
        {
            if (initialized)
            {
                throw new InvalidOperationException("既に初期化されています");
            }
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            OnInitializing();

            //シリアル化インスタンス
            //アプリケーション設定
            currentSettings = settings;
            currentSettings.MarkCrush();
            //ユーザー設定
            Options = options;
            Options.ItemsChanged += new EventHandler(Options_ItemsChanged);
            //巡回設定
            PatrolPatterns = patterns;

            //オプション
            InitializeOptions();

            //バージョンテキスト
            version = System.Windows.Forms.Application.ProductVersion;
            if (version.EndsWith(TrimedTailVersionText))
            {
                version = version.Substring(0, version.Length - TrimedTailVersionText.Length);
            }
            //if (IsBeta)
            //{
            //    version += "β";
            //}

            //詳細情報
            currentSettings.Information.LastUptime = DateTime.Now;

            //作業フォルダー
            if (System.IO.Directory.Exists(DeanCCCore.Core.VersionUp.VersionUpClient.NewVersionFolder))
            {
                System.IO.Directory.Delete(DeanCCCore.Core.VersionUp.VersionUpClient.NewVersionFolder, true);
            }

            //板一覧
            if (settings.FirstRunning)
            {
                try
                {
                    currentSettings.Boards.OnlineUpdate();
                }
                catch (ApplicationException ex)
                {
                    currentSettings.Boards.LoadBoardFromXml();
                    logs.Add("板一覧更新", ex.Message + " デフォルトの板一覧を読み込みます。", LogStatus.Error);
                }
                catch (System.Net.WebException ex)
                {
                    currentSettings.Boards.LoadBoardFromXml();
                    logs.Add("通信エラー", ex.Message + " デフォルトの板一覧を読み込みます。", LogStatus.Error);
                }
            }

            //スレッド一覧
            UpdateThreads();

            OnInitialized();
        }

        static void InitializeOptions()
        {
            //共通プロキシ
            if (Options.InternetOptions.Proxy.Enable)
            {
                System.Net.WebRequest.DefaultWebProxy =
                    Options.InternetOptions.Proxy.GetProxy();
            }

            //JaneStyle
            //ImageViewURLReplace.dat
            if (File.Exists(Options.BrowsersOptions.JaneOptions.ImageViewURLRepalcedatPath))
            {
                imageViewURLRepalcer = new ImageViewURLReplace(
                    Options.BrowsersOptions.JaneOptions.ImageViewURLRepalcedatPath);
            }
            //NGFiles.txt
            if (File.Exists(Options.NGOptions.NGFilestxtPath))
            {
                ngFiles = new NGFiles(Options.NGOptions.NGFilestxtPath);
            }
            //ReplaceStr.txt
            if (File.Exists(Options.BrowsersOptions.JaneOptions.ReplaceStrtxtPath))
            {
                replaceStr = new ReplaceStr(Options.BrowsersOptions.JaneOptions.ReplaceStrtxtPath);
            }
            //VwCache
            string cachePath = Options.BrowsersOptions.JaneOptions.GetViewCacheFolderPath();
            if (Directory.Exists(cachePath))
            {
                viewCacher = new ViewCache(cachePath);
            }

            //共通NGワード
            ApplyGlobalNGPatternRegex();

            //Dat取得間隔
            int datAccessInterval =
                ConnectionLimited.CalculateAccessInterval(Options.DatOptions.DatAccessRate);
            connectionLimiter = new ConnectionLimited(datAccessInterval);

            //期限切れスレッド
            if (Options.StartupOptions.RemoveExpirationThread)
            {
                foreach (DeanCCCore.Core._2ch.Thread thread in currentSettings.AllThreads)
                {
                    if (DateTime.Now - thread.Header.Since > TimeSpan.FromDays(Options.StartupOptions.ThreadLifeDate))
                    {
                        thread.Header.IsIgnored = true;
                    }
                }
            }
            //定期アップデートチェック
            if (Options.StartupOptions.AutoCheckNewVersion)
            {
                newVersionCheckTimer =
                    new System.Threading.Timer((state) => { VersionUp.VersionUpClient.CheckNewVersion(); }, null, 0, 24 * 60 * 60 * 1000);
            }
        }

        static void Options_ItemsChanged(object sender, EventArgs e)
        {
            //ToDo:巡回中に設定変更されたときの処理
            if (!isPatrolling)
            {
                InitializeOptions();
            }
        }

        /// <summary>
        /// 現在の状態を保存します
        /// </summary>
        public static void Save()
        {
            lock (Settings.SyncRoot)
            {
                currentSettings.Save();
            }

            if (downloadedImageHashes != null)
            {
                lock (((System.Collections.ICollection)downloadedImageHashes).SyncRoot)
                {
                    downloadedImageHashes.Save();
                }
            }
        }

        /// <summary>
        /// Coreを解放します
        /// アプリケーション終了時に呼び出す必要があります
        /// </summary>
        public static void Release()
        {
            if (patrolTimer != null)
            {
                patrolTimer.Dispose();
                patrolTimer = null;
            }
            if (newVersionCheckTimer != null)
            {
                newVersionCheckTimer.Dispose();
                newVersionCheckTimer = null;
            }
            //if (networkCounter != null)
            //{
            //    networkCounter.Dispose();
            //    networkCounter = null;
            //}
            if (mainForm != null)
            {
                mainForm.Dispose();
                mainForm = null;
            }
            Mutex.ReleaseMutex();
        }

        private static void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(null, EventArgs.Empty);
            }
        }

        private static void OnInitialized()
        {
            initialized = true;
            if (Initialized != null)
            {
                Initialized(null, EventArgs.Empty);
            }
        }

        //private static bool savableUserSettings = true;
        ///// <summary>
        ///// ユーザーが変更可能な設定を保存するかどうかを取得または設定します
        ///// </summary>
        //public static bool SavableUserSettings
        //{
        //    get { return savableUserSettings; }
        //    set { savableUserSettings = value; }
        //}

        private static Settings CreateSettings()
        {
            Settings settings = null;
            //if (loadBackup)
            //{
            //    try
            //    {
            //        settings = Settings.CreateBackUp();
            //        return settings;
            //    }
            //    catch (SerializationException ex)
            //    {
            //        MessageBox.Show(ex.Message,
            //            "バックアップ適用失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            try
            {
                settings = Settings.Create();
            }
            catch (SerializationException ex)
            {
                settings = new Settings();
                MessageBox.Show(ex.Message + "\n初期設定を適用します",
                    "設定ファイル読み込み失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return settings;
        }

        private static ImageHashCollection CreateHashes()
        {
            ImageHashCollection hashes = null;
            //if (loadBackup)
            //{
            //    try
            //    {
            //        hashes = ImageHashCollection.CreateBackUp();
            //        return hashes;
            //    }
            //    catch (SerializationException ex)
            //    {
            //        MessageBox.Show(ex.Message,
            //            "バックアップ適用失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            try
            {
                hashes = ImageHashCollection.CreateImageHashes();
            }
            catch (SerializationException ex)
            {
                hashes = new ImageHashCollection();
                MessageBox.Show(ex.Message + "\n初期設定を適用します",
                    "画像ハッシュ読み込み失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return hashes;
        }

        private static OptionItems CreateOptions()
        {
            OptionItems options = null;
            //if (loadBackup)
            //{
            //    try
            //    {
            //        options = OptionItems.CreateBackUp();
            //        return options;
            //    }
            //    catch (SerializationException ex)
            //    {
            //        MessageBox.Show(ex.Message,
            //            "バックアップ適用失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            try
            {
                options = OptionItems.Create();//OptionItems.Import() ?? OptionItems.Create();
            }
            catch (SerializationException ex)
            {
                options = new OptionItems();
                MessageBox.Show(ex.Message + "\n初期設定を適用します",
                    "ユーザー設定読み込み失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return options;
        }

        private static PatrolTable CreatePatrolPatterns()
        {
            PatrolTable patterns = null;
            //if (loadBackup)
            //{
            //    try
            //    {
            //        patterns = PatrolTable.CreateBackup();
            //        return patterns;
            //    }
            //    catch (SerializationException ex)
            //    {
            //        MessageBox.Show(ex.Message,
            //            "バックアップ適用失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            try
            {
                patterns = PatrolTable.Create();//PatrolTable.Import() ?? PatrolTable.Create();
            }
            catch (SerializationException ex)
            {
                patterns = new PatrolTable();
                MessageBox.Show(ex.Message + "\n初期設定を適用します",
                    "巡回設定読み込み失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return patterns;
        }

        private static string version;
        /// <summary>
        /// このアプリケーションのバージョンテキストを表します。
        /// </summary>
        public static string VersionText
        {
            get
            {
                return version;
            }
        }

        /// <summary>
        /// 共通NGワードを再適用します
        /// </summary>
        public static void ApplyGlobalNGPatternRegex()
        {
            StringBuilder sb = new StringBuilder();
            PatrolPattern.CreateNGPattern(sb, Options.NGOptions.GlobalNGPattern);
            GlobalNGPatternRegex = PatrolPattern.CreatePatternRegex(sb.ToString(), false);
        }

        private static Settings currentSettings;
        /// <summary>
        /// 現在適用されているアプリケーション設定
        /// </summary>
        public static Settings CurrentSettings
        {
            get
            {
                #region デザイナー用コード
                if (currentSettings == null)
                {
                    currentSettings = new Settings();
                }
                #endregion
                return currentSettings;
            }
        }

        /// <summary>
        /// ユーザーが登録した取得パターン設定
        /// </summary>
        public static PatrolTable PatrolPatterns
        {
            get;
            private set;
        }

        /// <summary>
        /// ユーザーが自由に編集可能な設定
        /// </summary>
        public static OptionItems Options
        {
            get;
            private set;
        }

        private static ImageHashCollection downloadedImageHashes;
        /// <summary>
        /// ダウンロード済み画像のMD5ハッシュリスト
        /// </summary>
        public static ImageHashCollection DownloadedImageHashes
        {
            get
            {
                if (downloadedImageHashes == null)
                {
                    lock (ImageHashCollection.SyncRoot)
                    {
                        if (downloadedImageHashes == null)
                        {
                            downloadedImageHashes = CreateHashes();
                            OnCreatedDownloadedImageHashes();
                        }
                    }
                }
                return downloadedImageHashes;
            }
        }

        private static bool isPatrolling;
        /// <summary>
        /// 定期巡回実行中であるかを示します
        /// </summary>
        public static bool IsPatrolling
        {
            get
            {
                return isPatrolling || IsQuickPatrolling;
            }
        }
        private static bool isQuickPatrolling;
        /// <summary>
        /// 定期巡回実行中であるかを示します
        /// </summary>
        public static bool IsQuickPatrolling
        {
            get
            {
                return isQuickPatrolling;
            }
        }

        /// <summary>
        /// 定期巡回実行中であればキャンセルします
        /// </summary>
        public static void CancelPatrol()
        {
            if (isPatrolling)
            {
                Patroller.Stop();
                Updater.Stop();
                Downloader.Stop();
            }
            if (isQuickPatrolling)
            {                
                QuickUpdater.Stop();
                QuickDownloader.Stop();
            }
        }

        private static Form mainForm;
        /// <summary>
        /// メインフォームを表します
        /// </summary>
        public static Form MainForm
        {
            get { return mainForm; }
        }
        /// <summary>
        /// メインフォームの参照を追加します
        /// これはスレッドリスト等の内容更新に使用されます
        /// </summary>
        /// <param name="mainForm"></param>
        public static void SetMainForm(Form form)
        {
            mainForm = form;
        }

        /// <summary>
        /// メインフォームでの実行が必要かどうかを示す値を表します
        /// </summary>
        public static bool InvokeRequired
        {
            get
            {
                return mainForm != null && mainForm.InvokeRequired;
            }
        }

        private static object patrolSyncRoot = new object();
        /// <summary>
        /// 巡回動作を同期するために使用できるオブジェクトを取得します
        /// </summary>
        public static object PatrolSyncRoot
        {
            get { return patrolSyncRoot; }
        }

        /// <summary>
        /// 指定したメソッドをメインフォームで実行します
        /// </summary>
        /// <param name="method"></param>
        public static void InvokeMainForm(Delegate method, params object[] args)
        {
            if (mainForm == null || mainForm.IsDisposed)
            {
                throw new InvalidOperationException("MainFormが破棄されているので実行できません");
            }
            mainForm.Invoke(method, args);
        }

        private static System.Threading.Timer newVersionCheckTimer;

        private static ThreadPatroller patroller;
        /// <summary>
        /// スレッドの取得・更新機能を提供します
        /// </summary>
        public static ThreadPatroller Patroller
        {
            get
            {
                if (patroller == null)
                {
                    patroller = new ThreadPatroller(PatrolPatterns,
                        Options.IndividualThreadOptions.PatrolPattern, currentSettings.AllThreads);
                    patroller.Ran += new EventHandler(patroller_Ran);
                }
                return patroller;
            }
        }

        private static QuickPatrolMarker quickPatrolMarker;
        /// <summary>
        /// 高頻度ダウンロードの対象自動選択機能を提供します
        /// </summary>
        public static QuickPatrolMarker QuickPatrolMarker
        {
            get
            {
                if (quickPatrolMarker == null)
                {
                    quickPatrolMarker = new QuickPatrolMarker(currentSettings.AllThreads);
                }
                return quickPatrolMarker;
            }
        }

        private static ThreadUpdater updater;
        /// <summary>
        /// スレッドのレス更新機能を提供します
        /// </summary>
        public static ThreadUpdater Updater
        {
            get
            {
                if (updater == null)
                {
                    updater = new ThreadUpdater(currentSettings.AllThreads);
                    updater.Running += new EventHandler<System.ComponentModel.CancelEventArgs>(updater_Running);
                    updater.Ran += new EventHandler(updater_Ran);
                }
                return updater;
            }
        }
        private static ThreadUpdater quickUpdater;
        /// <summary>
        /// 高頻度ダウンロード用スレッドのレス更新機能を提供します
        /// </summary>
        public static ThreadUpdater QuickUpdater
        {
            get
            {
                if (quickUpdater == null)
                {
                    quickUpdater = new ThreadUpdater(currentSettings.AllThreads, thread =>
                    {
                        return (thread.QuickDownloading & QuickDownloadState.Selected) == QuickDownloadState.Selected &&
                            !thread.Header.IsPastlog && !thread.Header.IsLimitOverThread;
                    });
                    quickUpdater.Running += new EventHandler<System.ComponentModel.CancelEventArgs>(updater_Running);
                    quickUpdater.Ran += new EventHandler(updater_Ran);
                }
                return quickUpdater;
            }
        }

        private static ImageDownloader downloader;
        /// <summary>
        /// 画像のダウンロード機能を提供します
        /// </summary>
        public static ImageDownloader Downloader
        {
            get
            {
                if (downloader == null)
                {
                    downloader = new ImageDownloader(currentSettings.AllThreads);
                    downloader.Running += new EventHandler<System.ComponentModel.CancelEventArgs>(downloader_Running);
                    downloader.Ran += new EventHandler(downloader_Ran);
                }
                return downloader;
            }
        }
        private static ImageDownloader quickDownloader;
        /// <summary>
        /// 高頻度画像ダウンロード機能を提供します
        /// </summary>
        public static ImageDownloader QuickDownloader
        {
            get
            {
                if (quickDownloader == null)
                {
                    quickDownloader = new ImageDownloader(currentSettings.AllThreads, thread =>
                    {
                        return (thread.QuickDownloading & QuickDownloadState.Selected) == QuickDownloadState.Selected && thread.Downloadable;
                    });
                    quickDownloader.Running += new EventHandler<System.ComponentModel.CancelEventArgs>(downloader_Running);
                    quickDownloader.Ran += new EventHandler(downloader_Ran);
                }
                return quickDownloader;
            }
        }


        /// <summary>
        /// スレッド取得・ダウンロードの開始直前に発生します
        /// </summary>
        public static event EventHandler<System.ComponentModel.CancelEventArgs> Patrolling;
        /// <summary>
        /// スレッド取得・ダウンロードの完了後に発生します
        /// </summary>
        public static event EventHandler Patrolled;
        /// <summary>
        /// 登録してあるパターンからのスレッド取得・ダウンロードの一連動作を実装
        /// </summary>
        private static void Patrol()
        {
            System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
            OnPatrolling(e);
            if (e.Cancel)
            {
                return;
            }
            lock (patrolSyncRoot)
            {
                Patroller.Run();
                Updater.Run();
            }
            Downloader.Run();

            OnPatrolled();
        }

        /// <summary>
        /// スレッド取得・ダウンロードの開始直前に発生します
        /// </summary>
        public static event EventHandler<System.ComponentModel.CancelEventArgs> QuickPatrolling;
        /// <summary>
        /// スレッド取得・ダウンロードの完了後に発生します
        /// </summary>
        public static event EventHandler QuickPatrolled;
        private static void QuickPatrol()
        {
            System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
            OnQuickPatrolling(e);
            if (e.Cancel)
            {
                return;
            }
            lock (patrolSyncRoot)
            {
                QuickPatrolMarker.Run();
                QuickUpdater.Run();
            }
            QuickDownloader.Run();

            OnQuickPatrolled();
        }

        static void downloader_Ran(object sender, EventArgs e)
        {
            lock (patrolSyncRoot)
            {
                InvokeUpdateThreads();
            }
        }

        static void downloader_Running(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lock (patrolSyncRoot)
            {
                if (imageViewURLRepalcer != null && !imageViewURLRepalcer.Loaded)
                {
                    imageViewURLRepalcer.Load();
                }
                //if (downloadedImageHashes == null)
                //{
                //    downloadedImageHashes = CreateHashes();
                //    OnCreatedDownloadedImageHashes();
                //}
                if (ngFiles != null && !ngFiles.Loaded)
                {
                    ngFiles.Load();
                }
            }
        }

        static void OnCreatedDownloadedImageHashes()
        {
            if (Options.StartupOptions.RemoveExpirationImageHash)
            {
                int removedItemsCount = downloadedImageHashes.RemoveAll(hash =>
                    DateTime.Now - hash.CreatedTime >
                    TimeSpan.FromDays(Options.StartupOptions.HashLifeDate));
                if (removedItemsCount > 0)
                {
                    Logs.Add("期限切れ画像ハッシュ削除",
                        string.Format("{0:N0}パターン", removedItemsCount), LogStatus.System);
                }
            }
        }

        static void patroller_Ran(object sender, EventArgs e)
        {
            InvokeUpdateThreads();
        }

        static void updater_Running(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lock (patrolSyncRoot)
            {
                if (replaceStr != null && !replaceStr.Loaded)
                {
                    replaceStr.Load();
                }
            }
        }

        static void updater_Ran(object sender, EventArgs e)
        {
            lock (patrolSyncRoot)
            {
                InvokeUpdateThreads();
            }
        }

        static void InvokeUpdateThreads()
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Invoke(new MethodInvoker(UpdateThreads));
            }
        }

        /// <summary>
        /// すべてのスレッドリストの内容を更新します
        /// </summary>
        public static void UpdateThreads()
        {
            lock (((System.Collections.ICollection)currentSettings.AllThreads).SyncRoot)
            {
                enableThreads.Update();
                downloadedThreads.Update();
                quickDownloadingThreads.Update();
                downloadingThreads.Update();
                downloadPausedThreads.Update();
                excludedThreads.Update();
                secureThreads.Update();
            }
        }

        private static void OnPatrolling(System.ComponentModel.CancelEventArgs e)
        {
            if (Patrolling != null)
            {
                Patrolling(null, e);
            }

            if (isPatrolling)
            {
                e.Cancel = true;
            }
            else
            {
                isPatrolling = true;
            }
        }

        private static void OnPatrolled()
        {
            isPatrolling = false;
            //currentSettings.BackUp();
            //DownloadedImageHashes.BackUp();
            //Options.BackUp();
            //PatrolPatterns.BackUp();
            if (Patrolled != null)
            {
                Patrolled(null, EventArgs.Empty);
            }
        }
        private static PatrolTimer patrolTimer = new PatrolTimer(state => { Patrol(); });
        /// <summary>
        /// 定期的な動作を実装します
        /// </summary>
        public static PatrolTimer PatrolTimer
        {
            get
            {
                return patrolTimer;
            }
        }

        private static void OnQuickPatrolling(System.ComponentModel.CancelEventArgs e)
        {
            if (QuickPatrolling != null)
            {
                QuickPatrolling(null, e);
            }

            if (isQuickPatrolling)
            {
                e.Cancel = true;
            }
            else
            {
                isQuickPatrolling = true;
            }
        }

        private static void OnQuickPatrolled()
        {
            isQuickPatrolling = false;
            if (QuickPatrolled != null)
            {
                QuickPatrolled(null, EventArgs.Empty);
            }
        }
        private static PatrolTimer quickPatrolTimer = new PatrolTimer(
            () => { return 5 * 60 * 1000; },
            state => { QuickPatrol(); });
        /// <summary>
        /// 定期的な動作を実装します
        /// </summary>
        public static PatrolTimer QuickPatrolTimer
        {
            get
            {
                return quickPatrolTimer;
            }
        }

        /// <summary>
        /// 非同期で指定したUrlを個別追加スレッドとして追加します
        /// </summary>
        /// <param name="url">指定するUrl</param>
        public static void AddIndividualThreadAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        patroller.AddIndividualThread(url);
                        OnAddedIndividualThread();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message + "\n追加しようとしたURLは無効です。",
                            "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(url + " は現在追加できません。\n" + ex.Message, "確認",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
        }

        /// <summary>
        /// 非同期で指定したUrlリストを個別追加スレッドとして追加します
        /// </summary>
        /// <param name="urls">指定するUrlリスト</param>
        public static void AddIndividualThreadAsync(string[] urls)
        {
            if (urls == null || urls.Length <= 0)
            {
                return;
            }

            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                foreach (string url in urls)
                {
                    try
                    {
                        patroller.AddIndividualThread(url);
                    }
                    catch (ArgumentException ex)
                    {
                        logs.Add("URL追加失敗",
                            ex.Message + "\n追加しようとしたURLは無効です。", LogStatus.Error);
                    }
                    catch (InvalidOperationException ex)
                    {
                        logs.Add("URL追加失敗",
                            url + " は現在追加できません。\n" + ex.Message, LogStatus.Error);
                    }
                }
                OnAddedIndividualThread();
            });
        }

        private static void OnAddedIndividualThread()
        {
            if (!isPatrolling)
            {
                patrolTimer.Start();
            }
        }
    }
}