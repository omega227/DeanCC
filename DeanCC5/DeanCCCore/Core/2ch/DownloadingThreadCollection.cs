using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public sealed class DownloadingThreadCollection : BindingThreadCollection
    {
        public DownloadingThreadCollection()
        {
        }


        protected override Func<Thread, bool> Applicable
        {
            get
            {
                return (thread) =>
                    {
                        return
                            (thread.QuickDownloading & QuickDownloadState.Selected) != QuickDownloadState.Selected &&
                            thread.Downloadable &&
                            base.Applicable(thread);
                    };
            }
        }
    }
}
