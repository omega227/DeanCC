namespace DeanCC.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainNotifyIconContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.existToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPatrolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoPatrolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.patternEditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.searchToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addThreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.timeStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ThreadCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.threadListControl1 = new DeanCC.GUI.ThreadListControl();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.mainNotifyIconContextMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.ContextMenuStrip = this.mainNotifyIconContextMenuStrip;
            this.mainNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("mainNotifyIcon.Icon")));
            this.mainNotifyIcon.Text = "DeanCC";
            this.mainNotifyIcon.Visible = true;
            this.mainNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainNotifyIcon_MouseClick);
            this.mainNotifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainNotifyIcon_MouseMove);
            // 
            // mainNotifyIconContextMenuStrip
            // 
            this.mainNotifyIconContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.existToolStripMenuItem});
            this.mainNotifyIconContextMenuStrip.Name = "mainNotifyIconContextMenuStrip";
            this.mainNotifyIconContextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // existToolStripMenuItem
            // 
            this.existToolStripMenuItem.Image = global::DeanCC.Properties.Resources.exclamation;
            this.existToolStripMenuItem.Name = "existToolStripMenuItem";
            this.existToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.existToolStripMenuItem.Text = "終了";
            this.existToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.BackColor = System.Drawing.SystemColors.Window;
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(682, 451);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.threadToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(680, 26);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPatrolToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.fileToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // editPatrolToolStripMenuItem
            // 
            this.editPatrolToolStripMenuItem.Image = global::DeanCC.Properties.Resources.page_add;
            this.editPatrolToolStripMenuItem.Name = "editPatrolToolStripMenuItem";
            this.editPatrolToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.editPatrolToolStripMenuItem.Text = "巡回設定を編集(&E)";
            this.editPatrolToolStripMenuItem.ToolTipText = "自動でスレッドを取得する設定を編集します";
            this.editPatrolToolStripMenuItem.Click += new System.EventHandler(this.editPatrolToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(174, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::DeanCC.Properties.Resources.exclamation;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitToolStripMenuItem.Text = "終了(&X)";
            this.exitToolStripMenuItem.ToolTipText = "このアプリケーションを終了します";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuListToolStripMenuItem,
            this.toolBarToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
            this.viewToolStripMenuItem.Text = "表示(&V)";
            // 
            // menuListToolStripMenuItem
            // 
            this.menuListToolStripMenuItem.CheckOnClick = true;
            this.menuListToolStripMenuItem.Name = "menuListToolStripMenuItem";
            this.menuListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.menuListToolStripMenuItem.Text = "メニューリスト(&M)";
            this.menuListToolStripMenuItem.CheckedChanged += new System.EventHandler(this.menuListToolStripMenuItem_CheckedChanged);
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toolBarToolStripMenuItem.Text = "ツールバー(&T)";
            this.toolBarToolStripMenuItem.CheckedChanged += new System.EventHandler(this.toolBarToolStripMenuItem_CheckedChanged);
            // 
            // threadToolStripMenuItem
            // 
            this.threadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoPatrolToolStripMenuItem});
            this.threadToolStripMenuItem.Name = "threadToolStripMenuItem";
            this.threadToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.threadToolStripMenuItem.Text = "スレッド(&T)";
            // 
            // autoPatrolToolStripMenuItem
            // 
            this.autoPatrolToolStripMenuItem.CheckOnClick = true;
            this.autoPatrolToolStripMenuItem.Name = "autoPatrolToolStripMenuItem";
            this.autoPatrolToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.autoPatrolToolStripMenuItem.Text = "自動巡回(&A)";
            this.autoPatrolToolStripMenuItem.ToolTipText = "自動でスレッド取得・画像ダウンロードを実行し続けます";
            this.autoPatrolToolStripMenuItem.CheckedChanged += new System.EventHandler(this.autoPatrolToolStripMenuItem_CheckedChanged);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clipboardToolStripMenuItem,
            this.toolStripSeparator4,
            this.optionToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(75, 22);
            this.toolToolStripMenuItem.Text = "ツール(&O)";
            // 
            // clipboardToolStripMenuItem
            // 
            this.clipboardToolStripMenuItem.CheckOnClick = true;
            this.clipboardToolStripMenuItem.Name = "clipboardToolStripMenuItem";
            this.clipboardToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.clipboardToolStripMenuItem.Text = "クリップボード監視(&C)";
            this.clipboardToolStripMenuItem.ToolTipText = "コピーしたスレッドURLを追加します";
            this.clipboardToolStripMenuItem.CheckedChanged += new System.EventHandler(this.clipboardToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(199, 6);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Image = global::DeanCC.Properties.Resources.wrench;
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.optionToolStripMenuItem.Text = "オプション(&O)";
            this.optionToolStripMenuItem.ToolTipText = "設定ウィンドウを開きます";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpViewToolStripMenuItem,
            this.toolStripSeparator3,
            this.versionToolStripMenuItem,
            this.updateToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(75, 22);
            this.helpToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // helpViewToolStripMenuItem
            // 
            this.helpViewToolStripMenuItem.Image = global::DeanCC.Properties.Resources.help;
            this.helpViewToolStripMenuItem.Name = "helpViewToolStripMenuItem";
            this.helpViewToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.helpViewToolStripMenuItem.Text = "ヘルプ(&H)";
            this.helpViewToolStripMenuItem.ToolTipText = "オンラインドキュメントを開きます。";
            this.helpViewToolStripMenuItem.Click += new System.EventHandler(this.helpViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(164, 6);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Image = global::DeanCC.Properties.Resources.application_form_magnify;
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.versionToolStripMenuItem.Text = "バージョン(&V)";
            this.versionToolStripMenuItem.ToolTipText = "このアプリケーションのバージョン情報を表示します";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Image = global::DeanCC.Properties.Resources.arrow_refresh;
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.updateToolStripMenuItem.Text = "最新版を確認(&U)";
            this.updateToolStripMenuItem.ToolTipText = "新しいバージョンを確認して、必要な場合はアップデートします。";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patternEditToolStripButton,
            this.settingsToolStripButton,
            this.toolStripSeparator1,
            this.searchToolStripTextBox,
            this.searchToolStripSplitButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 26);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(680, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // patternEditToolStripButton
            // 
            this.patternEditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.patternEditToolStripButton.Image = global::DeanCC.Properties.Resources.page_add;
            this.patternEditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.patternEditToolStripButton.Name = "patternEditToolStripButton";
            this.patternEditToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.patternEditToolStripButton.Text = "toolStripButton2";
            this.patternEditToolStripButton.ToolTipText = "巡回設定を編集";
            this.patternEditToolStripButton.Click += new System.EventHandler(this.patternEditToolStripButton_Click);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsToolStripButton.Image = global::DeanCC.Properties.Resources.wrench;
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.settingsToolStripButton.Text = "toolStripButton3";
            this.settingsToolStripButton.ToolTipText = "オプション";
            this.settingsToolStripButton.Click += new System.EventHandler(this.settingsToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // searchToolStripTextBox
            // 
            this.searchToolStripTextBox.Name = "searchToolStripTextBox";
            this.searchToolStripTextBox.Size = new System.Drawing.Size(350, 25);
            this.searchToolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchToolStripTextBox_KeyDown);
            // 
            // searchToolStripSplitButton
            // 
            this.searchToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripMenuItem,
            this.addThreadToolStripMenuItem});
            this.searchToolStripSplitButton.Image = global::DeanCC.Properties.Resources.magnifier;
            this.searchToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchToolStripSplitButton.Name = "searchToolStripSplitButton";
            this.searchToolStripSplitButton.Size = new System.Drawing.Size(32, 22);
            this.searchToolStripSplitButton.Text = "toolStripSplitButton1";
            this.searchToolStripSplitButton.ToolTipText = "検索";
            this.searchToolStripSplitButton.ButtonClick += new System.EventHandler(this.searchToolStripSplitButton_ButtonClick);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Image = global::DeanCC.Properties.Resources.page_white_magnify;
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.filterToolStripMenuItem.Text = "絞り込み検索";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // addThreadToolStripMenuItem
            // 
            this.addThreadToolStripMenuItem.Image = global::DeanCC.Properties.Resources.magnifier_zoom_in;
            this.addThreadToolStripMenuItem.Name = "addThreadToolStripMenuItem";
            this.addThreadToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addThreadToolStripMenuItem.Text = "スレッド追加";
            this.addThreadToolStripMenuItem.Click += new System.EventHandler(this.addThreadToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeStripStatusLabel,
            this.mainStatusLabel,
            this.ThreadCountStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 473);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(680, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // timeStripStatusLabel
            // 
            this.timeStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.timeStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.timeStripStatusLabel.Name = "timeStripStatusLabel";
            this.timeStripStatusLabel.Size = new System.Drawing.Size(4, 17);
            this.timeStripStatusLabel.ToolTipText = "次のスレッド取得・画像ダウンロードを開始するまでの時間（分：秒）です";
            // 
            // mainStatusLabel
            // 
            this.mainStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mainStatusLabel.Name = "mainStatusLabel";
            this.mainStatusLabel.Size = new System.Drawing.Size(626, 17);
            this.mainStatusLabel.Spring = true;
            this.mainStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ThreadCountStatusLabel
            // 
            this.ThreadCountStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.ThreadCountStatusLabel.Name = "ThreadCountStatusLabel";
            this.ThreadCountStatusLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.mainPanel);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.controlPanel);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(680, 397);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 51);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(680, 422);
            this.toolStripContainer1.TabIndex = 5;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(180, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(3);
            this.mainPanel.Size = new System.Drawing.Size(500, 397);
            this.mainPanel.TabIndex = 1;
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.threadListControl1);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Padding = new System.Windows.Forms.Padding(3);
            this.controlPanel.Size = new System.Drawing.Size(180, 397);
            this.controlPanel.TabIndex = 0;
            // 
            // threadListControl1
            // 
            this.threadListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.threadListControl1.Location = new System.Drawing.Point(3, 3);
            this.threadListControl1.Name = "threadListControl1";
            this.threadListControl1.SelectedMenu = DeanCCCore.Core.SelectedThreadListMenu.None;
            this.threadListControl1.Size = new System.Drawing.Size(174, 391);
            this.threadListControl1.TabIndex = 0;
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 495);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dean CC";
            this.mainNotifyIconContextMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon mainNotifyIcon;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton patternEditToolStripButton;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox searchToolStripTextBox;
        private System.Windows.Forms.ToolStripSplitButton searchToolStripSplitButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addThreadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPatrolToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoPatrolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel mainStatusLabel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripStatusLabel timeStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel ThreadCountStatusLabel;
        private System.Windows.Forms.ContextMenuStrip mainNotifyIconContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem existToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private ThreadListControl threadListControl1;
    }
}

