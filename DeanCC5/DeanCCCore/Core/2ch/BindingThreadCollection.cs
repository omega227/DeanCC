using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DeanCCCore.Core._2ch
{
    public class BindingThreadCollection : BindingCollection<Thread>
    {
        public BindingThreadCollection()
            : base()
        {
        }

        /// <summary>
        /// 内容を更新します
        /// </summary>
        public void Update()
        {
            Items.Clear();
            ((List<Thread>)Items).AddRange(Common.CurrentSettings.AllThreads.Where(Applicable));
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        /// <summary>
        /// 表示名を取得します
        /// </summary>
        public virtual string Name
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 内容に適用する条件を返します
        /// </summary>
        protected virtual Func<Thread, bool> Applicable
        {
            get
            {
                return thread => { return !thread.Header.IsIgnored; };
            }
        } 
    }
}
