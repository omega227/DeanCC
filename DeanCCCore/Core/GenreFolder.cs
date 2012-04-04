using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class GenreFolder : PatrolPatternCollection , IGenreFolder
    {
        public GenreFolder()
        {
        }

        public GenreFolder(string savePath)
        {
            savePath = savePath.TrimEnd(Path.DirectorySeparatorChar);
            if (Path.GetFileName(savePath) == string.Empty)
            {
                throw new ArgumentException("ルートディレクトリは保存フォルダーに設定できません");
            }
            localPath = savePath;
        }

        private string localPath;
        public string LocalPath
        {
            get
            {
                return localPath;
            }
            set
            {
                localPath = value;
            }
        }

        public string Name
        {
            get
            {
                return Path.GetFileName(localPath);
            }
        }
    }
}
