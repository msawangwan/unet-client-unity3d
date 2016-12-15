using System;

namespace UnityAPI.Game {
    [System.Serializable]
    public class Profile {
        public string Name;
        public string UUID;

        public DateTime DateCreated;
        public DateTime TimeLastSaved;

        public bool isLoaded { get; set; }

        public Profile(string name) {
            this.Name = name;
        }
    }
}
