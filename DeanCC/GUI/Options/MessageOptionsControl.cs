using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCC.GUI.Options
{
    public sealed class MessageOptionsControl : OptionsControl
    {
        public MessageOptionsControl()
        {
            InitializeComponent();
        }

        public override void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            timeoutNumericUpDown.Value = source.MessageOptions.Timeout;
            patroledCheckBox.Checked = source.MessageOptions.VisiblePatrolled;
            secureCheckBox.Checked = source.MessageOptions.VisibleSecureFile;
            base.Set(source);
        }

        public override void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.MessageOptions.Timeout = (int)timeoutNumericUpDown.Value;
            destination.MessageOptions.VisiblePatrolled = patroledCheckBox.Checked;
            destination.MessageOptions.VisibleSecureFile = secureCheckBox.Checked;
            base.Get(destination);
        }


        private System.Windows.Forms.CheckBox secureCheckBox;
        private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox patroledCheckBox;

        private void InitializeComponent()
        {
            this.patroledCheckBox = new System.Windows.Forms.CheckBox();
            this.secureCheckBox = new System.Windows.Forms.CheckBox();
            this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // patroledCheckBox
            // 
            this.patroledCheckBox.AutoSize = true;
            this.patroledCheckBox.Location = new System.Drawing.Point(3, 73);
            this.patroledCheckBox.Name = "patroledCheckBox";
            this.patroledCheckBox.Size = new System.Drawing.Size(115, 16);
            this.patroledCheckBox.TabIndex = 0;
            this.patroledCheckBox.Text = "ダウンロード完了後";
            this.toolTip.SetToolTip(this.patroledCheckBox, "画像のダウンロードが完了した時にバルーンメッセージを表示します");
            this.patroledCheckBox.UseVisualStyleBackColor = true;
            // 
            // secureCheckBox
            // 
            this.secureCheckBox.AutoSize = true;
            this.secureCheckBox.Location = new System.Drawing.Point(3, 105);
            this.secureCheckBox.Name = "secureCheckBox";
            this.secureCheckBox.Size = new System.Drawing.Size(152, 16);
            this.secureCheckBox.TabIndex = 1;
            this.secureCheckBox.Text = "パスワード付き画像取得時";
            this.toolTip.SetToolTip(this.secureCheckBox, "パスワード付き画像を取得した時にバルーンメッセージを表示します\r\n");
            this.secureCheckBox.UseVisualStyleBackColor = true;
            // 
            // timeoutNumericUpDown
            // 
            this.timeoutNumericUpDown.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Location = new System.Drawing.Point(3, 30);
            this.timeoutNumericUpDown.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Minimum = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
            this.timeoutNumericUpDown.Size = new System.Drawing.Size(120, 19);
            this.timeoutNumericUpDown.TabIndex = 2;
            this.timeoutNumericUpDown.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.timeoutNumericUpDown, "バルーンメッセージを表示する時間(ミリ秒)を指定します");
            this.timeoutNumericUpDown.Value = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "表示時間（ミリ秒）";
            // 
            // MessageOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeoutNumericUpDown);
            this.Controls.Add(this.secureCheckBox);
            this.Controls.Add(this.patroledCheckBox);
            this.Name = "MessageOptionsControl";
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
