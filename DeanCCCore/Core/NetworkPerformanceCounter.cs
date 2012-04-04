using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace DeanCCCore.Core
{
    /// <summary>
    /// アプリケーションの通信速度を提供します
    /// (PC全体の通信速度)
    /// </summary>
    public sealed class NetworkPerformanceCounter : IDisposable
    {
        private const int intervalSeconds = 1;
        private const int intervalMilliseconds = intervalSeconds * 1000;
        private const int kilo = 1024;
        private const string categoryName = "Network Interface";
        private const string instanceName = "MS TCP Loopback interface";
        private const string machineName = ".";
        private const string sentCounterName = "Bytes Sent/sec";
        private const string receiveCounterName = "Bytes Received/sec";

        PerformanceCounter bytesSentPerformanceCounter;
        PerformanceCounter bytesReceivedPerformanceCounter;
        private Timer timer;

        public NetworkPerformanceCounter()
        {
        }

        /// <summary>
        /// 送受信量の取得を開始します
        /// 開始前の送受信量も取得します
        /// </summary>
        public void Start()
        {
            if (bytesSentPerformanceCounter == null || bytesReceivedPerformanceCounter == null)
            {
                //int processId = Process.GetCurrentProcess().Id;
                //string title = Assembly.GetEntryAssembly().GetName().Name;
                //string instanceName = string.Format("{0}[{1}]", title, processId);

                bytesSentPerformanceCounter =
                    new PerformanceCounter(categoryName, sentCounterName, instanceName, machineName);
                //new PerformanceCounter(".NET CLR Networking 4.0.0.0", "Bytes Sent", instanceName, ".");
                bytesReceivedPerformanceCounter =
                    new PerformanceCounter(categoryName, receiveCounterName, instanceName, machineName);
                //new PerformanceCounter(".NET CLR Networking 4.0.0.0", "Bytes Received", instanceName, ".");
            }

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
            float sentValue = bytesSentPerformanceCounter.NextValue();
            TotalSentBytes += sentValue;
            SentKiloBytePerSecond = sentValue / kilo;

            float receiveValue = bytesReceivedPerformanceCounter.NextValue();
            TotalReceiveBytes += receiveValue;
            ReceiveKiloBytePerSecond = receiveValue / kilo;

            //float receiveValue;
            //float sentValue;
            //try
            //{
            //    receiveValue = bytesReceivedPerformanceCounter.NextValue();
            //    sentValue = bytesSentPerformanceCounter.NextValue();
            //}
            //catch (System.InvalidOperationException)
            //{
            //    receiveValue = 0;
            //    sentValue = 0;
            //}

            //if (sentValue > 0)
            //{
            //    SentKiloBytePerSecond = ((sentValue - TotalSentBytes) / intervalSeconds) / kilo;
            //    TotalSentBytes = sentValue;
            //}
            //if (receiveValue > 0)
            //{
            //    ReceiveKiloBytePerSecond = ((receiveValue - TotalReceiveBytes) / intervalSeconds) / kilo;
            //    TotalReceiveBytes = receiveValue;
            //}
        }

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
            if (bytesReceivedPerformanceCounter != null)
            {
                bytesReceivedPerformanceCounter.Dispose();
                bytesReceivedPerformanceCounter = null;
            }
            if (bytesSentPerformanceCounter != null)
            {
                bytesSentPerformanceCounter.Dispose();
                bytesSentPerformanceCounter = null;
            }
        }
    }
}
