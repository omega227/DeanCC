using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class DataGridViewHeaderStatus
    {
        public DataGridViewHeaderStatus(DataGridViewColumn column)
        {
            Name = column.DataPropertyName;
            Visible = column.Visible;
            DisplayIndex = column.DisplayIndex;
        }

        public DataGridViewHeaderStatus(string name, bool visible, int displayIndex)
        {
            Name = name;
            Visible = visible;
            DisplayIndex = displayIndex;
        }

        public string Name { get; set; }
        public bool Visible { get; set; }
        public int DisplayIndex { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DataGridViewHeaderStatus && 
                ((DataGridViewHeaderStatus)obj).Name.Equals(this.Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
