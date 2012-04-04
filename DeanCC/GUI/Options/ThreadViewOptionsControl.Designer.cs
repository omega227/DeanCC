namespace DeanCC.GUI.Options
{
    partial class ThreadViewOptionsControl
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.doubleClickComboBox = new System.Windows.Forms.ComboBox();
            this.evenColorSelectControl = new DeanCC.GUI.Options.ColorSelectControl();
            this.oddColorSelectControl = new DeanCC.GUI.Options.ColorSelectControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // doubleClickComboBox
            // 
            this.doubleClickComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doubleClickComboBox.FormattingEnabled = true;
            this.doubleClickComboBox.Location = new System.Drawing.Point(3, 121);
            this.doubleClickComboBox.Name = "doubleClickComboBox";
            this.doubleClickComboBox.Size = new System.Drawing.Size(222, 20);
            this.doubleClickComboBox.TabIndex = 2;
            this.toolTip.SetToolTip(this.doubleClickComboBox, "スレッドリストの行をダブルクリックした時に実行するメニューを指定します。");
            // 
            // evenColorSelectControl
            // 
            this.evenColorSelectControl.BackColor = System.Drawing.SystemColors.Control;
            this.evenColorSelectControl.Description = "偶数行の背景色";
            this.evenColorSelectControl.Location = new System.Drawing.Point(3, 35);
            this.evenColorSelectControl.Name = "evenColorSelectControl";
            this.evenColorSelectControl.SelectedColor = System.Drawing.Color.Empty;
            this.evenColorSelectControl.Size = new System.Drawing.Size(222, 26);
            this.evenColorSelectControl.TabIndex = 1;
            this.evenColorSelectControl.TargetPosion = DeanCC.GUI.Options.ColorSelectPosition.BackColor;
            this.toolTip.SetToolTip(this.evenColorSelectControl, "スレッドを表示するリストの偶数行の通常の背景色を指定します。\r\n");
            // 
            // oddColorSelectControl
            // 
            this.oddColorSelectControl.Description = "奇数行の背景色";
            this.oddColorSelectControl.Location = new System.Drawing.Point(3, 3);
            this.oddColorSelectControl.Name = "oddColorSelectControl";
            this.oddColorSelectControl.SelectedColor = System.Drawing.Color.Empty;
            this.oddColorSelectControl.Size = new System.Drawing.Size(222, 26);
            this.oddColorSelectControl.TabIndex = 0;
            this.oddColorSelectControl.TargetPosion = DeanCC.GUI.Options.ColorSelectPosition.BackColor;
            this.toolTip.SetToolTip(this.oddColorSelectControl, "スレッドを表示するリストの奇数行の通常の背景色を指定します。");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ダブルクリック時の動作";
            // 
            // ThreadViewOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doubleClickComboBox);
            this.Controls.Add(this.evenColorSelectControl);
            this.Controls.Add(this.oddColorSelectControl);
            this.Name = "ThreadViewOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorSelectControl oddColorSelectControl;
        private ColorSelectControl evenColorSelectControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox doubleClickComboBox;
        private System.Windows.Forms.Label label1;
    }
}
