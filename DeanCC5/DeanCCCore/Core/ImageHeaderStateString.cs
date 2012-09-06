using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public static class ImageHeaderStateString
    {
        public static string GetString(ImageState state)
        {
            switch (state)
            {
                case ImageState.DownloadComplete:
                    return "ダウンロード完了";
                case ImageState.DownloadFailed:
                    return "ダウンロード失敗";
                case ImageState.NGFile:
                    return "NG画像";
                case ImageState.Retry:
                    return "再ダウンロード";
                case ImageState.Secure:
                    return "パス付き画像";
                default:
                case ImageState.Non:
                    return "未ダウンロード";
            }
        }

        public static string GetString(ImageDownloadResultStatus result)
        {
            switch (result)
            {
                case ImageDownloadResultStatus.BlockedImagePass:
                    return "画像認証が必要です";
                case ImageDownloadResultStatus.ContentLengthMissMatch:
                    return "レスポンスヘッダーと取得したデータの長さが一致しません";
                case ImageDownloadResultStatus.ContentTypeMissMatch:
                    return "レスポンスヘッダーの内容の種類が画像と一致しません";
                case ImageDownloadResultStatus.Failed:
                    return "原因不明の失敗です";
                case ImageDownloadResultStatus.InvalidUriFormat:
                    return "ダウンロードURIの形式が正しくありません";
                case ImageDownloadResultStatus.FailedReplaceExtractUrl:
                    return "$EXTRACTオプションでの画像URLの取得に失敗しました";
                case ImageDownloadResultStatus.OverLimitDate:
                    return "最大試行日数を超えています";
                case ImageDownloadResultStatus.OverTriedCount:
                    return "最大試行回数を超えています";
                case ImageDownloadResultStatus.PasswordMissMatch:
                    return "パスワードが一致しません";
                case ImageDownloadResultStatus.Success:
                    return "成功";
                default:
                case ImageDownloadResultStatus.None:
                    return "なし";
            }
        }
    }
}
