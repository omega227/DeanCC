using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 2chへの接続制限を実装します
    /// </summary>
    public sealed class ConnectionLimited : IConnectionLimited
    {
        public event EventHandler Waiting;
        public event EventHandler Waited;
        /// <summary>
        /// 一時停止時間の最大倍率
        /// </summary>
        public const int MaximumIntervalRate = 10;
        /// <summary>
        /// デフォルトのスレッド取得中の一時停止最大時間[ms]
        /// </summary>
        public const int DefaultInterval = 15 * 1000;
        private readonly int interval;
        private readonly TimeSpan intervalSpan;
        private const int limitCapacity = 5;
        private Dictionary<string, AccessManager> serverList = new Dictionary<string, AccessManager>();

        public ConnectionLimited()
        {
            interval = DefaultInterval;
            intervalSpan = TimeSpan.FromMilliseconds(interval);
        }

        public ConnectionLimited(int intervalMilliSeconds)
        {
            this.interval = intervalMilliSeconds;
            intervalSpan = TimeSpan.FromMilliseconds(interval);
        }

        public static int CalculateAccessInterval(int rate)
        {
            if (rate <= 0 || rate > MaximumIntervalRate)
            {
                throw new ArgumentOutOfRangeException("rate");
            }

            int interval = (int)Math.Round((double)DefaultInterval * MaximumIntervalRate / rate);
            return interval;
        }

        public void Wait(string id)
        {
            if (serverList.ContainsKey(id))
            {
                int waitMilliSeconds = serverList[id].GetWaitMilliseconds();
                if (waitMilliSeconds > 0)
                {
                    OnWaiting();
                    Thread.Sleep(waitMilliSeconds);
                    OnWaited();
                }
            }
            else
            {
                serverList.Add(id, new AccessManager(intervalSpan));
            }
        }

        public void Release(string id)
        {
            throw new NotSupportedException("自動で解放されます。手動での解放はサポートされていません。");
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

        public sealed class AccessManager : Queue<DateTime>
        {
            private readonly TimeSpan intervalSpan;

            public AccessManager(TimeSpan intervalSpan)
            {
                this.intervalSpan = intervalSpan;
                Enqueue(DateTime.Now);
            }

            public int GetWaitMilliseconds()
            {
                if (Count < limitCapacity)
                {
                    Enqueue(DateTime.Now);
                    return 0;
                }

                TimeSpan span = Dequeue() + intervalSpan - DateTime.Now;
                if (span <= TimeSpan.Zero)
                {
                    Enqueue(DateTime.Now);
                    return 0;
                }
                else
                {
                    Clear();
                    return (int)span.TotalMilliseconds;
                }
            }
        }
    }
}
