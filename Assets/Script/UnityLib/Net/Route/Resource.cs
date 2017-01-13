namespace UnityLib.Net {
    public class Resource {
        private static readonly RouteHandle routeHandler = null;

        // TODO: build these in the RouteHandle class
        private static readonly string lGateway   = "http://10.0.0.76";
        private static readonly string rGateway   = "http://tyrant.systems";
        private static readonly string port       = "80";
        private static readonly string apiPrefix  = "api";

        private static readonly string lServerAddr = string.Format(
            "{0}:{1}/{2}", lGateway, port, apiPrefix
        );

        private static readonly string rServerAddr = string.Format(
            "{0}:{1}/{2}", rGateway, port, apiPrefix
        );
        
        private static  bool isLocal {
            get {
                return routeHandler.IsClientOnLAN;
            }
        }

        static Resource() {
            routeHandler = Globals.S.RouteHandle as RouteHandle;
            if (!routeHandler) {
                UnityEngine.Debug.LogErrorFormat("-- [--] missing route handler");
            }
        }

        public string Route {
            get {
                if (isLocal) {
                    return string.Format("{0}/{1}", lServerAddr, route);
                } else {
                    return string.Format("{0}/{1}", rServerAddr, route);
                }
            }
         }

        public string Path {
            get {
                return route;
            }
        }

        private readonly string route;

        public Resource(string route) {
            this.route = route;
        }

        public override string ToString() {
            return string.Format(
                "-- [+] sent sever request [path: {0}][route: {1}]", Path, route
            );
        }
    }
}
