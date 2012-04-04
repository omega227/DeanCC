using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DeanCC.GUI.Options
{
    public sealed class FileNameTextBox : FilterTextBox
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
        private static readonly string InvalidFileNameText = "\\/:*?\"<>|";

        public override char[] InvalidChars
        {
            get
            {
                return InvalidFileNameChars;
            }
        }

        protected override void OnInvalidTextInput(EventArgs e)
        {
            MessageBox.Show("ファイル名には次の文字は使えません\n" + InvalidFileNameText,
                "確認", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            base.OnInvalidTextInput(e);
        }
    }
}
