using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;

namespace DeanCCCore.Core._2ch
{
    /// <summary>
    /// 2chスレッドのコレクションを提供します
    /// </summary>
    [Serializable]
    public sealed class ThreadCollection : Collection<Thread>
    {
        public ThreadCollection()
            : base()
        {
        }

        /// <summary>
        /// 指定したスレッドが現在のインスタンスに存在するかどうかを判断します
        /// </summary>
        /// <param name="header">存在を確認するスレッド</param>
        /// <returns>存在している場合はtrue,それ以外はfalse</returns>
        public bool Contains(IThreadHeader header)
        {            
            return this.Any(thread => thread.Header.Equals(header));
        }
    }
}
