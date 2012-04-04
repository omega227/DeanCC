using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core
{
    public static class ErrorLogger
    {
        public static readonly string SavePath = Path.Combine(Settings.SaveFolder, "ErrorLog.txt");
        private const string Discription =
@"このファイルにはDeanCCで発生した致命的なエラーが記録されます。
エラーの内容と直前の操作を作者に報告してください。詳しい連絡先はreadme.txtを参照してください。

";
        private const string LogFormat =
@"{0}
======= ▼ここから▼ =======
DeanCC {1}
{2}
======= ▲ここまで▲ =======

";

        private const string SaveFolderFormat = "%DeanCC%";
        private const string UserNameFormat = "%UserName%";
        private const string UserNamePrefix = "\\";

        /// <summary>
        /// 指定した内容をファイルに書き込みます
        /// </summary>
        /// <exception cref="System.IO.IOException">ファイルへの書き込みに失敗しました</exception>
        /// <param name="log"></param>
        public static void Write(object error)
        {
            string log = error is AggregateException ?
                ((System.AggregateException)error).InnerException.ToString() : error.ToString();
            string formatedLog = string.Format(LogFormat, DateTime.Now, Common.VersionText, log);
            string text = File.Exists(SavePath) ? formatedLog : Discription + formatedLog;
            string maskedText = text.Replace(Settings.SaveFolder, SaveFolderFormat);
            maskedText = maskedText.Replace(UserNamePrefix + Environment.UserName, UserNamePrefix + UserNameFormat);
            string saveFolder = Path.GetDirectoryName(SavePath);
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            using (StreamWriter writer = new StreamWriter(SavePath, true))
            {
                writer.Write(maskedText);
            }
        }
    }
}
