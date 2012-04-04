namespace DeanCC.GUI.Options
{
    partial class InternetOptionsControl
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
            this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.BoardURLTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.encodingComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.useProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.useIECheckBox = new System.Windows.Forms.CheckBox();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.credentialCheckBox = new System.Windows.Forms.CheckBox();
            this.idTtextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Timeout(ミリ秒)";
            // 
            // timeoutNumericUpDown
            // 
            this.timeoutNumericUpDown.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Location = new System.Drawing.Point(12, 31);
            this.timeoutNumericUpDown.Maximum = new decimal(new int[] {
            180000,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Minimum = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
            this.timeoutNumericUpDown.Size = new System.Drawing.Size(154, 19);
            this.timeoutNumericUpDown.TabIndex = 1;
            this.timeoutNumericUpDown.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.timeoutNumericUpDown, "接続タイムアウト（ミリ秒）を設定します。");
            this.timeoutNumericUpDown.Value = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            // 
            // BoardURLTextBox
            // 
            this.BoardURLTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.BoardURLTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.BoardURLTextBox.Location = new System.Drawing.Point(12, 120);
            this.BoardURLTextBox.Name = "BoardURLTextBox";
            this.BoardURLTextBox.Size = new System.Drawing.Size(244, 19);
            this.BoardURLTextBox.TabIndex = 2;
            this.toolTip.SetToolTip(this.BoardURLTextBox, "板一覧を取得するURLを指定します。");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ボード一覧取得URL";
            // 
            // encodingComboBox
            // 
            this.encodingComboBox.DisplayMember = "EncodingName";
            this.encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingComboBox.FormattingEnabled = true;
            this.encodingComboBox.Location = new System.Drawing.Point(12, 77);
            this.encodingComboBox.Name = "encodingComboBox";
            this.encodingComboBox.Size = new System.Drawing.Size(154, 20);
            this.encodingComboBox.TabIndex = 4;
            this.toolTip.SetToolTip(this.encodingComboBox, "通常使用されるエンコードを指定します。");
            this.encodingComboBox.Enter += new System.EventHandler(this.encodingComboBox_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Encoding";
            // 
            // useProxyCheckBox
            // 
            this.useProxyCheckBox.AutoSize = true;
            this.useProxyCheckBox.Location = new System.Drawing.Point(12, 154);
            this.useProxyCheckBox.Name = "useProxyCheckBox";
            this.useProxyCheckBox.Size = new System.Drawing.Size(154, 16);
            this.useProxyCheckBox.TabIndex = 6;
            this.useProxyCheckBox.Text = "プロキシサーバーを使用する";
            this.useProxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // useIECheckBox
            // 
            this.useIECheckBox.AutoSize = true;
            this.useIECheckBox.Location = new System.Drawing.Point(34, 176);
            this.useIECheckBox.Name = "useIECheckBox";
            this.useIECheckBox.Size = new System.Drawing.Size(233, 16);
            this.useIECheckBox.TabIndex = 7;
            this.useIECheckBox.Text = "Internet Explorerのプロキシ設定を使用する";
            this.useIECheckBox.UseVisualStyleBackColor = true;
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(70, 214);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(186, 19);
            this.hostTextBox.TabIndex = 8;
            // 
            // portNumericUpDown
            // 
            this.portNumericUpDown.Location = new System.Drawing.Point(335, 214);
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.Size = new System.Drawing.Size(122, 19);
            this.portNumericUpDown.TabIndex = 9;
            // 
            // credentialCheckBox
            // 
            this.credentialCheckBox.AutoSize = true;
            this.credentialCheckBox.Location = new System.Drawing.Point(34, 250);
            this.credentialCheckBox.Name = "credentialCheckBox";
            this.credentialCheckBox.Size = new System.Drawing.Size(48, 16);
            this.credentialCheckBox.TabIndex = 10;
            this.credentialCheckBox.Text = "認証";
            this.credentialCheckBox.UseVisualStyleBackColor = true;
            // 
            // idTtextBox
            // 
            this.idTtextBox.Location = new System.Drawing.Point(94, 279);
            this.idTtextBox.Name = "idTtextBox";
            this.idTtextBox.Size = new System.Drawing.Size(162, 19);
            this.idTtextBox.TabIndex = 11;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(335, 279);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(122, 19);
            this.passwordTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "ホスト";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "ポート";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 279);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "ユーザーID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(277, 279);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "パスワード";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // InternetOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.idTtextBox);
            this.Controls.Add(this.credentialCheckBox);
            this.Controls.Add(this.portNumericUpDown);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.useIECheckBox);
            this.Controls.Add(this.useProxyCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.encodingComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BoardURLTextBox);
            this.Controls.Add(this.timeoutNumericUpDown);
            this.Controls.Add(this.label1);
            this.Name = "InternetOptionsControl";
            this.Size = new System.Drawing.Size(460, 308);
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
        private System.Windows.Forms.TextBox BoardURLTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox encodingComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox useProxyCheckBox;
        private System.Windows.Forms.CheckBox useIECheckBox;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.NumericUpDown portNumericUpDown;
        private System.Windows.Forms.CheckBox credentialCheckBox;
        private System.Windows.Forms.TextBox idTtextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
