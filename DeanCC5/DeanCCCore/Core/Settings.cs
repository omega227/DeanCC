using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using DeanCCCore.Core._2ch;

namespace DeanCCCore.Core
{
    /// <summary>
    /// 各種設定を保持します
    /// </summary>
    [Serializable]
    public sealed class Settings : ISavable
    {
        //[field : NonSerializable]
        //public event EventHandler CurrentSettingsChanged;
        /// <summary>
        /// 保存フォルダー
        /// </summary>
        public static readonly string SaveFolder = Application.StartupPath;
        /// <summary>
        /// ファイルの保存場所
        /// </summary>
        public static readonly string SavePath = Path.Combine(SaveFolder, "ApplicationSettings");
        /// <summary>
        /// バックアップの保存場所
        /// </summary>
        public static readonly string BackUpSavePath = SavePath + BackUpSuffix;

        public const string BackUpSuffix = ".backup";
        private static readonly string CrushMarkerPath = Path.Combine(SaveFolder, "Crush");
        public static object SyncRoot = new object();

        public Settings()
        {
            formStatuses = new FormStatus();
            FirstRunning = true;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
            FirstRunning = false;
        }

        /// <summary>
        /// 初回起動かどうかを示す値
        /// </summary>
        public bool FirstRunning
        {
            get;
            private set;
        }

        private ThreadCollection allThreads;
        /// <summary>
        /// 取得したすべてのスレッド
        /// </summary>
        public ThreadCollection AllThreads
        {
            get
            {
                if (allThreads == null)
                {
                    allThreads = new ThreadCollection();
                }
                return allThreads;
            }
            set
            {
                allThreads = value;
            }
        }

        private BoardTable boards;
        /// <summary>
        /// 取得した板一覧
        /// </summary>
        public BoardTable Boards
        {
            get
            {
                if (boards == null)
                {
                    boards = new BoardTable();
                    Common.Logs.AddBoardsUpdateEvent();
                }
                //if (!boards.UpdateCompleted)
                //{
                //    boards.OnlineUpdate();
                //}
                return boards;
            }
            set
            {
                boards = value;
            }
        }

        /// <summary>
        /// 正常に保存されていないかどうかを示す値
        /// </summary>
        public bool Crushed { get; private set; }

        private FormStatus formStatuses;
        /// <summary>
        /// フォームの状態を保持します
        /// </summary>
        public FormStatus FormStatuses
        {
            get
            {
                return formStatuses;
            }
        }

        private ApplicationInformation information;
        /// <summary>
        /// アプリケーション情報を提供します
        /// </summary>
        public ApplicationInformation Information
        {
            get
            {
                if (information == null)
                {
                    information = new ApplicationInformation();
                }
                return information;
            }
        }

        private void Save(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(fs, this);
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

        /// <summary>
        /// アプリケーション終了時にSaveメソッドの実行が完了していない場合にCrushedがTrueを示すようにします
        /// </summary>
        public void MarkCrush()
        {
            Crushed = File.Exists(CrushMarkerPath);
            //空ファイルを作成
            using (FileStream fs = new FileStream(CrushMarkerPath, FileMode.Create))
            {
            }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Settingsインスタンスを取得します
        /// 保存されているインスタンスが存在する場合は読み込みます
        /// </summary>
        /// <returns>取得したSettingsインスタンス</returns>
        public static Settings Create()
        {
            if (File.Exists(SavePath))
            {
                return (Settings)Deserialize(SavePath);
            }
            else
            {
                return new Settings();
            }
        }
        /// <summary>
        /// バックアップSettingsインスタンスを取得します
        /// バックアップが存在しない場合は例外はスローされずに既定のインスタンスを取得します
        /// </summary>
        /// <returns>取得したバックアップSettingsインスタンス</returns>
        public static Settings CreateBackUp()
        {
            return (Settings)Deserialize(BackUpSavePath);
        }

        /// <summary>
        /// 現在のインスタンスをローカルに保存します
        /// </summary>
        public void Save()
        {
            Save(SavePath);
            OnSaved();
        }

        private void OnSaved()
        {
            File.Delete(CrushMarkerPath);
        }
        /// <summary>
        /// 現在のインスタンスをバックアップとして保存します
        /// </summary>
        public void BackUp()
        {
            Save(BackUpSavePath);
        }
    }
}
