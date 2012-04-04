using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    /// <summary>
    /// スレッドの現在の状態を指定します
    /// </summary>
    public enum ThreadState
    {
        /// <summary>
        /// 未取得
        /// </summary>
        None,
        /// <summary>
        /// 通常
        /// </summary>
        Normal,
        /// <summary>
        /// dat落ち
        /// </summary>
        Pastlog,
        /// <summary>
        /// あぼーんを検出
        /// </summary>
        ABone,
        /// <summary>
        /// ダウンロード中
        /// </summary>
        Downloading,
        /// <summary>
        /// レス取得中
        /// </summary>
        Updating,
        /// <summary>
        /// ダウンロードキャンセル中
        /// </summary>
        CancelDownload,
        /// <summary>
        /// レス取得完了（新着あり）
        /// </summary>
        Updated,
        /// <summary>
        /// レス取得完了（新着なし）
        /// </summary>
        NotUpdate,
        /// <summary>
        /// ダウンロード完了
        /// </summary>
        DownloadComplete
    }
}
