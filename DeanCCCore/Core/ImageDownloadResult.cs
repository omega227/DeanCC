using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 画像のダウンロード結果を表します
    /// </summary>
    public sealed class ImageDownloadResult
    {
        public ImageDownloadResult(ImageDownloadResultStatus status)
        {
            Status = status;
        }

        public ImageDownloadResult(byte[] data, string url, string referer, DateTime lastModified, string contentType)
        {
            Status = ImageDownloadResultStatus.Success;
            Data = data;
            Url = url;
            Referer = referer;
            LastModified = lastModified;
            ContentType = contentType;
        }

        public ImageDownloadResultStatus Status { get; set; }
        public byte[] Data { get; private set; }
        public DateTime LastModified { get; private set; }
        public string Url { get; private set; }
        public string Referer { get; private set; }
        public string ContentType { get; private set; }
    }

    /// <summary>
    /// 画像ダウンロードの結果を示します
    /// </summary>
    public enum ImageDownloadResultStatus
    {
        /// <summary>
        /// なし
        /// </summary>
        None,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 原因不明の失敗です
        /// </summary>
        Failed,
        /// <summary>
        /// ダウンロードURIの形式が正しくありません
        /// </summary>
        InvalidUriFormat,
        /// <summary>
        /// 最大試行回数を超えています
        /// </summary>
        OverTriedCount,
        /// <summary>
        /// 最大試行日数を超えています
        /// </summary>
        OverLimitDate,
        /// <summary>
        /// レスポンスヘッダーの内容の種類が画像と一致しません
        /// </summary>
        ContentTypeMissMatch,
        /// <summary>
        /// レスポンスヘッダーと取得したデーターの長さが一致しません
        /// </summary>
        ContentLengthMissMatch,
        /// <summary>
        /// 画像認証が必要です
        /// </summary>
        BlockedImagePass,
        /// <summary>
        /// パスワードが一致しません
        /// </summary>
        PasswordMissMatch
    }
}
