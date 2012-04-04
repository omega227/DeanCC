using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class NGOptionsItem
    {
        public NGOptionsItem()
        {
            ngFilestxtPath = "";
            ngUrls = new NGUrlTextCollection();
            globalNGPattern = "";
        }

        private string globalNGPattern;
        /// <summary>
        /// すべての巡回設定に適用されるNGワード
        /// </summary>
        public string GlobalNGPattern
        {
            get { return globalNGPattern; }
            set
            {
                if (globalNGPattern != value)
                {
                    globalNGPattern = value;
                    Common.ApplyGlobalNGPatternRegex();
                }
            }
        }

        private string ngFilestxtPath;
        /// <summary>
        /// JaneのNGFiles.txtのパス
        /// </summary>
        public string NGFilestxtPath
        {
            get
            {
                return ngFilestxtPath;
            }
            set
            {
                ngFilestxtPath = value;
            }
        }
        private NGUrlTextCollection ngUrls;
        /// <summary>
        /// NG画像Urlに含まれる文字リスト
        /// </summary>
        public NGUrlTextCollection NGUrls
        {
            get
            {
                return ngUrls;
            }
            set
            {
                ngUrls = value;
            }
        }
    }
}
