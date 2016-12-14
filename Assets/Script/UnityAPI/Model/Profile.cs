namespace UnityAPI.Model {
    [System.Serializable]
    public class Profile {
        public string Name;
        public bool isLoaded;

        public Profile(string name) {
            this.Name = name;
        }
    }
}
