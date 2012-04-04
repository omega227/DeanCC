using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DeanCCCore.Core._2ch;
using System.Runtime.Serialization;

namespace DeanCCCore.Core
{
    [Serializable]
    public class GenreFolderCollection : Collection<GenreFolder>
    {
        public GenreFolderCollection()
        {
        }
    }
}
