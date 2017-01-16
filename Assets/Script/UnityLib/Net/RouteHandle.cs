namespace UnityLib.Net {
    public class RouteHandle : ControllerBehaviour {
        public string Scheme;

        public string LocalGateway;
        public string RemoteGateway;

        public string ServicePort;
        public string ApiPrefix;

        public bool IsClientOnLAN = false;

        protected override bool OnInit() {
            return true;
        }
    }
}