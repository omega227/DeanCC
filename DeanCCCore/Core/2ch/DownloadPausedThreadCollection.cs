using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public sealed class DownloadPausedThreadCollection : BindingThreadCollection
    {
        protected override Func<Thread, bool> Applicable
        {
            get
            {
                return thread =>
                    {
                        return thread.DownloadPaused &&
                            base.Applicable(thread);
                    };
            }
        }
    }
}
