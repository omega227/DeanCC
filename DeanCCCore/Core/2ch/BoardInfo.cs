using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace DeanCCCore.Core._2ch
{
    [Serializable]
    public sealed class BoardInfo : IBoardInfo, IComparable
    {
        public BoardInfo()
        {
        }

        public BoardInfo(string server, string path, string name)
        {
            this.server = server;
            this.path = path;
            this.name = RemoveTag(name);
        }

        /// <summary>
        /// 空の板情報を表します
        /// </summary>
        public static readonly BoardInfo Empty = new BoardInfo();

        /// <summary>
        /// ハッシュ関数
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        /// <summary>
        /// 現在のインスタンスとentryを比較
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is IBoardInfo && ((IBoardInfo)obj).DomainPath.Equals(this.DomainPath);
        }

        /// <summary>
        /// 現在のインスタンスを文字列形式に変換
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// 現在のインスタンスとobjを比較
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            BoardInfo b = obj as BoardInfo;
            if (b == null)
                return 1;

            return String.Compare(Url, b.Url);
        }

        private static Regex searchTagRegex = new Regex("</?[^>]*/?>", RegexOptions.Compiled);
        public static string RemoveTag(string html)
        {
            if (html == null)
            {
                return string.Empty;
            }
            return searchTagRegex.Replace(html, "");
        }

        /// <summary>
        ///ドメイン  2ch.net/news4vip
        /// </summary>
        public string DomainPath
        {
            get
            {
                if (string.IsNullOrEmpty(server))
                {
                    return string.Empty;
                }
                int index = this.server.IndexOf('.');
                if (this.server.IndexOf('.', index + 1) >= 0)
                {
                    return (this.server.Substring(index + 1) + "/" + this.path);
                }
                return (this.server + "/" + this.path);
            }
        }

        private string name;
        /// <summary>
        /// 板名 ニュー速VIP
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name");
                }
                this.name = RemoveTag(value);
            }
        }

        private string path = string.Empty;
        /// <summary>
        /// http:~/Path/ news4vip
        /// </summary>
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Path");
                }
                this.path = value;
            }
        }

        private string server;
        /// <summary>
        /// http://Server/~ raicho.2ch.net
        /// </summary>
        public string Server
        {
            get
            {
                return this.server;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Server");
                }
                if (server != value)
                {
                    this.server = value;
                    //OnServerChanged();
                }
            }
        }

        /// <summary>
        /// 鯖名 raicho
        /// </summary>
        public string ServerName
        {
            get
            {
                if (string.IsNullOrEmpty(server))
                {
                    return string.Empty;
                }
                int index = this.server.IndexOf('.');
                return this.server.Substring(0, index);
            }
        }

        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(server))
                {
                    return string.Empty;
                }
                return ("http://" + this.server + "/" + this.path + "/");
            }
        }

        public string SubjectUrl
        {
            get
            {
                if (string.IsNullOrEmpty(server))
                {
                    return string.Empty;
                }
                return ("http://" + this.server + "/" + this.path + "/subject.txt");
            }
        }

        /// <summary>
        /// このインスタンスが表す板のSubject.txtをサーバーから取得します
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            if (string.IsNullOrEmpty(server))
            {
                throw new InvalidOperationException("空の板情報か、特定の板を参照していません。");
            }

            HttpWebRequest request = InternetClient.Create(SubjectUrl, DateTime.MinValue);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.ContentLength == 0 ||//-1は板移転ではない
                    response.StatusCode == HttpStatusCode.Found)
                {
                    Common.CurrentSettings.Boards.FollowMovedBoard();
                    return Read();
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream,
                        Encoding.GetEncoding("shift_jis")))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            return string.Empty;
        }

        public void ChangeBoard(IBoardInfo newBoard)
        {
            Server = newBoard.Server;
        }
    }
}
