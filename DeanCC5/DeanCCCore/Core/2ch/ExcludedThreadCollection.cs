using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public sealed class ExcludedThreadCollection : BindingThreadCollection
    {
        public ExcludedThreadCollection()
        {
        }

        protected override Func<Thread, bool> Applicable
        {
            get
            {
                return thread =>
                {
                    return thread.Header.IsIgnored;
                };
            }
        }
    }
}
