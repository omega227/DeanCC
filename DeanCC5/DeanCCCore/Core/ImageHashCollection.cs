using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class ImageHashCollection : Collection<ImageHash>, ISavable
    {
        /// <summary>
        /// 逆シリアル化の直後に発生します。このメソッドはvirtualにできません
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
            Common.Logs.Add("ダウンロード済み画像ハッシュ読み込み完了",
                string.Format("{0:N0}パターン", Count), LogStatus.System);
        }

        private const int md5HashLength = 32;
        public static readonly string SavePath = Path.Combine(Settings.SaveFolder, "ImageHashes");
        private static readonly string BackUpPath = SavePath + Settings.BackUpSuffix;
        public static readonly object SyncRoot = new object();

        protected override void InsertItem(int index, ImageHash item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.MD5Hash.Length != md5HashLength)
            {
                throw new ArgumentException("MD5ハッシュ以外の値が入力されました。");
            }
            if (Contains(item))
            {
                return;
                //throw new ArgumentException("追加しようとしたハッシュは既に存在しています");
            }
            base.InsertItem(index, item);
        }

        public int RemoveAll(Predicate<ImageHash> match)
        {
            return ((List<ImageHash>)this.Items).RemoveAll(match);
        }

        public static ImageHashCollection CreateImageHashes()
        {
            if (File.Exists(SavePath))
            {
                return CreateImageHashes(SavePath);
            }
            else
            {
                return new ImageHashCollection();
            }
        }

        static ImageHashCollection CreateImageHashes(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bin = new BinaryFormatter();
                return (ImageHashCollection)bin.Deserialize(fs);
            }
        }

        /// <summary>
        /// バックアップImageHashCollectionインスタンスを取得します
        /// バックアップが存在しない場合は例外はスローされずに既定のインスタンスを取得します
        /// </summary>
        /// <returns>取得したバックアップImageHashCollectionインスタンス</returns>
        public static ImageHashCollection CreateBackUp()
        {
            return CreateImageHashes(BackUpPath);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 内容を既定の場所に保存します
        /// </summary>
        public void Save()
        {
            Save(SavePath);
        }

        void Save(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(fs, this);
            }
        }

        /// <summary>
        /// 内容を既定の場所にバックアップします
        /// </summary>
        public void BackUp()
        {
            Save(BackUpPath);
        }
    }
}
