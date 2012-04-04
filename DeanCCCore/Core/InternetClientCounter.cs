using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace DeanCCCore.Core
{
    public sealed class InternetClientCounter : IDisposable
    {
        private const int intervalSeconds = 1;
        private const int intervalMilliseconds = intervalSeconds * 1000;
        private const int kilo = 1024;

        private Timer timer;

        public InternetClientCounter()
        {
            InternetClient.Downloaded += new EventHandler<InternetClientEventArgs>(InternetClient_Downloaded);
        }

        void InternetClient_Downloaded(object sender, InternetClientEventArgs e)
        {
            TotalReceiveBytes += e.ReceiveBytes;
            TotalSentBytes += e.SentBytes;
        }

        /// <summary>
        /// 送受信量の取得を開始します
        /// 開始前の送受信量も取得します
        /// </summary>
        public void Start()
        {
            if (timer == null)
            {
                timer = new Timer((state) =>
                {
                    ComputeSpeeds();
                },
                null, 0, intervalMilliseconds);
            }
            else
            {
                throw new InvalidOperationException("既にスタートしています");
            }
        }

        private void ComputeSpeeds()
        {
            ReceiveKiloBytePerSecond = (TotalReceiveBytes - previousTotalReceiveBytes) / kilo;
            previousTotalReceiveBytes = TotalReceiveBytes;

            SentKiloBytePerSecond = (TotalSentBytes - previousTotalSentBytes) / kilo;
            previousTotalSentBytes = TotalSentBytes;
        }

        private float previousTotalReceiveBytes;
        private float previousTotalSentBytes;

        /// <summary>
        /// 受信速度[KB/s]を表します
        /// </summary>
        public float ReceiveKiloBytePerSecond { get; private set; }
        /// <summary>
        /// 送信速度[KB/s]を表します
        /// </summary>
        public float SentKiloBytePerSecond { get; private set; }
        /// <summary>
        /// 累計受信量[B]を表します
        /// </summary>
        public float TotalReceiveBytes { get; private set; }
        /// <summary>
        /// 累計送信量[B]を表します
        /// </summary>
        public float TotalSentBytes { get; private set; }

        private void ChangeTimer(int dueTime)
        {
            if (timer != null)
            {
                timer.Change(dueTime, intervalMilliseconds);
            }
        }

        /// <summary>
        /// 送受信量の取得を一時停止します
        /// </summary>
        public void Pause()
        {
            ChangeTimer(Timeout.Infinite);
        }

        /// <summary>
        /// 送受信量の取得を再開します
        /// 一時停止していた間の値も取得します
        /// </summary>
        public void Restart()
        {
            ChangeTimer(0);
        }

        /// <summary>
        /// 送受信量の取得を終了します
        /// </summary>
        public void Stop()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
