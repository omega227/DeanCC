using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core
{
    public interface IPatrolPattern
    {
        bool Initialized { get; }
        bool Enable { get; set; }
        //BoardInfo TargetBoard { get; set; }
        BoardInfoCollection TargetBoards { get; set; }
        bool IsIgnorePattern { get; set; }
        string ExtensionFormat { get; }
        bool EnableJpg { get; }
        bool EnablePng { get; }
        bool EnableGif { get; }
        bool EnableBmp { get; }
        bool EnableZip { get; }
        string Name { get; set; }
        string Pattern { get; set; }
        string NGPattern { get; set; }
        GenreFolder ParentFolder { get; }
        bool CreatesSubFolder { get; }
        string SubFolderFormat { get; set; }
        //string SaveFolderByFormat { get; }
        bool IsMatch(IThreadHeader input);
        //IThreadHeader[] Patrol();
    }
}
