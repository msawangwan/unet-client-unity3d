using UnityEngine;
using UnityAPI.Net.Util;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityAPI.Net.Client {
    public class ClientHandler : MonoBehaviour {
        [Serializable]
        public class Configuration {
            public string scheme;
            public string host;
            public string path;
            public int port;

            public string query;
            public string fragment;
        }

        [SerializeField] private Configuration clientConfiguration = null;

        public static Action<string> info = msg => Debug.LogFormat(
            "[ClientHandler][INFO][{0}]",
            msg
        );

        private void GET(Uri uri) { // todo: make asynchronous
            info("new GET req to remote addr: " + uri.OriginalString);

            try {
                WebRequest req = WebRequest.Create(uri);
                WebResponse res = req.GetResponse();

                req.Method = "GET";

                Stream dataStream = res.GetResponseStream();
                res.Close();

                using (var sr = new StreamReader(dataStream)) {
                    string resParsed = sr.ReadToEnd();
                    info(resParsed); // <- debug
                }

                dataStream.Close();
            } catch (WebException we) {
                info(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                info(NetUtil.PrintfException(e));
            }
        }

        private void OnEnable() {
            Uri remoteAddr = NetUtil.NewUriFrom(
                clientConfiguration.scheme,
                clientConfiguration.host,
                clientConfiguration.port
            );
        }
    }
}
