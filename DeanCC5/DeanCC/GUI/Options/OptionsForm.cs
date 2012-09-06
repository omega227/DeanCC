using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core.Options;
using System.Reflection;

namespace DeanCC.GUI.Options
{
    public sealed partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            controlTreeView.SelectedNode = controlTreeView.Nodes[0].Nodes[0];//先頭のメニューを選択状態にする
        }

        //optionsのEnumratorは変更された可能性のあるコントロールの列挙を返す
        private OptionsControlCollection options = new OptionsControlCollection();
        private const int OptionsControlLevel = 1;

        private UserControl GetOptionControl(string name)
        {
            PropertyInfo property = typeof(OptionsControlCollection).GetProperty(name);
            if (property == null)
            {
                return null;
            }
            return (UserControl)property.GetGetMethod().Invoke(options, null);
        }

        private bool SameControl(UserControl control)
        {
            return SelectedOptionsPanel.Controls.Count > 0 && control.Equals(SelectedOptionsPanel.Controls[0]);
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                foreach (IOptionsControl changedOptions in options)//変更された可能性のあるオプションをCurrentSettingsに適用
                {
                    try
                    {
                        changedOptions.Get(Common.Options);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message + "\n正しい値を入力しなおしてください。",
                            "確認", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        e.Cancel = true;
                        return;
                    }
                }
                Common.Options.PerformChanged();
                if (Common.IsPatrolling)
                {
                    MessageBox.Show("設定の編集が完了しました。\n巡回中は変更が反映されない項目があります。\n変更された設定は再起動後に有効になります",
                        "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void controlTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == OptionsControlLevel)
            {
                UserControl selectedControl = GetOptionControl(e.Node.Name);
                if (selectedControl != null && !SameControl(selectedControl))
                {
                    if (!((IOptionsControl)selectedControl).Loaded)
                    {
                        ((IOptionsControl)selectedControl).Set(Common.Options);
                    }
                    SelectedOptionsPanel.Controls.Clear();
                    SelectedOptionsPanel.Controls.Add(selectedControl);
                }
            }
        }

        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            if (Common.CurrentSettings.FirstRunning && !Common.ShowsFirstOptionDescription)
            {
                MessageBox.Show("設定を変更できます。特に必要ないならそのままOKを押してください。\nJaneStyleを使用している場合は詳細>ブラウザー を設定するとJaneStyleで取得可能な画像URLがそのまま利用できます。(ImageViewURLReplace.dat等)"
                    , "初回起動", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Common.ShowsFirstOptionDescription = true;
            }
        }
    }
}
