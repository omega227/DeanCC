using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;

namespace DeanCC.GUI
{
    public partial class ImageHeaderForm : Form
    {
        public ImageHeaderForm(BindingImageHeaderCollection source)
        {
            InitializeComponent();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = source;
            imageHeaderView1.DataSource = bindingSource;
        }
    }
}
