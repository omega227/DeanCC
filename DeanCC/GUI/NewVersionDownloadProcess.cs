using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core.VersionUp;
using DeanCCCore.Core;

namespace DeanCC.GUI
{
    public sealed class NewVersionDownloadProcess
    {
        WorkerDialog newVersionChecker = new WorkerDialog((MethodInvoker)delegate()
        {
            VersionUpClient.CheckNewVersion();
        }, "自動アップデート", "最新版を確認しています...");

        /// <summary>
        /// 最新版のダウンロードを実行します。
        /// </summary>
        /// <returns>最新版のダウンロードが実行されて完了した場合はtrue,それ以外はfalse</returns>
        public bool Run()
        {
            OnRunning();

            bool newVersionChecked = (newVersionChecker.ShowDialog() == DialogResult.OK);
            if (newVersionChecked)
            {
                if (existsNewVersion)
                {
                    NewVersionForm newVersionViewer = new NewVersionForm(releaseText);
                    DialogResult result = newVersionViewer.ShowDialog();
                    bool cancelUpdate = (result != DialogResult.OK);
                    if (!cancelUpdate)
                    {
                        WorkerDialog updater = new WorkerDialog((MethodInvoker)delegate()
                        {
                            VersionUpClient.DownloadNewVersion();
                        }, "自動アップデート", "最新版をダウンロードしています...");
                        updater.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(Common.VersionText + "\n最新版を使用しています",
                        "DeanCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            OnRan();
            return updateCompleted;
        }

        private void OnRunning()
        {
            VersionUpClient.CheckedNewVersion += new EventHandler<VersionUpEventArgs>(VersionUpClient_CheckedNewVersion);
            VersionUpClient.Downloaded += new EventHandler(VersionUpClient_Downloaded);
        }

        bool updateCompleted = false;
        void VersionUpClient_Downloaded(object sender, EventArgs e)
        {
            updateCompleted = true;
        }

        bool existsNewVersion = false;
        string releaseText;
        void VersionUpClient_CheckedNewVersion(object sender, VersionUpEventArgs e)
        {
            existsNewVersion = e.ExistsNewVersion;
            releaseText = e.ReleseText;
        }

        private void OnRan()
        {
            VersionUpClient.CheckedNewVersion -= new EventHandler<VersionUpEventArgs>(VersionUpClient_CheckedNewVersion);
            VersionUpClient.Downloaded -= new EventHandler(VersionUpClient_Downloaded);
        }
    }
}
