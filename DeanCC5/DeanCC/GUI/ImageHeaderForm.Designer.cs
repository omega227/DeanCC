namespace DeanCC.GUI
{
    partial class ImageHeaderForm
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
            this.imageHeaderView1 = new DeanCC.GUI.ImageHeaderView();
            ((System.ComponentModel.ISupportInitialize)(this.imageHeaderView1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageHeaderView1
            // 
            this.imageHeaderView1.AllowUserToAddRows = false;
            this.imageHeaderView1.AllowUserToDeleteRows = false;
            this.imageHeaderView1.AllowUserToOrderColumns = true;
            this.imageHeaderView1.AllowUserToResizeRows = false;
            this.imageHeaderView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.imageHeaderView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.imageHeaderView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.imageHeaderView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.imageHeaderView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageHeaderView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.imageHeaderView1.Location = new System.Drawing.Point(0, 0);
            this.imageHeaderView1.Name = "imageHeaderView1";
            this.imageHeaderView1.ReadOnly = true;
            this.imageHeaderView1.RowHeadersVisible = false;
            this.imageHeaderView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.imageHeaderView1.RowTemplate.Height = 18;
            this.imageHeaderView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.imageHeaderView1.ShowCellErrors = false;
            this.imageHeaderView1.ShowCellToolTips = false;
            this.imageHeaderView1.ShowEditingIcon = false;
            this.imageHeaderView1.ShowRowErrors = false;
            this.imageHeaderView1.Size = new System.Drawing.Size(612, 400);
            this.imageHeaderView1.TabIndex = 0;
            // 
            // ImageHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 400);
            this.Controls.Add(this.imageHeaderView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageHeaderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "画像プロパティ";
            ((System.ComponentModel.ISupportInitialize)(this.imageHeaderView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ImageHeaderView imageHeaderView1;
    }
}