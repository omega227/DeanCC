using System;
using System.Security.Permissions;
using System.Windows.Forms;
using DeanCC.GUI.Options;
using DeanCCCore.Core;
using DeanCCCore.Core._2ch;

namespace DeanCC.GUI
{
    public sealed partial class MainForm : Form
    {
        #region ステータステキスト
        private const string PatrollingStatusText = "レス読み込み中...";
        private const string PatrollingTimeStatusText = "00:00";
        private const string PatrolledStatusText = "";
        private const string ConnectionWaitingStatusText = "読み込み待機中...";
        private const string ConnectionWaitedStatusText = "レス読み込み中...";
        private const string DownloadingStatusTextFormat = "ダウンロード中： {0}";
        private const string MouseMoveTextFormat = "DeanCC\nDL開始まであと {0}";
        private const string PatrollingMouseMoveText = "DeanCC\nDL中...";
        private const string StartupMouseMoveText = "起動中...";
        private const string ThreadCountStatusTextFormat = "Threads: {0}";
        #endregion

        public const string ActiveArgument = "-active";
        public const string AddUrlArgmuent = "-add";
        public const char ArgumentSperator = ' ';
        private const string PipeServerNameFormat = "DeanCCServer.{0}";
        public const string SuccessToken = "Success";
        private const string ImagePassBalloonTitle = "DeanCC";
        private const string ImagePassBalloonTextFormat = "画像認証・パスワードが必要なZIPを取得しました。\n{0}\n{1}";
        private const string PatrolledBalloonTitle = "DeanCC";
        private const string PatrolledBalloonText = "ダウンロード完了";
        public const int HideInterval = 400;
        //public const int CloseInterval = 3000;
        private const string ThreadsChangedTextFormat = "{0}：{1}";

        private bool updatableThreadViewer;
        private bool isStartup;
        private bool closing;
        private IPCServer server = new IPCServer(PipeServerName);
        private ClipboardView clipboardViewer;

        private ThreadView threadViewer;
        public ThreadView ThreadViewer
        {
            get
            {
                if (threadViewer == null)
                {
                    threadViewer = new ThreadView();
                    threadViewer.RowsAdded += new DataGridViewRowsAddedEventHandler(threadViewer_RowsAdded);
                    threadViewer.RowsRemoved += new DataGridViewRowsRemovedEventHandler(threadViewer_RowsRemoved);
                    threadViewer.Downloading += new EventHandler(threadViewer_Downloading);
                    threadViewer.Downloaded += new EventHandler(threadViewer_Downloaded);
                }
                return threadViewer;
            }
        }

        private LogView logViewer;
        public LogView LogViewer
        {
            get
            {
                if (logViewer == null)
                {
                    logViewer = new LogView();
                }
                return logViewer;
            }
        }

        private InformationControl informationViewer;
        public InformationControl InformationViewer
        {
            get
            {
                if (informationViewer == null)
                {
                    informationViewer = new InformationControl();
                }
                return informationViewer;
            }
        }

        public static string PipeServerName
        {
            get
            {
                return string.Format(PipeServerNameFormat,
                    System.Diagnostics.Process.GetCurrentProcess().SessionId);
            }
        }
        
        public MainForm()
        {
            InitializeComponent();

            AddEvents();
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ErrorReport reporter = new ErrorReport(e.Exception);
            if (reporter.ShowDialog() == DialogResult.Ignore)
            {
                //Core.SavableSettings = false;
                return;
            }

            //this.Close();
            Application.Exit();
        }

        void threadViewer_Downloaded(object sender, EventArgs e)
        {
            updatableThreadViewer = false;
            if (!Common.IsPatrolling)
            {
                OnPatrolled(null, EventArgs.Empty);
            }
        }

        void threadViewer_Downloading(object sender, EventArgs e)
        {
            updatableThreadViewer = true;
            if (!Common.IsPatrolling)
            {
                OnPatrolling(null, new System.ComponentModel.CancelEventArgs());
            }
        }

        void threadViewer_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (threadViewer.CurrentThreads != null)
            {
                ThreadCountStatusLabel.Text =
                    string.Format(ThreadCountStatusTextFormat, threadViewer.CurrentThreads.Count);
            }
        }

        void threadViewer_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (threadViewer.CurrentThreads != null)
            {
                ThreadCountStatusLabel.Text =
                    string.Format(ThreadCountStatusTextFormat, threadViewer.CurrentThreads.Count);
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //UIスレッド以外で発生した例外
            try
            {
                ErrorLogger.Write(e.ExceptionObject);
                MessageBox.Show("致命的なエラーが発生しました。\n詳細は " + ErrorLogger.SavePath
                    + " に記録されています。\nこのダイアログを閉じるとアプリケーションを終了します",
                    "DeanCC", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("致命的なエラーが発生しました。\n詳細：\n" + e.ExceptionObject.ToString(),
                    "DeanCC", MessageBoxButtons.OK, MessageBoxIcon.Stop);               
            }
            Invoke(new MethodInvoker(Application.Exit));
        }

        void Common_Patrolled(object sender, EventArgs e)
        {
            if (!Disposing && !IsDisposed)
            {
                if (InvokeRequired)
                {
                    Invoke(new EventHandler(OnPatrolled));
                }
                else
                {
                    OnPatrolled(sender, e);
                }
            }
        }

        private void OnPatrolled(object sender, EventArgs e)
        {
            mainStatusLabel.Text = PatrolledStatusText;
            if (mainPanel.Controls.Count > 0)
            {
                Control activeControl = mainPanel.Controls[0];
                if (activeControl is ThreadView)
                {
                    ThreadView activeThreadView = (ThreadView)activeControl;
                    activeThreadView.ReSort();
                }
                activeControl.Invalidate();
            }

            if (Common.Options.MessageOptions.VisiblePatrolled)
            {
                //通知
                mainNotifyIcon.ShowBalloonTip(Common.Options.MessageOptions.Timeout, PatrolledBalloonTitle,
                     PatrolledBalloonText, ToolTipIcon.Info);
            }
        }

        void Common_Patrolling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Disposing && !IsDisposed)
            {
                if (InvokeRequired)
                {
                    Invoke(new EventHandler<System.ComponentModel.CancelEventArgs>(OnPatrolling), sender, e);
                }
                else
                {
                    OnPatrolling(sender, e);
                }
            }
        }

        private void OnPatrolling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainStatusLabel.Text = PatrollingStatusText;
        }

        void ConnectionLimiter_Waited(object sender, EventArgs e)
        {
            if (!Disposing && !IsDisposed)
            {
                if (InvokeRequired)
                {
                    Invoke(new EventHandler(OnConnectionWaited));
                }
                else
                {
                    OnConnectionWaited(sender, e);
                }
            }
        }

        void OnConnectionWaited(object sender, EventArgs e)
        {
            mainStatusLabel.Text = ConnectionWaitedStatusText;
        }
        void OnConnectionWaiting(object sender, EventArgs e)
        {
            mainStatusLabel.Text = ConnectionWaitingStatusText;
        }

        void ConnectionLimiter_Waiting(object sender, EventArgs e)
        {
            if (!Disposing && !IsDisposed)
            {
                if (InvokeRequired)
                {
                    Invoke(new EventHandler(OnConnectionWaiting));
                }
                else
                {
                    OnConnectionWaiting(sender, e);
                }
            }
        }

        void threadListControl1_Selected(object sender, ThreadListEventArgs e)
        {
            Control activeControl = null;
            switch (e.SelectedMenu)
            {
                case SelectedThreadListMenu.AllThread:
                    ThreadViewer.ChangeSource(Common.EnableThreads);
                    activeControl = ThreadViewer;
                    break;

                case SelectedThreadListMenu.DownloadingThread:
                    ThreadViewer.ChangeSource(Common.DownloadingThreads);
                    activeControl = ThreadViewer;
                    break;

                case SelectedThreadListMenu.DownloadPausedThread:
                    ThreadViewer.ChangeSource(Common.DownloadPausedThreads);
                    activeControl = ThreadViewer;
                    break;

                case SelectedThreadListMenu.DownloadedThread:
                    ThreadViewer.ChangeSource(Common.DownloadedThreads);
                    activeControl = ThreadViewer;
                    break;

                case SelectedThreadListMenu.ExcludedThread:
                    ThreadViewer.ChangeSource(Common.ExcludedThreads);
                    activeControl = ThreadViewer;
                    break;

                case SelectedThreadListMenu.SecureImage:
                    ThreadViewer.ChangeSource(Common.SecureThreads);
                    activeControl = ThreadViewer;
                    break;

                case SelectedThreadListMenu.Log:
                    activeControl = LogViewer;
                    break;

                case SelectedThreadListMenu.Information:
                    activeControl = InformationViewer;
                    break;
            }

            bool changeControl = mainPanel.Controls.Count <= 0 || !mainPanel.Controls[0].Equals(activeControl);
            if (changeControl)
            {
                mainPanel.Controls.Clear();
                mainPanel.Controls.Add(activeControl);
            }
        }
        

        protected override void OnShown(EventArgs e)
        {
            isStartup = true;
            if (Common.CurrentSettings.FirstRunning)
            {
                optionToolStripMenuItem.PerformClick();
                editPatrolToolStripMenuItem.PerformClick();
            }
            base.OnShown(e);
        }

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            optionToolStripMenuItem.PerformClick();
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm options = new OptionsForm();
            options.ShowDialog();
        }

        private void editPatrolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatrolPatternsEditForm patternEditor = new PatrolPatternsEditForm();
            patternEditor.ShowDialog();
        }

        private void patternEditToolStripButton_Click(object sender, EventArgs e)
        {
            editPatrolToolStripMenuItem.PerformClick();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (closing)
            {
                e.Cancel = true;
                return;
            }
            if (WindowState != FormWindowState.Minimized &&
                e.CloseReason == CloseReason.UserClosing &&
                Common.Options.WindowOptions.CloseInTasktray)
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
                return;
            }
            if (e.CloseReason == CloseReason.UserClosing &&
                Common.IsPatrolling &&
                MessageBox.Show("巡回中です。巡回を中止してアプリケーションを終了しますか？",
                    "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }


            closing = true;
            System.Threading.Thread.Sleep(HideInterval);
            mainNotifyIcon.Visible = false;
            Hide();
            //System.Threading.Thread.Sleep(CloseInterval);

            Common.PatrolTimer.Stop();
            Common.CancelPatrol();
            if (server != null)
            {
                server.Dispose();
                server = null;
            }
            RemoveEvents();
            updateTimer.Stop();
            StoreStatus(Common.CurrentSettings.FormStatuses);
            //if (!Common.IsPatrolling)
            //{
            //    Common.CurrentSettings.Save();
            //}
            //Common.Save();
            base.OnFormClosing(e);
        }

        /// <summary>
        /// イベントを登録します
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        private void AddEvents()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            threadListControl1.Selected += new EventHandler<ThreadListEventArgs>(threadListControl1_Selected);
            server.Recived += new EventHandler<ServerReciveEventArgs>(server_Recived);
            Common.ConnectionLimiter.Waiting += new EventHandler(ConnectionLimiter_Waiting);
            Common.ConnectionLimiter.Waited += new EventHandler(ConnectionLimiter_Waited);
            Common.Patrolling += new EventHandler<System.ComponentModel.CancelEventArgs>(Common_Patrolling);
            Common.Patrolled += new EventHandler(Common_Patrolled);
            Common.Patroller.ThreadsChanged += new EventHandler<ThreadsChangedEventArgs>(Patroller_ThreadsChanged);
            Thread.ImagePassRequired += new EventHandler<ImagePassEventArgs>(Thread_ImagePassRequired);
            Thread.Downloading += new EventHandler<ImageDownloadEventArgs>(Thread_Downloading);
        }

        void Thread_Downloading(object sender, ImageDownloadEventArgs e)
        {
            if (!closing && !Disposing && !IsDisposed)
            {
                if (InvokeRequired)
                {
                    Invoke(new EventHandler<ImageDownloadEventArgs>(OnThreadDownloadingInternal), sender, e);
                }
                else
                {
                    OnThreadDownloadingInternal(sender, e);
                }
            }
        }

        private void OnThreadDownloadingInternal(object sender, ImageDownloadEventArgs e)
        {
            mainStatusLabel.Text = string.Format(DownloadingStatusTextFormat, e.Image.OriginalUrl);
        }

        void Thread_ImagePassRequired(object sender, ImagePassEventArgs e)
        {
            if (!Common.Options.MessageOptions.VisibleSecureFile)
            {
                return;
            }

            ImageHeader image = e.BlockedImageHeader;
            //通知
            mainNotifyIcon.ShowBalloonTip(Common.Options.MessageOptions.Timeout, ImagePassBalloonTitle,
                string.Format(ImagePassBalloonTextFormat, e.Source.Title, image.OriginalUrl), ToolTipIcon.Info);

            //ToDo: 画像認証受信時の処理 コレクションに追加等
        }

        void Patroller_ThreadsChanged(object sender, ThreadsChangedEventArgs e)
        {
            Common.Logs.Add(ThreadsChangedStatusString.Get(e.Status),
               string.Format(ThreadsChangedTextFormat,
                e.Thread.Header.SourceBoard.Name, e.Thread.Title), LogStatus.Normal);
        }

        void server_Recived(object sender, ServerReciveEventArgs e)
        {
            string[] data = e.StreamReader.ReadLine().Split(ArgumentSperator);
            string command = data[0];
            string[] arguments = null;
            if (data.Length > 1)
            {
                arguments = new string[data.Length - 1];
                data.CopyTo(arguments, 1);
            }

            switch (command)
            {
                case ActiveArgument:
                    Activate();
                    break;

                case AddUrlArgmuent:
                    Common.AddIndividualThreadAsync(arguments);
                    break;
            }
            e.StreamWriter.WriteLine(SuccessToken);
        }

        /// <summary>
        /// 登録したイベントを解除します
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        private void RemoveEvents()
        {
            AppDomain.CurrentDomain.UnhandledException -=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Common.ConnectionLimiter.Waiting -= new EventHandler(ConnectionLimiter_Waiting);
            Common.ConnectionLimiter.Waited -= new EventHandler(ConnectionLimiter_Waited);
            Common.Patrolling -= new EventHandler<System.ComponentModel.CancelEventArgs>(Common_Patrolling);
            Common.Patrolled -= new EventHandler(Common_Patrolled);
            Common.Patroller.ThreadsChanged -= new EventHandler<ThreadsChangedEventArgs>(Patroller_ThreadsChanged);
            Thread.ImagePassRequired -= new EventHandler<ImagePassEventArgs>(Thread_ImagePassRequired);
            Thread.Downloading -= new EventHandler<ImageDownloadEventArgs>(Thread_Downloading);
        }

        private void StoreStatus(FormStatus destination)
        {
            //
            //フォームの状態を保存するにはここにコードを追加してください    
            //
            //サイズ・位置
            if (this.WindowState == FormWindowState.Normal)
            {
                destination.MainFormStatus.Size = this.Size;
                destination.MainFormStatus.Location = this.Location;
            }
            else if (!this.RestoreBounds.IsEmpty)
            {
                destination.MainFormStatus.Size = this.RestoreBounds.Size;
                destination.MainFormStatus.Location = this.RestoreBounds.Location;
            }
            //パネル表示・チェック状態
            destination.MainFormStatus.EnableAutoPatrol = autoPatrolToolStripMenuItem.Checked;
            destination.MainFormStatus.ShowsMenulistPanel = menuListToolStripMenuItem.Checked;
            destination.MainFormStatus.SelectedMenu = threadListControl1.SelectedMenu;
            destination.MainFormStatus.ShowsToolbar = toolBarToolStripMenuItem.Checked;
            destination.MainFormStatus.EnableClipboardViewer = clipboardToolStripMenuItem.Checked;
            //スレッドビュー
            if (threadViewer != null)
            {
                destination.ThreadViewStatus.Headers.Clear();
                foreach (DataGridViewColumn column in threadViewer.Columns)
                {
                    destination.ThreadViewStatus.Headers.Add(new DataGridViewHeaderStatus(column));
                }
            }
        }

        private void RestoreStatus(FormStatus source)
        {
            //
            //フォームの状態を復元するにはここにコードを追加してください
            //
            //サイズ・位置
            Location = source.MainFormStatus.Location;
            Size = source.MainFormStatus.Size;
            //パネル表示・チェック状態
            autoPatrolToolStripMenuItem.Checked = source.MainFormStatus.EnableAutoPatrol;
            menuListToolStripMenuItem.Checked = source.MainFormStatus.ShowsMenulistPanel;
            threadListControl1.SelectedMenu = source.MainFormStatus.SelectedMenu;
            toolBarToolStripMenuItem.Checked = source.MainFormStatus.ShowsToolbar;
            clipboardToolStripMenuItem.Checked = source.MainFormStatus.EnableClipboardViewer;
            //スレッドビューはthreadViewクラスで状態を復元
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void menuListToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            controlPanel.Visible = ((ToolStripMenuItem)sender).Checked;
        }

        private void toolBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            toolStrip.Visible = ((ToolStripMenuItem)sender).Checked;
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewVersionDownloadProcess downloader = new NewVersionDownloadProcess();
            bool newVersionDownloaded = downloader.Run();
            if (newVersionDownloaded)
            {
                DeanCCCore.Core.VersionUp.VersionUpClient.RunUpdater();
                Application.Exit();
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (mainPanel.Controls.Count > 0)
            {
                Control activeControl = mainPanel.Controls[0];
                if (activeControl is InformationControl)
                {
                    ((InformationControl)activeControl).Set();
                }
                else if (Common.IsPatrolling || updatableThreadViewer)
                {
                    if (activeControl is ThreadView)
                    {
                        ThreadView activeThreadView = (ThreadView)activeControl;
                        activeThreadView.ReSort();
                    }
                    activeControl.Invalidate();
                }
            }
            //Text = Core.GetTitle();
            timeStripStatusLabel.Text = Common.PatrolTimer.GetNextTimeText();
        }

        private void autoPatrolToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool autoPatrolling = Common.CurrentSettings.FormStatuses.MainFormStatus.EnableAutoPatrol = ((ToolStripMenuItem)sender).Checked;
            if (autoPatrolling)
            {
                Common.PatrolTimer.Start();
            }
            else
            {
                Common.PatrolTimer.Stop();
            }
        }

        private void mainNotifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            mainNotifyIcon.Text = !isStartup ? StartupMouseMoveText : Common.IsPatrolling ?
             PatrollingMouseMoveText : string.Format(MouseMoveTextFormat, Common.PatrolTimer.GetNextTimeText());
        }

        protected override void OnLoad(EventArgs e)
        {
            Common.SetMainForm(this);
            RestoreStatus(Common.CurrentSettings.FormStatuses);
            if (Common.Options.StartupOptions.Minimum)
            {
                WindowState = FormWindowState.Minimized;
            }
            Text = Common.GetSimpleTitle();
            timeStripStatusLabel.Text = PatrollingTimeStatusText;
            ThreadCountStatusLabel.Text = string.Format(ThreadCountStatusTextFormat, 0);

            updateTimer.Start();
            //Core.NetworkCounter.Start();
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized &&
                !Common.Options.WindowOptions.MinimumShowInTaskbar)
            {
                ShowInTaskbar = false;
            }
            base.OnResize(e);
        }

        private void RestoreWindowState()
        {
            Show();
            ShowInTaskbar = true;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeanCCCore.Core.Utility.ProcessUtility.OpenUrl(Common.OfficialHPDocumentPageUrl);
        }

        private void searchToolStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            if (searchToolStripSplitButton.DefaultItem != null)
            {
                searchToolStripSplitButton.DefaultItem.PerformClick();
            }
            else
            {
                searchToolStripSplitButton.DropDownItems[0].PerformClick();
            }
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadViewer.Filter(searchToolStripTextBox.Text);
        }

        private void addThreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = searchToolStripTextBox.Text;
            if (!string.IsNullOrWhiteSpace(url))
            {
                Common.AddIndividualThreadAsync(url);
                searchToolStripTextBox.Clear();
            }
        }

        private void searchToolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                searchToolStripSplitButton.PerformButtonClick();
            }
        }

        private void mainNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Minimized;
                }
                else
                {
                    RestoreWindowState();
                }
            }
        }

        private void clipboardToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool clipboardEnable = ((ToolStripMenuItem)sender).Checked;
            if (clipboardEnable)
            {
                if (clipboardViewer == null)
                {
                    clipboardViewer = new ClipboardView(this);
                    clipboardViewer.TextChanged += new EventHandler<ClipboardEventArgs>(clipboardViewer_TextChanged);
                }
                clipboardViewer.Enable = true;
            }
            else if (clipboardViewer != null)
            {
                clipboardViewer.Enable = false;
            }
        }

        void clipboardViewer_TextChanged(object sender, ClipboardEventArgs e)
        {
            Common.AddIndividualThreadAsync(e.Text);
        }
    }
}
