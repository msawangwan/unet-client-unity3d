namespace UnityLib.Framework.Net {
    [System.Serializable]
    public class ResourceEndpoint {
        public string Location;

        public ResourceEndpoint() {}

        public ResourceEndpoint(string location) {
            this.Location = location;
        }
    }
}
