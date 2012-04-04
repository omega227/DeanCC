using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 画像アップローダーへの接続制限機能を実装します
    /// </summary>
    public sealed class UploaderConnectionLimited : IConnectionLimited
    {
        public event EventHandler Waiting;
        public event EventHandler Waited;
        /// <summary>
        /// 同じアップローダーに続けて接続できる最小間隔
        /// </summary>
        public const int MinimumAccessInterval = 1500;
        List<string> lockedList = new List<string>();
        public void Wait(string id)
        {
            OnWaiting();
            do
            {
                Thread.Sleep(MinimumAccessInterval);
            }
            while (lockedList.Contains(id));

            lockedList.Add(id);
            OnWaited();
        }

        public void Release(string id)
        {
            lockedList.Remove(id);
        }

        private void OnWaiting()
        {
            if (Waiting != null)
            {
                Waiting(this, EventArgs.Empty);
            }
        }
        private void OnWaited()
        {
            if (Waited != null)
            {
                Waited(this, EventArgs.Empty);
            }
        }
    }
}
