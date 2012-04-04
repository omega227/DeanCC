using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core._2ch;

namespace DeanCC.GUI
{
    public sealed class BoardInfoCollectionTextBox : TextBox
    {
        private const string TextFormat = "{0} ";
        public BoardInfoCollectionTextBox()
        {
            Multiline = true;
        }

        public BoardInfoCollection Source
        {
            get { return source; }
            set { source = value; }
        }
        private BoardInfoCollection source;

        public void UpdateText()
        {
            if (source == null)
            {
                throw new InvalidOperationException("Sourceが指定されていません");
            }
            Clear();
            foreach (var board in source)
            {
                base.Text += string.Format(TextFormat, board.Name);
            }
        }
    }
}
