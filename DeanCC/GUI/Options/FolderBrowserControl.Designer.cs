namespace DeanCC.GUI.Options
{
    partial class FolderBrowserControl
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
            this.browseButton = new System.Windows.Forms.Button();
            this.pathTextBox = new DeanCC.GUI.Options.PathTextBox();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(377, 1);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(58, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "参照...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.pathTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.pathTextBox.Location = new System.Drawing.Point(3, 3);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(368, 19);
            this.pathTextBox.TabIndex = 2;
            // 
            // FolderBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.browseButton);
            this.Name = "FolderBrowserControl";
            this.Size = new System.Drawing.Size(437, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton;
        private PathTextBox pathTextBox;
    }
}
