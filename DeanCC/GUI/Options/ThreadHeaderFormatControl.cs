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
    public partial class ThreadHeaderFormatControl : UserControl
    {
        public ThreadHeaderFormatControl()
        {
            InitializeComponent();
            for (int i = 1; i < DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs.Length; i += 2)
            {
                formatComboBox.Items.Add(DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs[i]);
            }
            formatComboBox.SelectedIndex = 0;
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formatComboBox.SelectedIndex > 0)
            {
                textBox.AppendText(DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs[2 * (formatComboBox.SelectedIndex - 1)]);
            }
        }

        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
    }
}
