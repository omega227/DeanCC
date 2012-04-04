using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public enum PatrolPatternChangeStatus
    {
        Added,
        Removed
    }

    public enum GenreFolderChangeStaus
    {
        Added,
        Removed
    }

    public sealed class PatrolPatternChangedEventArgs : EventArgs
    {
        public PatrolPatternChangedEventArgs(IPatrolPattern changedPattern, PatrolPatternChangeStatus status)
        {
            ChangedPattern = changedPattern;
            ChangeStatus = status;
        }
        public IPatrolPattern ChangedPattern { get; private set; }
        public PatrolPatternChangeStatus ChangeStatus { get; private set; }
    }

    public sealed class GenreFolderChangedEventArgs : EventArgs
    {
        public GenreFolderChangedEventArgs(IGenreFolder changedFolder, GenreFolderChangeStaus status)
        {
            ChangedFolder = changedFolder;
            ChangeStatus = status;
        }
        public IGenreFolder ChangedFolder { get; private set; }
        public GenreFolderChangeStaus ChangeStatus { get; private set; }
    }
}
