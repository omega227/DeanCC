using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch.Jane
{
    public interface IViewCache
    {
        void Save(byte[] image, string contentType, DateTime lastModified, string url, string referer);
    }
}
