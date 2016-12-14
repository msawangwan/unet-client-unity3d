using UnityAPI.Framework.Net;

namespace UnityAPI.Model {
    [System.Serializable]
    public class ProfileSearch {
        public string Name;
        public bool isAvailable;

        public ProfileSearch(string name, bool isAvailable = true) {
            this.Name = name;
            this.isAvailable = isAvailable;
        }

        public static System.Func<bool> DoesProfileNameExist(string profileName) {
            System.Func<bool> r = null;
            if (profileName == string.Empty) {
                return null;
            }

            ServiceController service = Global.Globals.S.serviceController as ServiceController;
            ProfileSearch newProfile = new ProfileSearch(profileName);
            string json = UnityEngine.JsonUtility.ToJson(newProfile);

            // System.Func<string> thing = null;
            // ClientServiceHandler.POSTasync(json, thing);
            // if (thing != null) {
            //     string result = thing();
            // }
            // return service.ValidateProfileNameAvailable(json);
            return r;
        }
    }
}
