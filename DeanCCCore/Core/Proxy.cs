using System;
using System.Net;

namespace DeanCCCore.Core
{
    [Serializable]
    public sealed class Proxy
    {
        public Proxy()
        {
            Port = 80;
        }

        public bool Enable
        {
            get;
            set;
        }
        public Uri Adress
        {
            get
            {
                if (string.IsNullOrEmpty(Host))
                {
                    return null;
                }
                UriBuilder uriBuilder = new UriBuilder(Uri.UriSchemeHttp, Host, Port);
                return uriBuilder.Uri;
            }
        }
        public string Host
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public bool Credential
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public bool UseIEProxy
        {
            get;
            set;
        }

        /// <summary>
        /// このインスタンスが表すプロキシを取得します
        /// </summary>
        /// <returns></returns>
        public IWebProxy GetProxy()
        {
            if (!Enable)
            {
                throw new InvalidOperationException("有効なプロキシではありません");
            }
            if (UseIEProxy)
            {
                return WebRequest.GetSystemWebProxy();
            }

            IWebProxy proxy = null;
            if (Adress != null && Adress.IsAbsoluteUri)
            {
                proxy = new WebProxy(Adress);
                if (Credential)
                {
                    proxy.Credentials = new NetworkCredential(UserName, Password);
                }
            }

            return proxy;
        }
    }
}
