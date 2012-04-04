using System;
using System.Threading;

namespace DeanCCCore.Core
{
    public sealed class PatrolTimer : IDisposable
    {
        /// <summary>
        /// 既定の設定でインスタンスを初期化します
        /// タイマーは起動しません
        /// </summary>
        public PatrolTimer(TimerCallback callback)
        {
            timer = new Timer(callback);
            Common.Patrolling += new EventHandler<System.ComponentModel.CancelEventArgs>(Common_Patrolling);
            Common.Patrolled += new EventHandler(Common_Patrolled);
        }

        void Common_Patrolled(object sender, EventArgs e)
        {
            running = false;
            if (isReentry)
            {
                isReentry = false;
                Start();
            }
        }

        void Common_Patrolling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (running)
            {
                //再入した
                Stop();
                isReentry = true;
                e.Cancel = true;
            }
            else
            {
                startTime = DateTime.Now;
                running = true;
            }
        }

        /// <summary>
        /// スレッドを取得する巡回間隔
        /// </summary>
        public const int PatrolMinutes = 30;
        private const int PatrolMilliSeconds = PatrolMinutes * 60 * 1000;
        private static readonly TimeSpan PatrolSpan = TimeSpan.FromMinutes(PatrolMinutes);

        private DateTime startTime;
        private Timer timer;
        private bool running;
        private bool isReentry;
        private bool stopped;

        public string GetNextTimeText()
        {
            TimeSpan nextTime = GetNextTime();
            return nextTime.ToString(@"mm\:ss");
        }

        private TimeSpan GetNextTime()
        {
            if (stopped)
            {
                return TimeSpan.Zero;
            }
            else
            {
                TimeSpan nextTime = PatrolSpan - (DateTime.Now - startTime);
                return nextTime > TimeSpan.Zero ? nextTime : TimeSpan.Zero;
            }
        }

        private void Change(int dueTime)
        {
            if (timer != null)
            {
                timer.Change(dueTime, PatrolMilliSeconds);
            }
        }

        /// <summary>
        /// 定期的な動作を開始します
        /// </summary>
        public void Start()
        {
            Change(0);
            stopped = false;
            startTime = DateTime.Now;
        }

        /// <summary>
        /// 定期的な動作を停止します
        /// </summary>
        public void Stop()
        {
            stopped = true;
            Change(Timeout.Infinite);
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }
    }
}
