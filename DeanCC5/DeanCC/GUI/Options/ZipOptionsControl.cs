using System;
using System.Linq;
using System.Windows.Forms;

namespace DeanCC.GUI.Options
{
    public sealed partial class ZipOptionsControl : UserControl, IOptionsControl
    {
        public ZipOptionsControl()
        {
            InitializeComponent();
        }

        private static readonly string[] sampleKeywordFormats = { "%date=yyyyMMdd%", "%date=yyMMdd%", "%date=MMdd%", "%date=dd%", "%body=パス|ぱす|pass.*?([a-z0-9]+)%" };

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
            while (keywordListBox.SelectedIndices.Count > 0)
            {
                keywordListBox.Items.RemoveAt(keywordListBox.SelectedIndices[0]);
            }
        }

        private void smapleKeysComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (smapleKeysComboBox.SelectedIndex >= 0)
            {
                keywordTextBox.Text = sampleKeywordFormats[smapleKeysComboBox.SelectedIndex];
            }
        }
    }
}
