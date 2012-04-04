using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DeanCCCore.Core
{
    /// <summary>
    /// フォームの状態に関する設定を表します
    /// </summary>
    [Serializable]
    public sealed class FormStatus
    {
        public FormStatus()
        {
            mainFormStatus = new MainForm();
            ThreadViewStatus = new ThreadView();            
        }

        private MainForm mainFormStatus;
        /// <summary>
        /// メインフォームの状態を表します
        /// </summary>
        public MainForm MainFormStatus
        {
            get
            {
                return mainFormStatus;
            }
            set
            {
                mainFormStatus = value;
            }
        }
        /// <summary>
        /// スレッドビューの状態を表します
        /// </summary>
        public ThreadView ThreadViewStatus { get; set; }

        [Serializable]
        public sealed class MainForm
        {
            public MainForm()
            {
                location = new Point(100, 100);
                size = new Size(690, 531);
                enableAutoPatrol = true;
                showsMenulistPanel = true;
                showsToolbar = true;
                SelectedMenu = SelectedThreadListMenu.AllThread;
                EnableClipboardViewer = false;
            }

            private Point location;
            /// <summary>
            /// 位置
            /// </summary>
            public Point Location
            {
                get
                {
                    return location;
                }
                set
                {
                    location = value;
                }
            }
            private Size size;
            /// <summary>
            /// サイズ
            /// </summary>
            public Size Size
            {
                get
                {
                    return size;
                }
                set
                {
                    size = value;
                }
            }
            private bool enableAutoPatrol;
            /// <summary>
            /// 自動巡回
            /// </summary>
            public bool EnableAutoPatrol
            {
                get
                {
                    return enableAutoPatrol;
                }
                set
                {
                    enableAutoPatrol = value;
                }
            }
            private bool showsMenulistPanel;
            /// <summary>
            /// メニューコントロールの表示状態
            /// </summary>
            public bool ShowsMenulistPanel
            {
                get
                {
                    return showsMenulistPanel;
                }
                set
                {
                    showsMenulistPanel = value;
                }
            }
            private bool showsToolbar;
            /// <summary>
            /// ツールバーの表示状態
            /// </summary>
            public bool ShowsToolbar
            {
                get
                {
                    return showsToolbar;
                }
                set
                {
                    showsToolbar = value;
                }
            }
            /// <summary>
            /// メニューコントロールの選択状態
            /// </summary>
            public SelectedThreadListMenu SelectedMenu { get; set; }

            /// <summary>
            /// クリップボード監視のチェック状態
            /// </summary>
            public bool EnableClipboardViewer { get; set; }
        }

        [Serializable]
        public sealed class ThreadView
        {
            public ThreadView()
            {
                Headers = new DataGridViewHeaderStatusCollection();
            }

            public DataGridViewHeaderStatusCollection Headers { get; set; }
        }
    }
}
