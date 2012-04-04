using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core._2ch;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class Command
    {
        public Command(string name, string value, CommandMode commandMode)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name に有効な文字を入力してください。");
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value に有効な文字を入力してください。");
            }
            Name = name;
            Value = value;
            CommandMode = commandMode;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public CommandMode CommandMode { get; set; }

        /// <summary>
        /// このインスタンスに関連付けられているコマンドを実行します
        /// </summary>
        /// <param name="thread"></param>
        public void Execute(Thread thread)
        {
            string formatedValue = thread.Header.Format(Value).Trim();
            System.Diagnostics.ProcessStartInfo command = DeanCCCore.Core.Utility.ProcessUtility.CreateProcessStartInfo(formatedValue);
            System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        System.Diagnostics.Process.Start(command);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Common.Logs.Add("コマンド実行エラー", Name + ":" + ex.Message, LogStatus.Error);
                        MessageBox.Show(command.FileName + "\n" + ex.Message, "コマンド実行失敗",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (System.IO.IOException ex)
                    {
                        Common.Logs.Add("コマンド実行エラー", Name + ":" + ex.Message, LogStatus.Error);
                        MessageBox.Show(command.FileName + "\n" + ex.Message, "コマンド実行失敗",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (System.ComponentModel.Win32Exception ex)
                    {
                        Common.Logs.Add("コマンド実行エラー", Name + ":" + ex.Message, LogStatus.Error);
                        MessageBox.Show(command.FileName + "\n" + ex.Message, "コマンド実行失敗",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
        }
    }

    /// <summary>
    /// コマンドの種類を指定します
    /// </summary>
    public enum CommandMode
    {
        None = -1,
        ThreadListMenu = 0,
        DownloadCompleted = 1
    }

    public static class CommandModeString
    {
        public static string GetText(CommandMode commandMode)
        {
            switch (commandMode)
            {
                case CommandMode.ThreadListMenu:
                    return "右クリックメニュー";
                case CommandMode.DownloadCompleted:
                    return "ダウンロード完了後に実行";
                case CommandMode.None:
                default:
                    return string.Empty;
            }
        }
    }

    [Serializable]
    public sealed class CommandCollection : System.Collections.ObjectModel.Collection<Command>
    {
    }
}
