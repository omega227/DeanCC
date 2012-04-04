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
    public sealed partial class WindowOptionsControl : UserControl , IOptionsControl
    {
        public WindowOptionsControl()
        {
            InitializeComponent();
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.WindowOptions.MinimumShowInTaskbar = !minimumTaskTrayCheckBox.Checked;
            destination.WindowOptions.CloseInTasktray = closingTaskTrayCheckBox.Checked;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            minimumTaskTrayCheckBox.Checked = !source.WindowOptions.MinimumShowInTaskbar;
            closingTaskTrayCheckBox.Checked = source.WindowOptions.CloseInTasktray;
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
    }
}
