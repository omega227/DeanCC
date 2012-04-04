using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public interface IRes
    {
        string Name { get; }
        string Mail { get; }
        string ID { get; }
        string Body { get; }
        string DateString { get; }
        int Index { get; }
    }
}
