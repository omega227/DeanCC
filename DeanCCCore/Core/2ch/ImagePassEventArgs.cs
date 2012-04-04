using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    /// <summary>
    /// 画像認証・パスワードが要求される画像のイベントデータを表します
    /// </summary>
    public sealed class ImagePassEventArgs : EventArgs
    {
        public ImagePassEventArgs(Thread source, ImageHeader image)
        {
            Source = source;
            BlockedImageHeader = image;
        }
        /// <summary>
        /// 画像の提供元スレッド
        /// </summary>
        public Thread Source { get; private set; }
        /// <summary>
        /// 画像認証・パスワードが要求された画像
        /// </summary>
        public ImageHeader BlockedImageHeader { get; private set; }
    }
}
