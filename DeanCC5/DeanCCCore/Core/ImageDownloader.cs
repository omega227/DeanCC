using System;
using System.Linq;
using System.Collections.Generic;
using DeanCCCore.Core._2ch;
using System.Threading.Tasks;

namespace DeanCCCore.Core
{
    public sealed class ImageDownloader : ITimerExecution
    {
        public event EventHandler<System.ComponentModel.CancelEventArgs> Running;
        public event EventHandler Ran;

        private readonly ThreadCollection threads;
        private readonly Predicate<Thread> downloadable;
        public ImageDownloader(ThreadCollection threads)
            : this(threads, thread => thread.Downloadable && (thread.QuickDownloading & QuickDownloadState.Selected) != QuickDownloadState.Selected)
        {
            this.threads = threads;
        }
        public ImageDownloader(ThreadCollection threads, Predicate<Thread> downloadable)
        {
            this.threads = threads;
            this.downloadable = downloadable;
        }

        public void Run()
        {
            System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
            OnRunning(e);
            if (e.Cancel)
            {
                OnRan();
                return;
            }

            IEnumerable<Thread> downloadThreads = threads.Where(thread => downloadable(thread));
            Thread[] copyDownloadThreads = downloadThreads.ToArray();
            try
            {
                Parallel.ForEach(copyDownloadThreads, thread =>
                    {
                        try
                        {
                            thread.Run();
                        }
                        catch (InvalidOperationException ex)
                        {
                            Common.Logs.Add("ダウンロード中断", thread.Title + ": " + ex.Message, LogStatus.Error);
                        }
                    });
            }
            catch (AggregateException ex)
            {
                ex.Handle((innerException) =>
                {
                    if (innerException is System.IO.IOException)
                    {
                        Common.Logs.Add("保存エラー", innerException.Message, LogStatus.Error);
                        return true;
                    }
                    return false;
                });
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
            foreach (Thread thread in threads)
            {
                thread.StopDownload();
            }
            foreach (Thread thread in threads)
            {
                thread.WaitCnacellation();
            }
        }
    }
}
