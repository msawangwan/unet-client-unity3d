namespace UnityAPI.Model {
    [System.Serializable]
    public class Profile {
        public string Name;
        public bool isAvailable;

        public Profile(string name, bool isAvailable = true) {
            this.Name = name;
            this.isAvailable = isAvailable;
        }
    }
}
