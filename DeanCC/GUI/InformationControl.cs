using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;

namespace DeanCC.GUI
{
    public sealed partial class InformationControl : UserControl
    {
        private const int Kilo = 1024;
        private const string KiloFormat = "{0:N1} kB";
        private const int Mega = Kilo * Kilo;
        private const string MegaFormat = "{0:N1} MB";
        private const int Giga = Mega * Kilo;
        private const string GigaFormat = "{0:N1} GB";
        private const string ByteFormat = "{0:N0} B";
        private const string CurrentUploadFormat = "起動後のアップロード量：{0}";
        private const string CurrentDownloadFormat = "起動後のダウンロード量：{0}";
        private const string TotalUploadFormat = "総アップロード量：{0}";
        private const string TotalDownloadFormat = "総ダウンロード量：{0}";
        private const string TotalUpspanFormat = @"総起動時間：{0:d\.hh\:mm\:ss}";
        private const string LastUptimeFormat = "最終起動日時：{0}";
        private const string TotalAddThreadFormat = "追加スレッド数：{0:N0}";
        private const string TotalDownloadThreadFormat = "ダウンロード完了スレッド数：{0:N0}";
        private const string TotalSaveImageFormat = "保存画像数：{0:N0}";

        public InformationControl()
        {
            InitializeComponent();
            Set();
        }

        public void Set()
        {
            ApplicationInformation info = Common.CurrentSettings.Information;
            currentUploadLabel.Text = string.Format(CurrentUploadFormat, FormatByte(info.CurrentUploadByte));
            currentDownloadLabel.Text = string.Format(CurrentDownloadFormat, FormatByte(info.CurrentDownloadByte));
            totalUploadLabel.Text = string.Format(TotalUploadFormat, FormatByte(info.TotalUploadByte));
            totalDownloadLabel.Text = string.Format(TotalDownloadFormat, FormatByte(info.TotalDownloadByte));
            totalUpspanLabel.Text = string.Format(TotalUpspanFormat, info.TotalUpspan);
            lastUptimeLabel.Text = string.Format(LastUptimeFormat, info.LastUptime);
            totaladdThreadLabel.Text = string.Format(TotalAddThreadFormat, info.TotalAddedThreadCount);
            totalDownloadThreadLabel.Text = string.Format(TotalDownloadThreadFormat, info.TotalDownloadCompletedThreadCount);
            totalSaveImageLabel.Text = string.Format(TotalSaveImageFormat, info.TotalSavedImageCount);
        }

        private static string FormatByte(long value)
        {
            if (value > Giga)
            {
                return string.Format(GigaFormat, (double)value / Giga);
            }
            else if (value > Mega)
            {
                return string.Format(MegaFormat, (double)value / Mega);
            }
            else if (value > Kilo)
            {
                return string.Format(KiloFormat, (double)value / Kilo);
            }
            else
            {
                return string.Format(ByteFormat, value);
            }
        }
    }
}
