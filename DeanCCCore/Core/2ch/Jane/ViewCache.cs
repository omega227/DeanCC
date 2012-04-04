using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace DeanCCCore.Core._2ch.Jane
{
    public sealed class ViewCache : IViewCache
    {
        private const string fileExtension = ".vch";

        private const string headerFormat =
@"ContentType={0}
LastModified={1:r}
URL={2}
Referer={3}
";

        public ViewCache(string saveFolder)
        {
            this.saveFolder = saveFolder;
        }

        private string saveFolder;

        public void Save(byte[] image, string contentType, DateTime lastModified, string url, string referer, string threadUrl)
        {
            if (string.IsNullOrEmpty(referer) && string.IsNullOrEmpty(threadUrl))
            {
                throw new ArgumentException("referer と thradUrl を両方空にできません");
            }

            string validReferer = string.IsNullOrEmpty(referer) ? threadUrl : referer;
            Save(image, contentType, lastModified, url, validReferer);
        }

        public void Save(byte[] image, string contentType, DateTime lastModified, string url, string referer)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentException("contentType を空にできません");
            }
            if (lastModified == DateTime.MinValue)
            {
                throw new ArgumentException("lastModified を空にできません");
            }
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("url を空にできません");
            }
            if (string.IsNullOrEmpty(referer))
            {
                throw new ArgumentException("referer を空にできません");
            }

            Directory.CreateDirectory(saveFolder);
            string fileName = Encoder.EncodeB32(url) + fileExtension;
            string savePath = Path.Combine(saveFolder, fileName);
            if (!File.Exists(savePath))
            {
                string headerText = string.Format(headerFormat, contentType, lastModified, url, referer);
                byte[] headersize = BitConverter.GetBytes(headerText.Length);
                byte[] header = Encoding.GetEncoding("shift_jis").GetBytes(headerText);
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(headersize, 0, headersize.Length);
                    fs.Write(header, 0, header.Length);
                    fs.Write(image, 0, image.Length);
                }
            }
        }

        public static class Encoder
        {
            static readonly char[] B32Chars = new char[] { 
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' };
            const int fileNameLength = 22;
            const int md5Bit = 128;
            const int md5ByteLength = md5Bit / 8;
            //const int md5Length = md5ByteLength * 2;

            /// <summary>
            /// 指定したURLからエンコードされたファイル名を取得します
            /// </summary>
            /// <param name="url"></param>
            /// <returns></returns>
            public static string EncodeB32(string url)
            {
                byte[] md5 = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(url));
                return EncodeB32(md5);
            }

            /// <summary>
            /// 5bitエンコーディング関数　Base32ではない。
            /// </summary>
            /// <param name="md5"></param>
            /// <returns></returns>
            public static string EncodeB32(byte[] md5)
            {
                if (md5 == null)
                {
                    throw new ArgumentNullException("md5");
                }
                if (md5.Length != md5ByteLength)
                {
                    throw new ArgumentException("md5");
                }

                StringBuilder result = new StringBuilder(fileNameLength);
                for (int i = 0; i < fileNameLength; ++i)
                {
                    int c1 = md5[(i * 5) / 8 + 1] & 255;
                    int c2 = md5[(i * 5) / 8 + 0] & 255;
                    int x = (c1 << 8) + c2;
                    int sh = (i * 5) % 8;
                    x = x >> sh;
                    x = x & 31;

                    result.Append(B32Chars[x]);
                }

                return result.ToString();
            }
        }
    }
}
