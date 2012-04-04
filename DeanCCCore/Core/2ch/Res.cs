using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch
{
    public sealed class Res
    {
        private static readonly Regex ResPattern = new Regex(@"^(?<Name>.*)<>(?<Mail>.*)<>(?<ID>.*)<>(?<Body>.*)<>.*$", RegexOptions.Multiline | RegexOptions.Compiled);
        public const string IDFormat = "%id%";
        public const string MailFormat = "%mail%";
        public const string NameFormat = "%name%";
        public const string BodyFormat = "%body%";

        public Res(string dat)
        {
            Load(dat);
        }

        public void Load(string dat)
        {
            Match res = ResPattern.Match(dat);
            Name = res.Groups["Name"].Value;
            Mail = res.Groups["Mail"].Value;
            ID = res.Groups["ID"].Value;
            Body = res.Groups["Body"].Value;
            OriginalDat = dat;
        }

        public static bool ContainsFormat(string target)
        {
            return target.Contains(IDFormat) ||
                  target.Contains(MailFormat) ||
                  target.Contains(NameFormat) ||
                  target.Contains(BodyFormat);
        }

        public string Format(string target)
        {
            string replacedTarget = target.Replace(IDFormat, ID);
            replacedTarget = replacedTarget.Replace(MailFormat, Mail);
            replacedTarget = replacedTarget.Replace(NameFormat, Name);
            replacedTarget = replacedTarget.Replace(BodyFormat, Body);
            return replacedTarget;
        }

        public string Name { get; set; }
        public string ID { get; set; }
        public string Body { get; set; }
        public string Mail { get; set; }
        public string OriginalDat { get; private set; }
    }
}
