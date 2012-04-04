using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public interface ISecureImage : IImageHeader
    {
        IRes Res { get; set; }
        string GetResHtml();
    }
}
