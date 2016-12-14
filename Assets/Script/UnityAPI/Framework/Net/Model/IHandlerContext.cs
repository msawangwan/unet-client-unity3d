using System.Net;

namespace UnityAPI.Framework.Net {
    public interface IHandlerContext {
        HttpWebRequest Request { get; set; }
        HttpWebResponse Response { get; set; }

        byte[] Payload { get; set; }
        int PayloadSize { get; set; }
    }
}
