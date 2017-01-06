using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class GameHandle : MonoBehaviour {
        public SessionHandle Session { get; private set; }
        public PlayerHandle Player { get; private set; }

        public bool isReadyToLoad {get; private set;}
        public bool isExisting { get;private set; }

        public static GameHandle New(SessionHandle session, bool isExisting) {
            GameHandle gh = new GameObject("game_handle").AddComponent<GameHandle>();
            gh.Session = session;
            gh.isExisting = isExisting;
            return gh;
        }

        public void Load() {
            isReadyToLoad = true;
        }

        private void OnEnable() {
            StartCoroutine(this.Init());
        }

        private void OnDestroy() {
             Debug.LogFormat("game handle destroyed");
        }
    }
}
