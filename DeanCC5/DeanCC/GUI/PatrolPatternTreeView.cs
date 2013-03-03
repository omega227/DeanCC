using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core._2ch;
using DeanCCCore.Core.Utility;

namespace DeanCC.GUI
{
    /// <summary>
    /// 巡回設定を表示するツリーコントロールです
    /// </summary>
    public sealed class PatrolPatternTreeView : TreeView
    {
        // +Folder
        //  -PatrolPattern
        //  の階層で作成
        //  Node.TagにはそれぞれGenreFolderとPatrolPatternEditControlインスタンスを設置

        public const int FolderLevel = 0;
        private ImageList patrolPatternImageList;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem openFilderToolStripMenuItem;
        private ToolStripMenuItem copyPatternToolStripMenuItem;
        public const int PatternLevel = 1;

        public PatrolPatternTreeView()
        {
            InitializeComponent();
            openFilderToolStripMenuItem.Click += new EventHandler(openFolderToolStripMenuItem_Click);
            copyPatternToolStripMenuItem.Click += copyPatternToolStripMenuItem_Click;
        }

        void copyPatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null && SelectedNode.Level == PatternLevel)
            {
                PatrolPatternEditControl control = (PatrolPatternEditControl)SelectedNode.Tag;
                PatrolPattern clonedPattern = (PatrolPattern)control.GetInitializedCurrentPattern((GenreFolder)SelectedNode.Parent.Tag).Clone();
                AddPattern(SelectedNode.Parent, clonedPattern);
            }
        }

        void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null && SelectedNode.Level == FolderLevel)
            {
                GenreFolder folder = (GenreFolder)SelectedNode.Tag;
                ProcessUtility.OpenFolder(folder.LocalPath);
            }
        }

        /// <summary>
        /// 指定した巡回設定を読み込みます
        /// </summary>
        /// <param name="table">指定する設定</param>
        public void Set(IPatrolTable source)
        {
            BeginUpdate();
            foreach (GenreFolder folder in source)
            {
                TreeNode folderNode = new TreeNode(folder.Name)
                {
                    Tag = folder,
                    Name = folder.Name,
                    ImageIndex = PatrolPatternsEditForm.FolderImageIndex,
                    SelectedImageIndex = PatrolPatternsEditForm.FolderImageIndex
                };
                foreach (PatrolPattern child in folder)
                {
                    PatrolPatternEditControl patternControl = new PatrolPatternEditControl(child);
                    patternControl.PatternNameChanged +=
                        new EventHandler<PatrolPatternNameChangedEventArgs>(patternControl_PatternNameChanged);
                    TreeNode childNode = new TreeNode(child.Name)
                    {
                        Name = child.Name,
                        Tag = patternControl,
                        ImageIndex = PatrolPatternsEditForm.PatternImageIndex,
                        SelectedImageIndex = PatrolPatternsEditForm.PatternImageIndex
                    };
                    folderNode.Nodes.Add(childNode);
                }
                Nodes.Add(folderNode);
            }
            EndUpdate();
        }

        void patternControl_PatternNameChanged(object sender, PatrolPatternNameChangedEventArgs e)
        {
            UpdatePattern(e.Key, e.NewName);
        }

        /// <summary>
        /// 現在の状態を取得します
        /// </summary>
        /// <param name="table">反映させる設定</param>
        public bool Get(IPatrolTable destination)
        {
            bool retry = false;
            List<GenreFolder> folders = new List<GenreFolder>();
            foreach (TreeNode folderNode in Nodes)
            {
                GenreFolder folder = (GenreFolder)folderNode.Tag;
                folder.Clear();
                foreach (TreeNode patternNode in folderNode.Nodes)
                {
                    PatrolPatternEditControl patternControl = (PatrolPatternEditControl)patternNode.Tag;
                    PatrolPattern pattern = patternControl.GetInitializedCurrentPattern(folder);
                    if (pattern != null)
                    {
                        folder.Add(pattern);
                    }
                    else if (MessageBox.Show("設定項目を入力しなおしますか？\nここで無視した設定は保存されません",
                                "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        retry = true;
                        return retry;
                    }
                }
                folders.Add(folder);
            }

            destination.Clear();
            foreach (GenreFolder folder in folders)
            {
                destination.Add(folder);
            }
            return retry;
        }

        public void AddEmptyPattern()
        {
            if (SelectedNode == null || SelectedNode.Level != FolderLevel)
            {
                throw new InvalidOperationException("設定を追加するフォルダーを選択して下さい");
            }
            AddEmptyPattern(SelectedNode);
        }

        public void AddEmptyPattern(TreeNode targetNode)
        {
            GenreFolder parentFolder = (GenreFolder)targetNode.Tag;
            PatrolPattern emptyPattern = new PatrolPattern(parentFolder);            
            if (ContainsPatternKey(emptyPattern.Name))
            {
                emptyPattern.Name = Rename(emptyPattern.Name);
            }

            PatrolPatternEditControl patternControl = new PatrolPatternEditControl(emptyPattern);
            patternControl.PatternNameChanged +=
                new EventHandler<PatrolPatternNameChangedEventArgs>(patternControl_PatternNameChanged);
            TreeNode emptyNode = new TreeNode(emptyPattern.Name)
            {
                Tag = patternControl,
                Name = emptyPattern.Name,
                ImageIndex = PatrolPatternsEditForm.PatternImageIndex,
                SelectedImageIndex = PatrolPatternsEditForm.PatternImageIndex
            };
            targetNode.Nodes.Add(emptyNode);
            OnPatternNodeAdded(emptyNode);
        }

        public void AddPattern(TreeNode parentNode, PatrolPattern pattern)
        {
            GenreFolder parentFolder = (GenreFolder)parentNode.Tag;
            pattern.ParentFolder = parentFolder;
            if (ContainsPatternKey(pattern.Name))
            {
                pattern.Name = Rename(pattern.Name);
            }

            PatrolPatternEditControl patternControl = new PatrolPatternEditControl(pattern);          
            patternControl.PatternNameChanged +=
                new EventHandler<PatrolPatternNameChangedEventArgs>(patternControl_PatternNameChanged);
            TreeNode patternNode = new TreeNode(pattern.Name)
            {
                Tag = patternControl,
                Name = pattern.Name,
                ImageIndex = PatrolPatternsEditForm.PatternImageIndex,
                SelectedImageIndex = PatrolPatternsEditForm.PatternImageIndex
            };
            parentNode.Nodes.Add(patternNode);
            OnPatternNodeAdded(patternNode);
        }

        private string Rename(string oldName)
        {
            if (ContainsPatternKey(oldName))
            {
                string baseName = oldName;
                for (int i = 1; ; i++)
                {
                    string newName = string.Format(PatrolPattern.DefaultNameFormat, i);
                    if (!ContainsPatternKey(newName))
                    {
                        return newName;
                    }
                }
            }
            else
            {
                return oldName;
            }
        }


        private bool ContainsPatternKey(string name)
        {           
            foreach (TreeNode folderNode in Nodes)
            {
                foreach (TreeNode patternNode in folderNode.Nodes)
                {
                    if (patternNode.Name.Equals(name))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnPatternNodeAdded(TreeNode addedPatternNode)
        {
            SelectedNode = addedPatternNode;
        }

        /// <summary>
        /// 指定した空フォルダーを追加します
        /// </summary>
        /// <param name="emptyFolder"></param>
        public void AddEmptyFolder(string path)
        {
            GenreFolder emptyFolder = new GenreFolder(path);
            TreeNode folderNode = new TreeNode(emptyFolder.Name)
            {
                Tag = emptyFolder,
                Name = emptyFolder.Name,
                ImageIndex = PatrolPatternsEditForm.FolderImageIndex,
                SelectedImageIndex = PatrolPatternsEditForm.FolderImageIndex
            };
            Nodes.Add(folderNode);
            OnFolderAdded(folderNode);
        }

        private void OnFolderAdded(TreeNode addedFolder)
        {
            SelectedNode = addedFolder;
        }

        /// <summary>
        /// 指定した巡回設定の名前を適用します
        /// </summary>
        /// <param name="updatedPattern">指定する巡回設定</param>
        public void UpdatePattern(string key, string newName)
        {
            //TreeNode targetNode = FindPatternNode(updatedPattern);
            TreeNode targetNode = Nodes.Find(key, true)[0];
            if (targetNode != null)
            {
                targetNode.Text = newName;
            }
        }

        public void MoveNode(TreeNodeMovePattern direction)
        {
            if (SelectedNode == null)
            {
                throw new InvalidOperationException("移動対象が選択されていません");
            }
            switch (SelectedNode.Level)
            {
                case FolderLevel:
                    MoveFolderNode(direction, SelectedNode);
                    break;

                case PatternLevel:
                    MovePatternNode(direction, SelectedNode);
                    break;
            }
        }

        private void MovePatternNode(TreeNodeMovePattern move, TreeNode node)
        {
            switch (move)
            {
                case TreeNodeMovePattern.Up:
                    int index = node.Index - 1;
                    if (0 <= index)
                    {
                        MoveNode(index, node.Parent.Nodes, node);
                    }
                    else if (node.Parent.PrevNode != null)
                    {
                        MoveNode(node.Parent.PrevNode.Nodes.Count, node.Parent.PrevNode.Nodes, node);
                    }
                    break;
                case TreeNodeMovePattern.Down:
                    index = node.Index + 1;
                    if (node.Parent.Nodes.Count - 1 >= index)
                    {
                        MoveNode(index, node.Parent.Nodes, node);
                    }
                    else if (node.Parent.NextNode != null)
                    {
                        MoveNode(0, node.Parent.NextNode.Nodes, node);
                    }
                    break;
            }
        }

        private void MoveFolderNode(TreeNodeMovePattern move, TreeNode node)
        {
            switch (move)
            {
                case TreeNodeMovePattern.Up:
                    int index = node.Index - 1;
                    if (0 <= index)
                    {
                        MoveNode(index, Nodes, node);
                    }
                    break;
                case TreeNodeMovePattern.Down:
                    index = node.Index + 1;
                    if (Nodes.Count - 1 >= index)
                    {
                        MoveNode(index, Nodes, node);
                    }
                    break;
            }
        }

        private void MoveNode(int index, TreeNodeCollection nodes, TreeNode targetNode)
        {
            nodes.Remove(targetNode);
            nodes.Insert(index, targetNode);
            OnMovedNode(targetNode);
        }

        private void OnMovedNode(TreeNode movedNode)
        {
            SelectedNode = movedNode;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatrolPatternTreeView));
            this.patrolPatternImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFilderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // patrolPatternImageList
            // 
            this.patrolPatternImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("patrolPatternImageList.ImageStream")));
            this.patrolPatternImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.patrolPatternImageList.Images.SetKeyName(0, "folder.png");
            this.patrolPatternImageList.Images.SetKeyName(1, "images.png");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFilderToolStripMenuItem,
            this.copyPatternToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 48);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // openFilderToolStripMenuItem
            // 
            this.openFilderToolStripMenuItem.Image = global::DeanCC.Properties.Resources.folder_go;
            this.openFilderToolStripMenuItem.Name = "openFilderToolStripMenuItem";
            this.openFilderToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openFilderToolStripMenuItem.Text = "開く";
            // 
            // copyPatternToolStripMenuItem
            // 
            this.copyPatternToolStripMenuItem.Image = global::DeanCC.Properties.Resources.page_copy1;
            this.copyPatternToolStripMenuItem.Name = "copyPatternToolStripMenuItem";
            this.copyPatternToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.copyPatternToolStripMenuItem.Text = "コピー";
            // 
            // PatrolPatternTreeView
            // 
            this.ContextMenuStrip = this.contextMenuStrip;
            this.ImageIndex = 0;
            this.ImageList = this.patrolPatternImageList;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 0;
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }

            openFilderToolStripMenuItem.Enabled = (SelectedNode.Level == FolderLevel);
            copyPatternToolStripMenuItem.Enabled = (SelectedNode.Level == PatternLevel);
        }
    }

    public enum TreeNodeMovePattern
    {
        Up,
        Down
    }
}
