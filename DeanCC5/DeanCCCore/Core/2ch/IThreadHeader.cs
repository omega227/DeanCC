using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public interface IThreadHeader : IComparable
    {
        IPatrolPattern Parent { get; set; }
        IBoardInfo SourceBoard { get; }
        DateTime Since { get; set; }
        DateTime LastModified { get; set; }
        ThreadState State { get; set; }
        string DatUrl { get; }
        string ETag { get; set; }
        int GotByteCount { get; set; }
        int GotResCount { get; set; }
        int DownloadedCount { get; set; }
        bool IsLimitOverThread { get; }
        string Key { get; }
        string Title { get; }
        int NewResCount { get; set; }
        bool IsPastlog { get; set; }
        bool IsIgnored { get; set; }
        bool CreativeDirectory { get; set; }
        //int ResCount { get; set; }
        //string Subject { get; set; }       
        string Url { get; }
        float ResSpeed { get; }
        bool IsMark { get; set; }
        string Update();
        void SaveLog(string dat);
        string SubFolderName { get; }
        string ImageSaveFolder { get; set; }
        string LogSaveFolder { get; }
        string LogFileName { get; }
        string HtmlLogFileName { get; }
        string Format(string text);
        bool Repaired { get; }
        void ClearStatus();
    }
}
