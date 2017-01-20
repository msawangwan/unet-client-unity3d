using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class PollHandle : MonoBehaviour {
        public class PlayerReadyNotification : IJSONer {
            public int gameID;
            public string playerName;

            public PlayerReadyNotification() {}
            public PlayerReadyNotification(int gameID, string playerName) { this.gameID = gameID; this.playerName = playerName; }
            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        public static readonly Resource PollForGameStart = new Resource("poll/start");
        public static readonly Resource PollForGameUpdate = new Resource("poll/update");

        public static float PollInterval {
            get {
                return 0.75f;
            }
        }

        public int GameStartKey { get; set; }

        public static PollHandle New(string playername = "") {
            PollHandle ph = new GameObject(string.Format("poll_handle_[{0}]", playername)).AddComponent<PollHandle>();
            return ph;
        }

        private void OnEnable() {
            Debug.LogWarningFormat("[+] {0} callback: OnEnable ... [{1}]", gameObject.name, Time.time);
        }

        private void OnDisable() {
            Debug.LogWarningFormat("[+] {0} callback: OnDisable ... [{1}]", gameObject.name, Time.time);
        }

        private void OnDestroy() {
            Debug.LogWarningFormat("[+] {0} callback: OnDestroy ... [{1}]", gameObject.name, Time.time);
        }
        
    }
}
