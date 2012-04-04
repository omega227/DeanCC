using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 定期的に実行される機能をカプセル化します
    /// 自動で呼び出しは行われません
    /// </summary>
    public interface ITimerExecution
    {
        event EventHandler<System.ComponentModel.CancelEventArgs> Running;
        event EventHandler Ran;
        /// <summary>
        /// 定期的に実行されるメイン機能です
        /// </summary>
        void Run();
        /// <summary>
        /// 実行を停止します
        /// </summary>
        void Stop();
    }
}
