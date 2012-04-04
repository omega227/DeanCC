using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class OptionItems : ISavable
    {
        public static readonly string SavePath = Path.Combine(Settings.SaveFolder, "UserOptions");
        public static readonly string BackUpSavePath = SavePath + Settings.BackUpSuffix;
        [field: NonSerialized]
        public event EventHandler ItemsChanged;

        /// <summary>
        /// 逆シリアル化の直後に発生します。このメソッドはvirtualにできません
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
            if (CommandOptions == null)
            {
                CommandOptions = new CommandOptionsItem();
            }
        }

        public OptionItems()
        {
            internetOptions = new InternetOptionsItem();
            ngOptions = new NGOptionsItem();
            browsersOptions = new BrowsersOptionsItem();
            DatOptions = new DatOptionsItem();
            ThreadViewOptions = new ThreadViewOptionsItem();
            ImageSaveOptions = new ImageSaveOptionsItem();
            StartupOptions = new StartupOptionsItem();
            ZipOptions = new ZipOptionsItem();
            WindowOptions = new WindowOptionsItem();
            IndividualThreadOptions = new IndividualThreadOptionsItem();
            MessageOptions = new MessageOptionsItem();
            CommandOptions = new CommandOptionsItem();
        }

        private InternetOptionsItem internetOptions;
        /// <summary>
        /// 通信オプション
        /// </summary>
        public InternetOptionsItem InternetOptions
        {
            get
            {
                return internetOptions;
            }
        }
        private NGOptionsItem ngOptions;
        /// <summary>
        /// NGオプション
        /// </summary>
        public NGOptionsItem NGOptions
        {
            get
            {
                return ngOptions;
            }
        }
        private BrowsersOptionsItem browsersOptions;
        /// <summary>
        /// ブラウザーオプション
        /// </summary>
        public BrowsersOptionsItem BrowsersOptions
        {
            get
            {
                return browsersOptions;
            }
        }
        /// <summary>
        /// DATオプション
        /// </summary>
        public DatOptionsItem DatOptions { get; set; }
        /// <summary>
        /// スレッドリストオプション
        /// </summary>
        public ThreadViewOptionsItem ThreadViewOptions { get; set; }
        /// <summary>
        /// 画像保存オプション
        /// </summary>
        public ImageSaveOptionsItem ImageSaveOptions { get; set; }
        /// <summary>
        /// スタートアップオプション
        /// </summary>
        public StartupOptionsItem StartupOptions { get; set; }
        /// <summary>
        /// ZIPオプション
        /// </summary>
        public ZipOptionsItem ZipOptions { get; set; }
        /// <summary>
        /// ウィンドウオプション
        /// </summary>
        public WindowOptionsItem WindowOptions { get; set; }
        /// <summary>
        /// 個別追加スレッドオプション
        /// </summary>
        public IndividualThreadOptionsItem IndividualThreadOptions { get; set; }
        /// <summary>
        /// 通知オプション
        /// </summary>
        public MessageOptionsItem MessageOptions { get; set; }
        /// <summary>
        /// コマンドオプション
        /// </summary>        
        public CommandOptionsItem CommandOptions { get; set; }

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

        private static OptionItems Create(string path)
        {
            if (File.Exists(path))
            {
                return (OptionItems)Deserialize(path);
            }
            else
            {
                return new OptionItems();
            }
        }

        /// <summary>
        /// Settingsインスタンスを取得します
        /// 保存されているインスタンスが存在する場合は読み込みます
        /// </summary>
        /// <returns>取得したSettingsインスタンス</returns>
        public static OptionItems Create()
        {
            return Create(SavePath);
        }

        /// <summary>
        /// バックアップOptionItemsインスタンスを取得します
        /// バックアップが存在しない場合は例外はスローされずに既定のインスタンスを取得します
        /// </summary>
        /// <returns>取得したバックアップOptionItemsインスタンス</returns>
        public static OptionItems CreateBackUp()
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
        private static object Deserialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bin = new BinaryFormatter();
                return bin.Deserialize(fs);
            }
        }

        private void OnOptionChanged(EventArgs e)
        {
            Save();
            if (ItemsChanged != null)
            {
                ItemsChanged(this, e);
            }
        }

        /// <summary>
        /// オプション変更イベントを発生させます
        /// </summary>
        public void PerformChanged()
        {
            OnOptionChanged(EventArgs.Empty);
        }
    }
}
