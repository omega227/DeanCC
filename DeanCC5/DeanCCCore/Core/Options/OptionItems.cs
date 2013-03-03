using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class OptionItems : ISavable
    {
        public static readonly string SavePath = Path.Combine(Settings.SaveFolder, "UserOptions");
        public static readonly string BackUpSavePath = SavePath + Settings.BackUpSuffix;
        [field: NonSerialized]
        public event EventHandler ItemsChanged;
        private const string XmlFileName = "UserOptions.xml";
        public static readonly string ImportXmlFolder = Path.Combine(Settings.SaveFolder, "Import");
        public static readonly string ImportXmlPath = Path.Combine(ImportXmlFolder, XmlFileName);

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
        [XmlIgnore]
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

        /// <summary>
        /// オプションの内容をすべてXMLとして保存します
        /// </summary>
        /// <param name="path">保存先のパス</param>
        public void SaveAsXml(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(OptionItems));
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
        public static OptionItems Import()
        {
            if (!File.Exists(ImportXmlPath))
            {
                return null;
            }

            OptionItems importedOptions = CreateFromXml(ImportXmlPath);
            OptionItems beforOptions = Create();
            importedOptions.IndividualThreadOptions = beforOptions.IndividualThreadOptions;//XmlIgnoreなプロパティを設定

            Directory.Delete(ImportXmlFolder, true);

            return importedOptions;
        }

        /// <summary>
        /// XMLのパスを指定してインスタンスを取得します
        /// </summary>
        /// <param name="path">XMLのパス</param>
        /// <returns>作成したインスタンス</returns>
        public static OptionItems CreateFromXml(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(OptionItems));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return (OptionItems)xml.Deserialize(fs);
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
