namespace DeanCC.GUI.Options
{
    partial class ColorSelectControl
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
            this.customColorButton = new System.Windows.Forms.Button();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // customColorButton
            // 
            this.customColorButton.Location = new System.Drawing.Point(146, 1);
            this.customColorButton.Name = "customColorButton";
            this.customColorButton.Size = new System.Drawing.Size(75, 23);
            this.customColorButton.TabIndex = 1;
            this.customColorButton.Text = "参照...";
            this.customColorButton.UseVisualStyleBackColor = true;
            this.customColorButton.Click += new System.EventHandler(this.customColorButton_Click);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(3, 6);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(0, 12);
            this.descriptionLabel.TabIndex = 2;
            // 
            // ColorSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.customColorButton);
            this.Name = "ColorSelectControl";
            this.Size = new System.Drawing.Size(222, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button customColorButton;
        private System.Windows.Forms.Label descriptionLabel;
    }
}
