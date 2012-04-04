namespace DeanCC.GUI.Options
{
    partial class WindowOptionsControl
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
            this.minimumTaskTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.closingTaskTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // minimumTaskTrayCheckBox
            // 
            this.minimumTaskTrayCheckBox.AutoSize = true;
            this.minimumTaskTrayCheckBox.Location = new System.Drawing.Point(3, 3);
            this.minimumTaskTrayCheckBox.Name = "minimumTaskTrayCheckBox";
            this.minimumTaskTrayCheckBox.Size = new System.Drawing.Size(173, 16);
            this.minimumTaskTrayCheckBox.TabIndex = 0;
            this.minimumTaskTrayCheckBox.Text = "最小化時にタスクトレイに入れる";
            this.toolTip.SetToolTip(this.minimumTaskTrayCheckBox, "メインウィンドウを最小化した時に、タスクトレイに格納します");
            this.minimumTaskTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // closingTaskTrayCheckBox
            // 
            this.closingTaskTrayCheckBox.AutoSize = true;
            this.closingTaskTrayCheckBox.Location = new System.Drawing.Point(3, 39);
            this.closingTaskTrayCheckBox.Name = "closingTaskTrayCheckBox";
            this.closingTaskTrayCheckBox.Size = new System.Drawing.Size(186, 16);
            this.closingTaskTrayCheckBox.TabIndex = 1;
            this.closingTaskTrayCheckBox.Text = "閉じられた時にタスクトレイに入れる";
            this.toolTip.SetToolTip(this.closingTaskTrayCheckBox, "メインウィンドウの閉じるボタンを押したときに、\r\n終了せずにタスクトレイに格納します。\r\nタスクトレイのアイコンを右クリックして終了します。");
            this.closingTaskTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // WindowOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.closingTaskTrayCheckBox);
            this.Controls.Add(this.minimumTaskTrayCheckBox);
            this.Name = "WindowOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox minimumTaskTrayCheckBox;
        private System.Windows.Forms.CheckBox closingTaskTrayCheckBox;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
