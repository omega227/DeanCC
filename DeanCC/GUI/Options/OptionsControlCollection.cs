using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    public sealed class OptionsControlCollection : IEnumerable<IOptionsControl>
    {
        public OptionsControlCollection()
        {
        }

        #region オプションリスト
        private InternetOptionsControl internetOptions;
        public InternetOptionsControl InternetOptions
        {
            get
            {
                if (internetOptions == null)
                {
                    internetOptions = new InternetOptionsControl();
                }
                return internetOptions;
            }
        }

        private WindowOptionsControl windowOptions;
        public WindowOptionsControl WindowOptions
        {
            get
            {
                if (windowOptions == null)
                {
                    windowOptions = new WindowOptionsControl();
                }
                return windowOptions;
            }
        }

        private NGOptionsControl ngOptions;
        public NGOptionsControl NGOptions
        {
            get
            {
                if (ngOptions == null)
                {
                    ngOptions = new NGOptionsControl();
                }
                return ngOptions;
            }
        }

        private DatOptionsControl datOptions;
        public DatOptionsControl DatOptions
        {
            get
            {
                if (datOptions == null)
                {
                    datOptions = new DatOptionsControl();
                }
                return datOptions;
            }
        }

        private ThreadViewOptionsControl threadViewOptions;
        public ThreadViewOptionsControl ThreadViewOptions
        {
            get
            {
                if (threadViewOptions == null)
                {
                    threadViewOptions = new ThreadViewOptionsControl();
                }
                return threadViewOptions;
            }
        }

        private BrowserOptionsControl browserOptions;
        public BrowserOptionsControl BrowserOptions
        {
            get
            {
                if (browserOptions == null)
                {
                    browserOptions = new BrowserOptionsControl();
                }
                return browserOptions;
            }
        }

        private IndividualThreadOptionsControl individualThreadOptions;
        public IndividualThreadOptionsControl IndividualThreadOptions
        {
            get
            {
                if (individualThreadOptions == null)
                {
                    individualThreadOptions = new IndividualThreadOptionsControl();
                }
                return individualThreadOptions;
            }
        }

        private ImageSaveOptionsControl imageSaveOptions;
        public ImageSaveOptionsControl ImageSaveOptions
        {
            get
            {
                if (imageSaveOptions == null)
                {
                    imageSaveOptions = new ImageSaveOptionsControl();
                }
                return imageSaveOptions;
            }
        }

        private ZipOptionsControl zipOptions;
        public ZipOptionsControl ZipOptions
        {
            get
            {
                if (zipOptions == null)
                {
                    zipOptions = new ZipOptionsControl();
                }
                return zipOptions;
            }
        }

        private StartupOptionsControl startupOptions;
        public StartupOptionsControl StartupOptions
        {
            get
            {
                if (startupOptions == null)
                {
                    startupOptions = new StartupOptionsControl();
                }
                return startupOptions;
            }
        }

        private MessageOptionsControl messageOptions;
        public MessageOptionsControl MessageOptions
        {
            get
            {
                if (messageOptions == null)
                {
                    messageOptions = new MessageOptionsControl();
                }
                return messageOptions;
            }
        }

        private CommandOptionsControl commandOptions;
        public CommandOptionsControl CommandOptions
        {
            get
            {
                if (commandOptions == null)
                {
                    commandOptions = new CommandOptionsControl();
                }
                return commandOptions;
            }
        }
        #endregion

        //ToDo: オプションを追加したらここに
        // if("オプション" != null)
        //    yield return "オプション";　を追記する
        public IEnumerator<IOptionsControl> GetEnumerator()
        {
            //基本
            if (internetOptions != null)
            {
                yield return internetOptions;
            }
            if (windowOptions != null)
            {
                yield return windowOptions;
            }
            if (ngOptions != null)
            {
                yield return ngOptions;
            }
            if (datOptions != null)
            {
                yield return datOptions;
            }
            if (startupOptions != null)
            {
                yield return startupOptions;
            }
            //外観
            if (threadViewOptions != null)
            {
                yield return threadViewOptions;
            }
            //応用
            if (browserOptions != null)
            {
                yield return browserOptions;
            }
            if (messageOptions != null)
            {
                yield return messageOptions;
            }
            if (commandOptions != null)
            {
                yield return commandOptions;
            }
            if (individualThreadOptions != null)
            {
                yield return individualThreadOptions;
            }
            if (imageSaveOptions != null)
            {
                yield return imageSaveOptions;
            }
            if (zipOptions != null)
            {
                yield return zipOptions;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
