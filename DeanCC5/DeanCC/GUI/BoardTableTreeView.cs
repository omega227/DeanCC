using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core._2ch;

namespace DeanCC.GUI
{
    public sealed class BoardTableTreeView : TreeView
    {
        public event EventHandler<BoardTableTreeViewItemSelectedEventArgs> BoardSelected;
        public const int CategoryLevel = 0;
        public const int CategoryImageIndex = 0;
        public const int BoardLevel = 1;
        public const int BoardImageIndex = 1;
        //public const int BoardDeleteImageIndex = 2;
        private ImageList boardTableImageList;
        private System.ComponentModel.IContainer components;
        private const int HideInterval = 150;

        private bool loaded;
        private BoardInfo selectedBoard;
        /// <summary>
        /// 選択している板情報を取得または設定します
        /// 選択しているノードとは必ずしも一致しません
        /// </summary>
        public BoardInfo SelectedBoard
        {
            get { return selectedBoard; }
            set
            {
                if (selectedBoard == null || !selectedBoard.Equals(value))
                {
                    selectedBoard = value;
                    OnBoardSelected(new BoardTableTreeViewItemSelectedEventArgs(selectedBoard));
                }
            }
        }

        public BoardTableTreeView()
        {
            InitializeComponent();
            //↓のコードがあるとデザイナ実行時にエラー
            //# if !DEBUG
            if (!DeanCCCore.Core.Common.CurrentSettings.Boards.UpdateCompleted)
            {
                System.Threading.Thread updateThread = new System.Threading.Thread(delegate()
                {
                    DeanCCCore.Core.Common.CurrentSettings.Boards.OnlineUpdate();
                });
                updateThread.Start();
            }
            //#endif
        }

        private void OnBoardSelected(BoardTableTreeViewItemSelectedEventArgs e)
        {
            if (BoardSelected != null)
            {
                BoardSelected(this, e);
            }
            System.Threading.Thread.Sleep(HideInterval);
            Hide();
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == BoardLevel)
            {
                SelectedBoard = (BoardInfo)e.Node.Tag;
            }
            base.OnNodeMouseClick(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible && !loaded)
            {
                if (!DeanCCCore.Core.Common.CurrentSettings.Boards.UpdateCompleted)
                {
                    System.Threading.Thread updateThread = new System.Threading.Thread(delegate()
                        {
                            DeanCCCore.Core.Common.CurrentSettings.Boards.OnlineUpdate();
                        });
                    updateThread.Start();
                    updateThread.Join();
                }
                LoadTable(DeanCCCore.Core.Common.CurrentSettings.Boards);
            }
            base.OnVisibleChanged(e);
        }

        private void LoadTable(BoardTable source)
        {
            BeginUpdate();
            foreach (Category category in source)
            {
                TreeNode categoryNode = new TreeNode(category.Name)
                {
                    Name = category.Name,
                    Tag = category,
                    ImageIndex = CategoryImageIndex,
                    SelectedImageIndex = CategoryImageIndex
                };
                foreach (BoardInfo board in category)
                {
                    TreeNode boardNode = new TreeNode(board.Name)
                    {
                        Name = board.Name,
                        Tag = board,
                        ImageIndex = BoardImageIndex,
                        SelectedImageIndex = BoardImageIndex
                    };
                    categoryNode.Nodes.Add(boardNode);
                }
                Nodes.Add(categoryNode);
            }
            EndUpdate();
            loaded = true;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardTableTreeView));
            this.boardTableImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // boardTableImageList
            // 
            this.boardTableImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("boardTableImageList.ImageStream")));
            this.boardTableImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.boardTableImageList.Images.SetKeyName(0, "folder.png");
            this.boardTableImageList.Images.SetKeyName(1, "page_white_world.png");
            // 
            // BoardTableTreeView
            // 
            this.ImageIndex = 0;
            this.ImageList = this.boardTableImageList;
            this.SelectedImageIndex = 0;
            this.ResumeLayout(false);

        }
    }
}
