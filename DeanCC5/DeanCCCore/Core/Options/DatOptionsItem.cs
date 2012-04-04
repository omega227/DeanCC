using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class DatOptionsItem
    {
        public DatOptionsItem()
        {
            LogSaveMode = LogSaveModes.DatOnly;
            DefaultSaveFolder = Settings.SaveFolder;
            SavesSameImagesFolder = true;
            FileNameFormat = ThreadHeader.KeyFormat;
            DatAccessRate = ConnectionLimited.MaximumIntervalRate;
            GetatableBg20Server = false;
        }
        /// <summary>
        /// dat(html)の保存パターン
        /// </summary>
        public LogSaveModes LogSaveMode { get; set; }
        /// <summary>
        /// 画像と同じ場所に保存されない場合のdat(html)の保存先
        /// </summary>
        public string DefaultSaveFolder { get; set; }
        /// <summary>
        /// dat(html)を画像と同じ場所に保存する
        /// </summary>
        public bool SavesSameImagesFolder { get; set; }
        //private string fileNameFormat;
        /// <summary>
        /// datのファイル名フォーマット
        /// </summary>
        public string FileNameFormat
        {
            get;
            set;
            //get { return fileNameFormat; }
            //set
            //{
            //    if (value.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) > -1)
            //    {
            //        throw new ArgumentException("無効な文字が含まれています", "DATのファイル名フォーマット");
            //    }
            //    fileNameFormat = value;
            //}
        }
        /// <summary>
        /// dat取得間隔の倍率
        /// </summary>
        public int DatAccessRate { get; set; }
        /// <summary>
        /// bg20サーバーからDatを初回取得する
        /// </summary>
        public bool GetatableBg20Server { get; set; }
    }

    /// <summary>
    /// ログテキストを保存する方法を指定します
    /// </summary>
    public enum LogSaveModes
    {
        /// <summary>
        /// 保存しません
        /// </summary>
        None,
        /// <summary>
        /// DATのみ保存します
        /// </summary>
        DatOnly,
        //HtmlOnly,
        /// <summary>
        /// DATとHTML両方を保存します
        /// </summary>
        BothDatAndHtml
    }
}
