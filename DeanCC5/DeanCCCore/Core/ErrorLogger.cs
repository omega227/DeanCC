using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using DeanCCCore.Core.Options;
using System.Reflection;

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
変更されたオプション：
{3}
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
            //string changedOptions = GetChangedOptions();
            //changedOptions = string.IsNullOrEmpty(changedOptions) ? "なし" : changedOptions;
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

        //private static string GetChangedOptions()
        //{
        //    StringBuilder changedOptions = new StringBuilder();
        //    OptionItems defaultOption = new OptionItems();
        //    typeof(OptionItems).GetMembers().Where(member => member.MemberType == System.Reflection.MemberTypes.Property).ToList().ForEach(option =>
        //        {
        //            option.GetType()..GetMembers().Where(member => member.MemberType == System.Reflection.MemberTypes.Property).ToList().ForEach(member =>
        //                {
        //                    PropertyInfo optionProperty = (PropertyInfo)member;
        //                    if (optionProperty.GetValue(defaultOption, null) != optionProperty.GetValue(defaultOption, null))
        //                    {
        //                        changedOptions.AppendLine(optionProperty.Name);
        //                    }
        //                });
        //        });

        //    return changedOptions.ToString();
        //}
    }
}
