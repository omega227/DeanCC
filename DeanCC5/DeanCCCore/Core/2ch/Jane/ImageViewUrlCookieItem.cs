using System.Net;

namespace DeanCCCore.Core._2ch.Jane
{
    public sealed class ImageViewUrlCookieItem : ImageViewUrlItem
    {
        public ImageViewUrlCookieItem(string key, string replacement, string referer, string optionReferer)
            : base(key, replacement, referer)
        {
            this.OptionReferer = optionReferer;
        }

        public override bool WithOption
        {
            get
            {
                return true;
            }
        }
        public string OptionReferer { get; private set; }

        public override ImageViewURLReplaceItem Replace(string url)
        {
            ImageViewURLReplaceItem result = base.Replace(url);
            string referer = string.IsNullOrEmpty(OptionReferer) ? result.Referer : Pattern.Replace(url, OptionReferer);
            result.Cookie = GetCookie(referer);

            return result;
        }

        private CookieContainer GetCookie(string referer)
        {
            CookieContainer cookieContainer = new CookieContainer();
            using (HttpWebResponse res = InternetClient.GetResponse(referer))
            {
                cookieContainer.Add(res.Cookies);
            }
            return cookieContainer;
        }
    }
}
