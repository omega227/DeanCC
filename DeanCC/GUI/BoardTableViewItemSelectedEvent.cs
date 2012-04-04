using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;

namespace DeanCC.GUI
{
    public sealed class BoardTableTreeViewItemSelectedEventArgs : EventArgs
    {
        public BoardTableTreeViewItemSelectedEventArgs(IBoardInfo selectedBoard)
        {
            SelectedBoard = selectedBoard;
        }
        public IBoardInfo SelectedBoard { get; private set; }
    }
}
