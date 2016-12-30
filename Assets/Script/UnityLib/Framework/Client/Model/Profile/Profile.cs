using System;

namespace UnityLib.Framework.Client {
    [System.Serializable]
    public class Profile {
        public string Name;
        public string UUID;

        public long Seed;

        public DateTime DateCreated;
        public DateTime TimeLastSaved;

        public bool isLoaded { get; set; }

        public Profile(string name) {
            this.Name = name;
        }
    }
}
