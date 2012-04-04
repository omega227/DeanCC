using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class WindowOptionsItem
    {
        public WindowOptionsItem()
        {
            MinimumShowInTaskbar = true;
            CloseInTasktray = false;
        }
        /// <summary>
        /// 最小化時にタスクバーに表示する
        /// </summary>
        public bool MinimumShowInTaskbar { get; set; }
        /// <summary>
        /// 閉じられた時にタスクトレイに格納する
        /// </summary>
        public bool CloseInTasktray { get; set; }
    }
}
