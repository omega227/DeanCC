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
    public sealed partial class OpenFileControl : UserControl
    {
        public OpenFileControl()
        {
            InitializeComponent();
        }

        private OpenFileDialog dialog;
        private string selectedPath;
        /// <summary>
        /// 選択しているパスを取得または設定します
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
        public string Title { get; set; }
        public string Filter { get; set; }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (dialog == null)
            {
                dialog = new OpenFileDialog()
                {
                    Multiselect = false
                };
            }
            dialog.Title = Title;
            dialog.Filter = Filter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = pathTextBox.Text = dialog.FileName;
            }
        }
    }
}
