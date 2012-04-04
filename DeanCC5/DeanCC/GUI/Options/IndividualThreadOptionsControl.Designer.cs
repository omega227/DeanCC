namespace DeanCC.GUI.Options
{
    partial class IndividualThreadOptionsControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.saveFolderBrowserControl = new DeanCC.GUI.Options.FolderBrowserControl();
            this.zipCheckBox = new System.Windows.Forms.CheckBox();
            this.bmpCheckBox = new System.Windows.Forms.CheckBox();
            this.gifCheckBox = new System.Windows.Forms.CheckBox();
            this.pngCheckBox = new System.Windows.Forms.CheckBox();
            this.jpgCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.subFolderFormatControl = new DeanCC.GUI.Options.ThreadHeaderFormatControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "保存フォルダー";
            // 
            // saveFolderBrowserControl
            // 
            this.saveFolderBrowserControl.Description = "個別追加スレッドで取得した画像の保存場所を指定してください。";
            this.saveFolderBrowserControl.Location = new System.Drawing.Point(3, 91);
            this.saveFolderBrowserControl.Name = "saveFolderBrowserControl";
            this.saveFolderBrowserControl.SelectedPath = null;
            this.saveFolderBrowserControl.ShowNewFolderButton = false;
            this.saveFolderBrowserControl.Size = new System.Drawing.Size(440, 27);
            this.saveFolderBrowserControl.TabIndex = 30;
            // 
            // zipCheckBox
            // 
            this.zipCheckBox.AutoSize = true;
            this.zipCheckBox.Location = new System.Drawing.Point(94, 215);
            this.zipCheckBox.Name = "zipCheckBox";
            this.zipCheckBox.Size = new System.Drawing.Size(38, 16);
            this.zipCheckBox.TabIndex = 28;
            this.zipCheckBox.Text = "zip";
            this.toolTip.SetToolTip(this.zipCheckBox, "拡張子がzipのファイルを保存します");
            this.zipCheckBox.UseVisualStyleBackColor = true;
            // 
            // bmpCheckBox
            // 
            this.bmpCheckBox.AutoSize = true;
            this.bmpCheckBox.Location = new System.Drawing.Point(3, 215);
            this.bmpCheckBox.Name = "bmpCheckBox";
            this.bmpCheckBox.Size = new System.Drawing.Size(45, 16);
            this.bmpCheckBox.TabIndex = 27;
            this.bmpCheckBox.Text = "bmp";
            this.toolTip.SetToolTip(this.bmpCheckBox, "拡張子がbmpのファイルを保存します");
            this.bmpCheckBox.UseVisualStyleBackColor = true;
            // 
            // gifCheckBox
            // 
            this.gifCheckBox.AutoSize = true;
            this.gifCheckBox.Location = new System.Drawing.Point(193, 193);
            this.gifCheckBox.Name = "gifCheckBox";
            this.gifCheckBox.Size = new System.Drawing.Size(37, 16);
            this.gifCheckBox.TabIndex = 26;
            this.gifCheckBox.Text = "gif";
            this.toolTip.SetToolTip(this.gifCheckBox, "拡張子がgifのファイルを保存します");
            this.gifCheckBox.UseVisualStyleBackColor = true;
            // 
            // pngCheckBox
            // 
            this.pngCheckBox.AutoSize = true;
            this.pngCheckBox.Location = new System.Drawing.Point(94, 193);
            this.pngCheckBox.Name = "pngCheckBox";
            this.pngCheckBox.Size = new System.Drawing.Size(42, 16);
            this.pngCheckBox.TabIndex = 24;
            this.pngCheckBox.Text = "png";
            this.toolTip.SetToolTip(this.pngCheckBox, "拡張子がpngのファイルを保存します");
            this.pngCheckBox.UseVisualStyleBackColor = true;
            // 
            // jpgCheckBox
            // 
            this.jpgCheckBox.AutoSize = true;
            this.jpgCheckBox.Location = new System.Drawing.Point(3, 193);
            this.jpgCheckBox.Name = "jpgCheckBox";
            this.jpgCheckBox.Size = new System.Drawing.Size(68, 16);
            this.jpgCheckBox.TabIndex = 23;
            this.jpgCheckBox.Text = "jpg(jpeg)";
            this.toolTip.SetToolTip(this.jpgCheckBox, "拡張子がjpg,jpegのファイルを保存します");
            this.jpgCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "設定名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "サブフォルダー名";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(3, 27);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(371, 19);
            this.nameTextBox.TabIndex = 22;
            this.toolTip.SetToolTip(this.nameTextBox, "設定ファイル名を表します。変更はできません。");
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // subFolderFormatControl
            // 
            this.subFolderFormatControl.Location = new System.Drawing.Point(5, 150);
            this.subFolderFormatControl.Name = "subFolderFormatControl";
            this.subFolderFormatControl.Size = new System.Drawing.Size(424, 26);
            this.subFolderFormatControl.TabIndex = 32;
            this.toolTip.SetToolTip(this.subFolderFormatControl, "画像は\"保存フォルダー\\サブフォルダー\"以下に保存されます");
            // 
            // IndividualThreadOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.subFolderFormatControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveFolderBrowserControl);
            this.Controls.Add(this.zipCheckBox);
            this.Controls.Add(this.bmpCheckBox);
            this.Controls.Add(this.gifCheckBox);
            this.Controls.Add(this.pngCheckBox);
            this.Controls.Add(this.jpgCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameTextBox);
            this.Name = "IndividualThreadOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FolderBrowserControl saveFolderBrowserControl;
        private System.Windows.Forms.CheckBox zipCheckBox;
        private System.Windows.Forms.CheckBox bmpCheckBox;
        private System.Windows.Forms.CheckBox gifCheckBox;
        private System.Windows.Forms.CheckBox pngCheckBox;
        private System.Windows.Forms.CheckBox jpgCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ToolTip toolTip;
        private ThreadHeaderFormatControl subFolderFormatControl;
    }
}
