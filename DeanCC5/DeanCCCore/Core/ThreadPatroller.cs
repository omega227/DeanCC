using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;
using DeanCCCore.Core._2ch.Utility;

namespace DeanCCCore.Core
{
    /// <summary>
    /// スレッド取得・更新を実装します
    /// </summary>
    public sealed class ThreadPatroller : ITimerExecution
    {
        public event EventHandler<System.ComponentModel.CancelEventArgs> Running;
        public event EventHandler Ran;
        public event EventHandler<ThreadsChangedEventArgs> ThreadsChanged;

        public ThreadPatroller()
        {
        }

        public ThreadPatroller(PatrolTable patterns, IndividualPatrolPattern individualPattern, ThreadCollection store)
            : this()
        {
            this.patterns = patterns;
            this.individualPattern = individualPattern;
            this.store = store;
        }

        private readonly PatrolTable patterns;
        private readonly IndividualPatrolPattern individualPattern;
        private readonly ThreadCollection store;

        private List<PatrolResult> aliveThreads = new List<PatrolResult>();
        /// <summary>
        /// 巡回パターンに含まれるすべての板のスレッドを更新します
        /// </summary>
        public void Update()
        {
            if (patterns == null)
            {
                throw new InvalidOperationException("patterns is null");
            }

            List<PatrolResult> results = new List<PatrolResult>();
            //自動取得スレッド
            lock (((System.Collections.ICollection)patterns).SyncRoot)
            {
                foreach (GenreFolder genre in patterns)
                {
                    foreach (PatrolPattern pattern in genre)
                    {
                        foreach (BoardInfo board in pattern.TargetBoards)
                        {
                            if (results.All(result => !result.Source.Equals(board)))
                            {
                                results.Add(UpdateBoard(board));
                            }
                        }
                    }
                }
            }
            //個別追加スレッド
            IEnumerable<IThread> individualThreads =
                store.Where(thread => thread.Header.Parent.Equals(individualPattern));
            foreach (IThread individualThread in individualThreads)
            {
                if (results.All(result => !result.Source.Equals(individualThread.Header.SourceBoard)))
                {
                    results.Add(UpdateBoard(individualThread.Header.SourceBoard));
                }
            }
            aliveThreads.Clear();
            aliveThreads.AddRange(results);
        }

        private PatrolResult UpdateBoard(IBoardInfo board, bool followBoard = true)
        {
            try
            {
                IThreadHeader[] allHeaders = ThreadParser.Parse(board, board.Read());
                bool success = allHeaders.Length > 0;
                if (!success && followBoard)
                {
                    Common.CurrentSettings.Boards.FollowMovedBoard();
                    UpdateBoard(board, false);
                }
                return new PatrolResult(success, board, allHeaders, DateTime.Now);
            }
            catch (System.Net.WebException ex)
            {
                Common.Logs.Add("通信エラー", board.Name + ex.Message, LogStatus.Error);
                return new PatrolResult(false, board, null, DateTime.Now);
            }
        }

        /// <summary>
        /// 更新したSubject.txtに存在しないスレッドをDat落ち状態にします
        /// </summary>
        public void UpdatePastLogThreads()
        {
            if (store == null)
            {
                throw new InvalidOperationException("store is null");
            }

            lock (((System.Collections.ICollection)store).SyncRoot)
            {
                foreach (IThread thread in store)
                {
                    if (thread.Header.SourceBoard == null)
                    {
                        //現在の板一覧に属さないスレッド
                        continue;
                    }

                    PatrolResult subjects =
                        aliveThreads.FirstOrDefault(result => result.Source.Equals(thread.Header.SourceBoard));
                    if (subjects == null || !subjects.Success)
                    {
                        continue;
                    }
                    if (thread.Header.Since < subjects.PatrolledTime && !subjects.AliveThreads.Contains(thread.Header))
                    {
                        //subject.txtに存在しない
                        thread.Header.IsPastlog = true;
                    }
                }
            }
        }

        /// <summary>
        /// 現在のインスタンスが保持しているSubject.txtの中から新着スレッドを追加します
        /// </summary>
        public void AddAliveThreads()
        {
            if (patterns == null)
            {
                throw new InvalidOperationException("patterns is null");
            }

            lock (((System.Collections.ICollection)patterns).SyncRoot)
            {
                foreach (GenreFolder genre in patterns)
                {
                    foreach (PatrolPattern pattern in genre)
                    {
                        foreach (BoardInfo board in pattern.TargetBoards)
                        {
                            PatrolResult subjects =
                                aliveThreads.First(result => result.Source.Equals(board));
                            if (!subjects.Success)
                            {
                                continue;
                            }
                            foreach (IThreadHeader header in subjects.AliveThreads)
                            {
                                if (pattern.IsMatch(header) && !store.Contains(header))
                                {
                                    header.Parent = pattern;
                                    Thread thread = new Thread(header);
                                    Common.InvokeMainForm(new AddThreadHandler(target =>
                                        {
                                            Common.CurrentSettings.AllThreads.Add(target);
                                        }), thread);
                                    OnThreadsChanged(
                                        new ThreadsChangedEventArgs(thread, ThreadsChangedStatus.Add));
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 指定したURLを個別追加スレッドとして追加します
        /// </summary>
        /// <param name="url"></param>
        public void AddIndividualThread(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            string boardUrl = ThreadUrlFormatter.FormatBoardUrl(url);
            IBoardInfo board = Common.CurrentSettings.Boards.FindFromUrl(boardUrl);
            if (board == null)
            {
                throw new ArgumentException("板一覧に存在しないカテゴリーのスレッドは追加できません");
            }

            PatrolResult subjects = UpdateBoard(board);
            if (!subjects.Success)
            {
                throw new InvalidOperationException("Subject.txtの更新に失敗しました。時間をおいて再度実行してください");
            }

            string threadUrl = ThreadUrlFormatter.FormatUrl(url);
            IThreadHeader aliveThread = subjects.AliveThreads.FirstOrDefault(header => header.Url.Equals(threadUrl));
            if (aliveThread == null)
            {
                throw new InvalidOperationException("Dat落ちしているか、存在しないスレッドです");
            }
            if (store.Contains(aliveThread))
            {
                throw new InvalidOperationException("既に追加されているスレッドです");
            }

            aliveThread.Parent = Common.Options.IndividualThreadOptions.PatrolPattern;
            Thread thread = new Thread(aliveThread);
            Common.InvokeMainForm(new AddThreadHandler(target => Common.CurrentSettings.AllThreads.Add(target)), thread);
            OnThreadsChanged(new ThreadsChangedEventArgs(thread, ThreadsChangedStatus.Add));
        }

        private void OnThreadsChanged(ThreadsChangedEventArgs e)
        {
            if (e.Status == ThreadsChangedStatus.Add)
            {
                Common.CurrentSettings.Information.TotalAddedThreadCount++;
            }
            if (ThreadsChanged != null)
            {
                ThreadsChanged(this, e);
            }
        }

        public delegate void RemoveDownloadCompletedThreadsHandler(ThreadCollection threads);
        public delegate void AddThreadHandler(Thread thread);

        public void RemoveDownloadCompletedThreads()
        {
            Common.InvokeMainForm(new RemoveDownloadCompletedThreadsHandler(store =>
                {
                    for (int i = store.Count - 1; i >= 0; i--)
                    {
                        Thread thread = store[i];
                        if ((thread.DownloadPaused && thread.Header.IsPastlog) ||
                            (!thread.DownloadPaused && !thread.Downloadable && thread.DownloadedCount <= 0))
                        {
                            store.Remove(thread);
                            OnThreadsChanged(new ThreadsChangedEventArgs(thread, ThreadsChangedStatus.Remove));
                        }
                    }
                }), this.store);
        }

        /// <summary>
        /// メイン動作を実行します
        /// 新規スレッドを取得して現在のスレッドを更新します
        /// </summary>
        public void Run()
        {
            System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
            OnRunning(e);
            if (e.Cancel)
            {
                OnRan();
                return;
            }

            Update();
            AddAliveThreads();
            UpdatePastLogThreads();
            RemoveDownloadCompletedThreads();

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

        public sealed class PatrolResult
        {
            public static readonly PatrolResult Empty = new PatrolResult();

            public PatrolResult()
            {
            }

            public PatrolResult(bool success, IBoardInfo source,
                IList<IThreadHeader> aliveThreads, DateTime patrolledTime)
            {
                Success = success;
                Source = source;
                AliveThreads = aliveThreads;
                PatrolledTime = patrolledTime;
            }
            public bool Success { get; private set; }
            public IBoardInfo Source { get; private set; }
            public IList<IThreadHeader> AliveThreads { get; private set; }
            public DateTime PatrolledTime { get; private set; }
        }

        public void Stop()
        {
            return;
        }
    }
}
