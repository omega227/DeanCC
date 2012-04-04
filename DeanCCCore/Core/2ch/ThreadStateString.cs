using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    public static class ThreadStateString
    {
        public static string GetText(ThreadState state)
        {
            switch(state)
            {
                case ThreadState.CancelDownload:
                    return "（　＾ω＾）ダウンロードキャンセル中だお";

                case ThreadState.DownloadComplete:
                    return "（　＾ω＾）ダウンロード完了だお";

                case ThreadState.Downloading:
                    return "(｀・ω・´)ダウンロード中";

                case ThreadState.Updated:
                    return "（　＾ω＾）更新してるお";

                case ThreadState.NotUpdate:
                    return "（　＾ω＾）更新してないお";

                case ThreadState.Pastlog:
                    return "(´・ω・｀)dat落ち";

                case ThreadState.Updating:
                    return "(｀・ω・´)レス取得中";

                case ThreadState.None:
                    return "('A`)ﾏﾀﾞDLｼﾃﾅｲﾖ";

                case ThreadState.Normal:
                default :
                    return "（　＾ω＾）レス取得待機中だお";
            }
        }
    }
}
