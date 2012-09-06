using System;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    public sealed partial class BrowserOptionsControl : UserControl, IOptionsControl
    {
        public BrowserOptionsControl()
        {
            InitializeComponent();
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.BrowsersOptions.WebBroserOptions.WebBrowserPath = WebBrowserOpenFileControl.SelectedPath;
            destination.BrowsersOptions.JaneOptions.JanePath = janeOpenFileControl.SelectedPath;
            destination.BrowsersOptions.JaneOptions.ImageSaveMode = imageAndCacheRadioButton.Checked ? ImageSaveMode.BothImageandCache :
                cacheRadioButton.Checked ? ImageSaveMode.OnlyCache :
                ImageSaveMode.OnlyImage;
            destination.BrowsersOptions.JaneOptions.EnableImageViewURLReplacedatOption = enableImageViewOptionCheckBox.Checked;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            WebBrowserOpenFileControl.SelectedPath = source.BrowsersOptions.WebBroserOptions.WebBrowserPath;
            DefaultWebBrowserCheckBox.Checked = source.BrowsersOptions.WebBroserOptions.UsesDefaultWebBrowser;
            janeOpenFileControl.SelectedPath = source.BrowsersOptions.JaneOptions.JanePath;
            switch (source.BrowsersOptions.JaneOptions.ImageSaveMode)
            {
                case ImageSaveMode.OnlyImage:
                default:
                    imageRadioButton.Checked = true;
                    break;

                case ImageSaveMode.OnlyCache:
                    cacheRadioButton.Checked = true;
                    break;

                case ImageSaveMode.BothImageandCache:
                    imageAndCacheRadioButton.Checked = true;
                    break;
            }
            enableImageViewOptionCheckBox.Checked = source.BrowsersOptions.JaneOptions.EnableImageViewURLReplacedatOption;
            loaded = true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        private bool loaded;
        public bool Loaded
        {
            get { return loaded; }
        }
    }
}
