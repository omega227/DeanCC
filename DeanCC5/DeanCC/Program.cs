using System;
using System.Text;
using System.Windows.Forms;
using DeanCC.GUI;
using DeanCCCore.Core;

namespace DeanCC
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            RunEventArgs e = new RunEventArgs(args);
            OnRunning(e);
            if (!e.Cancel)
            {
                Application.Run(new MainForm());
                OnRan();
            }
        }

        /// <summary>
        /// メッセージループ実行直前に発生します
        /// </summary>
        static void OnRunning(RunEventArgs e)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            if (!Common.Mutex.WaitOne(0, false))
            {
                string command = e.Args.Length > 0 ? e.Args[0].ToLower() : MainForm.ActiveArgument;
                switch (command)
                {
                    case MainForm.AddUrlArgmuent:
                        StringBuilder urls = new StringBuilder();
                        for (int i = 0; i < e.Args.Length; i++)
                        {
                            if (e.Args[i].IndexOf(MainForm.ArgumentSperator) >= 0)
                            {
                                continue;
                            }

                            if (i > 0)
                            {
                                urls.Append(MainForm.ArgumentSperator);
                            }
                            urls.Append(e.Args[i]);
                        }
                        IPCClient.Send(MainForm.PipeServerName, urls.ToString());
                        break;

                    case MainForm.ActiveArgument:
                        string result = IPCClient.Send(MainForm.PipeServerName, MainForm.ActiveArgument);
                        if (result != MainForm.SuccessToken)
                        {
                            MessageBox.Show("既に起動しています", "多重起動禁止", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        break;
                }
                e.Cancel = true;
                return;
            }
            else if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4.0") == null)
            {
                if (MessageBox.Show("DeanCCを起動できませんでした。Microsoft .NET Framework 4 が必要です。",
                    "DeanCC", MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK)
                {
                    DeanCCCore.Core.Utility.ProcessUtility.OpenUrl("http://www.microsoft.com/downloads/details.aspx?displaylang=ja&FamilyID=0a391abd-25c1-4fc0-919f-b21f31ab88b7");
                }
                e.Cancel = true;
                return;
            }
            else
            {
                Common.Logs.Add("DeanCC起動", "", LogStatus.System);
                Common.Initialize();
            }
        }

        /// <summary>
        /// メッセージループ実行後（アプリケーション終了時）に発生します
        /// </summary>
        static void OnRan()
        {
            Common.Save();
            Common.Release();
        }
    }

    public sealed class RunEventArgs : System.ComponentModel.CancelEventArgs
    {
        public RunEventArgs(string[] args)
        {
            Args = args;
        }
        public string[] Args { get; set; }
    }
}
