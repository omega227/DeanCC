using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class ImageHash
    {
        private const int md5HashLength = 32;

        public string MD5Hash { get; set; }
        public DateTime CreatedTime { get; set; }
        public string SavePath { get; set; }

        public ImageHash(string md5Hash, string savePath)
        {
            if (md5Hash == null)
            {
                throw new ArgumentNullException("MD5Hash");
            }
            if (md5Hash.Length != md5HashLength)
            {
                throw new ArgumentException("MD5ハッシュ以外の値が入力されました。");
            }

            MD5Hash = md5Hash;
            CreatedTime = DateTime.Now;
            SavePath = savePath;
        }

        public override bool Equals(object obj)
        {
            return obj is ImageHash && ((ImageHash)obj).MD5Hash.Equals(MD5Hash);
        }
        public override int GetHashCode()
        {
            return MD5Hash.GetHashCode();
        }
    }
}
