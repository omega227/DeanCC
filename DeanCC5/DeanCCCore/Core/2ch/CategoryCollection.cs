using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeanCCCore.Core._2ch
{
    [Serializable]
    public  class CategoryCollection :System.Collections.ObjectModel.Collection<ICategory>//List<ICategory>
    {
        public CategoryCollection()
        {
        }
    }
}
