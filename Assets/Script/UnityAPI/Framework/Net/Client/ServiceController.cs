using UnityEngine;
using System;
using System.IO;
using System.Net;

namespace UnityAPI.Framework.Net {
    public class ServiceController : ControllerBehaviour {
        [SerializeField] private ClientHandler.Configuration clientConfiguration;

        private Uri remoteAddr = null;

        public bool ValidateNewProfile(string profileAsJson) {
            try {
                Uri uri = BuildRoute("/ProfileSearch");
                // Debug.LogFormat("requesting... {0}", uri.OriginalString);
                HttpWebRequest req = WebRequest.Create("http://10.0.0.76:8000/ProfileSearch") as HttpWebRequest;

                req.ContentType = "application/json; charset=utf-8";
                req.Method = "POST";

                using (var sw = new StreamWriter(req.GetRequestStream())) {
                    sw.Write(profileAsJson);
                }

                // WebResponse res = req.GetResponse();
                // res.Close

                // req.Method = "GET";

                // Stream dataStream = res.GetResponseStream();
                // res.Close();

                // using (var sr = new StreamReader(dataStream)) {
                //     string resParsed = sr.ReadToEnd();
                //     print(resParsed); // <- debug
                // }

                // dataStream.Close();
                return true;
            } catch (WebException we) {
                print(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                print(NetUtil.PrintfException(e));
            }
            return false;
        }

        private Uri BuildRoute(string resource) {
            return NetUtil.NewUriFrom(
                clientConfiguration.scheme,
                clientConfiguration.host + resource,
                clientConfiguration.port
            );
        }

        protected override bool OnInit() {
            if (remoteAddr == null) {
                remoteAddr = NetUtil.NewUriFrom(
                    clientConfiguration.scheme,
                    clientConfiguration.host,
                    clientConfiguration.port
                );
            }
            return true;
        }
    }
}