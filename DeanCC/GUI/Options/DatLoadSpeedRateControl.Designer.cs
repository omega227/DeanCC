namespace DeanCC.GUI.Options
{
    partial class DatLoadSpeedRateControl
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
            this.rateTrackBar = new System.Windows.Forms.TrackBar();
            this.spanLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rateTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rateTrackBar
            // 
            this.rateTrackBar.LargeChange = 1;
            this.rateTrackBar.Location = new System.Drawing.Point(3, 3);
            this.rateTrackBar.Minimum = 1;
            this.rateTrackBar.Name = "rateTrackBar";
            this.rateTrackBar.Size = new System.Drawing.Size(208, 45);
            this.rateTrackBar.TabIndex = 0;
            this.toolTip.SetToolTip(this.rateTrackBar, "スレッドのデータをサーバーから受信する間隔の範囲を設定します。");
            this.rateTrackBar.Value = 1;
            this.rateTrackBar.ValueChanged += new System.EventHandler(this.rateTrackBar_ValueChanged);
            // 
            // spanLabel
            // 
            this.spanLabel.AutoSize = true;
            this.spanLabel.Location = new System.Drawing.Point(3, 51);
            this.spanLabel.Name = "spanLabel";
            this.spanLabel.Size = new System.Drawing.Size(35, 12);
            this.spanLabel.TabIndex = 1;
            this.spanLabel.Text = "SPAN";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // DatLoadSpeedRateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spanLabel);
            this.Controls.Add(this.rateTrackBar);
            this.Name = "DatLoadSpeedRateControl";
            this.Size = new System.Drawing.Size(213, 73);
            ((System.ComponentModel.ISupportInitialize)(this.rateTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar rateTrackBar;
        private System.Windows.Forms.Label spanLabel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
