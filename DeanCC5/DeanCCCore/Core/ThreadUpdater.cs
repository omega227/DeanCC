using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core
{
    public sealed class ThreadUpdater : ITimerExecution
    {
        public event EventHandler<System.ComponentModel.CancelEventArgs> Running;
        public event EventHandler Ran;

        private readonly ThreadCollection threads;
        private readonly Predicate<Thread> updateable;
        public ThreadUpdater(ThreadCollection threads)
            : this(threads, thread => (thread.QuickDownloading & QuickDownloadState.Selected) != QuickDownloadState.Selected &&
                !thread.Header.IsPastlog && !thread.Header.IsLimitOverThread)
        {
        }

        public ThreadUpdater(ThreadCollection threads, Predicate<Thread> updateable)        
        {
            this.threads = threads;
            this.updateable = updateable;
        }

        public void Run()
        {
            if (threads == null)
            {
                throw new InvalidOperationException("threads is null");
            }
            System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
            OnRunning(e);
            if (e.Cancel)
            {
                OnRan();
                return;
            }
            
            IEnumerable<Thread> updateThreads;
            lock (Common.PatrolSyncRoot)
            {
                updateThreads = threads.Where(thread => updateable(thread));
            }
            Thread[] copyUpdateThreads = updateThreads.ToArray();
            foreach (Thread thread in copyUpdateThreads)
            {
                try
                {
                    thread.Update();
                }
                catch (System.Net.WebException ex)
                {
                    Common.Logs.Add("通信エラー", ex.Message, LogStatus.Error);
                    continue;
                }
            }
            OnRan();
        }

        private void OnRunning(System.ComponentModel.CancelEventArgs e)
        {
            if (Running != null)
            {
                Running(this, e);
            }
        }

        private void OnRan()
        {
            if (Ran != null)
            {
                Ran(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            return;
        }
    }
}
