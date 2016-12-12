using UnityEngine;
using System;
using System.IO;
using System.Net;

namespace UnityAPI.Framework.Net {
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
                req.Method = "GET";

                WebResponse res = req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                res.Close();

                using (var sr = new StreamReader(dataStream)) {
                    string resParsed = sr.ReadToEnd();
                    info(resParsed); // <- debug
                }

                dataStream.Close(); // dont need
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
