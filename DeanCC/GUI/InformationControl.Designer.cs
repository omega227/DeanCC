namespace DeanCC.GUI
{
    partial class InformationControl
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
            this.currentDownloadLabel = new System.Windows.Forms.Label();
            this.currentUploadLabel = new System.Windows.Forms.Label();
            this.totalDownloadLabel = new System.Windows.Forms.Label();
            this.totalUploadLabel = new System.Windows.Forms.Label();
            this.totalUpspanLabel = new System.Windows.Forms.Label();
            this.lastUptimeLabel = new System.Windows.Forms.Label();
            this.totaladdThreadLabel = new System.Windows.Forms.Label();
            this.totalDownloadThreadLabel = new System.Windows.Forms.Label();
            this.totalSaveImageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // currentDownloadLabel
            // 
            this.currentDownloadLabel.AutoSize = true;
            this.currentDownloadLabel.Location = new System.Drawing.Point(32, 22);
            this.currentDownloadLabel.Name = "currentDownloadLabel";
            this.currentDownloadLabel.Size = new System.Drawing.Size(35, 12);
            this.currentDownloadLabel.TabIndex = 0;
            this.currentDownloadLabel.Text = "label1";
            // 
            // currentUploadLabel
            // 
            this.currentUploadLabel.AutoSize = true;
            this.currentUploadLabel.Location = new System.Drawing.Point(32, 60);
            this.currentUploadLabel.Name = "currentUploadLabel";
            this.currentUploadLabel.Size = new System.Drawing.Size(35, 12);
            this.currentUploadLabel.TabIndex = 1;
            this.currentUploadLabel.Text = "label2";
            // 
            // totalDownloadLabel
            // 
            this.totalDownloadLabel.AutoSize = true;
            this.totalDownloadLabel.Location = new System.Drawing.Point(32, 98);
            this.totalDownloadLabel.Name = "totalDownloadLabel";
            this.totalDownloadLabel.Size = new System.Drawing.Size(35, 12);
            this.totalDownloadLabel.TabIndex = 2;
            this.totalDownloadLabel.Text = "label3";
            // 
            // totalUploadLabel
            // 
            this.totalUploadLabel.AutoSize = true;
            this.totalUploadLabel.Location = new System.Drawing.Point(32, 136);
            this.totalUploadLabel.Name = "totalUploadLabel";
            this.totalUploadLabel.Size = new System.Drawing.Size(35, 12);
            this.totalUploadLabel.TabIndex = 3;
            this.totalUploadLabel.Text = "label4";
            // 
            // totalUpspanLabel
            // 
            this.totalUpspanLabel.AutoSize = true;
            this.totalUpspanLabel.Location = new System.Drawing.Point(32, 174);
            this.totalUpspanLabel.Name = "totalUpspanLabel";
            this.totalUpspanLabel.Size = new System.Drawing.Size(35, 12);
            this.totalUpspanLabel.TabIndex = 4;
            this.totalUpspanLabel.Text = "label5";
            // 
            // lastUptimeLabel
            // 
            this.lastUptimeLabel.AutoSize = true;
            this.lastUptimeLabel.Location = new System.Drawing.Point(32, 212);
            this.lastUptimeLabel.Name = "lastUptimeLabel";
            this.lastUptimeLabel.Size = new System.Drawing.Size(35, 12);
            this.lastUptimeLabel.TabIndex = 5;
            this.lastUptimeLabel.Text = "label6";
            // 
            // totaladdThreadLabel
            // 
            this.totaladdThreadLabel.AutoSize = true;
            this.totaladdThreadLabel.Location = new System.Drawing.Point(32, 250);
            this.totaladdThreadLabel.Name = "totaladdThreadLabel";
            this.totaladdThreadLabel.Size = new System.Drawing.Size(35, 12);
            this.totaladdThreadLabel.TabIndex = 6;
            this.totaladdThreadLabel.Text = "label1";
            // 
            // totalDownloadThreadLabel
            // 
            this.totalDownloadThreadLabel.AutoSize = true;
            this.totalDownloadThreadLabel.Location = new System.Drawing.Point(32, 288);
            this.totalDownloadThreadLabel.Name = "totalDownloadThreadLabel";
            this.totalDownloadThreadLabel.Size = new System.Drawing.Size(35, 12);
            this.totalDownloadThreadLabel.TabIndex = 7;
            this.totalDownloadThreadLabel.Text = "label1";
            // 
            // totalSaveImageLabel
            // 
            this.totalSaveImageLabel.AutoSize = true;
            this.totalSaveImageLabel.Location = new System.Drawing.Point(32, 326);
            this.totalSaveImageLabel.Name = "totalSaveImageLabel";
            this.totalSaveImageLabel.Size = new System.Drawing.Size(35, 12);
            this.totalSaveImageLabel.TabIndex = 8;
            this.totalSaveImageLabel.Text = "label1";
            // 
            // InformationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.totalSaveImageLabel);
            this.Controls.Add(this.totalDownloadThreadLabel);
            this.Controls.Add(this.totaladdThreadLabel);
            this.Controls.Add(this.lastUptimeLabel);
            this.Controls.Add(this.totalUpspanLabel);
            this.Controls.Add(this.totalUploadLabel);
            this.Controls.Add(this.totalDownloadLabel);
            this.Controls.Add(this.currentUploadLabel);
            this.Controls.Add(this.currentDownloadLabel);
            this.Name = "InformationControl";
            this.Size = new System.Drawing.Size(475, 370);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label currentDownloadLabel;
        private System.Windows.Forms.Label currentUploadLabel;
        private System.Windows.Forms.Label totalDownloadLabel;
        private System.Windows.Forms.Label totalUploadLabel;
        private System.Windows.Forms.Label totalUpspanLabel;
        private System.Windows.Forms.Label lastUptimeLabel;
        private System.Windows.Forms.Label totaladdThreadLabel;
        private System.Windows.Forms.Label totalDownloadThreadLabel;
        private System.Windows.Forms.Label totalSaveImageLabel;

    }
}
