using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core.Utility
{
    public static class FileNameFormat
    {
        private const int safePathLength = 200;
        private const int escapedOldTextIndex = 0;
        private const int escapedNewTextIndex = 1;
        private static readonly string[][] escapedTextPairs = 
        { new string[] { "&gt;", "＞" },new string[] { "&lt;", "＜" },new string[] { "&quot;", "”"}, 
            new string[] { "&amp;", "＆"},new string[] {"<", "＜" }, new string[] {">", "＞" }, 
            new string[] { "&", "＆"}, new string[] { "/", "／"}, new string[] { ":", "："},
            new string[] { "*", "＊"}, new string[] { "?", "？"}, new string[] { "|", "｜"}, 
            new string[] { "\"", "”"}, new string[] {"\\", "￥" } };
        private static readonly Regex InvalidTextPattern = new Regex(@"(^\.+)|([\s.]+$)");
        private const int renameStartIndex = 2;
        private const char InvalidCharReplaceChar = '_';
        public static readonly Regex ImageFileExtensionPattern = new Regex(@"\.(jpe?g|png|bmp|gif|zip)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// ファイル名またはフォルダー名をパスに使用できるようにエスケープします
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string EscapeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            string maybeEscapedFileName = fileName;
            foreach (string[] escapedTextPair in escapedTextPairs)
            {
                maybeEscapedFileName = maybeEscapedFileName.Replace(escapedTextPair[escapedOldTextIndex],
                    escapedTextPair[escapedNewTextIndex]);
            }

            string escapedFileName = maybeEscapedFileName;
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                escapedFileName = escapedFileName.Replace(invalidChar, InvalidCharReplaceChar);
            }

            string safeFileName = InvalidTextPattern.Replace(escapedFileName, string.Empty);
            if (string.IsNullOrWhiteSpace(safeFileName))
            {
                throw new ArgumentException("有効な文字が含まれていません");
            }
            if (safeFileName.Length > safePathLength)
            {
                safeFileName = safeFileName.Substring(0, safePathLength);//パスの長さを考慮していない
            }

            return safeFileName;
        }

        /// <summary>
        /// 指定のパスを同名のパスが存在しないようにファイル名を変更します
        /// </summary>
        /// <param name="oldPath"></param>
        /// <returns></returns>
        public static string Rename(string oldPath)
        {
            if (File.Exists(oldPath))
            {
                string saveDirectory = Path.GetDirectoryName(oldPath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(oldPath);
                string extension = Path.GetExtension(oldPath);

                for (int i = renameStartIndex; ; i++)
                {
                    string newFileName = string.Format("{0} ({1}){2}",
                        fileNameWithoutExtension, i, extension);

                    string newPath = Path.Combine(saveDirectory, newFileName);

                    if (!File.Exists(newPath))
                    {
                        return newPath;
                    }
                }
            }
            else
            {
                return oldPath;
            }
        }

        /// <summary>
        /// ファイル名から画像拡張子を取得します<br></br>
        /// 拡張子が含まれていない場合はString.Emptyを返します
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        public static string GetImageExteinsion(string fileName)
        {
            return ImageFileExtensionPattern.Match(fileName).Value;
        }
    }
}
