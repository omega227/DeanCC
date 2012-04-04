using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core;

namespace DeanCC.GUI.Options
{
    public sealed class CommandOptionsControl : OptionsControl
    {
        public CommandOptionsControl()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader eventColumnHeader;
        private System.Windows.Forms.ColumnHeader commandColumnHeader;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private ThreadHeaderFormatControl commandControl;
        private System.Windows.Forms.TextBox nameTextBox;

        private void InitializeComponent()
        {
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commandColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.commandControl = new DeanCC.GUI.Options.ThreadHeaderFormatControl();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(15, 25);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(135, 19);
            this.nameTextBox.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.eventColumnHeader,
            this.commandColumnHeader});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(15, 153);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(432, 123);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "名前";
            // 
            // eventColumnHeader
            // 
            this.eventColumnHeader.Text = "種類";
            // 
            // commandColumnHeader
            // 
            this.commandColumnHeader.Text = "コマンド";
            this.commandColumnHeader.Width = 277;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "右クリックメニュー",
            "ダウンロード完了後"});
            this.comboBox1.Location = new System.Drawing.Point(15, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(135, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "名前";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "種類";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "コマンド";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(280, 282);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "追加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(372, 282);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "削除";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // commandControl
            // 
            this.commandControl.Location = new System.Drawing.Point(15, 121);
            this.commandControl.Name = "commandControl";
            this.commandControl.Size = new System.Drawing.Size(424, 26);
            this.commandControl.TabIndex = 2;
            // 
            // CommandOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.commandControl);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.nameTextBox);
            this.Name = "CommandOptionsControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public override void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            foreach (Command command in source.CommandOptions.CommandList)
            {
                System.Windows.Forms.ListViewItem item =
                    new System.Windows.Forms.ListViewItem(new string[] { command.Name, CommandModeString.GetText(command.CommandMode), command.Value });
                item.Tag = command.CommandMode;
                listView1.Items.Add(item);
            }
            base.Set(source);
        }

        public override void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.CommandOptions.CommandList.Clear();
            foreach (System.Windows.Forms.ListViewItem item in listView1.Items)
            {
                Command command = new Command(item.SubItems[0].Text, item.SubItems[2].Text, (CommandMode)item.Tag);
                destination.CommandOptions.CommandList.Add(command);
            }
            base.Get(destination);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            CommandMode mode = (CommandMode)comboBox1.SelectedIndex;
            System.Windows.Forms.ListViewItem item =
                new System.Windows.Forms.ListViewItem(new string[] { nameTextBox.Text, CommandModeString.GetText(mode), commandControl.Text });
            item.Tag = mode;
            listView1.Items.Add(item);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                System.Windows.Forms.ListViewItem selectedItem = listView1.SelectedItems[0];
                listView1.Items.Remove(selectedItem);
            }
        }
    }
}
