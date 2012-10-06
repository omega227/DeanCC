using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DeanCCCore.Core._2ch
{
    public sealed class Res
    {
        private static readonly Regex ResPattern = new Regex(@"^(?<Name>.*)<>(?<Mail>.*)<>(?<Date>.*?)(ID:(?<ID>.*))?<>(?<Body>.*)<>.*$", RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex DateTimeFormatPattern = new Regex(@"(\d+)/(\d+)/(\d+)\D*((\d+):(\d+))?", RegexOptions.Compiled);
        private static readonly string[] DateTimeFormats = { "yyyyMMddHHmm", "yyyyMMdd" };
        public const string IDFormat = "%id%";
        public const string MailFormat = "%mail%";
        public const string NameFormat = "%name%";
        private static readonly Regex BodyFormatPattern = new Regex("%body=(.+?)%", RegexOptions.IgnoreCase);
        private static readonly Regex DateFormatPattern = new Regex("%date=(.+?)%", RegexOptions.IgnoreCase);

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string ID { get; set; }
        public string Body { get; set; }
        public string Mail { get; set; }
        public string OriginalDat { get; private set; }

        public Res(string dat)
        {
            Load(dat);
        }

        public void Load(string dat)
        {
            Match res = ResPattern.Match(dat);
            Name = res.Groups["Name"].Value;
            Mail = res.Groups["Mail"].Value;
            Date = TryParseDateTime(res.Groups["Date"].Value);
            ID = res.Groups["ID"].Value;
            Body = res.Groups["Body"].Value;

            OriginalDat = dat;
        }

        private DateTime TryParseDateTime(string dateString)
        {
            Match dateMatch = DateTimeFormatPattern.Match(dateString);
            DateTime date;
            if (dateMatch.Success &&
                DateTime.TryParseExact(dateMatch.Result("$1$2$3$5$6"),
            DateTimeFormats, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
            {
                return date;
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static bool ContainsFormat(string target)
        {
            return target.Contains(IDFormat) ||
                  target.Contains(MailFormat) ||
                  target.Contains(NameFormat) ||
                  BodyFormatPattern.IsMatch(target) ||
                  DateFormatPattern.IsMatch(target);
        }

        public string Format(string target)
        {
            string replacedTarget = target.Replace(IDFormat, ID);
            replacedTarget = replacedTarget.Replace(MailFormat, Mail);
            replacedTarget = replacedTarget.Replace(NameFormat, Name);
            replacedTarget = ReplaceBodyFormat(replacedTarget);
            replacedTarget = ReplaceDateFormat(replacedTarget);

            return replacedTarget;
        }

        private string ReplaceBodyFormat(string target)
        {
            //1つだけ本文に正規表現マッチをして置換する
            Match bodyFormatMatch = BodyFormatPattern.Match(target);
            if (!bodyFormatMatch.Success)
            {
                return target;
            }

            string bodyFormat = bodyFormatMatch.Groups[1].Value;
            string bodyMatch = Regex.Match(Body, bodyFormat).Groups[1].Value;
            string replacedTarget = target.Replace(bodyFormatMatch.Value, bodyMatch);

            return replacedTarget;
        }

        private string ReplaceDateFormat(string target)
        {
            //一つだけ日付フォーマットを置換する
            Match dateFormatMatch = DateFormatPattern.Match(target);
            if (!dateFormatMatch.Success)
            {
                return target;
            }

            string dateFormat = dateFormatMatch.Groups[1].Value;
            string dateString = Date.ToString(dateFormat);
            string replacedTarget = target.Replace(dateFormatMatch.Value, dateString);

            return replacedTarget;
        }
    }
}
