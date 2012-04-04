using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public interface IBoardInfo
    {
        string DomainPath { get; }
        string Name { get; set; }
        string Path { get; set; }
        string Server { get; set; }
        string ServerName { get; }
        string Url { get; }

        string Read();
    }
}
