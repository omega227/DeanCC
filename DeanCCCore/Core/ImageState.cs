using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public enum ImageState
    {
        /// <summary>
        /// 未取得
        /// </summary>
        Non,
        /// <summary>
        /// ダウンロード完了
        /// </summary>
        DownloadComplete,
        /// <summary>
        /// ダウンロード失敗
        /// </summary>
        DownloadFailed,
        /// <summary>
        /// NG画像
        /// </summary>
        NGFile,
        /// <summary>
        /// 再取得
        /// </summary>
        Retry,
        /// <summary>
        /// パス付・画像認証
        /// </summary>
        Secure,
        /// <summary>
        /// 重複ダウンロード
        /// </summary>
        RepeatedDownload
    }
}
