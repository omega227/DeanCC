using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace DeanCCCore.Core
{
    public sealed class LogItem
    {
        static Color systemGroupTextColor = Color.ForestGreen;
        static Color errorGroupTextColor = Color.Red;
        [DisplayName("時間")]
        public DateTime Time { get; private set; }
        [DisplayName("分類")]
        public string GroupString { get; private set; }
        [DisplayName("ログ")]
        public string Text { get; private set; }
        [Browsable(false)]
        public LogStatus Status { get; private set; }
        [Browsable(false)]
        public Color TextColor
        {
            get
            {
                switch (Status)
                {
                    case LogStatus.System:
                        return systemGroupTextColor;

                    case LogStatus.Error:
                        return errorGroupTextColor;

                    default:
                        return SystemColors.ControlText;
                }
            }
        }

        public LogItem(string group, string text, LogStatus status)
        {
            GroupString = group;
            Text = text;
            Time = DateTime.Now;
            Status = status;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Time, GroupString, Text);
        }
    }
}
