using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core
{
    [Serializable]
    public class PatrolPatternCollection : System.Collections.ObjectModel.Collection<PatrolPattern>
    {
        public PatrolPatternCollection()
        {            
        }

        protected override void InsertItem(int index, PatrolPattern item)
        {
            if (Contains(item))
            {
                throw new ArgumentException("追加しようとした巡回設定は既に存在しています。");
            }

            base.InsertItem(index, item);
        }
    }
}
