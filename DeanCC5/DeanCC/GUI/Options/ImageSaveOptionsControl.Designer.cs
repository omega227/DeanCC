namespace DeanCC.GUI.Options
{
    partial class ImageSaveOptionsControl
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
            this.blockImageCheckBox = new System.Windows.Forms.CheckBox();
            this.thresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.movePathCheckBox = new System.Windows.Forms.CheckBox();
            this.retryCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.retryDateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.originalTimestampCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moveFolderBrowserControl = new DeanCC.GUI.Options.FolderBrowserControl();
            this.fileNameControl = new DeanCC.GUI.Options.ThreadHeaderFormatControl(DeanCC.GUI.Options.ThreadHeaderFormatControl.FormatType.ImageHeader);
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryDateNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // blockImageCheckBox
            // 
            this.blockImageCheckBox.AccessibleDescription = "";
            this.blockImageCheckBox.AutoSize = true;
            this.blockImageCheckBox.Location = new System.Drawing.Point(3, 12);
            this.blockImageCheckBox.Name = "blockImageCheckBox";
            this.blockImageCheckBox.Size = new System.Drawing.Size(189, 16);
            this.blockImageCheckBox.TabIndex = 0;
            this.blockImageCheckBox.Text = "ダウンロード済み画像は保存しない";
            this.toolTip.SetToolTip(this.blockImageCheckBox, "既にダウンロードしている画像を保存しないようにします。");
            this.blockImageCheckBox.UseVisualStyleBackColor = true;
            // 
            // thresholdNumericUpDown
            // 
            this.thresholdNumericUpDown.AccessibleDescription = "";
            this.thresholdNumericUpDown.Location = new System.Drawing.Point(14, 118);
            this.thresholdNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.thresholdNumericUpDown.Name = "thresholdNumericUpDown";
            this.thresholdNumericUpDown.Size = new System.Drawing.Size(90, 19);
            this.thresholdNumericUpDown.TabIndex = 1;
            this.toolTip.SetToolTip(this.thresholdNumericUpDown, "画像を保存するしきい値です。\r\n設定した枚数未満の画像のスレッドはダウンロード一時停止状態になります。");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "しきい値";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // movePathCheckBox
            // 
            this.movePathCheckBox.AutoSize = true;
            this.movePathCheckBox.Location = new System.Drawing.Point(3, 246);
            this.movePathCheckBox.Name = "movePathCheckBox";
            this.movePathCheckBox.Size = new System.Drawing.Size(161, 16);
            this.movePathCheckBox.TabIndex = 3;
            this.movePathCheckBox.Text = "ダウンロード完了後の移動先";
            this.toolTip.SetToolTip(this.movePathCheckBox, "画像をダウンロード完了した後に、サブフォルダーの移動をします\r\n巡回設定でサブフォルダーを作成している場合に実行されます。");
            this.movePathCheckBox.UseVisualStyleBackColor = true;
            // 
            // retryCountNumericUpDown
            // 
            this.retryCountNumericUpDown.Location = new System.Drawing.Point(145, 118);
            this.retryCountNumericUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.retryCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.retryCountNumericUpDown.Name = "retryCountNumericUpDown";
            this.retryCountNumericUpDown.Size = new System.Drawing.Size(90, 19);
            this.retryCountNumericUpDown.TabIndex = 5;
            this.toolTip.SetToolTip(this.retryCountNumericUpDown, "画像のダウンロードに失敗した時に実行される最大ダウンロード回数を指定します");
            this.retryCountNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // retryDateNumericUpDown
            // 
            this.retryDateNumericUpDown.Location = new System.Drawing.Point(296, 118);
            this.retryDateNumericUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.retryDateNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.retryDateNumericUpDown.Name = "retryDateNumericUpDown";
            this.retryDateNumericUpDown.Size = new System.Drawing.Size(90, 19);
            this.retryDateNumericUpDown.TabIndex = 7;
            this.toolTip.SetToolTip(this.retryDateNumericUpDown, "画像のダウンロードに失敗した時に実行される最大ダウンロード日数を指定します");
            this.retryDateNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // originalTimestampCheckBox
            // 
            this.originalTimestampCheckBox.AutoSize = true;
            this.originalTimestampCheckBox.Location = new System.Drawing.Point(3, 55);
            this.originalTimestampCheckBox.Name = "originalTimestampCheckBox";
            this.originalTimestampCheckBox.Size = new System.Drawing.Size(170, 16);
            this.originalTimestampCheckBox.TabIndex = 9;
            this.originalTimestampCheckBox.Text = "元画像の更新日時を適用する";
            this.toolTip.SetToolTip(this.originalTimestampCheckBox, "ダウンロードした画像の更新日時プロパティを通常のダウンロード日時ではなく、\r\n元ファイルの更新日時に設定します。");
            this.originalTimestampCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "有効ダウンロード回数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "有効ダウンロード日数";
            // 
            // moveFolderBrowserControl
            // 
            this.moveFolderBrowserControl.Description = "移動先の保存フォルダーを指定してください";
            this.moveFolderBrowserControl.Location = new System.Drawing.Point(14, 268);
            this.moveFolderBrowserControl.Name = "moveFolderBrowserControl";
            this.moveFolderBrowserControl.SelectedPath = null;
            this.moveFolderBrowserControl.ShowNewFolderButton = false;
            this.moveFolderBrowserControl.Size = new System.Drawing.Size(437, 27);
            this.moveFolderBrowserControl.TabIndex = 4;
            // 
            // fileNameControl
            // 
            this.fileNameControl.Location = new System.Drawing.Point(14, 189);
            this.fileNameControl.Name = "fileNameControl";
            this.fileNameControl.Size = new System.Drawing.Size(424, 26);
            this.fileNameControl.TabIndex = 13;
            this.toolTip.SetToolTip(this.fileNameControl, "ダウンロードした画像の拡張子抜きのファイル名を設定します。");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "ファイル名";
            // 
            // ImageSaveOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileNameControl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.originalTimestampCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.retryDateNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.retryCountNumericUpDown);
            this.Controls.Add(this.moveFolderBrowserControl);
            this.Controls.Add(this.movePathCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.thresholdNumericUpDown);
            this.Controls.Add(this.blockImageCheckBox);
            this.Name = "ImageSaveOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            ((System.ComponentModel.ISupportInitialize)(this.thresholdNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryDateNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox blockImageCheckBox;
        private System.Windows.Forms.NumericUpDown thresholdNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox movePathCheckBox;
        private FolderBrowserControl moveFolderBrowserControl;
        private System.Windows.Forms.NumericUpDown retryCountNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown retryDateNumericUpDown;
        private System.Windows.Forms.CheckBox originalTimestampCheckBox;
        private ThreadHeaderFormatControl fileNameControl;
        private System.Windows.Forms.Label label4;
    }
}
