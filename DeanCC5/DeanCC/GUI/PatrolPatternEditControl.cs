using System;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core._2ch;
using DeanCCCore.Core._2ch.Utility;
using System.Linq;

namespace DeanCC.GUI
{
    public sealed partial class PatrolPatternEditControl : UserControl
    {
        public event EventHandler<PatrolPatternNameChangedEventArgs> PatternNameChanged;

        public PatrolPatternEditControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public PatrolPatternEditControl(PatrolPattern pattern)
            : this()
        {
            SetPattern(pattern);
        }

        private PatrolPattern currentPattern;
        public PatrolPattern CurrentPattern
        {
            get
            {
                if (currentPattern == null)
                {
                    currentPattern = new PatrolPattern();
                }
                return currentPattern;
            }
        }
        private BoardInfoCollection currentBoards;

        public void SetBoards(BoardInfoCollection boards)
        {
            currentBoards = boards;
        }

        public void AddBoard(BoardInfo board)
        {
            BoardInfoCollection boards = currentBoards != null ? currentBoards : new BoardInfoCollection();
            if (!boards.Contains(board))
            {
                boards.Add(board);
                targetBoardsListBox.Items.Add(board);
            }
            SetBoards(boards);
        }

        public void RemoveBoard(BoardInfo board)
        {
            if (currentBoards != null)
            {
                currentBoards.Remove(board);
                targetBoardsListBox.Items.Remove(board);
            }
        }

        public void SetPattern(PatrolPattern source)
        {
            Name = source.Name;
            currentPattern = source;
            currentBoards = source.TargetBoards;
            targetBoardsListBox.Items.AddRange(currentBoards.ToArray());
            //targetBoardsListBox.DisplayMember = "Name";
            //boardsTextBox.Source = currentBoards;
            SetParameters(source.Pattern,
             source.NGPattern,
                //source.IsIgnorePattern,
             source.SubFolderFormat,
             source.Name,
             source.EnableJpg,
             source.EnablePng,
             source.EnableGif,
             source.EnableBmp,
             source.EnableZip);
        }

        private void SetParameters(string pattern, string ngPattern, //bool ignorePattern,
            string subFolderFormat, string name, bool jpg, bool png, bool gif, bool bmp, bool zip)
        {
            //boardsTextBox.UpdateText();
            patternTextBox.Text = pattern;
            ngPatternTextBox.Text = ngPattern;
            //ignorePatternCheckBox.Checked = ignorePattern;
            subFolderFormatControl.Text = subFolderFormat;
            nameTextBox.Text = name;
            jpgCheckBox.Checked = jpg;
            pngCheckBox.Checked = png;
            gifCheckBox.Checked = gif;
            bmpCheckBox.Checked = bmp;
            zipCheckBox.Checked = zip;
        }

        public PatrolPattern GetInitializedCurrentPattern(GenreFolder parent)
        {
            try
            {
                CurrentPattern.ParentFolder = parent;
                CurrentPattern.Initialize(currentBoards,
                    patternTextBox.Text,
                    ngPatternTextBox.Text,
                    //ignorePatternCheckBox.Checked,
                    jpgCheckBox.Checked,
                    pngCheckBox.Checked,
                    gifCheckBox.Checked,
                    bmpCheckBox.Checked,
                    zipCheckBox.Checked,
                    nameTextBox.Text,
                    subFolderFormatControl.Text);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(string.Format("{0}：{1}\n正しい値を入力しなおしてください。", CurrentPattern.Name, ex.Message),
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return CurrentPattern;
        }

        private void OnPatternNameChanged(PatrolPatternNameChangedEventArgs e)
        {
            if (PatternNameChanged != null)
            {
                PatternNameChanged(this, e);
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            OnPatternNameChanged(new PatrolPatternNameChangedEventArgs(CurrentPattern.Name, nameTextBox.Text));
        }

        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (targetBoardsListBox.SelectedItem != null)
            {
                RemoveBoard((BoardInfo)targetBoardsListBox.SelectedItem);
            }
        }
    }
}
