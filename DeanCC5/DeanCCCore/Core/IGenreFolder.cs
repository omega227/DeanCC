using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    public interface IGenreFolder
    {
        string LocalPath { get; set; }
        string Name { get; }
    }
}
