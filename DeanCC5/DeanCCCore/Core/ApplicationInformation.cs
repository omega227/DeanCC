using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class ApplicationInformation
    {
        public ApplicationInformation()
        {
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext sc)
        {
            oldTotalUploadByte = TotalUploadByte;
            oldTotalDownloadByte = TotalDownloadByte;
            oldTotalUpspan = TotalUpspan;
        }
        [NonSerialized]
        private long currentUploadByte;
        public long CurrentUploadByte
        {
            get { return currentUploadByte; }
            set { currentUploadByte = value; }
        }
        [NonSerialized]
        private long currentDownloadByte;
        public long CurrentDownloadByte
        {
            get { return currentDownloadByte; }
            set { currentDownloadByte = value; }
        }
        private long oldTotalUploadByte;
        public long TotalUploadByte
        {
            get
            {
                return oldTotalUploadByte + CurrentUploadByte;
            }
        }
        private long oldTotalDownloadByte;
        public long TotalDownloadByte
        {
            get
            {
                return oldTotalDownloadByte + CurrentDownloadByte;
            }
        }
        public DateTime LastUptime { get; set; }
        private TimeSpan oldTotalUpspan;
        public TimeSpan TotalUpspan
        {
            get
            {
                return (DateTime.Now - LastUptime) + oldTotalUpspan;
            }
        }
        public int TotalAddedThreadCount { get; set; }
        public int TotalDownloadCompletedThreadCount { get; set; }
        public int TotalSavedImageCount { get; set; }
    }
}
