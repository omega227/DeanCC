using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DeanCC.GUI.Options
{
    public sealed class PathTextBox : FilterTextBox
    {
        private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();
        private static readonly string InvalidPathText = "\\/:*?\"<>|";

        public override char[] InvalidChars
        {
            get
            {
                return InvalidPathChars;
            }
        }

        protected override void OnInvalidTextInput(EventArgs e)
        {
            MessageBox.Show("パスには次の文字は使えません\n" + InvalidPathText,
                "確認", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            base.OnInvalidTextInput(e);
        }
    }
}
