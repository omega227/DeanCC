using System; 
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Diagnostics; 
using System.Linq; 
using System.Text; 
using System.Windows.Forms;

namespace DeanCC.GUI
{
    public sealed class SeparatorLable : Label
    {
        private const int SeparatorHeight = 2;
        public SeparatorLable()
        {
            AutoSize = false;
            BorderStyle = BorderStyle.Fixed3D;
            Text = string.Empty;
            Height = SeparatorHeight;
        }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);

        //    Left = 0;
        //    Top = (int)(this.Height / 2.0);
        //    Height = 2;
        //}
    }
}
