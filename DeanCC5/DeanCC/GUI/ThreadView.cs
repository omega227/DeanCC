using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core._2ch;
using DeanCCCore.Core.Utility;
using System.Text;

namespace DeanCC.GUI
{
    public sealed class ThreadView : BindingDataGridView
    {
        public event EventHandler Downloading;
        public event EventHandler Downloaded;

        private static readonly Type sourceType = typeof(Thread);
        private static readonly Type[] changedAligmentPropertyTypes = { typeof(int), typeof(float) };
        private static readonly Type changedTextFormatPropertyType = typeof(float);
        private const string changedTextFormat = "F1";
        private const string JustNowImageGotText = "たった今";
        private const string MinuitesTextFormat = "{0}分前";
        private const string HoursTextFormat = "{0}時間前";
        private const string DaysTextFormat = "{0}";
        private ContextMenuStrip rowsContextMenuStrip;
        private IContainer components;
        private ToolStripMenuItem openFolderToolStripMenuItem;
        private ToolStripMenuItem openThreadToolStripMenuItem;
        private ToolStripMenuItem onlineToolStripMenuItem;
        private ToolStripMenuItem localToolStripMenuItem;
        private ToolStripMenuItem janeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem urlToolStripMenuItem;
        private ToolStripMenuItem titleToolStripMenuItem;
        private ToolStripMenuItem titleAndUrlToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem startDownloadToolStripMenuItem;
        private ToolStripMenuItem stopDownloadToolStripMenuItem;
        private ToolStripMenuItem redownloadToolStripMenuItem;
        private ToolStripMenuItem viewImagesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem userCommandsToolStripMenuItem;

        /// <summary>
        /// フィルターを解除するために指定するキーを表します
        /// </summary>
        public const string ClearFilterToken = "";

        public ThreadView()
        {
            InitializeComponent();

            SetRowStripMenuItem();
            SetBackColors();
            Common.Options.ItemsChanged += new EventHandler(Options_ItemsChanged);
        }

        void Options_ItemsChanged(object sender, EventArgs e)
        {
            SetRowStripMenuItem();
            SetBackColors();
        }

        private void SetRowStripMenuItem()
        {
            ToolStripItemCollection items = ((ToolStripMenuItem)rowsContextMenuStrip.Items["userCommandsToolStripMenuItem"]).DropDownItems;
            items.Clear();
            IEnumerable<Command> commands = Common.Options.CommandOptions.CommandList.Where(command => command.CommandMode == CommandMode.ThreadListMenu);
            foreach (Command command in commands)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(command.Name)
                {
                    Name = command.Name
                };

                item.Click += delegate
                    {
                        ForEachSelectedThreads((thread) =>
                            {
                                command.Execute(thread);
                            });
                    };
                items.Add(item);
            }
        }

        private void SetBackColors()
        {
            RowsDefaultCellStyle.BackColor = Common.Options.ThreadViewOptions.OddRowsColor;
            AlternatingRowsDefaultCellStyle.BackColor = Common.Options.ThreadViewOptions.EvenRowsColor;
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DateTime)
            {
                e.Value = FormatDateTimeValue((DateTime)e.Value);
                e.FormattingApplied = true;
            }

            base.OnCellFormatting(e);
        }

        private string FormatDateTimeValue(DateTime time)
        {
            if (time == DateTime.MinValue)
            {
                return string.Empty;
            }

            TimeSpan spentTime = DateTime.Now - time;
            if (spentTime < TimeSpan.FromMinutes(1))
            {
                return JustNowImageGotText;
            }
            else if (spentTime < TimeSpan.FromHours(1))
            {
                return string.Format(MinuitesTextFormat, spentTime.Minutes);
            }
            else if (spentTime < TimeSpan.FromDays(1))
            {
                return string.Format(HoursTextFormat, spentTime.Hours);
            }
            else
            {
                return string.Format(DaysTextFormat, time);
            }
        }

        private DataGridViewHeaderContextMenuStrip columnHeaderContextMenuStrip;

        private BindingSource source = new BindingSource();
        /// <summary>
        /// 現在表示しているコレクション
        /// </summary>
        public BindingThreadCollection CurrentThreads { get; private set; }

        /// <summary>
        /// 指定したキーで内容をフィルター処理します
        /// </summary>
        /// <param name="key"></param>
        public void Filter(string key)
        {
            if (CurrentThreads != null)
            {
                if (key == ClearFilterToken)
                {
                    CurrentThreads.ClearFilter();
                }
                else
                {
                    CurrentThreads.Filter(thread => thread.Title.Contains(key));
                }
            }
        }

        /// <summary>
        /// 表示するコレクションを変更します
        /// </summary>
        /// <param name="threads"></param>
        public void ChangeSource(BindingThreadCollection threads)
        {
            if (threads != null && !threads.Equals(source.DataSource))
            {
                CurrentThreads = threads;
                source.DataSource = threads;
                DataSource = source;
                //OnDataSourceChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 現在指定されている列・方向で再ソートします
        /// </summary>
        public void ReSort()
        {
            if (SortOrder != System.Windows.Forms.SortOrder.None)
            {
                Sort(SortedColumn, SortOrder == System.Windows.Forms.SortOrder.Ascending ?
                    ListSortDirection.Ascending : ListSortDirection.Descending);
            }
        }

        protected override Type SourceType
        {
            get
            {
                return sourceType;
            }
        }

        protected override DataGridViewColumn CreatePropertyColumn(PropertyInfo property,
            string displayName, DisplayDataGridViewColumnTypeAttribute columnType = null)
        {
            DataGridViewColumn column;
            if (columnType != null &&
                columnType.ColumnType == DataGridViewColumnType.ProgressBar)
            {
                //プログレスバー列
                column = new DataGridViewProgressBarColumn()
                {
                    DataPropertyName = Name = property.Name,
                    HeaderText = displayName
                };
            }
            else
            {
                //プログレスバー列以外（テキストボックス列）
                column = new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = Name = property.Name,
                    HeaderText = displayName,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                };
            }
            SetColumnStatus(property, column);
            return column;
        }

        private void SetColumnStatus(PropertyInfo property, DataGridViewColumn column)
        {
            column.HeaderCell.ContextMenuStrip = columnHeaderContextMenuStrip;
            if (changedAligmentPropertyTypes.Contains(property.PropertyType))
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (property.PropertyType.Equals(changedTextFormatPropertyType))
            {
                column.DefaultCellStyle.Format = changedTextFormat;
            }
            string key = property.Name;
            DataGridViewColumn previousColumn = GetColumnFromDataPropertyName(key);
            if (previousColumn != null)
            {
                //直前の状態を適用
                column.Visible = previousColumn.Visible;
                column.DisplayIndex = previousColumn.DisplayIndex;
            }
            else
            {
                DataGridViewHeaderStatus status =
                    Common.CurrentSettings.FormStatuses.ThreadViewStatus.Headers.FirstOrDefault(
                    header => header.Name.Equals(key));
                if (status != null)
                {
                    //前回終了時の状態を適用
                    column.Visible = status.Visible;
                    column.DisplayIndex = status.DisplayIndex;
                }
            }
        }

        private DataGridViewColumn GetColumnFromDataPropertyName(string dataPropertyName)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                if (column.DataPropertyName.Equals(dataPropertyName))
                {
                    return column;
                }
            }
            return null;
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            columnHeaderContextMenuStrip.GenerateItem(e.Column);
            base.OnColumnAdded(e);
        }

        public void ForEachSelectedThreads(Action<Thread> action)
        {
            foreach (DataGridViewRow row in SelectedRows)
            {
                action((Thread)row.DataBoundItem);
            }
        }

        public ContextMenuStrip GetRowMenuStrip()
        {
            return rowsContextMenuStrip;
        }

        #region デザイナー
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.columnHeaderContextMenuStrip = new DeanCC.GUI.DataGridViewHeaderContextMenuStrip();
            this.rowsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openThreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.janeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.urlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleAndUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.startDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.userCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rowsContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // columnHeaderContextMenuStrip
            // 
            this.columnHeaderContextMenuStrip.Name = "columnHeaderContextMenuStrip";
            this.columnHeaderContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // rowsContextMenuStrip
            // 
            this.rowsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.openThreadToolStripMenuItem,
            this.viewImagesToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.toolStripSeparator2,
            this.removeToolStripMenuItem,
            this.toolStripSeparator3,
            this.startDownloadToolStripMenuItem,
            this.stopDownloadToolStripMenuItem,
            this.redownloadToolStripMenuItem,
            this.toolStripSeparator4,
            this.userCommandsToolStripMenuItem});
            this.rowsContextMenuStrip.Name = "rowsContextMenuStrip";
            this.rowsContextMenuStrip.Size = new System.Drawing.Size(173, 226);
            this.rowsContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.rowsContextMenuStrip_Opening);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Image = global::DeanCC.Properties.Resources.folder;
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openFolderToolStripMenuItem.Text = "フォルダーを開く";
            this.openFolderToolStripMenuItem.ToolTipText = "ダウンロードした画像のある保存フォルダーを開きます";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // openThreadToolStripMenuItem
            // 
            this.openThreadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.localToolStripMenuItem,
            this.janeToolStripMenuItem});
            this.openThreadToolStripMenuItem.Image = global::DeanCC.Properties.Resources.application_form;
            this.openThreadToolStripMenuItem.Name = "openThreadToolStripMenuItem";
            this.openThreadToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openThreadToolStripMenuItem.Text = "ブラウザーで開く";
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Image = global::DeanCC.Properties.Resources.world_go;
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.onlineToolStripMenuItem.Text = "オンライン";
            this.onlineToolStripMenuItem.ToolTipText = "このスレッドURLをWEBブラウザーで開きます";
            this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
            // 
            // localToolStripMenuItem
            // 
            this.localToolStripMenuItem.Image = global::DeanCC.Properties.Resources.html;
            this.localToolStripMenuItem.Name = "localToolStripMenuItem";
            this.localToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.localToolStripMenuItem.Text = "ローカル";
            this.localToolStripMenuItem.ToolTipText = "画像を埋め込んだHTMLファイルを表示します。";
            this.localToolStripMenuItem.Click += new System.EventHandler(this.localToolStripMenuItem_Click);
            // 
            // janeToolStripMenuItem
            // 
            this.janeToolStripMenuItem.Image = global::DeanCC.Properties.Resources.application_go;
            this.janeToolStripMenuItem.Name = "janeToolStripMenuItem";
            this.janeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.janeToolStripMenuItem.Text = "JaneStyleで開く";
            this.janeToolStripMenuItem.ToolTipText = "このスレッドURLをJaneStyleで開きます";
            this.janeToolStripMenuItem.Click += new System.EventHandler(this.janeToolStripMenuItem_Click);
            // 
            // viewImagesToolStripMenuItem
            // 
            this.viewImagesToolStripMenuItem.Image = global::DeanCC.Properties.Resources.pictures;
            this.viewImagesToolStripMenuItem.Name = "viewImagesToolStripMenuItem";
            this.viewImagesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.viewImagesToolStripMenuItem.Text = "画像情報を表示";
            this.viewImagesToolStripMenuItem.ToolTipText = "このスレッドの画像に関する詳細情報を表示します";
            this.viewImagesToolStripMenuItem.Click += new System.EventHandler(this.viewImagesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.urlToolStripMenuItem,
            this.titleToolStripMenuItem,
            this.titleAndUrlToolStripMenuItem});
            this.copyToolStripMenuItem.Image = global::DeanCC.Properties.Resources.page_copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.copyToolStripMenuItem.Text = "コピー";
            // 
            // urlToolStripMenuItem
            // 
            this.urlToolStripMenuItem.Name = "urlToolStripMenuItem";
            this.urlToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.urlToolStripMenuItem.Text = "URL";
            this.urlToolStripMenuItem.Click += new System.EventHandler(this.urlToolStripMenuItem_Click);
            // 
            // titleToolStripMenuItem
            // 
            this.titleToolStripMenuItem.Name = "titleToolStripMenuItem";
            this.titleToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.titleToolStripMenuItem.Text = "タイトル";
            this.titleToolStripMenuItem.Click += new System.EventHandler(this.titleToolStripMenuItem_Click);
            // 
            // titleAndUrlToolStripMenuItem
            // 
            this.titleAndUrlToolStripMenuItem.Name = "titleAndUrlToolStripMenuItem";
            this.titleAndUrlToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.titleAndUrlToolStripMenuItem.Text = "タイトルとURL";
            this.titleAndUrlToolStripMenuItem.Click += new System.EventHandler(this.titleAndUrlToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::DeanCC.Properties.Resources.cross;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.removeToolStripMenuItem.Text = "削除";
            this.removeToolStripMenuItem.ToolTipText = "このスレッドを除外します。\r\nすでに除外されている場合は、削除して自動取得可能にします。";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
            // 
            // startDownloadToolStripMenuItem
            // 
            this.startDownloadToolStripMenuItem.Image = global::DeanCC.Properties.Resources.page_go;
            this.startDownloadToolStripMenuItem.Name = "startDownloadToolStripMenuItem";
            this.startDownloadToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.startDownloadToolStripMenuItem.Text = "ダウンロード開始";
            this.startDownloadToolStripMenuItem.ToolTipText = "画像ダウンロードを開始します。";
            this.startDownloadToolStripMenuItem.Click += new System.EventHandler(this.startDownloadToolStripMenuItem_Click);
            // 
            // stopDownloadToolStripMenuItem
            // 
            this.stopDownloadToolStripMenuItem.Image = global::DeanCC.Properties.Resources.page_red;
            this.stopDownloadToolStripMenuItem.Name = "stopDownloadToolStripMenuItem";
            this.stopDownloadToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.stopDownloadToolStripMenuItem.Text = "ダウンロード中止";
            this.stopDownloadToolStripMenuItem.ToolTipText = "画像ダウンロードを中止します。\r\nこの操作が完了するまで時間がかかる場合もあります。";
            this.stopDownloadToolStripMenuItem.Click += new System.EventHandler(this.stopDownloadToolStripMenuItem_Click);
            // 
            // redownloadToolStripMenuItem
            // 
            this.redownloadToolStripMenuItem.Image = global::DeanCC.Properties.Resources.page_lightning;
            this.redownloadToolStripMenuItem.Name = "redownloadToolStripMenuItem";
            this.redownloadToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.redownloadToolStripMenuItem.Text = "強制ダウンロード";
            this.redownloadToolStripMenuItem.ToolTipText = "既にダウンロードに失敗した画像を再ダウンロードします。";
            this.redownloadToolStripMenuItem.Click += new System.EventHandler(this.redownloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
            // 
            // userCommandsToolStripMenuItem
            // 
            this.userCommandsToolStripMenuItem.Image = global::DeanCC.Properties.Resources.application_lightning;
            this.userCommandsToolStripMenuItem.Name = "userCommandsToolStripMenuItem";
            this.userCommandsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.userCommandsToolStripMenuItem.Text = "コマンド";
            this.userCommandsToolStripMenuItem.ToolTipText = "オプションで設定できるコマンドです";
            // 
            // ThreadView
            // 
            this.ContextMenuStrip = this.rowsContextMenuStrip;
            this.RowTemplate.Height = 18;
            this.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ThreadView_CellDoubleClick);
            this.rowsContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        void viewImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindingImageHeaderCollection images = new BindingImageHeaderCollection();
            ForEachSelectedThreads(thread =>
                {
                    if (!thread.DownloadCompleted)
                    {
                        foreach (ImageHeader image in thread.ImageHeaders)
                        {
                            image.SourceThreadTitle = thread.Title;
                            images.Add(image);
                        }
                        //foreach (ImageHeader maybeImage in thread.MaybeImageHeaders)
                        //{
                        //    maybeImage.SourceThreadTitle = thread.Title;
                        //    images.Add(maybeImage);
                        //}
                    }
                });
            if (images.Count > 0)
            {
                ImageHeaderForm imageViewer = new ImageHeaderForm(images);
                imageViewer.ShowDialog();
            }
            else
            {
                MessageBox.Show("ダウンロード完了後は画像情報を表示できません",
                    "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(GetSelectedThreadsName() + "を削除して、自動取得から除外しますか？", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ForEachSelectedThreads(thread =>
                {
                    if (CurrentThreads is ExcludedThreadCollection)
                    {
                        Common.CurrentSettings.AllThreads.Remove(thread);
                        DeleteThreadFolderProcess(thread);
                    }
                    else if (!thread.Header.IsIgnored)
                    {
                        thread.CancelDownload();
                        thread.Header.IsIgnored = true;
                        DeleteThreadFolderProcess(thread);
                    }
                });
                Common.UpdateThreads();
            }
        }

        void DeleteThreadFolderProcess(Thread thread)
        {
            if (Directory.Exists(thread.Header.ImageSaveFolder))
            {
                try
                {
                    thread.DeleteThreadFolder();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string GetSelectedThreadsName()
        {
            if (SelectedRows.Count <= 0)
            {
                throw new InvalidOperationException("スレッドが選択されていません");
            }
            else if (SelectedRows.Count == 1)
            {
                return ((Thread)SelectedRows[0].DataBoundItem).Title;
            }
            else
            {
                return "選択された" + SelectedRows.Count + "個のスレッド";
            }
        }

        void redownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = (Thread)SelectedRows[0].DataBoundItem;
            thread.ImageHeaders.ToList().ForEach(image =>
            {
                if (!image.DownloadCompleted && !image.Downloadable)
                {
                    image.ResetState();
                }
            });
            startDownloadToolStripMenuItem.PerformClick();
        }

        void stopDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForEachSelectedThreads(thread => thread.CancelDownload());
        }

        void startDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = (Thread)SelectedRows[0].DataBoundItem;
            if (thread.Downloadable)
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {

                    OnDownloading(EventArgs.Empty);
                    try
                    {
                        thread.Run();
                    }
                    catch (IOException ex)
                    {
                        Common.Logs.Add("保存エラー", ex.Message, LogStatus.Error);
                        MessageBox.Show(ex.Message + "\nダウンロードを中断しました。", "保存失敗",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (System.Net.WebException ex)
                    {
                        Common.Logs.Add("通信エラー", ex.Message, LogStatus.Error);
                        MessageBox.Show(ex.Message + "\nダウンロードを中断しました。", "ダウンロード失敗",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    OnDownloaded(EventArgs.Empty);
                });
            }
            else
            {
                MessageBox.Show("ダウンロードできません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void OnDownloading(EventArgs e)
        {
            if (Downloading != null)
            {
                Downloading(this, e);
            }
        }

        private void OnDownloaded(EventArgs e)
        {
            if (Downloaded != null)
            {
                Downloaded(this, e);
            }
        }

        void titleAndUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder text = new StringBuilder();
            ForEachSelectedThreads(thread =>
                text.AppendLine(thread.Header.Title + Environment.NewLine + thread.Header.Url));
            Clipboard.SetText(text.ToString());
        }

        void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder text = new StringBuilder();
            ForEachSelectedThreads(thread => text.AppendLine(thread.Header.Title));
            Clipboard.SetText(text.ToString());
        }

        void urlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder text = new StringBuilder();
            ForEachSelectedThreads(thread => text.AppendLine(thread.Header.Url));
            Clipboard.SetText(text.ToString());
        }

        void janeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Thread> threads = new List<Thread>();
            ForEachSelectedThreads(thread =>
            {
                threads.Add(thread);
            });
            ProcessUtility.OpenThreads(threads.ToArray());
        }

        void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForEachSelectedThreads(thread =>
            {
                if (CurrentThreads is SecureThreadCollection && File.Exists(thread.LogFilePath))
                {
                    ProcessUtility.OpenUrl(thread.SaveSecureImageLogHtml());
                }
                else if (thread.DownloadCompleted && File.Exists(thread.HtmlFilePath))
                {
                    ProcessUtility.OpenUrl(thread.HtmlFilePath);
                }
                else if (!thread.DownloadCompleted && File.Exists(thread.LogFilePath))
                {
                    ProcessUtility.OpenUrl(thread.SaveLogHtml());
                }
                else
                {
                    MessageBox.Show(thread.Title +
                        "\nHtmlファイルを作成できませんでした。\nDatファイルを保存する設定に変更してください。",
                        "Html作成失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            });
        }

        void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForEachSelectedThreads(thread => ProcessUtility.OpenUrl(thread.Header.Url));
        }

        void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForEachSelectedThreads(thread => ProcessUtility.OpenFolder(thread.Header.ImageSaveFolder));
        }

        private void ThreadView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rowsContextMenuStrip.Items.ContainsKey(Common.Options.ThreadViewOptions.DoubleClickPerformItemName))
            {
                rowsContextMenuStrip.Items[Common.Options.ThreadViewOptions.DoubleClickPerformItemName].PerformClick();
                return;
            }
            else
            {
                foreach (ToolStripItem item in rowsContextMenuStrip.Items)
                {
                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    if (menuItem != null &&
                        menuItem.HasDropDownItems &&
                        menuItem.DropDownItems.ContainsKey(Common.Options.ThreadViewOptions.DoubleClickPerformItemName))
                    {
                        menuItem.DropDownItems[Common.Options.ThreadViewOptions.DoubleClickPerformItemName].PerformClick();
                        return;
                    }
                }
            }
            MessageBox.Show("コマンド:" + Common.Options.ThreadViewOptions.DoubleClickPerformItemName +
                "が見つかりませんでした。\n再度オプションから設定しなおしてください。",
                "コマンド実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }        

        private void rowsContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SelectedRows.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
            //
            //ToDo: 状態によって項目の有効を設定する
            //
            janeToolStripMenuItem.Enabled = File.Exists(Common.Options.BrowsersOptions.JaneOptions.JanePath);
            removeToolStripMenuItem.Enabled = !Common.IsPatrolling;
            redownloadToolStripMenuItem.Enabled = startDownloadToolStripMenuItem.Enabled =
                (CurrentThreads is DownloadingThreadCollection && !Common.IsPatrolling);
        }
    }
}
