using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DeanCCCore.Core.Utility
{
    /// <summary>
    /// Win32API
    /// </summary>
    public static class NativeMethod
    {
        [DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName,
            string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);

        [DllImport("user32")]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32")]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32")]
        public extern static int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int SetWindowTheme(IntPtr hWnd, string appName, string partList);

        public const string APPNAME_EXPLORER = "Explorer";

        public const int WM_DRAWCLIPBOARD = 0x0308;
        public const int WM_CHANGECBCHAIN = 0x030D;

        public const uint ERROR_SUCCESS = 0;
        public const uint INFINITE = 0xffffffff;
        public const uint WAIT_ABANDONED = 0x00000080;
        public const uint WAIT_OBJECT_0 = 0;
        public const uint WAIT_TIMEOUT = 0x00000102;
        public const uint WAIT_FAILED = 0xffffffff;
        public const uint STILL_ACTIVE = 259;
    }
}
