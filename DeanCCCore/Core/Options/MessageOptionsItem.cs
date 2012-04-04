using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class MessageOptionsItem
    {
        public MessageOptionsItem()
        {
            VisibleSecureFile = true;
            VisiblePatrolled = false;
            Timeout = 15 * 1000;
        }

        /// <summary>
        /// パスワード付きZIPを取得した時に通知するかどうかを示します
        /// </summary>
        public bool VisibleSecureFile { get; set; }

        /// <summary>
        /// 巡回後に通知するかどうかを示します
        /// </summary>
        public bool VisiblePatrolled { get; set; }

        /// <summary>
        /// バルーンを表示する時間（ミリ秒）を表します
        /// </summary>
        public int Timeout { get; set; }
    }
}
