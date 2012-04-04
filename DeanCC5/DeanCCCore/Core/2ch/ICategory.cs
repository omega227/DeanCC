using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public interface ICategory : IEnumerable<IBoardInfo>
    {
        BoardInfoCollection Children { get; }
        int Count { get; }
        bool IsExpanded { get; set; }
        string Name { get; set; }
    }
}
