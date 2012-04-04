using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using DeanCCCore.Core._2ch.Jane;
using DeanCCCore.Core.Utility;

namespace DeanCCCore.Core
{
    [Serializable]
    public class ZipFileHeader : ImageHeader
    {
        public ZipFileHeader()
        {
        }

        public ZipFileHeader(int sourceResIndex , string url)
        {
            SourceResIndex = sourceResIndex;
            OriginalUrl = url;
        }

        public override bool IsZip
        {
            get
            {
                return true;
            }
        }
    }
}
