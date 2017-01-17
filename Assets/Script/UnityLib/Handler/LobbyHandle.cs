using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class LobbyHandle : MonoBehaviour {
        public class GameList : IJSONer {
            public string[] listing;
            public GameList() {}
            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        public static Resource FetchLobbyList = new Resource("session/handle/lobby/list");

        public static LobbyHandle New() {
            LobbyHandle lh = new GameObject("lobby_handle").AddComponent<LobbyHandle>();
            return lh;
        }
    }
}
