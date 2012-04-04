using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core;
using System.Windows.Forms;

namespace DeanCC.GUI
{
    public sealed class ImageHeaderView : BindingDataGridView
    {
        protected override Type SourceType
        {
            get
            {
                return typeof(ImageHeader);
            }
        }

        protected override DataGridViewColumn CreatePropertyColumn(System.Reflection.PropertyInfo property,
            string displayName, DisplayDataGridViewColumnTypeAttribute columnType = null)
        {
            DataGridViewColumn column = base.CreatePropertyColumn(property, displayName, columnType);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            return column;
        }
    }
}
