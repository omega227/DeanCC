using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public interface ILog
    {
        void Add(string title, string text, LogStatus status);
    }
}
