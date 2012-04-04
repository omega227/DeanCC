using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core.Utility;

namespace DeanCC.GUI
{
    /// <summary>
    /// バインドしたオブジェクトのデータを表示します。
    /// </summary>
    public class BindingDataGridView : DataGridView
    {
        public BindingDataGridView()
        {
            InitializeComponent();
            DoubleBuffered = true;
            AutoGenerateColumns = false;
            ShowRowErrors = false;
            ShowEditingIcon = false;
            ShowCellToolTips = false;
            ShowCellErrors = false;
            RowHeadersVisible = false;
            Dock = DockStyle.Fill;
            BackgroundColor = SystemColors.Window;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ReadOnly = true;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            AllowUserToResizeRows = false;
            AllowUserToOrderColumns = true;
            AllowUserToDeleteRows = false;
            AllowUserToAddRows = false;
        }       

        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
            base.OnRowPrePaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = HitTest(e.X, e.Y);
                if (hti.Type == DataGridViewHitTestType.Cell && !Rows[hti.RowIndex].Selected)
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        Rows[hti.RowIndex].Selected = true;
                    }
                    else
                    {
                        ClearSelection(-1, hti.RowIndex, true);
                    }
                }
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// バインドされているコレクションの元の型を表します
        /// </summary>
        protected virtual Type SourceType
        {
            get
            {
                if (DataSource != null && DataSource.GetType().IsGenericType)
                {
                    return DataSource.GetType().GetGenericArguments()[0];
                }
                else
                {
                    return typeof(object);
                }
            }
        }       

        /// <summary>
        /// 指定したプロパティの列を作成します
        /// </summary>
        /// <param name="property">表示するプロパティ</param>
        /// <param name="columnType">列の種類</param>
        /// <param name="name">列の名前</param>
        /// <returns>プロパティを表示する列のインスタンス</returns>
        protected virtual DataGridViewColumn CreatePropertyColumn(PropertyInfo property, string displayName,
            DisplayDataGridViewColumnTypeAttribute columnType = null)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = Name = property.Name,
                HeaderText = displayName,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            };

            return column;
        }

        private void CreatePropertyColumns()
        {
            List<DataGridViewColumn> columns = new List<DataGridViewColumn>();
            foreach (PropertyInfo property in SourceType.GetProperties())
            {
                Attribute browsableAttribute =
                    Attribute.GetCustomAttribute(property, typeof(BrowsableAttribute));
                if (browsableAttribute != null && !((BrowsableAttribute)browsableAttribute).Browsable)
                {
                    //非表示属性
                    continue;
                }

                Attribute nameAttribute = Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute));
                if (nameAttribute != null)
                {
                    //非表示属性でなく、表示名が明示的に属性で指定されているものを表示                    
                    Attribute columnAttribute =
                        Attribute.GetCustomAttribute(property, typeof(DisplayDataGridViewColumnTypeAttribute));
                    columns.Add(CreatePropertyColumn(property, ((DisplayNameAttribute)nameAttribute).DisplayName,
                        (DisplayDataGridViewColumnTypeAttribute)columnAttribute));
                }
            }
            SuspendLayout();
            Columns.Clear();
            Columns.AddRange(columns.ToArray());
            ResumeLayout();
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            if (Columns.Count <= 0)
            {
                CreatePropertyColumns();
            }
            base.OnDataSourceChanged(e);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // BindingDataGridView
            // 
            this.RowTemplate.Height = 21;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
