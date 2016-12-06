﻿using UnityEngine;
using System;
using System.Net;

namespace UnityFramework.Net.Util {
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
    }
}
