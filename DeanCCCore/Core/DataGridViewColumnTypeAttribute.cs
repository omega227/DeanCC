using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 表示するDataGridView の列タイプを指定します
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,Inherited=false)]
    public sealed class DisplayDataGridViewColumnTypeAttribute : Attribute
    {
        public DisplayDataGridViewColumnTypeAttribute(DataGridViewColumnType columnType)
        {
            ColumnType = columnType;
        }
        public DataGridViewColumnType ColumnType { get; set; }
    }

    public enum DataGridViewColumnType
    {
        /// <summary>
        /// 標準の列
        /// </summary>
        TextBox,
        /// <summary>
        /// プログレスバーの列
        /// </summary>
        ProgressBar
    }
}
