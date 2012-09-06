namespace DeanCC.GUI
{
    partial class NewVersionForm
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.updateButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.releaseTextBox = new System.Windows.Forms.TextBox();
            this.separatorLable1 = new DeanCC.GUI.SeparatorLable();
            this.SuspendLayout();
            // 
            // updateButton
            // 
            this.updateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.updateButton.Location = new System.Drawing.Point(285, 239);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(101, 23);
            this.updateButton.TabIndex = 0;
            this.updateButton.Text = "更新して再起動";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(392, 239);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 48);
            this.label1.TabIndex = 3;
            this.label1.Text = "DeanCCのバージョンアップが可能です。\r\n新バージョンをダウンロードして再起動しますか？\r\n\r\n更新内容：";
            // 
            // releaseTextBox
            // 
            this.releaseTextBox.Location = new System.Drawing.Point(12, 70);
            this.releaseTextBox.Multiline = true;
            this.releaseTextBox.Name = "releaseTextBox";
            this.releaseTextBox.ReadOnly = true;
            this.releaseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.releaseTextBox.Size = new System.Drawing.Size(455, 137);
            this.releaseTextBox.TabIndex = 4;
            // 
            // separatorLable1
            // 
            this.separatorLable1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separatorLable1.Location = new System.Drawing.Point(12, 226);
            this.separatorLable1.Name = "separatorLable1";
            this.separatorLable1.Size = new System.Drawing.Size(455, 2);
            this.separatorLable1.TabIndex = 2;
            this.separatorLable1.Text = "separatorLable1";
            // 
            // NewVersionForm
            // 
            this.AcceptButton = this.updateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(479, 272);
            this.Controls.Add(this.releaseTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.separatorLable1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.updateButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewVersionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "自動アップデート";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button cancelButton;
        private SeparatorLable separatorLable1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox releaseTextBox;
    }
}