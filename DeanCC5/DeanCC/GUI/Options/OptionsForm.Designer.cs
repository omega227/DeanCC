namespace DeanCC.GUI.Options
{
    partial class OptionsForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("画像保存");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("NG");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ウィンドウ");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("スタートアップ");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("基本", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("スレッドリスト");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("外観", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("通信");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("通知");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("コマンド");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("個別追加スレッド");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("DAT");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("ブラウザー");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("ZIP");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("詳細", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            this.controlTreeView = new System.Windows.Forms.TreeView();
            this.SelectedOptionsPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.separatorLable1 = new DeanCC.GUI.SeparatorLable();
            this.SuspendLayout();
            // 
            // controlTreeView
            // 
            this.controlTreeView.Location = new System.Drawing.Point(12, 12);
            this.controlTreeView.Name = "controlTreeView";
            treeNode1.Name = "ImageSaveOptions";
            treeNode1.Text = "画像保存";
            treeNode2.Name = "NGOptions";
            treeNode2.Text = "NG";
            treeNode3.Name = "WindowOptions";
            treeNode3.Text = "ウィンドウ";
            treeNode4.Name = "StartupOptions";
            treeNode4.Text = "スタートアップ";
            treeNode5.Name = "ノード0";
            treeNode5.Text = "基本";
            treeNode6.Name = "ThreadViewOptions";
            treeNode6.Text = "スレッドリスト";
            treeNode7.Name = "ノード1";
            treeNode7.Text = "外観";
            treeNode8.Name = "InternetOptions";
            treeNode8.Text = "通信";
            treeNode9.Name = "MessageOptions";
            treeNode9.Text = "通知";
            treeNode10.Name = "CommandOptions";
            treeNode10.Text = "コマンド";
            treeNode11.Name = "IndividualThreadOptions";
            treeNode11.Text = "個別追加スレッド";
            treeNode12.Name = "DatOptions";
            treeNode12.Text = "DAT";
            treeNode13.Name = "BrowserOptions";
            treeNode13.Text = "ブラウザー";
            treeNode14.Name = "ZipOptions";
            treeNode14.Text = "ZIP";
            treeNode15.Name = "ノード2";
            treeNode15.Text = "詳細";
            this.controlTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7,
            treeNode15});
            this.controlTreeView.Size = new System.Drawing.Size(149, 308);
            this.controlTreeView.TabIndex = 0;
            this.controlTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.controlTreeView_AfterSelect);
            // 
            // SelectedOptionsPanel
            // 
            this.SelectedOptionsPanel.Location = new System.Drawing.Point(167, 12);
            this.SelectedOptionsPanel.Name = "SelectedOptionsPanel";
            this.SelectedOptionsPanel.Size = new System.Drawing.Size(460, 308);
            this.SelectedOptionsPanel.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(471, 343);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(552, 343);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // separatorLable1
            // 
            this.separatorLable1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separatorLable1.Location = new System.Drawing.Point(12, 332);
            this.separatorLable1.Name = "separatorLable1";
            this.separatorLable1.Size = new System.Drawing.Size(616, 2);
            this.separatorLable1.TabIndex = 4;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(639, 371);
            this.Controls.Add(this.separatorLable1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.SelectedOptionsPanel);
            this.Controls.Add(this.controlTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "オプション";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.Shown += new System.EventHandler(this.OptionsForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView controlTreeView;
        private System.Windows.Forms.Panel SelectedOptionsPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private SeparatorLable separatorLable1;
    }
}