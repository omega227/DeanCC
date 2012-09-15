using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using DeanCCCore.Core;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    public sealed partial class NGOptionsControl : UserControl , IOptionsControl
    {
        public NGOptionsControl()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ngTextBox.Text) && !ngListBox.Items.Contains(ngTextBox.Text))
            {
                ngListBox.Items.Add(ngTextBox.Text);
            }
            ngTextBox.Clear();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            while (ngListBox.SelectedIndices.Count > 0)
            {
                ngListBox.Items.RemoveAt(ngListBox.SelectedIndices[0]);
            }
        }

        private void addClipboardButton_Click(object sender, EventArgs e)
        {
            string text = Clipboard.GetText();
            if (MessageBox.Show("クリップボードにコピーされているURLのDNSホスト名をすべて追加します。\nURL・ホスト名は空白・改行で複数に分けることができます。",
                "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (text != string.Empty)
                {
                    List<string> list = new List<string>();
                    StringReader sr = new StringReader(text);
                    Regex regexURL = new Regex(@"^[-_.!~*'a-zA-Z0-9;?/:@&=+$,%#]+$");
                    Regex regex = new Regex(@"h?(?<url>ttp://[-_.!~*'a-zA-Z0-9;?:@&=+$,%#]+/)");
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line == string.Empty || regexURL.IsMatch(line) == false)
                        {
                            continue;
                        }

                        Match m = regex.Match(line);
                        if (m.Success)
                        {
                            if (list.Contains(m.Groups["url"].Value) == false && ngListBox.Items.Contains(m.Groups["url"].Value) == false)
                                list.Add(m.Groups["url"].Value);
                        }
                        else if (list.Contains(line) == false && ngListBox.Items.Contains(line) == false)
                        {
                            list.Add(line);
                        }
                    }
                    ngListBox.Items.AddRange(list.ToArray());
                }
            }
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            if (globalNGTextBox.Modified)//setterでイベント発生するため、変更を確認
            {
                destination.NGOptions.GlobalNGPattern = globalNGTextBox.Text;
            }
            destination.NGOptions.NGFilestxtPath = NGFilesOpenFileControl.SelectedPath;
            destination.NGOptions.NGUrls.Clear();
            foreach (string ngUrl in ngListBox.Items)
            {
                destination.NGOptions.NGUrls.Add(ngUrl);
            }
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            globalNGTextBox.Text = source.NGOptions.GlobalNGPattern;
            NGFilesOpenFileControl.SelectedPath = source.NGOptions.NGFilestxtPath;
            ngListBox.Items.AddRange(source.NGOptions.NGUrls.ToArray());
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

        private void ngListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ngListBox.SelectedItem != null)
            {
                ngTextBox.Text = ngListBox.SelectedItem.ToString();
            }
        }
    }
}
