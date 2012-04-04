namespace DeanCC.GUI.Options
{
    partial class BrowserOptionsControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.WebBrowserOpenFileControl = new DeanCC.GUI.Options.OpenFileControl();
            this.DefaultWebBrowserCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.janeOpenFileControl = new DeanCC.GUI.Options.OpenFileControl();
            this.imageAndCacheRadioButton = new System.Windows.Forms.RadioButton();
            this.cacheRadioButton = new System.Windows.Forms.RadioButton();
            this.imageRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.WebBrowserOpenFileControl);
            this.groupBox1.Controls.Add(this.DefaultWebBrowserCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Webブラウザー";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "使用するWebブラウザーを指定";
            // 
            // WebBrowserOpenFileControl
            // 
            this.WebBrowserOpenFileControl.Filter = "Webブラウザー(*.exe)|*.exe|すべてのファイル(*.*)*.*|";
            this.WebBrowserOpenFileControl.Location = new System.Drawing.Point(6, 71);
            this.WebBrowserOpenFileControl.Name = "WebBrowserOpenFileControl";
            this.WebBrowserOpenFileControl.SelectedPath = null;
            this.WebBrowserOpenFileControl.Size = new System.Drawing.Size(442, 26);
            this.WebBrowserOpenFileControl.TabIndex = 1;
            this.WebBrowserOpenFileControl.Title = "ファイルを選択";
            // 
            // DefaultWebBrowserCheckBox
            // 
            this.DefaultWebBrowserCheckBox.AutoSize = true;
            this.DefaultWebBrowserCheckBox.Location = new System.Drawing.Point(6, 18);
            this.DefaultWebBrowserCheckBox.Name = "DefaultWebBrowserCheckBox";
            this.DefaultWebBrowserCheckBox.Size = new System.Drawing.Size(158, 16);
            this.DefaultWebBrowserCheckBox.TabIndex = 0;
            this.DefaultWebBrowserCheckBox.Text = "既定のWebブラウザーを使用";
            this.toolTip.SetToolTip(this.DefaultWebBrowserCheckBox, "URLを開くブラウザーをOS既定のものに設定します");
            this.DefaultWebBrowserCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.janeOpenFileControl);
            this.groupBox2.Controls.Add(this.imageAndCacheRadioButton);
            this.groupBox2.Controls.Add(this.cacheRadioButton);
            this.groupBox2.Controls.Add(this.imageRadioButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(454, 137);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2chブラウザー（Jane Style）";
            // 
            // janeOpenFileControl
            // 
            this.janeOpenFileControl.Filter = "Jane2ch.exe|Jane2ch.exe|すべてのファイル|*.*";
            this.janeOpenFileControl.Location = new System.Drawing.Point(6, 47);
            this.janeOpenFileControl.Name = "janeOpenFileControl";
            this.janeOpenFileControl.SelectedPath = null;
            this.janeOpenFileControl.Size = new System.Drawing.Size(442, 25);
            this.janeOpenFileControl.TabIndex = 5;
            this.janeOpenFileControl.Title = "JaneStyleの実行ファイル（Jane2ch.exe）を指定してください";
            this.toolTip.SetToolTip(this.janeOpenFileControl, "JaneStyleの実行ファイルを指定します。\r\n指定したJaneStyleで有効な機能である\r\n画像キャッシュの保存・ImageViewURLReplace.d" +
                    "at・ReplaceStr.txt\r\nなどが使用可能になります。");
            // 
            // imageAndCacheRadioButton
            // 
            this.imageAndCacheRadioButton.AutoSize = true;
            this.imageAndCacheRadioButton.Location = new System.Drawing.Point(279, 102);
            this.imageAndCacheRadioButton.Name = "imageAndCacheRadioButton";
            this.imageAndCacheRadioButton.Size = new System.Drawing.Size(155, 16);
            this.imageAndCacheRadioButton.TabIndex = 4;
            this.imageAndCacheRadioButton.TabStop = true;
            this.imageAndCacheRadioButton.Text = "画像とキャッシュ両方を保存";
            this.toolTip.SetToolTip(this.imageAndCacheRadioButton, "画像をダウンロードした時に画像とJaneStyleのキャッシュとして保存します。\r\nJane2ch.exeを指定した場合に有効です。");
            this.imageAndCacheRadioButton.UseVisualStyleBackColor = true;
            // 
            // cacheRadioButton
            // 
            this.cacheRadioButton.AutoSize = true;
            this.cacheRadioButton.Location = new System.Drawing.Point(132, 102);
            this.cacheRadioButton.Name = "cacheRadioButton";
            this.cacheRadioButton.Size = new System.Drawing.Size(118, 16);
            this.cacheRadioButton.TabIndex = 3;
            this.cacheRadioButton.TabStop = true;
            this.cacheRadioButton.Text = "キャッシュだけを保存";
            this.toolTip.SetToolTip(this.cacheRadioButton, "画像をダウンロードした時にJaneStyleのキャッシュとして保存します。\r\nJane2ch.exeを指定した場合に有効です。");
            this.cacheRadioButton.UseVisualStyleBackColor = true;
            // 
            // imageRadioButton
            // 
            this.imageRadioButton.AutoSize = true;
            this.imageRadioButton.Location = new System.Drawing.Point(6, 102);
            this.imageRadioButton.Name = "imageRadioButton";
            this.imageRadioButton.Size = new System.Drawing.Size(99, 16);
            this.imageRadioButton.TabIndex = 2;
            this.imageRadioButton.TabStop = true;
            this.imageRadioButton.Text = "画像だけを保存";
            this.toolTip.SetToolTip(this.imageRadioButton, "画像をダウンロードした時に、画像のみを保存します。");
            this.imageRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Jane2ch.exeを指定";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // BrowserOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BrowserOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private OpenFileControl WebBrowserOpenFileControl;
        private System.Windows.Forms.CheckBox DefaultWebBrowserCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton imageAndCacheRadioButton;
        private System.Windows.Forms.RadioButton cacheRadioButton;
        private System.Windows.Forms.RadioButton imageRadioButton;
        private System.Windows.Forms.Label label1;
        private OpenFileControl janeOpenFileControl;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
