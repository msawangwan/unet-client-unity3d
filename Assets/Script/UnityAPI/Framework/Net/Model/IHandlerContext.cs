using System.Net;

namespace UnityAPI.Framework.Net {
    public interface IHandlerContext {// ADD GENERIC TYPE
        HttpWebRequest Request { get; set; }
        HttpWebResponse Response { get; set; }

        byte[] Payload { get; set; }
        int PayloadSize { get; set; }
    }
}
