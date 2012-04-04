using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public interface IBoardTable : IEnumerable<ICategory>
    {
        void Add(IBoardTable table);
        void Clear();
        bool Contains(IBoardInfo board);
        IBoardInfo FindFromName(string name, string domainPath);
        IBoardInfo FindFromUrl(string url);
        void OnlineUpdate(string url);
        void Replace(IBoardInfo oldBoard, IBoardInfo newBoard);
        DateTime LastModified { get; set; }
        IBoardInfo[] ToArray();

        //CategoryCollection Categories { get; }
    }
}
