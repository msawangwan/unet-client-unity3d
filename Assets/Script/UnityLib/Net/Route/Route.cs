namespace UnityLib {
    public class Route {
        private static readonly string lGateway    = "http://10.0.0.76";
        private static readonly string rGateway    = "http://tyrant.systems";
        private static readonly string port        = "80";
        private static readonly string apiPrefix   = "api";
        private static readonly bool useLocalAddr  = false;

        private static readonly string lServerAddr = string.Format(
            "{0}:{1}/{2}", lGateway, port, apiPrefix
        );

        private static readonly string rServerAddr = string.Format(
            "{0}:{1}/{2}", rGateway, port, apiPrefix
        );

        static Route() {
            useLocalAddr = false; // TODO: read from config
        }

        public string Resource {
            get {
                if (useLocalAddr) {
                    return string.Format("{0}/{1}", lServerAddr, resource);
                } else {
                    return string.Format("{0}/{1}", rServerAddr, resource);
                }
            }
         }

        public string Raw {
            get {
                return resource;
            }
        }

        private readonly string resource;

        public Route(string resource) {
            this.resource = resource;
        }

        public override string ToString() {
            return string.Format(
                "-- [+] sent sever request [raw: {0}][resource: {1}]", Raw, Resource
            );
        }
    }
}
