using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{    
    public sealed class SecureThreadCollection : BindingThreadCollection
    {
        public SecureThreadCollection()
        {
        }

        protected override Func<Thread, bool> Applicable
        {
            get
            {
                return thread =>
                    {
                        return !thread.DownloadCompleted &&
                            thread.ImageHeaders.Any(image => image.Secure) &&
                            base.Applicable(thread);
                    };
            }
        }
    }
}
