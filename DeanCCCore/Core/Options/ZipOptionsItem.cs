using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class ZipOptionsItem
    {
        public ZipOptionsItem()
        {
            Keywords = new KeywordCollection();
            DefaultSaveFolder = Settings.SaveFolder;
            SavesSameImagesFolder = true;
        }
        /// <summary>
        /// キーワードリスト
        /// </summary>
        public KeywordCollection Keywords { get; set; }
        /// <summary>
        /// 画像と同じ場所に保存されない場合のzipの保存先
        /// </summary>
        public string DefaultSaveFolder { get; set; }
        /// <summary>
        /// zipを画像と同じ場所に保存する
        /// </summary>
        public bool SavesSameImagesFolder { get; set; }
    }
}
