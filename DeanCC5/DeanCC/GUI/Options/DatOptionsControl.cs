using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    public sealed partial class DatOptionsControl : UserControl, IOptionsControl
    {
        public DatOptionsControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.DatOptions.LogSaveMode = datAndHtmlRadioButton.Checked ? LogSaveModes.BothDatAndHtml :
                datRadioButton.Checked ? LogSaveModes.DatOnly : LogSaveModes.None;
            destination.DatOptions.SavesSameImagesFolder = SavesSameImagePathCheckBox.Checked;
            destination.DatOptions.DefaultSaveFolder = savePathFolderBrowserControl.SelectedPath;
            destination.DatOptions.FileNameFormat = fileNameControl.Text;
            destination.DatOptions.DatAccessRate = datLoadSpeedRateControl1.Value;
            destination.DatOptions.GetatableBg20Server = getBg20CheckBox.Checked;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            switch (source.DatOptions.LogSaveMode)
            {
                default:
                case LogSaveModes.None:
                    noSaveDatRadioButton.Checked = true;
                    break;
                case LogSaveModes.DatOnly:
                    datRadioButton.Checked = true;
                    break;
                case LogSaveModes.BothDatAndHtml:
                    datAndHtmlRadioButton.Checked = true;
                    break;
            }
            SavesSameImagePathCheckBox.Checked = source.DatOptions.SavesSameImagesFolder;
            savePathFolderBrowserControl.SelectedPath = source.DatOptions.DefaultSaveFolder;
            fileNameControl.Text = source.DatOptions.FileNameFormat;
            datLoadSpeedRateControl1.Value = source.DatOptions.DatAccessRate;
            getBg20CheckBox.Checked = source.DatOptions.GetatableBg20Server;
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
