using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch.Jane
{
    public interface IImageViewURLReplace
    {
        ImageViewURLReplaceItem Replace(string url);
        bool IsMatch(string url);
        void Reload();
    }
}
