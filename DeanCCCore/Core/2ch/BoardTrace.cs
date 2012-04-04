using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public sealed class BoardTrace
    {
        public event EventHandler<BoardTraceEventArgs> BoardTraced;
        public BoardTrace(BoardTable boards)
        {
            this.boards = boards;
        }

        private readonly BoardTable boards;

        public void TraceFrom2ch(BoardInfoCollection oldBoards)
        {
            BoardTraceEventArgs e = new BoardTraceEventArgs();


            OnBoardTraced(e);
        }

        private void OnBoardTraced(BoardTraceEventArgs e)
        {
            if (BoardTraced != null)
            {
                BoardTraced(this, e);
            }
        }
    }

    public sealed class BoardTraceEventArgs : EventArgs
    {
        public BoardTraceEventArgs()
        {
        }
        public BoardInfoCollection UpdatedBoards { get; set; }
    }
}
