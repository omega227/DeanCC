using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    /// <summary>
    /// サーバーへの接続を制限する機能を提供します
    /// </summary>
    public interface IConnectionLimited
    {
        event EventHandler Waiting;
        event EventHandler Waited;
        void Wait(string id);
        void Release(string id);
    }
}
