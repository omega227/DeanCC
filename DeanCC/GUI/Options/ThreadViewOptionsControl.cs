using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    public sealed partial class ThreadViewOptionsControl : UserControl, IOptionsControl
    {
        private List<KeyValuePair<string, string>> contextMenuTextNameList = new List<KeyValuePair<string, string>>();
        private const string DropDownMenuItemTextFormat = "{0} {1}";

        public ThreadViewOptionsControl()
        {
            InitializeComponent();
            SetDoubleClickItems();
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.ThreadViewOptions.OddRowsColor = oddColorSelectControl.SelectedColor;
            destination.ThreadViewOptions.EvenRowsColor = evenColorSelectControl.SelectedColor;
            destination.ThreadViewOptions.DoubleClickPerformItemName = 
                contextMenuTextNameList.Find(pair => pair.Key.Equals(doubleClickComboBox.SelectedItem)).Value;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            oddColorSelectControl.SelectedColor = source.ThreadViewOptions.OddRowsColor;
            evenColorSelectControl.SelectedColor = source.ThreadViewOptions.EvenRowsColor;
            doubleClickComboBox.SelectedIndex =
                contextMenuTextNameList.FindIndex(pair => pair.Value.Equals(source.ThreadViewOptions.DoubleClickPerformItemName));
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

        private void SetDoubleClickItems()
        {
            foreach (ToolStripItem item in ((MainForm)Common.MainForm).ThreadViewer.GetRowMenuStrip().Items)
            {
                ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                if (menuItem == null)
                {
                    continue;
                }
                if (menuItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem dropDownItem in menuItem.DropDownItems)
                    {
                        string text = string.Format(DropDownMenuItemTextFormat, menuItem.Text, dropDownItem.Text);
                        doubleClickComboBox.Items.Add(text);
                        contextMenuTextNameList.Add(new KeyValuePair<string, string>(text, dropDownItem.Name));
                    }
                }
                else
                {
                    doubleClickComboBox.Items.Add(menuItem.Text);
                    contextMenuTextNameList.Add(new KeyValuePair<string, string>(menuItem.Text, menuItem.Name));
                }
            }
        }
    }
}
