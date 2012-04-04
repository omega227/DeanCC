using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeanCC.GUI.Options
{
    public sealed partial class FolderBrowserControl : UserControl
    {
        public FolderBrowserControl()
        {
            InitializeComponent();
        }

        public string Description { get; set; }
        private string selectedPath;
        /// <summary>
        /// 選択しているフォルダーパスを取得または設定します
        /// </summary>
        public string SelectedPath
        {
            get { return selectedPath; }
            set
            {
                if (selectedPath != value)
                {
                    selectedPath = pathTextBox.Text = value;
                }
            }
        }
        public bool ShowNewFolderButton { get; set; }
        private FolderBrowserDialog dialog;

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (dialog == null)
            {
                dialog = new FolderBrowserDialog();
            }
            dialog.Description = Description;
            dialog.ShowNewFolderButton = ShowNewFolderButton;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = pathTextBox.Text = dialog.SelectedPath;
            }
        }
    }
}
