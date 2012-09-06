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
        public enum FormatType
        {
            None,
            ThreadHeader,
            ImageHeader
        }

        public ThreadHeaderFormatControl()
            :this(FormatType.ThreadHeader)
        {
        }

        public ThreadHeaderFormatControl(FormatType formatType)
        {
            InitializeComponent();
            switch (formatType)
            {
                case FormatType.ImageHeader:
                    for (int i = 1; i < DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs.Length; i += 2)
                    {
                        formatComboBox.Items.Add(DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs[i]);
                    }
                    for (int i = 1; i < DeanCCCore.Core.ImageHeader.FormatNamePairs.Length; i += 2)
                    {
                        formatComboBox.Items.Add(DeanCCCore.Core.ImageHeader.FormatNamePairs[i]);
                    }
                    break;

                default:
                case FormatType.ThreadHeader:
                    for (int i = 1; i < DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs.Length; i += 2)
                    {
                        formatComboBox.Items.Add(DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs[i]);
                    }
                    break;
            }
            formatComboBox.SelectedIndex = 0;
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formatComboBox.SelectedIndex > 0)
            {
                if (2*formatComboBox.SelectedIndex <= DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs.Length)
                {
                    textBox.AppendText(DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs[2 * (formatComboBox.SelectedIndex - 1)]);
                }
                else
                {
                    int index = 2 * (formatComboBox.SelectedIndex-1) - DeanCCCore.Core._2ch.ThreadHeader.FormatNamePairs.Length;
                    textBox.AppendText(DeanCCCore.Core.ImageHeader.FormatNamePairs[index]);
                }
            }
        }

        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
    }
}
