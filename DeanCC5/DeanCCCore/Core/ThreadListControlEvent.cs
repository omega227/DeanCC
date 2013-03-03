using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public enum SelectedThreadListMenu
    {
        /// <summary>
        /// なし
        /// </summary>
        None,
        /// <summary>
        /// 取得したすべてのスレッド
        /// </summary>
        AllThread,
        /// <summary>
        /// 高頻度ダウンロード対象のスレッド
        /// </summary>
        QuickDownloadingThread,
        /// <summary>
        /// ダウンロード対象のスレッド
        /// </summary>
        DownloadingThread,
        /// <summary>
        /// しきい値未満(ダウンロード対象外)のスレッド
        /// </summary>
        DownloadPausedThread,
        /// <summary>
        /// ダウンロード完了(dat落ち)スレッド
        /// </summary>
        DownloadedThread,
        /// <summary>
        /// 自動取得から除外されたスレッド
        /// </summary>
        ExcludedThread,
        /// <summary>
        /// 画像認証zip・パス付zip
        /// </summary>
        SecureImage,
        /// <summary>
        /// 動作ログ
        /// </summary>
        Log,
        /// <summary>
        /// 詳細情報
        /// </summary>
        Information,
    }
    public sealed class ThreadListEventArgs : EventArgs
    {
        public ThreadListEventArgs(SelectedThreadListMenu menu)
        {
            SelectedMenu = menu;
        }
        public SelectedThreadListMenu SelectedMenu { get; private set; }
    }
}
