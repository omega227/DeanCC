using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class StartupOptionsItem
    {
        public StartupOptionsItem()
        {
            RemoveExpirationThread = false;
            ThreadLifeDate = 30;
            RemoveExpirationImageHash = false;
            HashLifeDate = 30;
            Minimum = false;
            AutoCheckNewVersion = false;
        }

        /// <summary>
        /// 逆シリアル化の直後に発生します。このメソッドはvirtualにできません
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
            if (ThreadLifeDate == 0)
            {
                RemoveExpirationThread = false;
                ThreadLifeDate = 30;
            }
        }

        /// <summary>
        /// 期限切れの画像ハッシュを削除するかどうかを示す値
        /// </summary>
        public bool RemoveExpirationImageHash { get; set; }
        /// <summary>
        /// ダウンロード済み画像ハッシュの有効日数
        /// </summary>
        public int HashLifeDate { get; set; }
        /// <summary>
        /// 期限切れのスレッドを削除するかどうかを示す値
        /// </summary>
        public bool RemoveExpirationThread { get; set; }
        /// <summary>
        /// スレッドの有効日数
        /// </summary>
        public int ThreadLifeDate { get; set; }
        /// <summary>
        /// 最小化状態にするか
        /// </summary>
        public bool Minimum { get; set; }
        /// <summary>
        /// 自動的に最新版を確認するか
        /// </summary>
        public bool AutoCheckNewVersion { get; set; }
    }
}
