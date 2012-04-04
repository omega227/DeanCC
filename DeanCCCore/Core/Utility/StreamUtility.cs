using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeanCCCore.Core.Utility
{
    public static class StreamUtility
    {
        private const int bufferSize = 1024;

        /// <summary>
        /// ストリームをバイト配列に読み込みます
        /// </summary>
        public static byte[] ReadBytes(Stream stream)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count = -1;
                while (count != 0)
                {
                    count = stream.Read(buffer, 0, buffer.Length);
                    memory.Write(buffer, 0, count);
                }

                return memory.ToArray();
            }
        }
    }
}
