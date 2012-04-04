using System;
using System.Windows.Forms;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    /// <summary>
    /// 設定項目を表示・編集するベースコントロールを提供します
    /// </summary>
    public partial class OptionsControl : UserControl, IOptionsControl
    {
        public OptionsControl()
        {
            InitializeComponent();
        }

        public virtual void Get(OptionItems destination)
        {
        }

        public virtual void Set(OptionItems source)
        {
            OnSet(EventArgs.Empty);
        }

        protected virtual void OnSet(EventArgs e)
        {
            loaded = true;
        }

        public virtual void Reset()
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
