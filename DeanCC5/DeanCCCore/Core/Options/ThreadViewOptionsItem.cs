using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class ThreadViewOptionsItem
    {
        public ThreadViewOptionsItem()
        {
            OddRowsColor = SystemColors.Window;
            EvenRowsColor = Color.WhiteSmoke;
            DoubleClickPerformItemName = "openFolderToolStripMenuItem";
            //DoubleClickPerformItemOwnerName = string.Empty;
        }

        /// <summary>
        /// 奇数行の背景色
        /// </summary>
        public Color OddRowsColor
        {
            get;
            set;
        }

        /// <summary>
        /// 偶数行の背景色
        /// </summary>
        public Color EvenRowsColor
        {
            get;
            set;
        }

        /// <summary>
        /// ダブルクリック時に実行するコンテキストメニュー名
        /// </summary>
        public string DoubleClickPerformItemName { get; set; }
        ///// <summary>
        ///// ダブルクリック時に実行するコンテキストメニューの親のメニュー名
        ///// </summary>
        //public string DoubleClickPerformItemOwnerName { get; set; }
        ///// <summary>
        ///// ダブルクリック時に実行するコンテキストメニューがドロップダウンアイテムかどうかを示す値
        ///// </summary>
        //public bool IsDropDownItem
        //{
        //    get
        //    {
        //        return !string.IsNullOrEmpty(DoubleClickPerformItemOwnerName);
        //    }
        //}
    }
}
