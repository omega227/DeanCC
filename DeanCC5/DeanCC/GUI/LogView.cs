using System;
using System.Windows.Forms;
using DeanCCCore.Core;

namespace DeanCC.GUI
{
    public sealed class LogView : BindingDataGridView
    {
        public LogView()
        {
            DataSource = Common.Logs;
        }

        protected override Type SourceType
        {
            get
            {
                return typeof(LogItem);
            }
        }

        protected override DataGridViewColumn CreatePropertyColumn(System.Reflection.PropertyInfo property,
            string displayName, DisplayDataGridViewColumnTypeAttribute columnType = null)
        {
            DataGridViewColumn column = base.CreatePropertyColumn(property, displayName, columnType);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            return column;
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                DataGridViewRow row = Rows[i];
                LogItem item = (LogItem)row.DataBoundItem;
                row.DefaultCellStyle.ForeColor = item.TextColor;
            }
            FirstDisplayedScrollingRowIndex = Rows.Count - 1;//最後の列までスクロール
            Invalidate(true);
            base.OnRowsAdded(e);
        }
    }
}
