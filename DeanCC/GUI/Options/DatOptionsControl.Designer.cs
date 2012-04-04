namespace DeanCC.GUI.Options
{
    partial class DatOptionsControl
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
            this.noSaveDatRadioButton = new System.Windows.Forms.RadioButton();
            this.datRadioButton = new System.Windows.Forms.RadioButton();
            this.datAndHtmlRadioButton = new System.Windows.Forms.RadioButton();
            this.SavesSameImagePathCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.savePathFolderBrowserControl = new DeanCC.GUI.Options.FolderBrowserControl();
            this.getBg20CheckBox = new System.Windows.Forms.CheckBox();
            this.datLoadSpeedRateControl1 = new DeanCC.GUI.Options.DatLoadSpeedRateControl();
            this.fileNameControl = new DeanCC.GUI.Options.ThreadHeaderFormatControl();
            this.SuspendLayout();
            // 
            // noSaveDatRadioButton
            // 
            this.noSaveDatRadioButton.AutoSize = true;
            this.noSaveDatRadioButton.Location = new System.Drawing.Point(16, 27);
            this.noSaveDatRadioButton.Name = "noSaveDatRadioButton";
            this.noSaveDatRadioButton.Size = new System.Drawing.Size(76, 16);
            this.noSaveDatRadioButton.TabIndex = 0;
            this.noSaveDatRadioButton.TabStop = true;
            this.noSaveDatRadioButton.Text = "保存しない";
            this.toolTip.SetToolTip(this.noSaveDatRadioButton, "取得したスレッドのDAT等は保存しません。\r\nHTML作成やDAT落ち後のレス表示などができなくなります。\r\n画像は通常通りに保存されます。");
            this.noSaveDatRadioButton.UseVisualStyleBackColor = true;
            // 
            // datRadioButton
            // 
            this.datRadioButton.AutoSize = true;
            this.datRadioButton.Location = new System.Drawing.Point(158, 27);
            this.datRadioButton.Name = "datRadioButton";
            this.datRadioButton.Size = new System.Drawing.Size(98, 16);
            this.datRadioButton.TabIndex = 2;
            this.datRadioButton.TabStop = true;
            this.datRadioButton.Text = "DATだけを保存";
            this.toolTip.SetToolTip(this.datRadioButton, "取得したスレッドのデータを表すDATファイルを保存します。");
            this.datRadioButton.UseVisualStyleBackColor = true;
            // 
            // datAndHtmlRadioButton
            // 
            this.datAndHtmlRadioButton.AutoSize = true;
            this.datAndHtmlRadioButton.Location = new System.Drawing.Point(311, 27);
            this.datAndHtmlRadioButton.Name = "datAndHtmlRadioButton";
            this.datAndHtmlRadioButton.Size = new System.Drawing.Size(141, 16);
            this.datAndHtmlRadioButton.TabIndex = 3;
            this.datAndHtmlRadioButton.TabStop = true;
            this.datAndHtmlRadioButton.Text = "DATとHTML両方を保存";
            this.toolTip.SetToolTip(this.datAndHtmlRadioButton, "取得したスレッドのデータを表すDATファイルと\r\n画像を埋め込んだHTMLを作成して保存します。");
            this.datAndHtmlRadioButton.UseVisualStyleBackColor = true;
            // 
            // SavesSameImagePathCheckBox
            // 
            this.SavesSameImagePathCheckBox.AutoSize = true;
            this.SavesSameImagePathCheckBox.Location = new System.Drawing.Point(16, 70);
            this.SavesSameImagePathCheckBox.Name = "SavesSameImagePathCheckBox";
            this.SavesSameImagePathCheckBox.Size = new System.Drawing.Size(101, 16);
            this.SavesSameImagePathCheckBox.TabIndex = 5;
            this.SavesSameImagePathCheckBox.Text = "画像と同じ場所";
            this.toolTip.SetToolTip(this.SavesSameImagePathCheckBox, "DATファイルやHTMLファイルを画像と同じフォルダーに保存します");
            this.SavesSameImagePathCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "ファイル名";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // savePathFolderBrowserControl
            // 
            this.savePathFolderBrowserControl.Description = "DAT,HTMLの保存場所をしてしてください。画像と同じ場所に保存する場合は、ここで設定した値は無視されます。";
            this.savePathFolderBrowserControl.Location = new System.Drawing.Point(16, 92);
            this.savePathFolderBrowserControl.Name = "savePathFolderBrowserControl";
            this.savePathFolderBrowserControl.SelectedPath = null;
            this.savePathFolderBrowserControl.ShowNewFolderButton = true;
            this.savePathFolderBrowserControl.Size = new System.Drawing.Size(439, 27);
            this.savePathFolderBrowserControl.TabIndex = 4;
            this.toolTip.SetToolTip(this.savePathFolderBrowserControl, "DATファイルやHTMLファイルの保存場所を指定します。\r\n画像と同じ場所に保存する場合はこの設定は無視されます。");
            // 
            // getBg20CheckBox
            // 
            this.getBg20CheckBox.AutoSize = true;
            this.getBg20CheckBox.Location = new System.Drawing.Point(16, 276);
            this.getBg20CheckBox.Name = "getBg20CheckBox";
            this.getBg20CheckBox.Size = new System.Drawing.Size(181, 16);
            this.getBg20CheckBox.TabIndex = 10;
            this.getBg20CheckBox.Text = "bg20サーバーからDATを取得する";
            this.toolTip.SetToolTip(this.getBg20CheckBox, "DATを取得するサーバーをbg20サーバーに設定します。\r\nこの設定を有効にすると2ちゃんねるサーバーへの負荷を増やさずに、バーボン規制を避けやすくなります。\r\n" +
        "初回のDAT取得のみが対象です。");
            this.getBg20CheckBox.UseVisualStyleBackColor = true;
            // 
            // datLoadSpeedRateControl1
            // 
            this.datLoadSpeedRateControl1.Location = new System.Drawing.Point(16, 188);
            this.datLoadSpeedRateControl1.Name = "datLoadSpeedRateControl1";
            this.datLoadSpeedRateControl1.Size = new System.Drawing.Size(213, 73);
            this.datLoadSpeedRateControl1.TabIndex = 6;
            this.datLoadSpeedRateControl1.Value = 1;
            // 
            // fileNameControl
            // 
            this.fileNameControl.Location = new System.Drawing.Point(16, 149);
            this.fileNameControl.Name = "fileNameControl";
            this.fileNameControl.Size = new System.Drawing.Size(424, 26);
            this.fileNameControl.TabIndex = 11;
            this.toolTip.SetToolTip(this.fileNameControl, "DATファイルとHTMLファイルの拡張子抜きのファイル名を設定します。");
            // 
            // DatOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileNameControl);
            this.Controls.Add(this.getBg20CheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datLoadSpeedRateControl1);
            this.Controls.Add(this.SavesSameImagePathCheckBox);
            this.Controls.Add(this.savePathFolderBrowserControl);
            this.Controls.Add(this.datAndHtmlRadioButton);
            this.Controls.Add(this.datRadioButton);
            this.Controls.Add(this.noSaveDatRadioButton);
            this.Name = "DatOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton noSaveDatRadioButton;
        private System.Windows.Forms.RadioButton datRadioButton;
        private System.Windows.Forms.RadioButton datAndHtmlRadioButton;
        private FolderBrowserControl savePathFolderBrowserControl;
        private System.Windows.Forms.CheckBox SavesSameImagePathCheckBox;
        private DatLoadSpeedRateControl datLoadSpeedRateControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox getBg20CheckBox;
        private ThreadHeaderFormatControl fileNameControl;
    }
}
