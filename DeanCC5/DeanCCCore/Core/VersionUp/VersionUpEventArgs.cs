using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core.VersionUp
{
    public sealed class VersionUpEventArgs : System.ComponentModel.CancelEventArgs
    {
        public VersionUpEventArgs()
        {
        }

        public bool ExistsNewVersion { get; set; }
        public string Version { get; set; }
        public string ReleseText { get; set; }
    }    
}
