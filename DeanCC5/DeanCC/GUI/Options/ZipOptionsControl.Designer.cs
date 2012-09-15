namespace DeanCC.GUI.Options
{
    partial class ZipOptionsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZipOptionsControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.smapleKeysComboBox = new System.Windows.Forms.ComboBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.keywordListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.keywordTextBox = new System.Windows.Forms.TextBox();
            this.SavesSameImagePathCheckBox = new System.Windows.Forms.CheckBox();
            this.savePathFolderBrowserControl = new DeanCC.GUI.Options.FolderBrowserControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.smapleKeysComboBox);
            this.groupBox1.Controls.Add(this.removeButton);
            this.groupBox1.Controls.Add(this.keywordListBox);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.keywordTextBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 210);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "キーワード（DLパスワード）";
            // 
            // smapleKeysComboBox
            // 
            this.smapleKeysComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.smapleKeysComboBox.FormattingEnabled = true;
            this.smapleKeysComboBox.Items.AddRange(new object[] {
            "日付8桁 (20000101)",
            "日付6桁 (000101)",
            "日付4桁 (0101)",
            "日付2桁 (01)",
            "パス： (パス：pass)"});
            this.smapleKeysComboBox.Location = new System.Drawing.Point(312, 12);
            this.smapleKeysComboBox.Name = "smapleKeysComboBox";
            this.smapleKeysComboBox.Size = new System.Drawing.Size(126, 20);
            this.smapleKeysComboBox.TabIndex = 9;
            this.smapleKeysComboBox.SelectedIndexChanged += new System.EventHandler(this.smapleKeysComboBox_SelectedIndexChanged);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(363, 183);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "削除";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // keywordListBox
            // 
            this.keywordListBox.FormattingEnabled = true;
            this.keywordListBox.ItemHeight = 12;
            this.keywordListBox.Location = new System.Drawing.Point(6, 41);
            this.keywordListBox.Name = "keywordListBox";
            this.keywordListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.keywordListBox.Size = new System.Drawing.Size(432, 136);
            this.keywordListBox.TabIndex = 6;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(282, 183);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "追加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // keywordTextBox
            // 
            this.keywordTextBox.AutoCompleteCustomSource.AddRange(new string[] {
            "%id%",
            "%key%",
            "%name%",
            "%mail%"});
            this.keywordTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.keywordTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.keywordTextBox.Location = new System.Drawing.Point(6, 13);
            this.keywordTextBox.Name = "keywordTextBox";
            this.keywordTextBox.Size = new System.Drawing.Size(300, 19);
            this.keywordTextBox.TabIndex = 5;
            this.toolTip.SetToolTip(this.keywordTextBox, resources.GetString("keywordTextBox.ToolTip"));
            // 
            // SavesSameImagePathCheckBox
            // 
            this.SavesSameImagePathCheckBox.AutoSize = true;
            this.SavesSameImagePathCheckBox.Location = new System.Drawing.Point(6, 20);
            this.SavesSameImagePathCheckBox.Name = "SavesSameImagePathCheckBox";
            this.SavesSameImagePathCheckBox.Size = new System.Drawing.Size(101, 16);
            this.SavesSameImagePathCheckBox.TabIndex = 7;
            this.SavesSameImagePathCheckBox.Text = "画像と同じ場所";
            this.toolTip.SetToolTip(this.SavesSameImagePathCheckBox, "取得したZIPファイルを画像と同じ場所に保存します。");
            this.SavesSameImagePathCheckBox.UseVisualStyleBackColor = true;
            // 
            // savePathFolderBrowserControl
            // 
            this.savePathFolderBrowserControl.Description = "取得したZIPを保存する場所を指定してください。画像と同じ場所に保存する場合は、ここで設定した値は無視されます。";
            this.savePathFolderBrowserControl.Location = new System.Drawing.Point(6, 42);
            this.savePathFolderBrowserControl.Name = "savePathFolderBrowserControl";
            this.savePathFolderBrowserControl.SelectedPath = null;
            this.savePathFolderBrowserControl.ShowNewFolderButton = false;
            this.savePathFolderBrowserControl.Size = new System.Drawing.Size(445, 27);
            this.savePathFolderBrowserControl.TabIndex = 6;
            this.toolTip.SetToolTip(this.savePathFolderBrowserControl, "取得したZIPファイルの保存場所を指定します。\r\n画像と同じ場所に保存する場合は、この値は無視されます。");
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // ZipOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SavesSameImagePathCheckBox);
            this.Controls.Add(this.savePathFolderBrowserControl);
            this.Controls.Add(this.groupBox1);
            this.Name = "ZipOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ListBox keywordListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox keywordTextBox;
        private System.Windows.Forms.CheckBox SavesSameImagePathCheckBox;
        private FolderBrowserControl savePathFolderBrowserControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox smapleKeysComboBox;
    }
}
