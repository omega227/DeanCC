namespace DeanCC.GUI.Options
{
    partial class StartupOptionsControl
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
            this.removeHashCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hashNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.minimumCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.threadNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.threadCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.autoNewVersionCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.hashNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // removeHashCheckBox
            // 
            this.removeHashCheckBox.AutoSize = true;
            this.removeHashCheckBox.Location = new System.Drawing.Point(3, 129);
            this.removeHashCheckBox.Name = "removeHashCheckBox";
            this.removeHashCheckBox.Size = new System.Drawing.Size(213, 16);
            this.removeHashCheckBox.TabIndex = 0;
            this.removeHashCheckBox.Text = "起動時に期限切れのハッシュを削除する";
            this.toolTip.SetToolTip(this.removeHashCheckBox, "ダウンロード済み画像のハッシュを指定した期間で削除します。\r\nハッシュが削除されると、再び画像が保存されます");
            this.removeHashCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ハッシュの有効期限(日)";
            // 
            // hashNumericUpDown
            // 
            this.hashNumericUpDown.Location = new System.Drawing.Point(138, 146);
            this.hashNumericUpDown.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.hashNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hashNumericUpDown.Name = "hashNumericUpDown";
            this.hashNumericUpDown.Size = new System.Drawing.Size(59, 19);
            this.hashNumericUpDown.TabIndex = 2;
            this.toolTip.SetToolTip(this.hashNumericUpDown, "ハッシュが作成されてからの有効期限（日）を指定します。");
            this.hashNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // minimumCheckBox
            // 
            this.minimumCheckBox.AutoSize = true;
            this.minimumCheckBox.Location = new System.Drawing.Point(3, 16);
            this.minimumCheckBox.Name = "minimumCheckBox";
            this.minimumCheckBox.Size = new System.Drawing.Size(125, 16);
            this.minimumCheckBox.TabIndex = 3;
            this.minimumCheckBox.Text = "最小状態で起動する";
            this.toolTip.SetToolTip(this.minimumCheckBox, "起動時にメインウィンドウを最小化状態に設定します。");
            this.minimumCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // threadNumericUpDown
            // 
            this.threadNumericUpDown.Location = new System.Drawing.Point(138, 82);
            this.threadNumericUpDown.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.threadNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadNumericUpDown.Name = "threadNumericUpDown";
            this.threadNumericUpDown.Size = new System.Drawing.Size(59, 19);
            this.threadNumericUpDown.TabIndex = 6;
            this.toolTip.SetToolTip(this.threadNumericUpDown, "スレッドが作成されてからの有効期限（日）を指定します。");
            this.threadNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // threadCheckBox
            // 
            this.threadCheckBox.AutoSize = true;
            this.threadCheckBox.Location = new System.Drawing.Point(3, 65);
            this.threadCheckBox.Name = "threadCheckBox";
            this.threadCheckBox.Size = new System.Drawing.Size(212, 16);
            this.threadCheckBox.TabIndex = 4;
            this.threadCheckBox.Text = "起動時に期限切れのスレッドを削除する";
            this.toolTip.SetToolTip(this.threadCheckBox, "スレッドを指定した期間で削除します。\r\nまた、新規に取得する場合にも指定した期間をすでに過ぎているスレッドは取得しません。");
            this.threadCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "スレッドの有効期限(日)";
            // 
            // autoNewVersionCheckBox
            // 
            this.autoNewVersionCheckBox.AutoSize = true;
            this.autoNewVersionCheckBox.Location = new System.Drawing.Point(3, 185);
            this.autoNewVersionCheckBox.Name = "autoNewVersionCheckBox";
            this.autoNewVersionCheckBox.Size = new System.Drawing.Size(157, 16);
            this.autoNewVersionCheckBox.TabIndex = 7;
            this.autoNewVersionCheckBox.Text = "自動的に最新版を確認する";
            this.toolTip.SetToolTip(this.autoNewVersionCheckBox, "起動時と1日ごとに最新版を確認します。\r\n最新版が存在する場合のみ通知します。");
            this.autoNewVersionCheckBox.UseVisualStyleBackColor = true;
            // 
            // StartupOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.autoNewVersionCheckBox);
            this.Controls.Add(this.threadNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.threadCheckBox);
            this.Controls.Add(this.minimumCheckBox);
            this.Controls.Add(this.hashNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeHashCheckBox);
            this.Name = "StartupOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            ((System.ComponentModel.ISupportInitialize)(this.hashNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox removeHashCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown hashNumericUpDown;
        private System.Windows.Forms.CheckBox minimumCheckBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown threadNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox threadCheckBox;
        private System.Windows.Forms.CheckBox autoNewVersionCheckBox;
    }
}
