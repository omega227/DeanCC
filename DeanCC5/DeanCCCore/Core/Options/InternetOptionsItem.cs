using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class InternetOptionsItem
    {
        public InternetOptionsItem()
        {
            timeout = InternetClient.DefaultTimeout;
            currentEncoding = Encoding.GetEncoding("shift_jis");
            boardTableSourceUrl = "http://menu.2ch.net/bbsmenu.html";
            Proxy = new Proxy();
        }
        //public string Name
        //{
        //    get
        //    {
        //        return "通信";
        //    }
        //}
        private int timeout;
        /// <summary>
        /// Http通信に基本的に適用されるタイムアウト(ms)
        /// </summary>
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }
        private Encoding currentEncoding;
        /// <summary>
        /// 基本的に適用されるエンコーディング
        /// </summary>
        public Encoding CurrentEncoding
        {
            get
            {
                return currentEncoding;
            }
            set
            {
                currentEncoding = value;
            }
        }
        private string boardTableSourceUrl;
        /// <summary>
        /// 板一覧取得Url
        /// </summary>
        public string BoardTableSourceUrl
        {
            get
            {
                return boardTableSourceUrl;
            }
            set
            {
                boardTableSourceUrl = value;
            }
        }
        private DateTime boardTableLastModified;
        /// <summary>
        /// 板一覧取得先の最終更新
        /// </summary>
        public DateTime BoardTableLastModified
        {
            get
            {
                return boardTableLastModified;
            }
            set
            {
                boardTableLastModified = value;
            }
        }
        /// <summary>
        /// プロキシ
        /// </summary>
        public Proxy Proxy
        {
            get;
            set;
        }
    }
}
