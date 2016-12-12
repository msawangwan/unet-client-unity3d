namespace UnityAPI.Model {
    [System.Serializable]
    public class ProfileSearch {
        public string Name;
        public bool isAvailable;

        public ProfileSearch(string name, bool isAvailable = true) {
            this.Name = name;
            this.isAvailable = isAvailable;
        }
    }
}
