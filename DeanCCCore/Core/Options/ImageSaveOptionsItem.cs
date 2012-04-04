using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class ImageSaveOptionsItem
    {
        public ImageSaveOptionsItem()
        {
            MovesSaveFolder = false;
            BlockDownloadedImage = true;
            Threshold = 0;
            RetryImageLifeDate = 3;
            MaximumRetryCount = 10;
            ApplyOriginalTimestamp = false;
        }

        ///// <summary>
        ///// 画像ファイル名フォーマット
        ///// </summary>
        //public string FileNameFormat { get; set; }
        /// <summary>
        /// ダウンロード済み画像を保存しない
        /// </summary>
        public bool BlockDownloadedImage { get; set; }
        /// <summary>
        /// ダウンロード失敗画像の有効日数
        /// </summary>
        public int RetryImageLifeDate { get; set; }        
        /// <summary>
        /// ダウンロード失敗画像のダウンロード制限回数
        /// </summary>
        public int MaximumRetryCount { get; set; }        
        /// <summary>
        /// 画像保存のしきい値
        /// </summary>
        public int Threshold { get; set; }
        /// <summary>
        /// ダウンロード完了後にサブフォルダーを移動するかどうか
        /// </summary>
        public bool MovesSaveFolder { get; set; }
        /// <summary>
        /// ダウンロード完了後の移動先フォルダー
        /// </summary>
        public string MovedDestinationFolder { get; set; }
        /// <summary>
        /// 元画像の更新日時を適用するかどうか
        /// </summary>
        public bool ApplyOriginalTimestamp { get; set; }
    }
}
