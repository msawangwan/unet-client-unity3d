using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class GameHandle : MonoBehaviour {
        public static Resource LoadGameWorld = new Resource("game/world/load");
        public static Resource JoinGameWorld = new Resource("game/world/join");

        public string GameName { get; private set; }
        public int GameKey { get; set; }
        public long GameSeed { get; set; }
        public bool isHost { get; private set; }
        public bool isReadyToLoad { get; set; }

        public static GameHandle New(string gameName, bool isHost) {
            GameHandle gh = new GameObject(string.Format("game_handle_[{0}]", gameName)).AddComponent<GameHandle>();
            gh.GameName = gameName;
            gh.isHost = isHost;
            return gh;
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
