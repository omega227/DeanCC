namespace DeanCC.GUI
{
    partial class PatrolPatternEditControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.patternTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ngPatternTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.jpgCheckBox = new System.Windows.Forms.CheckBox();
            this.pngCheckBox = new System.Windows.Forms.CheckBox();
            this.gifCheckBox = new System.Windows.Forms.CheckBox();
            this.bmpCheckBox = new System.Windows.Forms.CheckBox();
            this.zipCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.targetBoardsListBox = new System.Windows.Forms.ListBox();
            this.boardListBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subFolderFormatControl = new DeanCC.GUI.Options.ThreadHeaderFormatControl();
            this.boardListBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "検索する板";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "キーワード";
            // 
            // patternTextBox
            // 
            this.patternTextBox.Location = new System.Drawing.Point(4, 96);
            this.patternTextBox.Name = "patternTextBox";
            this.patternTextBox.Size = new System.Drawing.Size(314, 19);
            this.patternTextBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.patternTextBox, "スレッドタイトルに含まれる語を指定します。\r\n空白文字（スペース）：AND検索\r\n｜：OR検索");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "NGワード";
            // 
            // ngPatternTextBox
            // 
            this.ngPatternTextBox.Location = new System.Drawing.Point(4, 139);
            this.ngPatternTextBox.Name = "ngPatternTextBox";
            this.ngPatternTextBox.Size = new System.Drawing.Size(314, 19);
            this.ngPatternTextBox.TabIndex = 2;
            this.toolTip.SetToolTip(this.ngPatternTextBox, "除外するスレッドタイトルに含まれる語を指定します。\r\n大文字と小文字の区別はされません。\r\n複数指定する場合は、空白文字（スペース）で語を区切ってください。");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "サブフォルダー名";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(4, 228);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(314, 19);
            this.nameTextBox.TabIndex = 4;
            this.toolTip.SetToolTip(this.nameTextBox, "この巡回設定の名前を設定します。\r\n名前はスレッド取得に影響しませんが、一意のものである必要があります。");
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "設定名";
            // 
            // jpgCheckBox
            // 
            this.jpgCheckBox.AutoSize = true;
            this.jpgCheckBox.Location = new System.Drawing.Point(4, 265);
            this.jpgCheckBox.Name = "jpgCheckBox";
            this.jpgCheckBox.Size = new System.Drawing.Size(68, 16);
            this.jpgCheckBox.TabIndex = 5;
            this.jpgCheckBox.Text = "jpg(jpeg)";
            this.toolTip.SetToolTip(this.jpgCheckBox, "拡張子がjpg,jpegのファイルを保存します");
            this.jpgCheckBox.UseVisualStyleBackColor = true;
            // 
            // pngCheckBox
            // 
            this.pngCheckBox.AutoSize = true;
            this.pngCheckBox.Location = new System.Drawing.Point(95, 265);
            this.pngCheckBox.Name = "pngCheckBox";
            this.pngCheckBox.Size = new System.Drawing.Size(42, 16);
            this.pngCheckBox.TabIndex = 6;
            this.pngCheckBox.Text = "png";
            this.toolTip.SetToolTip(this.pngCheckBox, "拡張子がpngのファイルを保存します");
            this.pngCheckBox.UseVisualStyleBackColor = true;
            // 
            // gifCheckBox
            // 
            this.gifCheckBox.AutoSize = true;
            this.gifCheckBox.Location = new System.Drawing.Point(194, 265);
            this.gifCheckBox.Name = "gifCheckBox";
            this.gifCheckBox.Size = new System.Drawing.Size(37, 16);
            this.gifCheckBox.TabIndex = 7;
            this.gifCheckBox.Text = "gif";
            this.toolTip.SetToolTip(this.gifCheckBox, "拡張子がgifファイルを保存します");
            this.gifCheckBox.UseVisualStyleBackColor = true;
            // 
            // bmpCheckBox
            // 
            this.bmpCheckBox.AutoSize = true;
            this.bmpCheckBox.Location = new System.Drawing.Point(4, 287);
            this.bmpCheckBox.Name = "bmpCheckBox";
            this.bmpCheckBox.Size = new System.Drawing.Size(45, 16);
            this.bmpCheckBox.TabIndex = 8;
            this.bmpCheckBox.Text = "bmp";
            this.toolTip.SetToolTip(this.bmpCheckBox, "拡張子がbmpのファイルを保存します");
            this.bmpCheckBox.UseVisualStyleBackColor = true;
            // 
            // zipCheckBox
            // 
            this.zipCheckBox.AutoSize = true;
            this.zipCheckBox.Location = new System.Drawing.Point(95, 287);
            this.zipCheckBox.Name = "zipCheckBox";
            this.zipCheckBox.Size = new System.Drawing.Size(38, 16);
            this.zipCheckBox.TabIndex = 9;
            this.zipCheckBox.Text = "zip";
            this.toolTip.SetToolTip(this.zipCheckBox, "拡張子がzipのファイルを保存します");
            this.zipCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // targetBoardsListBox
            // 
            this.targetBoardsListBox.ContextMenuStrip = this.boardListBoxContextMenuStrip;
            this.targetBoardsListBox.FormattingEnabled = true;
            this.targetBoardsListBox.ItemHeight = 12;
            this.targetBoardsListBox.Location = new System.Drawing.Point(6, 25);
            this.targetBoardsListBox.MultiColumn = true;
            this.targetBoardsListBox.Name = "targetBoardsListBox";
            this.targetBoardsListBox.Size = new System.Drawing.Size(312, 52);
            this.targetBoardsListBox.TabIndex = 10;
            // 
            // boardListBoxContextMenuStrip
            // 
            this.boardListBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.削除ToolStripMenuItem});
            this.boardListBoxContextMenuStrip.Name = "boardListBoxContextMenuStrip";
            this.boardListBoxContextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // subFolderFormatControl
            // 
            this.subFolderFormatControl.Location = new System.Drawing.Point(6, 186);
            this.subFolderFormatControl.Name = "subFolderFormatControl";
            this.subFolderFormatControl.Size = new System.Drawing.Size(312, 24);
            this.subFolderFormatControl.TabIndex = 11;
            this.toolTip.SetToolTip(this.subFolderFormatControl, "画像は\"保存フォルダー\\サブフォルダー\"以下に保存されます。");
            // 
            // PatrolPatternEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.targetBoardsListBox);
            this.Controls.Add(this.subFolderFormatControl);
            this.Controls.Add(this.zipCheckBox);
            this.Controls.Add(this.bmpCheckBox);
            this.Controls.Add(this.gifCheckBox);
            this.Controls.Add(this.pngCheckBox);
            this.Controls.Add(this.jpgCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ngPatternTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.patternTextBox);
            this.Controls.Add(this.label1);
            this.Name = "PatrolPatternEditControl";
            this.Size = new System.Drawing.Size(332, 311);
            this.boardListBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox patternTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ngPatternTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox jpgCheckBox;
        private System.Windows.Forms.CheckBox pngCheckBox;
        private System.Windows.Forms.CheckBox gifCheckBox;
        private System.Windows.Forms.CheckBox bmpCheckBox;
        private System.Windows.Forms.CheckBox zipCheckBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ListBox targetBoardsListBox;
        private System.Windows.Forms.ContextMenuStrip boardListBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private Options.ThreadHeaderFormatControl subFolderFormatControl;
    }
}
