using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core.Utility;

namespace DeanCC.GUI
{
    public sealed partial class ThreadListControl : UserControl
    {
        public event EventHandler<ThreadListEventArgs> Selected;
        private const int menuTitleNodeLevel = 0;
        private const int menuItemNodeLevel = 1;
        private const string ThreadNodeName = "ThreadNode";
        private const string AllThreadNodeName = "AllThreadNode";
        private const string AllThreadDisplayNameFormat = "すべて ({0:N0})";
        private const string DownloadedThreadNodeName = "DownloadedThreadNode";
        private const string DownloadedThreadDisplayNameFormat = "ダウンロード完了 ({0:N0})";
        private const string DownloadPausedThreadNodeName = "DownloadPausedThreadNode";
        private const string DownloadPausedThreadDisplayNameFormat = "一時停止 ({0:N0})";
        private const string DownloadingThreadNodeName = "DownloadingThreadNode";
        private const string DownloadingThreadDisplayNameFormat = "ダウンロード中 ({0:N0})";
        private const string ExcludedThreadNodeName = "ExcludedThreadNode";
        private const string ExcludedThreadDisplayNameFormat = "除外 ({0:N0})";
        private const string ImageNodeName = "ImageNode";
        private const string SecureResNodeName = "SecureResNode";
        private const string SecureResDisplayNameFormat = "未取得 ({0:N0})";
        private const string DetalisNodeName = "DetalisNode";
        private const string LogNodeName = "LogNode";
        private const string InformationNodename = "InformationNode";

        public ThreadListControl()
        {
            InitializeComponent();
            Common.EnableThreads.ListChanged += new ListChangedEventHandler(EnableThreads_ListChanged);
            Common.DownloadingThreads.ListChanged += new ListChangedEventHandler(DownloadingThreads_ListChanged);
            Common.DownloadPausedThreads.ListChanged += new ListChangedEventHandler(DownloadPausedThreads_ListChanged);
            Common.DownloadedThreads.ListChanged += new ListChangedEventHandler(DownloadedThreads_ListChanged);
            Common.ExcludedThreads.ListChanged += new ListChangedEventHandler(ExcludedThreads_ListChanged);
            Common.SecureThreads.ListChanged += new ListChangedEventHandler(SecureThreads_ListChanged);

            treeView.ExpandAll();
        }

        void SecureThreads_ListChanged(object sender, ListChangedEventArgs e)
        {
            treeView.Nodes[ImageNodeName].Nodes[SecureResNodeName].Text = string.Format(SecureResDisplayNameFormat, Common.SecureThreads.Count);
        }

        void ExcludedThreads_ListChanged(object sender, ListChangedEventArgs e)
        {
            treeView.Nodes[ThreadNodeName].Nodes[ExcludedThreadNodeName].Text = string.Format(ExcludedThreadDisplayNameFormat, Common.ExcludedThreads.Count);
        }

        void DownloadedThreads_ListChanged(object sender, ListChangedEventArgs e)
        {
            treeView.Nodes[ThreadNodeName].Nodes[DownloadedThreadNodeName].Text = string.Format(DownloadedThreadDisplayNameFormat, Common.DownloadedThreads.Count);
        }

        void DownloadPausedThreads_ListChanged(object sender, ListChangedEventArgs e)
        {
            treeView.Nodes[ThreadNodeName].Nodes[DownloadPausedThreadNodeName].Text = string.Format(DownloadPausedThreadDisplayNameFormat, Common.DownloadPausedThreads.Count);
        }

        void DownloadingThreads_ListChanged(object sender, ListChangedEventArgs e)
        {
            treeView.Nodes[ThreadNodeName].Nodes[DownloadingThreadNodeName].Text = string.Format(DownloadingThreadDisplayNameFormat, Common.DownloadingThreads.Count);
        }

        void EnableThreads_ListChanged(object sender, ListChangedEventArgs e)
        {
            treeView.Nodes[ThreadNodeName].Nodes[AllThreadNodeName].Text = string.Format(AllThreadDisplayNameFormat, Common.EnableThreads.Count);
        }

        private SelectedThreadListMenu selectedMenu;
        public SelectedThreadListMenu SelectedMenu
        {
            get { return selectedMenu; }
            set
            {
                if (selectedMenu != value)
                {
                    selectedMenu = value;
                    OnSelectedMenuChaged(EventArgs.Empty);
                }
            }
        }

        private void OnSelectedMenuChaged(EventArgs e)
        {
            switch (selectedMenu)
            {
                case SelectedThreadListMenu.AllThread:
                    treeView.SelectedNode = treeView.Nodes[ThreadNodeName].Nodes[AllThreadNodeName];
                    break;
                case SelectedThreadListMenu.DownloadedThread:
                    treeView.SelectedNode = treeView.Nodes[ThreadNodeName].Nodes[DownloadedThreadNodeName];
                    break;
                case SelectedThreadListMenu.DownloadingThread:
                    treeView.SelectedNode = treeView.Nodes[ThreadNodeName].Nodes[DownloadingThreadNodeName];
                    break;
                case SelectedThreadListMenu.DownloadPausedThread:
                    treeView.SelectedNode = treeView.Nodes[ThreadNodeName].Nodes[DownloadPausedThreadNodeName];
                    break;
                case SelectedThreadListMenu.ExcludedThread:
                    treeView.SelectedNode = treeView.Nodes[ThreadNodeName].Nodes[ExcludedThreadNodeName];
                    break;
                case SelectedThreadListMenu.SecureImage:
                    treeView.SelectedNode = treeView.Nodes[ImageNodeName].Nodes[SecureResNodeName];
                    break;
                case SelectedThreadListMenu.Log:
                    treeView.SelectedNode = treeView.Nodes[DetalisNodeName].Nodes[LogNodeName];
                    break;
                case SelectedThreadListMenu.Information:
                    treeView.SelectedNode = treeView.Nodes[DetalisNodeName].Nodes[InformationNodename];
                    break;
            }
            Selected(this, new ThreadListEventArgs(selectedMenu));
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Selected != null && e.Node.Level == menuItemNodeLevel)
            {
                SelectedThreadListMenu menu;
                switch (e.Node.Name)
                {
                    case AllThreadNodeName:
                    default:
                        menu = SelectedThreadListMenu.AllThread;
                        break;
                    case DownloadedThreadNodeName:
                        menu = SelectedThreadListMenu.DownloadedThread;
                        break;
                    case DownloadPausedThreadNodeName:
                        menu = SelectedThreadListMenu.DownloadPausedThread;
                        break;
                    case DownloadingThreadNodeName:
                        menu = SelectedThreadListMenu.DownloadingThread;
                        break;
                    case ExcludedThreadNodeName:
                        menu = SelectedThreadListMenu.ExcludedThread;
                        break;
                    case LogNodeName:
                        menu = SelectedThreadListMenu.Log;
                        break;
                    case SecureResNodeName:
                        menu = SelectedThreadListMenu.SecureImage;
                        break;
                    case InformationNodename:
                        menu = SelectedThreadListMenu.Information;
                        break;
                }
                SelectedMenu = menu;
            }
        }
    }
}
