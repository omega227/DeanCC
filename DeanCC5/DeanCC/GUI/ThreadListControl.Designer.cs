namespace DeanCC.GUI
{
    partial class ThreadListControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("すべて", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("高速ダウンロード中", 11, 11);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ダウンロード中", 2, 2);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("一時停止", 3, 3);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ダウンロード完了", 0, 0);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("除外", 5, 5);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("スレッド", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("未取得", 7, 7);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("画像", 6, 6, new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("ログ", 9, 9);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("情報");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("詳細", 8, 8, new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreadListControl));
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList1;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "AllThreadNode";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "すべて";
            treeNode2.ImageIndex = 11;
            treeNode2.Name = "QuickDownloadingThreadNode";
            treeNode2.SelectedImageIndex = 11;
            treeNode2.Text = "高速ダウンロード中";
            treeNode3.ImageIndex = 2;
            treeNode3.Name = "DownloadingThreadNode";
            treeNode3.SelectedImageIndex = 2;
            treeNode3.Text = "ダウンロード中";
            treeNode4.ImageIndex = 3;
            treeNode4.Name = "DownloadPausedThreadNode";
            treeNode4.SelectedImageIndex = 3;
            treeNode4.Text = "一時停止";
            treeNode5.ImageIndex = 0;
            treeNode5.Name = "DownloadedThreadNode";
            treeNode5.SelectedImageIndex = 0;
            treeNode5.Text = "ダウンロード完了";
            treeNode6.ImageIndex = 5;
            treeNode6.Name = "ExcludedThreadNode";
            treeNode6.SelectedImageIndex = 5;
            treeNode6.Text = "除外";
            treeNode7.ImageIndex = 0;
            treeNode7.Name = "ThreadNode";
            treeNode7.SelectedImageIndex = 0;
            treeNode7.Text = "スレッド";
            treeNode8.ImageIndex = 7;
            treeNode8.Name = "SecureResNode";
            treeNode8.SelectedImageIndex = 7;
            treeNode8.Text = "未取得";
            treeNode9.ImageIndex = 6;
            treeNode9.Name = "ImageNode";
            treeNode9.SelectedImageIndex = 6;
            treeNode9.Text = "画像";
            treeNode10.ImageIndex = 9;
            treeNode10.Name = "LogNode";
            treeNode10.SelectedImageIndex = 9;
            treeNode10.Text = "ログ";
            treeNode11.ImageKey = "information.png";
            treeNode11.Name = "InformationNode";
            treeNode11.SelectedImageKey = "information.png";
            treeNode11.Text = "情報";
            treeNode12.ImageIndex = 8;
            treeNode12.Name = "DetalisNode";
            treeNode12.SelectedImageIndex = 8;
            treeNode12.Text = "詳細";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode9,
            treeNode12});
            this.treeView.Scrollable = false;
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(150, 150);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table.png");
            this.imageList1.Images.SetKeyName(1, "table_multiple.png");
            this.imageList1.Images.SetKeyName(2, "table_refresh.png");
            this.imageList1.Images.SetKeyName(3, "table_lightning.png");
            this.imageList1.Images.SetKeyName(4, "table.png");
            this.imageList1.Images.SetKeyName(5, "table_delete.png");
            this.imageList1.Images.SetKeyName(6, "image.png");
            this.imageList1.Images.SetKeyName(7, "table_error.png");
            this.imageList1.Images.SetKeyName(8, "page_white_magnify.png");
            this.imageList1.Images.SetKeyName(9, "page_white_text.png");
            this.imageList1.Images.SetKeyName(10, "information.png");
            this.imageList1.Images.SetKeyName(11, "table_gear.png");
            // 
            // ThreadListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "ThreadListControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ImageList imageList1;
    }
}
