using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeanCC.GUI.Options
{
    /// <summary>
    /// 禁止文字フィルターを含むテキストボックスを提供します
    /// </summary>
    public class FilterTextBox : TextBox
    {
        public virtual char[] InvalidChars
        {
            get
            {
                return new char[] { };
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Text.IndexOfAny(InvalidChars) > -1)
            {
                OnInvalidTextInput(EventArgs.Empty);
                return;
            }
            base.OnTextChanged(e);
        }

        protected virtual void OnInvalidTextInput(EventArgs e)
        {
            if (CanUndo)
            {
                Undo();
            }
            else
            {
                Clear();
            }
        }
    }
}
