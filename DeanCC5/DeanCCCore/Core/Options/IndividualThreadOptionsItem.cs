using System;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class IndividualThreadOptionsItem
    {
        public IndividualThreadOptionsItem()
        {
            PatrolPattern = new IndividualPatrolPattern(new GenreFolder(Settings.SaveFolder));
        }
        /// <summary>
        /// 個別追加スレッドの巡回パターンを表します
        /// すべての個別追加スレッドでこの巡回パターンが使用されます
        /// </summary>
        public IndividualPatrolPattern PatrolPattern { get; set; }
    }
}
