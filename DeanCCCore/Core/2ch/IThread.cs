using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public interface IThread
    {
        IThreadHeader Header { get; set; }
        string StateText { get; }
        bool DownloadCompleted { get; }
        ImageHeaderCollection ImageHeaders { get; }
        void Run();
        bool Update();        
        string SaveLogHtml();
        string SaveSecureImageLogHtml();
        void CancelDownload();
    }
}
