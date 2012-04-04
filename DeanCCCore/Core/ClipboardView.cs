using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DeanCCCore.Core.Utility;
using System.Diagnostics;

namespace DeanCCCore.Core
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public sealed class ClipboardView : NativeWindow
    {
        private IntPtr nextHandle;
        public event EventHandler<ClipboardEventArgs> TextChanged;
        private int previousTextHash = Clipboard.GetText().GetHashCode();
        private readonly Form mainForm;

        public ClipboardView(Form mainForm)
        {
            this.mainForm = mainForm;
            mainForm.HandleCreated += new EventHandler(mainForm_HandleCreated);
            mainForm.HandleDestroyed += new EventHandler(mainForm_HandleDestroyed);
            if (mainForm.Handle != IntPtr.Zero)
            {
                AssignHandle(mainForm.Handle);
                nextHandle = NativeMethod.SetClipboardViewer(Handle);
            }
        }

        void mainForm_HandleDestroyed(object sender, EventArgs e)
        {
            NativeMethod.ChangeClipboardChain(Handle, nextHandle);
            ReleaseHandle();
        }

        void mainForm_HandleCreated(object sender, EventArgs e)
        {
            AssignHandle(mainForm.Handle);
            nextHandle = NativeMethod.SetClipboardViewer(Handle);
        }

        private void OnTextChanged(ClipboardEventArgs e)
        {
            if (Enable)
            {
                if (TextChanged != null)
                {
                    TextChanged(this, e);
                }
            }
        }

        public bool Enable { get; set; }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                case NativeMethod.WM_DRAWCLIPBOARD:
                    if (Clipboard.ContainsText())
                    {
                        string text = Clipboard.GetText();
                        if (!text.GetHashCode().Equals(previousTextHash))
                        {
                            OnTextChanged(new ClipboardEventArgs(text));
                        }
                    }
                    if (nextHandle != IntPtr.Zero)
                    {
                        NativeMethod.SendMessage(nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    }
                    break;

                case NativeMethod.WM_CHANGECBCHAIN:
                    if (msg.WParam == nextHandle)
                    {
                        nextHandle = (IntPtr)msg.LParam;
                    }
                    else if (nextHandle != IntPtr.Zero)
                    {
                        NativeMethod.SendMessage(nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    }
                    break;

                default:
                    base.WndProc(ref msg);
                    break;
            }
        }
    }

    public sealed class ClipboardEventArgs : EventArgs
    {
        public ClipboardEventArgs(string text)
        {
            Text = text;
        }
        public string Text { get; set; }
    }
}
