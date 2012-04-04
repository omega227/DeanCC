using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core
{
    public sealed class LogItemCollection : BindingList<LogItem>, ILog
    {
        public LogItemCollection()
        {          
        }

        public void AddBoardsUpdateEvent()
        {
            Common.CurrentSettings.Boards.OnlineUpdated +=
                new EventHandler<BoardTableUpdateEventArgs>(Boards_OnlineUpdated);  
        }
        void Boards_OnlineUpdated(object sender, BoardTableUpdateEventArgs e)
        {
            Add(new LogItem("板一覧更新確認",
                e.Updated ? "更新完了" : string.Format("{0} 最終更新：{1:yy/MM/dd}", e.Message, e.LastModified),
                LogStatus.System));
        }

        delegate void InsertItemHandler(int index, LogItem item);

        /// <summary>
        /// ログを追加します
        /// </summary>
        /// <param name="title">動作の概要を表すタイトル</param>
        /// <param name="text">動作の内容を表す詳細</param>
        /// <param name="status">ログのランク</param>
        public void Add(string title, string text, LogStatus status)
        {
            base.Add(new LogItem(title, text, status));
        }

        protected override void InsertItem(int index, LogItem item)
        {
            if (Common.InvokeRequired)
            {
                Common.InvokeMainForm(new InsertItemHandler(InsertItem), index, item);
            }
            else
            {
                base.InsertItem(index, item);
            }
        }
    }
}
