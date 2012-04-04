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

        private ThreadCollection threads;
        public ThreadUpdater(ThreadCollection threads)
        {
            this.threads = threads;
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

            Thread[] copyThreads = new Thread[threads.Count];
            threads.CopyTo(copyThreads, 0);
            foreach (IThread thread in copyThreads)
            {
                if (!thread.Header.IsPastlog && !thread.Header.IsLimitOverThread)
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
