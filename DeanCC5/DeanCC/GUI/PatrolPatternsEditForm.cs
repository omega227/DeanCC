using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core._2ch;

namespace DeanCC.GUI
{
    public sealed partial class PatrolPatternsEditForm : Form
    {
        public const int FolderImageIndex = 0;
        public const int PatternImageIndex = 1;

        public PatrolPatternsEditForm()
        {
            InitializeComponent();
        }

        private void addFolderToolStripButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.Path.GetFileName(folderBrowserDialog1.SelectedPath) == string.Empty)
                {
                    MessageBox.Show("ルートフォルダーは追加できません。\nそれ以外のフォルダーを追加してください。",
                        "確認", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //再表示
                    addFolderToolStripButton.PerformClick();
                    return;
                }

                patrolPatternTreeView1.AddEmptyFolder(folderBrowserDialog1.SelectedPath);
                OnAddedFolder();
            }
        }
        private void OnAddedFolder()
        {
            //巡回設定を追加
            addPatternToolStripButton.PerformClick();
        }

        private void OnAddingPattern()
        {
            if (patrolPatternTreeView1.Nodes.Count == 1)
            {
                patrolPatternTreeView1.SelectedNode = patrolPatternTreeView1.Nodes[0];
            }
        }
        private void OnAddedPattern()
        {
            //前に選択した板を設定
            if (boardTableTreeView.SelectedBoard != null)
            {
                PatrolPatternEditControl selectedPatternControl
                    = (PatrolPatternEditControl)patrolPatternPanel.Controls[0];
                //if (!selectedPatternControl.CurrentPattern.TargetBoards.Contains(boardTableTreeView.SelectedBoard))
                //{
                //    selectedPatternControl.CurrentPattern.TargetBoards.Add(boardTableTreeView.SelectedBoard);
                //}
                selectedPatternControl.AddBoard(boardTableTreeView.SelectedBoard);
            }
        }

        private void addPatternToolStripButton_Click(object sender, EventArgs e)
        {
            OnAddingPattern();
            try
            {
                patrolPatternTreeView1.AddEmptyPattern();
                OnAddedPattern();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RemovePatternToolStripButton_Click(object sender, EventArgs e)
        {
            if (patrolPatternTreeView1.SelectedNode == null)
            {
                return;
            }
            if (MessageBox.Show(string.Format("{0}を削除します", patrolPatternTreeView1.SelectedNode.Text),
                "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                patrolPatternTreeView1.Nodes.Remove(patrolPatternTreeView1.SelectedNode);
            }
        }

        private void PatrolPatternsEditForm_Load(object sender, EventArgs e)
        {
            patrolPatternTreeView1.Set(Common.PatrolPatterns);
        }

        private void UpToolStripButton_Click(object sender, EventArgs e)
        {
            if (patrolPatternTreeView1.SelectedNode == null)
            {
                return;
            }
            patrolPatternTreeView1.MoveNode(TreeNodeMovePattern.Up);
        }

        private void DownToolStripButton_Click(object sender, EventArgs e)
        {
            if (patrolPatternTreeView1.SelectedNode == null)
            {
                return;
            }

            patrolPatternTreeView1.MoveNode(TreeNodeMovePattern.Down);
        }

        private void PatrolPatternsEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                bool retry = patrolPatternTreeView1.Get(Common.PatrolPatterns);
                if (retry)
                {
                    e.Cancel = true;
                    return;
                }
                Common.PatrolPatterns.PerformChanged();
                if (restartsTimer)
                {
                    Common.PatrolTimer.Start();
                    Common.QuickPatrolTimer.Start();
                }
            }
            else if (DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                if (MessageBox.Show("変更内容を保存せずに閉じますか？", "確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        void PatrolPatternsEditForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK && !restartsTimer && !Common.IsPatrolling)
            {
                if (MessageBox.Show("設定が完了しました。\n今すぐスレッドの取得を開始しますか。", "巡回確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Common.PatrolTimer.Start();
                    Common.QuickPatrolTimer.Start();
                }
            }
        }

        private bool restartsTimer;

        private void PatrolPatternsEditForm_Shown(object sender, EventArgs e)
        {
            if (Common.CurrentSettings.FirstRunning && !Common.ShowsFirstPatrolPatternEditDescription)
            {
                MessageBox.Show("最初に巡回するスレッドの設定をする必要があります。\n画像を保存するフォルダーの登録と、巡回設定の作成をして下さい。"
                    , "初回起動", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Common.ShowsFirstPatrolPatternEditDescription = true;
                restartsTimer = true;
            }
        }

        private void patrolPatternTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PatrolPatternEditControl selectedPattern = e.Node.Tag as PatrolPatternEditControl;
            if (selectedPattern != null)
            {
                patrolPatternPanel.Controls.Clear();
                patrolPatternPanel.Controls.Add(selectedPattern);
                //検索対象板のアイコンを変える
            }
        }

        private void boardTableVisibleButton_Click(object sender, EventArgs e)
        {
            boardTableTreeView.Visible = !boardTableTreeView.Visible;
        }

        private void boardTableTreeView_BoardSelected(object sender, BoardTableTreeViewItemSelectedEventArgs e)
        {
            if (patrolPatternPanel.Controls.Count > 0)
            {
                PatrolPatternEditControl selectedPatternControl
                    = (PatrolPatternEditControl)patrolPatternPanel.Controls[0];
                selectedPatternControl.AddBoard((BoardInfo)e.SelectedBoard);
                //if (!selectedPatternControl.CurrentPattern.TargetBoards.Contains((BoardInfo)e.SelectedBoard))
                //{
                //    selectedPatternControl.CurrentPattern.TargetBoards.Add((BoardInfo)e.SelectedBoard);
                //}
                //selectedPatternControl.AddBoard((BoardInfo)e.SelectedBoard);
            }
        }

        private void patrolPatternPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            boardTableVisibleButton.Visible = true;
        }
    }
}
