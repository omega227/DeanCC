namespace DeanCC.GUI.Options
{
    partial class NGOptionsControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addClipboardButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.ngListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.ngTextBox = new System.Windows.Forms.TextBox();
            this.NGFilesOpenFileControl = new DeanCC.GUI.Options.OpenFileControl();
            this.globalNGTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "NGFiles.txtを指定";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addClipboardButton);
            this.groupBox1.Controls.Add(this.removeButton);
            this.groupBox1.Controls.Add(this.ngListBox);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.ngTextBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 197);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "URLに含まれる文字を指定";
            // 
            // addClipboardButton
            // 
            this.addClipboardButton.Location = new System.Drawing.Point(6, 168);
            this.addClipboardButton.Name = "addClipboardButton";
            this.addClipboardButton.Size = new System.Drawing.Size(156, 23);
            this.addClipboardButton.TabIndex = 9;
            this.addClipboardButton.Text = "クリップボードからまとめて追加";
            this.addClipboardButton.UseVisualStyleBackColor = true;
            this.addClipboardButton.Click += new System.EventHandler(this.addClipboardButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(361, 168);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "削除";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // ngListBox
            // 
            this.ngListBox.FormattingEnabled = true;
            this.ngListBox.ItemHeight = 12;
            this.ngListBox.Location = new System.Drawing.Point(6, 41);
            this.ngListBox.MultiColumn = true;
            this.ngListBox.Name = "ngListBox";
            this.ngListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ngListBox.Size = new System.Drawing.Size(430, 124);
            this.ngListBox.Sorted = true;
            this.ngListBox.TabIndex = 6;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(280, 168);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "追加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // ngTextBox
            // 
            this.ngTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ngTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.ngTextBox.Location = new System.Drawing.Point(6, 13);
            this.ngTextBox.Name = "ngTextBox";
            this.ngTextBox.Size = new System.Drawing.Size(430, 19);
            this.ngTextBox.TabIndex = 5;
            this.toolTip.SetToolTip(this.ngTextBox, "ここで指定した文字を含む画像URLはダウンロードされません。\r\n大文字と小文字の区別はされません。");
            // 
            // NGFilesOpenFileControl
            // 
            this.NGFilesOpenFileControl.Filter = "NGFiles.txt|NGFiles.txt|すべてのファイル(*.*)|*.*";
            this.NGFilesOpenFileControl.Location = new System.Drawing.Point(15, 76);
            this.NGFilesOpenFileControl.Name = "NGFilesOpenFileControl";
            this.NGFilesOpenFileControl.SelectedPath = null;
            this.NGFilesOpenFileControl.Size = new System.Drawing.Size(436, 26);
            this.NGFilesOpenFileControl.TabIndex = 1;
            this.NGFilesOpenFileControl.Title = "NGFiles.txtの参照";
            // 
            // globalNGTextBox
            // 
            this.globalNGTextBox.Location = new System.Drawing.Point(15, 24);
            this.globalNGTextBox.Name = "globalNGTextBox";
            this.globalNGTextBox.Size = new System.Drawing.Size(436, 19);
            this.globalNGTextBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.globalNGTextBox, "全巡回パターンで適用されるNGワードを指定します。\r\n複数指定する場合は、\"空白文字（スペース）\"で単語を区切ってください。");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "共通NGワード";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // NGOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.globalNGTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.NGFilesOpenFileControl);
            this.Controls.Add(this.label1);
            this.Name = "NGOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private OpenFileControl NGFilesOpenFileControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addClipboardButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ListBox ngListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox ngTextBox;
        private System.Windows.Forms.TextBox globalNGTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
