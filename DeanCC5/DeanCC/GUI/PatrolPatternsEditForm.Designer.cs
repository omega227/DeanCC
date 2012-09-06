namespace DeanCC.GUI
{
    partial class PatrolPatternsEditForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.addPatternToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RemovePatternToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DownToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.patrolPatternPanel = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.boardTableVisibleButton = new System.Windows.Forms.Button();
            this.separatorLable1 = new DeanCC.GUI.SeparatorLable();
            this.patrolPatternEditControl1 = new DeanCC.GUI.PatrolPatternEditControl();
            this.patrolPatternTreeView1 = new DeanCC.GUI.PatrolPatternTreeView();
            this.boardTableTreeView = new DeanCC.GUI.BoardTableTreeView();
            this.toolStrip1.SuspendLayout();
            this.patrolPatternPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFolderToolStripButton,
            this.addPatternToolStripButton,
            this.RemovePatternToolStripButton,
            this.toolStripSeparator1,
            this.UpToolStripButton,
            this.DownToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(642, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addFolderToolStripButton
            // 
            this.addFolderToolStripButton.Image = global::DeanCC.Properties.Resources.folder_add;
            this.addFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addFolderToolStripButton.Name = "addFolderToolStripButton";
            this.addFolderToolStripButton.Size = new System.Drawing.Size(148, 22);
            this.addFolderToolStripButton.Text = "保存フォルダーを追加";
            this.addFolderToolStripButton.Click += new System.EventHandler(this.addFolderToolStripButton_Click);
            // 
            // addPatternToolStripButton
            // 
            this.addPatternToolStripButton.Image = global::DeanCC.Properties.Resources.add;
            this.addPatternToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addPatternToolStripButton.Name = "addPatternToolStripButton";
            this.addPatternToolStripButton.Size = new System.Drawing.Size(112, 22);
            this.addPatternToolStripButton.Text = "巡回設定を追加";
            this.addPatternToolStripButton.Click += new System.EventHandler(this.addPatternToolStripButton_Click);
            // 
            // RemovePatternToolStripButton
            // 
            this.RemovePatternToolStripButton.Image = global::DeanCC.Properties.Resources.delete;
            this.RemovePatternToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemovePatternToolStripButton.Name = "RemovePatternToolStripButton";
            this.RemovePatternToolStripButton.Size = new System.Drawing.Size(112, 22);
            this.RemovePatternToolStripButton.Text = "巡回設定を削除";
            this.RemovePatternToolStripButton.Click += new System.EventHandler(this.RemovePatternToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // UpToolStripButton
            // 
            this.UpToolStripButton.Image = global::DeanCC.Properties.Resources.arrow_up;
            this.UpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpToolStripButton.Name = "UpToolStripButton";
            this.UpToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.UpToolStripButton.Text = "上へ移動";
            this.UpToolStripButton.Click += new System.EventHandler(this.UpToolStripButton_Click);
            // 
            // DownToolStripButton
            // 
            this.DownToolStripButton.Image = global::DeanCC.Properties.Resources.arrow_down;
            this.DownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DownToolStripButton.Name = "DownToolStripButton";
            this.DownToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.DownToolStripButton.Text = "下へ移動";
            this.DownToolStripButton.Click += new System.EventHandler(this.DownToolStripButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(474, 374);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(555, 374);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "優先度（高）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "優先度（低）";
            // 
            // patrolPatternPanel
            // 
            this.patrolPatternPanel.Controls.Add(this.patrolPatternEditControl1);
            this.patrolPatternPanel.Location = new System.Drawing.Point(194, 28);
            this.patrolPatternPanel.Name = "patrolPatternPanel";
            this.patrolPatternPanel.Size = new System.Drawing.Size(332, 311);
            this.patrolPatternPanel.TabIndex = 6;
            this.patrolPatternPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.patrolPatternPanel_ControlAdded);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "追加する保存フォルダーを指定して下さい";
            // 
            // boardTableVisibleButton
            // 
            this.boardTableVisibleButton.Location = new System.Drawing.Point(555, 50);
            this.boardTableVisibleButton.Name = "boardTableVisibleButton";
            this.boardTableVisibleButton.Size = new System.Drawing.Size(75, 23);
            this.boardTableVisibleButton.TabIndex = 7;
            this.boardTableVisibleButton.Text = "板一覧";
            this.boardTableVisibleButton.UseVisualStyleBackColor = true;
            this.boardTableVisibleButton.Visible = false;
            this.boardTableVisibleButton.Click += new System.EventHandler(this.boardTableVisibleButton_Click);
            // 
            // separatorLable1
            // 
            this.separatorLable1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separatorLable1.Location = new System.Drawing.Point(12, 360);
            this.separatorLable1.Name = "separatorLable1";
            this.separatorLable1.Size = new System.Drawing.Size(618, 2);
            this.separatorLable1.TabIndex = 9;
            this.separatorLable1.Text = "separatorLable1";
            // 
            // patrolPatternEditControl1
            // 
            this.patrolPatternEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patrolPatternEditControl1.Location = new System.Drawing.Point(0, 0);
            this.patrolPatternEditControl1.Name = "patrolPatternEditControl1";
            this.patrolPatternEditControl1.Size = new System.Drawing.Size(332, 311);
            this.patrolPatternEditControl1.TabIndex = 0;
            // 
            // patrolPatternTreeView1
            // 
            this.patrolPatternTreeView1.ImageIndex = 0;
            this.patrolPatternTreeView1.Location = new System.Drawing.Point(12, 50);
            this.patrolPatternTreeView1.Name = "patrolPatternTreeView1";
            this.patrolPatternTreeView1.SelectedImageIndex = 0;
            this.patrolPatternTreeView1.Size = new System.Drawing.Size(176, 284);
            this.patrolPatternTreeView1.TabIndex = 3;
            this.patrolPatternTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.patrolPatternTreeView1_AfterSelect);
            // 
            // boardTableTreeView
            // 
            this.boardTableTreeView.ImageIndex = 0;
            this.boardTableTreeView.Location = new System.Drawing.Point(444, 84);
            this.boardTableTreeView.Name = "boardTableTreeView";
            this.boardTableTreeView.SelectedBoard = null;
            this.boardTableTreeView.SelectedImageIndex = 0;
            this.boardTableTreeView.Size = new System.Drawing.Size(186, 255);
            this.boardTableTreeView.TabIndex = 8;
            this.boardTableTreeView.Visible = false;
            this.boardTableTreeView.BoardSelected += new System.EventHandler<DeanCC.GUI.BoardTableTreeViewItemSelectedEventArgs>(this.boardTableTreeView_BoardSelected);
            // 
            // PatrolPatternsEditForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(642, 409);
            this.Controls.Add(this.boardTableTreeView);
            this.Controls.Add(this.separatorLable1);
            this.Controls.Add(this.boardTableVisibleButton);
            this.Controls.Add(this.patrolPatternPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.patrolPatternTreeView1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatrolPatternsEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "巡回設定を編集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatrolPatternsEditForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatrolPatternsEditForm_FormClosed);
            this.Load += new System.EventHandler(this.PatrolPatternsEditForm_Load);
            this.Shown += new System.EventHandler(this.PatrolPatternsEditForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.patrolPatternPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addFolderToolStripButton;
        private System.Windows.Forms.ToolStripButton addPatternToolStripButton;
        private System.Windows.Forms.ToolStripButton RemovePatternToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton UpToolStripButton;
        private System.Windows.Forms.ToolStripButton DownToolStripButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private PatrolPatternTreeView patrolPatternTreeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel patrolPatternPanel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button boardTableVisibleButton;
        private BoardTableTreeView boardTableTreeView;
        private PatrolPatternEditControl patrolPatternEditControl1;
        private SeparatorLable separatorLable1;

    }
}