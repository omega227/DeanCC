using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace DeanCCCore.Core
{
    public sealed class ServerReciveEventArgs : EventArgs
    {
        /// <summary>
        /// クライアントからのリクエストを格納するオブジェクト
        /// </summary>
        /// <remarks>サーバー側で閉じられるので、使用後にClose()を呼ぶ必要はありません</remarks>
        public StreamReader StreamReader;
        /// <summary>
        /// クライアントへレスポンスを送るのに必要なオブジェクト
        /// </summary>
        /// <remarks>サーバー側で閉じられるので、Close()を呼ぶ必要はありません</remarks>
        public StreamWriter StreamWriter;
        public ServerReciveEventArgs(StreamReader sr, StreamWriter sw)
        {
            this.StreamReader = sr;
            this.StreamWriter = sw;
        }
    }

    public sealed class IPCServer : IDisposable
    {
        private EventWaitHandle closeApplicationEvent = new EventWaitHandle(false, EventResetMode.ManualReset);
        private string ServerName;

        public IPCServer(string serverName)
        {
            this.ServerName = serverName;
            this.Recived += new EventHandler<ServerReciveEventArgs>((s, e) => { });
            Thread Thread = new Thread(pipeServerThread);
            Thread.Start();
        }

        /// <summary>
        /// クライアントから何か送られてきたときに実行されるイベント
        /// </summary>
        /// <remarks>
        /// このイベントはオブジェクトを生成したのとは別のスレッドで実行されます。
        /// また、処理後はクライアントに適切な値を送る必要があります。送らない場合、クライアントの動作は保証されません
        /// </remarks>
        public event EventHandler<ServerReciveEventArgs> Recived;

        #region IDisposable メンバー

        public void Dispose()
        {
            closeApplicationEvent.Set();
            closeApplicationEvent.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
        void pipeServerThread(object o)
        {
            NamedPipeServerStream pipeServer = null;
            try
            {
                while (true)
                {
                    pipeServer = new NamedPipeServerStream(
                        this.ServerName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

                    IAsyncResult async = pipeServer.BeginWaitForConnection(null, null);
                    int index = WaitHandle.WaitAny(new WaitHandle[] { async.AsyncWaitHandle, closeApplicationEvent });
                    switch (index)
                    {
                        case 0:
                            pipeServer.EndWaitForConnection(async);
                            using (StreamReader sr = new StreamReader(pipeServer))
                            using (StreamWriter sw = new StreamWriter(pipeServer))
                            {
                                this.Recived(this, new ServerReciveEventArgs(sr, sw));
                            }
                            if (pipeServer.IsConnected)
                            {
                                pipeServer.Disconnect();
                            }
                            break;
                        case 1:
                            return;
                    }
                }
            }
            finally
            {
                if (pipeServer != null)
                {
                    pipeServer.Close();
                }
            }
        }
    }
}
