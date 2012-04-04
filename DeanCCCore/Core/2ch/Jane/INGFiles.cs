using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch.Jane
{
    public interface INGFiles
    {
        /// <summary>
        /// 指定したデータがこのインスタンスに含まれるか
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Exists(byte[] data);
        bool Exists(string md5);
        /// <summary>
        /// 再読み込み
        /// </summary>
        void Reload();
    }
}
