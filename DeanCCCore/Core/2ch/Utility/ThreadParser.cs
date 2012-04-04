using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Utility
{
    public static class ThreadParser
    {
        private static Regex subjectPattern =
            new Regex(@"^(?<key>\d+)\.dat<>(?<title>.+) \((?<count>\d+)\)$", RegexOptions.Multiline | RegexOptions.Compiled);

        public static IThreadHeader[] Parse(IBoardInfo source, string subjects)
        {
            List<IThreadHeader> result = new List<IThreadHeader>();
            foreach (Match thread in subjectPattern.Matches(subjects))
            {
                result.Add(new ThreadHeader(
                    source, thread.Groups["key"].Value, System.Web.HttpUtility.HtmlDecode(thread.Groups["title"].Value)));
            }

            return result.ToArray();
        }
    }
}
