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
    public sealed partial class InternetOptionsControl : UserControl, IOptionsControl
    {
        public InternetOptionsControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            encodingComboBox.Items.Add(DeanCCCore.Core.Common.Options.InternetOptions.CurrentEncoding);
        }

        public void Get(DeanCCCore.Core.Options.OptionItems destination)
        {
            destination.InternetOptions.Timeout = (int)timeoutNumericUpDown.Value;
            destination.InternetOptions.CurrentEncoding = (Encoding)encodingComboBox.SelectedItem;
            destination.InternetOptions.BoardTableSourceUrl = BoardURLTextBox.Text;
            Proxy proxy = destination.InternetOptions.Proxy;
            proxy.Enable = useProxyCheckBox.Checked;
            proxy.UseIEProxy = useIECheckBox.Checked;
            proxy.Host = hostTextBox.Text;
            proxy.Port = (int)portNumericUpDown.Value;
            proxy.Credential = credentialCheckBox.Checked;
            proxy.UserName = idTtextBox.Text;
            proxy.Password = passwordTextBox.Text;
        }

        public void Set(DeanCCCore.Core.Options.OptionItems source)
        {
            timeoutNumericUpDown.Value = (decimal)source.InternetOptions.Timeout;
            encodingComboBox.SelectedItem = source.InternetOptions.CurrentEncoding;
            BoardURLTextBox.Text = source.InternetOptions.BoardTableSourceUrl;
            Proxy proxy = source.InternetOptions.Proxy;
            useProxyCheckBox.Checked = proxy.Enable;
            useIECheckBox.Checked = proxy.UseIEProxy;
            hostTextBox.Text = proxy.Host;
            portNumericUpDown.Value = proxy.Port;
            credentialCheckBox.Checked = proxy.Credential;
            idTtextBox.Text = proxy.UserName;
            passwordTextBox.Text = proxy.Password;
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

        private bool encodingComboBoxInitialized;
        private void encodingComboBox_Enter(object sender, EventArgs e)//すべてのエンコーディングを表示
        {
            if (encodingComboBoxInitialized)
            {
                return;
            }

            Encoding selectedEncoding = (Encoding)encodingComboBox.SelectedItem;
            encodingComboBox.Items.Clear();
            foreach (EncodingInfo info in Encoding.GetEncodings())
            {
                encodingComboBox.Items.Add(info.GetEncoding());
            }
            encodingComboBox.SelectedItem = selectedEncoding;
            encodingComboBoxInitialized = true;
        }
    }
}
