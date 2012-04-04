using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    [Serializable]
    public sealed class IndividualPatrolPattern : PatrolPattern
    {
        public const string IndividualPatrolPatternName = "個別追加スレッド";

        public IndividualPatrolPattern()
        {
        }

        public IndividualPatrolPattern(GenreFolder parentFolder)
        {
            ParentFolder = parentFolder;
            ExtensionFormat = CreateExtensionFormat();
        }

        public override bool Equals(object obj)
        {
            return obj is IndividualPatrolPattern &&
                Name.Equals(((IndividualPatrolPattern)obj).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Initialized
        {
            get
            {
                return true;
            }
        }

        public override BoardInfoCollection TargetBoards
        {
            get
            {
                return null;
            }
            //set
            //{
            //    throw new NotSupportedException("個別追加スレッドの板情報は変更できません");
            //}
        }

        public override string Name
        {
            get
            {
                return IndividualPatrolPatternName;
            }
            //set
            //{
            //    throw new NotSupportedException("個別追加スレッドの名前は変更できません");
            //}
        }

        //public override IThreadHeader[] Patrol()
        //{
        //    throw new NotSupportedException("個別追加スレッドは巡回を実行できません");
        //}
    }
}
