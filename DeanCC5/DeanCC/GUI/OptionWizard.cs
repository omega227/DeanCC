using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI
{
    public partial class OptionWizard : Form
    {
        private int currentPosition;
        private UserControl[] settings = 
        {

        };

        public OptionItems Option
        {
            get;
            set;
        }

        public OptionWizard()
        {
            InitializeComponent();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (currentPosition >= settings.Length)
            {
                nextButton.Text = "完了";
                nextButton.Click -= new EventHandler(nextButton_Click);
                nextButton.Click += new EventHandler(OnComplete);
            }
            if (currentPosition <= 0)
            {
                beforeButton.Enabled = true;
            }

            currentPosition++;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(settings[currentPosition]);
        }

        private void OnComplete(object sender, EventArgs e)
        {
            Close();
        }

        private void beforeButton_Click(object sender, EventArgs e)
        {
            if (currentPosition >= settings.Length)
            {
                nextButton.Text = "次へ";
                nextButton.Click -= new EventHandler(OnComplete);
                nextButton.Click +=new EventHandler(nextButton_Click);
            }
            if (currentPosition <= 1)
            {
                beforeButton.Enabled = false;
            }

            currentPosition--;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(settings[currentPosition]);
        }

        private void OptionWizard_Load(object sender, EventArgs e)
        {
            Option = new OptionItems();
            beforeButton.Enabled = false;
            mainPanel.Controls.Add(settings[currentPosition]);
        }
    }
}
