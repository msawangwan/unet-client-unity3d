
namespace UnityAPI.Framework.Net {
    public abstract class ClientServiceHandler {
        public const string kGET = "GET";
        public const string kPUT = "PUT";
        public const string kPOST = "POST";
        public const string kDEL = "DELETE";

        public static string debug_route0 = "http://tyrant.systems:80/api/availability";
        public static string debug_route1 = "http://10.0.0.76:80/api/availability";
        public static bool isBlockingOnRequest;
        public static ResourceEndpoint[] Routes;
    }
}
