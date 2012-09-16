using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DeanCCCore.Core.Options;

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
                ((System.AggregateException)error).GetBaseException().ToString() : error.ToString();
            string changedOptions = GetChangedOptions();
            string formatedLog = string.Format(LogFormat, DateTime.Now, Common.VersionText, log, changedOptions);
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

        private static string GetChangedOptions()
        {
            StringBuilder changedOptions = new StringBuilder();
            OptionItems defaultOption = new OptionItems();
            typeof(OptionItems).GetProperties().ToList().ForEach(prop =>
                {
                    int undoLength = changedOptions.Length;
                    changedOptions.AppendFormat("[{0}]\r\n", prop.Name);//タグ
                    int length = changedOptions.Length;
                    object defaultObject = prop.GetGetMethod().Invoke(defaultOption, null);
                    object currentObject = prop.GetGetMethod().Invoke(Common.Options, null);

                    GetChangedOptionsInternal(prop, defaultObject, currentObject, changedOptions);
                    if (length == changedOptions.Length)
                    {
                        //変更された値が書き込まれなかったときはタグを削除
                        changedOptions.Remove(undoLength, length - undoLength);
                    }
                });

            return changedOptions.Length > 0 ? changedOptions.ToString() : "なし";
        }

        private static void GetChangedOptionsInternal(PropertyInfo prop, object defaultObject, object currentObject, StringBuilder changedOptions)
        {
            if (!prop.IsSpecialName &&
                prop.CanRead &&
                !prop.PropertyType.IsEnum &&
                prop.PropertyType.Namespace == "DeanCCCore.Core.Options")
            {
                prop.PropertyType.GetProperties().ToList().ForEach(nextProp =>
                    {
                        //オプションアイテムから値プロパティを列挙
                        object nextDefaultObject = nextProp.GetGetMethod().Invoke(defaultObject, null);
                        object nextCurrentObject = nextProp.GetGetMethod().Invoke(currentObject, null);

                        GetChangedOptionsInternal(nextProp, nextDefaultObject, nextCurrentObject, changedOptions);
                    });
            }
            else if (prop.CanWrite &&
                (prop.PropertyType.IsValueType || (currentObject is ICollection)) &&
                prop.PropertyType != typeof(DateTime) &&
                prop.PropertyType != typeof(char))
            {
                //有効な値を書き出し
                if (!defaultObject.Equals(currentObject))
                {
                    changedOptions.AppendFormat("{0}={1}\r\n", prop.Name,
                        (currentObject is ICollection) ? ((ICollection)currentObject).Count.ToString() : currentObject.ToString());
                }
            }
        }
    }
}
