using System;
using System.IO;
using System.Net;

namespace UnityAPI.Framework.Net {
    public class ConnectionState {
        public const int BufferSize = 1024;

        public HttpWebRequest Request { get; set; }
        public HttpWebResponse Response { get; set; }
        public Stream RWStream { get; set; }

        public Func<string> onComplete;

        public int PayloadSize { get; set; }
        public byte[] Payload { get; set; }
    }
}
