namespace DeanCC.GUI
{
    partial class TargetBoardEditForm
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
            this.boardTableTreeView1 = new DeanCC.GUI.BoardTableTreeView();
            this.SuspendLayout();
            // 
            // boardTableTreeView1
            // 
            this.boardTableTreeView1.ImageIndex = 0;
            this.boardTableTreeView1.Location = new System.Drawing.Point(133, 31);
            this.boardTableTreeView1.Name = "boardTableTreeView1";
            this.boardTableTreeView1.SelectedBoard = null;
            this.boardTableTreeView1.SelectedImageIndex = 0;
            this.boardTableTreeView1.Size = new System.Drawing.Size(121, 97);
            this.boardTableTreeView1.TabIndex = 0;
            // 
            // TargetBoardEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.boardTableTreeView1);
            this.Name = "TargetBoardEditForm";
            this.Text = "TargetBoardEditForm";
            this.ResumeLayout(false);

        }

        #endregion

        private BoardTableTreeView boardTableTreeView1;
    }
}