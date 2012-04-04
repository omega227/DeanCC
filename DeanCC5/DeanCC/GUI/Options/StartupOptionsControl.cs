using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    public sealed partial class StartupOptionsControl : UserControl , IOptionsControl
    {
        public StartupOptionsControl()
        {
            InitializeComponent();
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.StartupOptions.Minimum = minimumCheckBox.Checked;
            destination.StartupOptions.RemoveExpirationThread = threadCheckBox.Checked;
            destination.StartupOptions.ThreadLifeDate = (int)threadNumericUpDown.Value;
            destination.StartupOptions.RemoveExpirationImageHash = removeHashCheckBox.Checked;
            destination.StartupOptions.HashLifeDate = (int)hashNumericUpDown.Value;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            minimumCheckBox.Checked = source.StartupOptions.Minimum;
            threadCheckBox.Checked = source.StartupOptions.RemoveExpirationThread;
            threadNumericUpDown.Value = source.StartupOptions.ThreadLifeDate;
            removeHashCheckBox.Checked = source.StartupOptions.RemoveExpirationImageHash;
            hashNumericUpDown.Value = source.StartupOptions.HashLifeDate;
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
