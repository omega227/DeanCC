using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core.Options;

namespace DeanCC.GUI.Options
{
    public sealed partial class ImageSaveOptionsControl : UserControl , IOptionsControl
    {
        public ImageSaveOptionsControl()
        {
            InitializeComponent();
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.ImageSaveOptions.BlockDownloadedImage = blockImageCheckBox.Checked;
            destination.ImageSaveOptions.Threshold = (int)thresholdNumericUpDown.Value;
            destination.ImageSaveOptions.MaximumRetryCount = (int)retryCountNumericUpDown.Value;
            destination.ImageSaveOptions.RetryImageLifeDate = (int)retryDateNumericUpDown.Value;
            destination.ImageSaveOptions.MovesSaveFolder = movePathCheckBox.Checked;
            destination.ImageSaveOptions.MovedDestinationFolder = moveFolderBrowserControl.SelectedPath;
            destination.ImageSaveOptions.FileNameFormat = fileNameControl.Text;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            blockImageCheckBox.Checked = source.ImageSaveOptions.BlockDownloadedImage;
            thresholdNumericUpDown.Value = source.ImageSaveOptions.Threshold;
            retryCountNumericUpDown.Value = source.ImageSaveOptions.MaximumRetryCount;
            retryDateNumericUpDown.Value = source.ImageSaveOptions.RetryImageLifeDate;
            movePathCheckBox.Checked = source.ImageSaveOptions.MovesSaveFolder;
            moveFolderBrowserControl.SelectedPath = source.ImageSaveOptions.MovedDestinationFolder;
            fileNameControl.Text = source.ImageSaveOptions.FileNameFormat;
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
