using System;
using System.Linq;
using System.Windows.Forms;

namespace DeanCC.GUI.Options
{
    public sealed partial class ZipOptionsControl : UserControl , IOptionsControl
    {
        public ZipOptionsControl()
        {
            InitializeComponent();
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.ZipOptions.SavesSameImagesFolder = SavesSameImagePathCheckBox.Checked;
            destination.ZipOptions.DefaultSaveFolder = savePathFolderBrowserControl.SelectedPath;
            destination.ZipOptions.Keywords.Clear();
            foreach (string keyword in keywordListBox.Items)
            {
                destination.ZipOptions.Keywords.Add(keyword);
            }
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            SavesSameImagePathCheckBox.Checked = source.ZipOptions.SavesSameImagesFolder;
            savePathFolderBrowserControl.SelectedPath = source.ZipOptions.DefaultSaveFolder;
            keywordListBox.Items.AddRange(source.ZipOptions.Keywords.ToArray());
            loaded = true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        private bool loaded;
        public bool Loaded
        {
            get { return loaded; }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(keywordTextBox.Text) && !keywordListBox.Items.Contains(keywordTextBox.Text))
            {
                keywordListBox.Items.Add(keywordTextBox.Text);
            }
            keywordTextBox.Clear();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (keywordListBox.SelectedIndices != null)
            {
                foreach (int i in keywordListBox.SelectedIndices)
                {
                    keywordListBox.Items.RemoveAt(i);
                }
            }
        }
    }
}
