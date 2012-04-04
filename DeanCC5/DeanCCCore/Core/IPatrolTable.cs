using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core
{
    public interface IPatrolTable : IList<GenreFolder>, IEnumerable<GenreFolder>
    {
        void Add(PatrolPattern pattern);
        void Remove(PatrolPattern pattern);
        void Contains(PatrolPattern pattern);
    }
}
