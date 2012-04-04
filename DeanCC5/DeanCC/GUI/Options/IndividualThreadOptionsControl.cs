using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeanCCCore.Core._2ch;

namespace DeanCC.GUI.Options
{
    public sealed partial class IndividualThreadOptionsControl : UserControl, IOptionsControl
    {
        public IndividualThreadOptionsControl()
        {
            InitializeComponent();
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            IndividualPatrolPattern pattern = destination.IndividualThreadOptions.PatrolPattern;            
            pattern.ParentFolder.LocalPath = saveFolderBrowserControl.SelectedPath;
            pattern.SubFolderFormat = subFolderFormatControl.Text;
            pattern.EnableJpg = jpgCheckBox.Checked;
            pattern.EnablePng = pngCheckBox.Checked;
            pattern.EnableGif = gifCheckBox.Checked;
            pattern.EnableBmp = bmpCheckBox.Checked;
            pattern.EnableZip = zipCheckBox.Checked;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            IndividualPatrolPattern pattern = source.IndividualThreadOptions.PatrolPattern;
            saveFolderBrowserControl.SelectedPath = pattern.ParentFolder.LocalPath;
            subFolderFormatControl.Text = pattern.SubFolderFormat;
            nameTextBox.Text = pattern.Name;
            jpgCheckBox.Checked = pattern.EnableJpg;
            pngCheckBox.Checked = pattern.EnablePng;
            gifCheckBox.Checked = pattern.EnableGif;
            bmpCheckBox.Checked = pattern.EnableBmp;
            zipCheckBox.Checked = pattern.EnableZip;
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
