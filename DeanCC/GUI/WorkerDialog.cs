using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DeanCC.GUI
{
    /// <summary>
    /// 時間のかかる作業の実行中に表示する待機中ダイアログを提供します。
    /// 実行を完了すると、自動で閉じます。
    /// </summary>
    public sealed partial class WorkerDialog : Form
    {
        /// <summary>
        /// 時間のかかるメソッドを指定して、初期化します
        /// </summary>
        /// <param name="method">実行するメソッド</param>
        /// <param name="title">表示するタイトル</param>
        /// <param name="message">表示するメッセージ</param>
        public WorkerDialog(MethodInvoker method, string title, string message)
        {
            InitializeComponent();

            this.method = method;
            Title = title;
            Message = message;
        }
        
        private readonly MethodInvoker method;
        /// <summary>
        /// ダイアログのタイトルを取得または設定します
        /// </summary>
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        /// <summary>
        /// ダイアログの中央に表示されるメッセージを取得または設定します
        /// </summary>
        public string Message
        {
            get { return messageLabel.Text; }
            set { messageLabel.Text = value; }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            method();
        }

        private void WorkerDialog_Shown(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void WorkerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }
    }
}
