using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core.VersionUp;
using DeanCCCore.Core;

namespace DeanCC.GUI
{
    public partial class NewVersionForm : Form
    {
        public NewVersionForm(string releaseText)
        {
            InitializeComponent();
            releaseTextBox.Text = releaseText;
        }
    }
}
