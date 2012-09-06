using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DeanCCCore.Core._2ch.Jane
{
    public sealed class ImageViewURLReplaceItem
    {
        public static ImageViewURLReplaceItem Empty = new ImageViewURLReplaceItem();

        public ImageViewURLReplaceItem()
        {
        }

        public ImageViewURLReplaceItem(string url)
        {
            ReplacedUrl = url;
            Referer = "";
            Cookie = null;
        }

        public ImageViewURLReplaceItem(string replacedUrl, string referer, CookieContainer cookie)
        {
            ReplacedUrl = replacedUrl;
            Referer = referer;
            Cookie = cookie;
        }
        public string ReplacedUrl { get; set; }
        public string Referer { get; set; }
        public CookieContainer Cookie { get; set; }
    }
}
