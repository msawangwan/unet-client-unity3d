using UnityEngine;
using UnityFramework.Net.Util;
using System;
using System.Net;

namespace UnityFramework.Net.Client {
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

        public static Action<string> info = msg => Debug.LogFormat("[ClientHandler][INFO][{0}]", msg);

        private void OnEnable() {
            Uri remoteAddr = NetUtil.NewUriFrom(
                clientConfiguration.scheme,
                clientConfiguration.host,
                clientConfiguration.port
            );
            info("host addr: " + remoteAddr.OriginalString);
        }
    }
}
