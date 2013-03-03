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
            getPatrolTime = GetPatrolMilliSeconds;
            timer = new Timer(callback);
            Common.Patrolling += new EventHandler<System.ComponentModel.CancelEventArgs>(Common_Patrolling);
            Common.Patrolled += new EventHandler(Common_Patrolled);
        }
        public PatrolTimer(Func<int> getPatrolTime, TimerCallback callback)
        {
            //高頻度用タイマーコンストラクタ
            this.getPatrolTime = getPatrolTime;
            timer = new Timer(callback);
            Common.QuickPatrolling += new EventHandler<System.ComponentModel.CancelEventArgs>(Common_Patrolling);
            Common.QuickPatrolled += new EventHandler(Common_Patrolled);
        }

        private DateTime startTime;
        TimeSpan patrolSpan;
        private Timer timer;
        private bool running;
        private bool isReentry;
        private bool stopped;
        private readonly Func<int> getPatrolTime;

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
                TimeSpan nextTime = patrolSpan - (DateTime.Now - startTime);
                return nextTime > TimeSpan.Zero ? nextTime : TimeSpan.Zero;
            }
        }

        private int GetPatrolMilliSeconds()
        {
            int hour = DateTime.Now.Hour;
            if (4 <= hour && hour < 12)
            {
                return 60 * 60 * 1000;
            }
            else if (12 <= hour && hour < 20)
            {
                return 30 * 60 * 1000;
            }
            else
            {
                return 15 * 60 * 1000;
            }
        }

        private void Change(int dueTime)
        {
            if (timer != null)
            {
                int patrolMillSeconds = getPatrolTime();
                patrolSpan = TimeSpan.FromMilliseconds((double)patrolMillSeconds);
                timer.Change(dueTime, patrolMillSeconds);
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
