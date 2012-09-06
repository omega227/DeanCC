namespace DeanCC.GUI
{
    partial class OptionWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.beforeButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.separatorLable1 = new DeanCC.GUI.SeparatorLable();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // beforeButton
            // 
            this.beforeButton.Location = new System.Drawing.Point(396, 345);
            this.beforeButton.Name = "beforeButton";
            this.beforeButton.Size = new System.Drawing.Size(75, 23);
            this.beforeButton.TabIndex = 0;
            this.beforeButton.Text = "前へ";
            this.beforeButton.UseVisualStyleBackColor = true;
            this.beforeButton.Click += new System.EventHandler(this.beforeButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(477, 345);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "次へ";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // separatorLable1
            // 
            this.separatorLable1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separatorLable1.Location = new System.Drawing.Point(7, 327);
            this.separatorLable1.Name = "separatorLable1";
            this.separatorLable1.Size = new System.Drawing.Size(550, 2);
            this.separatorLable1.TabIndex = 2;
            this.separatorLable1.Text = "separatorLable1";
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(7, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(550, 320);
            this.mainPanel.TabIndex = 3;
            // 
            // OptionWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 380);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.separatorLable1);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.beforeButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionWizard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "初期設定ウィザード";
            this.Load += new System.EventHandler(this.OptionWizard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button beforeButton;
        private System.Windows.Forms.Button nextButton;
        private SeparatorLable separatorLable1;
        private System.Windows.Forms.Panel mainPanel;
    }
}