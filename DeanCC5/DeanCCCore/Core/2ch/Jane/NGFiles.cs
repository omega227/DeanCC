using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace DeanCCCore.Core._2ch.Jane
{
    public sealed class NGFiles : INGFiles
    {
        public NGFiles(string path)
        {
            this.path = path;
        }

        private bool loaded;
        public bool Loaded { get { return loaded; } }

        private string path;
        public string Path
        {
            get
            {
                return path;
            }
        }

        private List<NGFilesItem> items = new List<NGFilesItem>();

        private void Load(string path)
        {
            if (loaded)
            {
                throw new InvalidOperationException("This file is already loaded");
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("path");
            }

            lock (((System.Collections.ICollection)items).SyncRoot)
            {
                this.path = path;

                string text = string.Empty;
                using (StreamReader sr = new StreamReader(path, Common.Options.InternetOptions.CurrentEncoding))
                {
                    text = sr.ReadToEnd();
                }

                List<NGFilesItem> list = new List<NGFilesItem>();
                foreach (Match matchHash in Regex.Matches(text, @"^[0-9A-V]{26}", RegexOptions.Multiline))
                {
                    NGFilesItem item = new NGFilesItem(string.Empty, matchHash.Value);
                    list.Add(item);
                }

                items.Clear();
                items.AddRange(list);
            }

            OnLoaded();
        }

        private void OnLoaded()
        {
            loaded = true;
            Common.Logs.Add("NGFiles.txt読み込み完了", string.Format("{0:N0}パターン", items.Count), LogStatus.System);
        }

        public bool Exists(byte[] data)
        {
            // ハッシュ値を計算
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string md5Hash = BitConverter.ToString(md5.ComputeHash(data)).Replace("-", "").ToLower();

            return Exists(md5Hash);
        }

        public bool Exists(string md5)
        {
            string janeHash = JaneHash.ComputeJaneHash(md5);
            return ExistsJaneHash(janeHash);
        }

        private bool ExistsJaneHash(string janeHash)
        {
            return items.Exists(item => item.JaneHash.Equals(janeHash));
        }

        public void Load()
        {
            Load(path);
        }

        public void Reload()
        {
            loaded = false;
            Load(path);
        }

        public sealed class NGFilesItem
        {
            public NGFilesItem(string md5, string janeHash)
            {
                MD5 = md5;
                JaneHash = janeHash;
            }

            public string MD5 { get; set; }
            public string JaneHash { get; set; }
        }

        public static class JaneHash
        {
            private static readonly char[] hashChars = new char[]
        {
           '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
           'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
           'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
           'U', 'V'
        };

            private const int ngHashLength = 26;
            private const int md5CharsLength = 16;

            private static string Encode(char[] md5)
            {
                char[] ngHash = new char[ngHashLength];
                for (int i = 0; i < ngHashLength; ++i)
                {
                    ngHash[i] = hashChars[((ComputeHashCharIndex(md5[5 * i / 8], md5[5 * i / 8 + 1])) >> (5 * i % 8)) & 0x1F];
                }
                return new string(ngHash);
            }

            private static char[] ReadMD5(string md5)
            {
                char[] convertedChars = new char[md5CharsLength + 1];
                for (int i = 0; i < md5CharsLength; ++i)
                {
                    convertedChars[i] = Convert.ToChar(Convert.ToInt64(md5.Substring(i * 2, 2), 16));
                }
                convertedChars[md5CharsLength] = '\0';
                return convertedChars;
            }

            private static int ComputeHashCharIndex(char lowWord, char highWord)
            {
                return lowWord + (highWord << 8);
            }

            /// <summary>
            /// MD5からJaneハッシュを算出
            /// </summary>
            public static string ComputeJaneHash(string md5)
            {
                char[] convertedChars = ReadMD5(md5);
                return Encode(convertedChars);
            }
        }
    }
}
