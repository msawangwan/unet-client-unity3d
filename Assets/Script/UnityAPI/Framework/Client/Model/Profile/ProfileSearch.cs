namespace UnityAPI.Framework.Client {
    [System.Serializable]
    public class ProfileSearch {
        public string Name;
        public bool IsAvailable = false;

        public ProfileSearch() {}

        public ProfileSearch(string name) {
            this.Name = name;
        }

        public ProfileSearch(string name, bool isAvailable) {
            this.Name = name;
            this.IsAvailable = isAvailable;
        }
    }
}
