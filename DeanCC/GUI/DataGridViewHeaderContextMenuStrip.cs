using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeanCC.GUI
{
    public sealed class DataGridViewHeaderContextMenuStrip : ContextMenuStrip
    {
        public DataGridViewHeaderContextMenuStrip()
        {
        }

        /// <summary>
        /// 項目を作成します
        /// </summary>
        public void GenerateItem(DataGridViewColumn column)
        {
            //ヘッダーのメニューに登録
            column.HeaderCell.ContextMenuStrip = this;

            //メニュー項目を追加
            ToolStripMenuItem item = new ToolStripMenuItem(column.HeaderText);
            item.Click += new EventHandler(item_Click);
            item.Name = column.DataPropertyName;//Nameプロパティが設定しても""になる
            item.CheckOnClick = true;
            item.Checked = column.Visible;

            Items.Add(item);
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (DataGridViewColumn column in ((DataGridView)SourceControl).Columns)
            {
                if (column.DataPropertyName.Equals(item.Name))
                {
                    column.Visible = item.Checked;
                }
            }
        }
    }
}
