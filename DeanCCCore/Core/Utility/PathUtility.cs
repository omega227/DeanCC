using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeanCCCore.Core.Utility
{
    public static class PathUtility
    {
        /// <summary>
        /// 基準パスを指定して、相対パスを取得します
        /// </summary>
        /// <param name="basePath">指定する基準パス</param>
        /// <param name="sourcePath">取得元の絶対パス</param>
        /// <exception cref="System.ArgumentException">basePathまたはsoucePathが無効です</exception>
        /// <returns>相対パス</returns>
        public static string MakeRelative(string basePath, string sourcePath)
        {
            if (basePath.EndsWith(@"\") == false)
            {
                basePath += @"\";
            }
            try
            {
                Uri baseUri = new Uri(basePath);
                Uri targetUri = new Uri(baseUri, sourcePath);
                string relativePath = baseUri.MakeRelativeUri(targetUri).ToString();
                return Uri.UnescapeDataString(relativePath).Replace('/', '\\');//Uri.Tostring()でエンコードされた文字をデコード
            }
            catch (UriFormatException ex)
            {
                throw new ArgumentException("入力されたパスが無効です", ex);
            }
        }
    }
}
