
namespace UnityAPI.Framework.Net {
    public abstract class ClientServiceHandler {
        public const string kGET = "GET";
        public const string kPUT = "PUT";
        public const string kPOST = "POST";
        public const string kDEL = "DELETE";

        public static string debug_route = "http://tyrant.systems:80/api/availability";
        public static bool isBlockingOnRequest;
        public static ResourceEndpoint[] Routes;
    }
}
