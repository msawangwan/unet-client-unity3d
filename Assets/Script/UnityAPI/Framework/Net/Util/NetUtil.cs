using UnityEngine;
using System;
using System.Net;

namespace UnityAPI.Framework.Net {
    public static class NetUtil {
        public static string UrlCombinePath(string url1, string url2) {
            if (url1.Length == 0) {
                return url2;
            }
            if (url2.Length == 0) {
                return url1;
            }

            string urla = url1.TrimEnd('/', '\\');
            string urlb = url2.TrimStart('/', '\\');

            return string.Format("{0}/{1}", urla, urlb);
        }

        public static Uri NewUriFrom(string scheme, string host, int port) {
            return new UriBuilder(scheme, host, port).Uri;
        }

        public static string PrintfWebException(WebException we) {
            string err = "client caught web exception: [" + we.ToString() + "]";

            if (we.Status == WebExceptionStatus.ProtocolError) {
                err += "[" + ((int)((HttpWebResponse)we.Response).StatusCode).ToString() + " " + ((HttpWebResponse)we.Response).StatusCode + "]";
            }

            return err;
        }

        public static string PrintfException(Exception e) {
            return "client caught exception: [" + e.ToString() + "]";
        }
    }
}
