using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    /// <summary>
    /// スレッドリストの変更を指定します
    /// </summary>
    public enum ThreadsChangedStatus
    {
        /// <summary>
        /// 変更なし
        /// </summary>
        None,
        /// <summary>
        /// スレッドが追加されました
        /// </summary>
        Add,
        /// <summary>
        /// スレッドが削除されました
        /// </summary>
        Remove,
        /// <summary>
        /// 不要なスレッドが削除されました
        /// </summary>
        Clean
    }

    public static class ThreadsChangedStatusString
    {
        public static string Get(ThreadsChangedStatus destination)
        {
            switch (destination)
            {
                case ThreadsChangedStatus.Add:
                    return "新規スレッド追加";
                case ThreadsChangedStatus.Remove:
                    return "スレッド削除";

                default:
                case ThreadsChangedStatus.None:
                    return string.Empty;
            }
        }
    }

    public sealed class ThreadsChangedEventArgs : EventArgs
    {
        public ThreadsChangedEventArgs(Thread thread, ThreadsChangedStatus status)
        {
            Thread = thread;
            Status = status;
        }
        public Thread Thread { get; set; }
        public ThreadsChangedStatus Status { get; set; }
    }
}
