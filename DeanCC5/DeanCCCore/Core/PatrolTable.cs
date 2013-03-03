using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DeanCCCore.Core._2ch;
using System.Xml.Serialization;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class PatrolTable : GenreFolderCollection, IPatrolTable, ISavable
    {
        public static readonly string SavePath = Path.Combine(Settings.SaveFolder, "PatrolPatterns");
        public static readonly string BackUpSavePath = SavePath + Settings.BackUpSuffix;
        public event EventHandler Changed;
        private const string XmlFileName = "PatrolPatterns.xml";
        public static readonly string ImportXmlFolder = Path.Combine(Settings.SaveFolder, "Import");
        public static readonly string ImportXmlPath = Path.Combine(ImportXmlFolder, XmlFileName);

        public PatrolTable()
        {
        }
        //public event EventHandler<PatrolPatternChangedEventArgs> PatternChanged;
        //public event EventHandler<GenreFolderChangedEventArgs> FolderChanged;

        public void Add(PatrolPattern pattern)
        {
            throw new NotImplementedException();
            //GenreFolder targetFolder = FindParentFolder(pattern);
            //if (targetFolder != null)
            //{
            //    targetFolder.Add(pattern);
            //}
        }

        public void Remove(PatrolPattern pattern)
        {
            throw new NotImplementedException();
            //GenreFolder parent = FindParentFolder(pattern);
            //if (parent != null)
            //{
            //    parent.Remove(pattern);
            //}
        }

        //private GenreFolder FindParentFolder(PatrolPattern child)
        //{
        //    if (child == null)
        //    {
        //        throw new ArgumentNullException("pattern");
        //    }
        //    return this.FirstOrDefault(folder => folder.Equals(child.ParentFolder));
        //}

        public void Contains(PatrolPattern pattern)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 現在のインスタンスをローカルに保存します
        /// </summary>
        public void Save()
        {
            Save(SavePath);
        }
        /// <summary>
        /// 現在のインスタンスをバックアップとして保存します
        /// </summary>
        public void BackUp()
        {
            Save(BackUpSavePath);
        }

        private static PatrolTable Create(string path)
        {
            if (File.Exists(path))
            {
                return (PatrolTable)Deserialize(path);
            }
            else
            {
                return new PatrolTable();
            }
        }

        /// <summary>
        /// Settingsインスタンスを取得します
        /// 保存されているインスタンスが存在する場合は読み込みます
        /// </summary>
        /// <returns>取得したSettingsインスタンス</returns>
        public static PatrolTable Create()
        {
            return Create(SavePath);
        }
        /// <summary>
        /// バックアップPatrolTableインスタンスを取得します
        /// バックアップが存在しない場合は例外はスローされずに既定のインスタンスを取得します
        /// </summary>
        /// <returns>取得したバックアップPatrolTableインスタンス</returns>
        public static PatrolTable CreateBackup()
        {
            return Create(BackUpSavePath);
        }

        private void Save(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(fs, this);
            }
        }

        /// <summary>
        /// オプションの内容をすべてXMLとして保存します
        /// </summary>
        /// <param name="path">保存先のパス</param>
        public void SaveAsXml(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(PatrolTable));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xml.Serialize(fs, this);
            }
        }

        /// <summary>
        /// 既定のパスにあるXMLファイルからインスタンスを取得します        
        /// </summary>
        /// <remarks>XMLファイルがない場合はnullを返します</remarks>
        /// <returns></returns>
        public static PatrolTable Import()
        {
            if (!File.Exists(ImportXmlPath))
            {
                return null;
            }

            PatrolTable importedPatrols = CreateFromXml(ImportXmlPath);

            Directory.Delete(ImportXmlFolder, true);

            return importedPatrols;
        }

        /// <summary>
        /// XMLのパスを指定してインスタンスを取得します
        /// </summary>
        /// <param name="path">XMLのパス</param>
        /// <returns>作成したインスタンス</returns>
        public static PatrolTable CreateFromXml(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(PatrolTable));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return (PatrolTable)xml.Deserialize(fs);
            }
        }


        private static object Deserialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bin = new BinaryFormatter();
                return bin.Deserialize(fs);
            }
        }

        private void OnChanged(EventArgs e)
        {
            Save();
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public void PerformChanged()
        {
            OnChanged(EventArgs.Empty);
        }
    }
}
