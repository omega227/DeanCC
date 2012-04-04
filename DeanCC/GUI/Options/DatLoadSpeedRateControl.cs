using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;

namespace DeanCC.GUI.Options
{
    public sealed partial class DatLoadSpeedRateControl : UserControl
    {
        private const string SpanTextFormat = @"DAT読み込み間隔 {0:mm\:ss}";

        public DatLoadSpeedRateControl()
        {
            InitializeComponent();
            rateTrackBar.Maximum = ConnectionLimited.MaximumIntervalRate;
        }

        private void rateTrackBar_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan span = TimeSpan.FromMilliseconds(ConnectionLimited.CalculateAccessInterval(rateTrackBar.Value));
            spanLabel.Text = string.Format(SpanTextFormat, span);
        }

        public int Value
        {
            get { return rateTrackBar.Value; }
            set { rateTrackBar.Value = value; }
        }
    }
}
