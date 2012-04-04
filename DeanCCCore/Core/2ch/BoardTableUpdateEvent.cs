using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public sealed class BoardTableUpdateEventArgs : EventArgs
    {
        public BoardTableUpdateEventArgs()
        {
        }

        public BoardTableUpdateEventArgs( bool updated , string message ,DateTime lastModified )
        {
            Updated = updated;
            Message = message;
            LastModified = lastModified;
        }
        public bool Updated { get;  set; }
        public string Message { get; set; }
        public DateTime LastModified { get; set; }
    }
}
