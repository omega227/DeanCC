using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 保存可能な設定をカプセル化します
    /// </summary>
    public interface ISavable
    {
        void Reset();
        void Save();
        void BackUp();
    }
}
