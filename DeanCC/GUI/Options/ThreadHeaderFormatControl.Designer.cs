namespace DeanCC.GUI.Options
{
    partial class ThreadHeaderFormatControl
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(320, 19);
            this.textBox.TabIndex = 0;
            // 
            // formatComboBox
            // 
            this.formatComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.formatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatComboBox.FormattingEnabled = true;
            this.formatComboBox.Items.AddRange(new object[] {
            "フォーマット選択"});
            this.formatComboBox.Location = new System.Drawing.Point(326, 0);
            this.formatComboBox.Name = "formatComboBox";
            this.formatComboBox.Size = new System.Drawing.Size(94, 20);
            this.formatComboBox.TabIndex = 1;
            this.formatComboBox.SelectedIndexChanged += new System.EventHandler(this.formatComboBox_SelectedIndexChanged);
            // 
            // ThreadHeaderFormatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.formatComboBox);
            this.Controls.Add(this.textBox);
            this.Name = "ThreadHeaderFormatControl";
            this.Size = new System.Drawing.Size(420, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ComboBox formatComboBox;
    }
}
