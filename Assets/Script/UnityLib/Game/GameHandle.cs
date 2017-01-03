using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Framework.Net;

namespace UnityLib {
    public class GameHandle : MonoBehaviour {
        public SessionHandle CurrentSession { get; private set; }
        public bool isLoaded { get; private set; }

        public static GameHandle New(SessionHandle session) {
            GameHandle gh = new GameObject("game_handle").AddComponent<GameHandle>();
            gh.CurrentSession = session;
            return gh;
        }

        private IEnumerator OnLoad() {
            do {
                yield return null;
                if (CurrentSession.SessionInstance == null) {
                    continue;
                } else {
                    break;
                }
            } while (true);

            // get the first frame
            Handler<Frame> handler = new Handler<Frame>(
                JsonUtility.ToJson(CurrentSession.SessionInstance.sessionID)
            );

            handler.POST(RouteHandle.Game_FetchFrameUpdate);

            do {
                yield return null;
                if (handler.onDone != null) {
                    Frame first = handler.onDone();
                    break;
                }
            } while (true);

            Debug.LogFormat("got first frame");
        }

        private void OnEnable() {
            // StartCoroutine(OnLoad());
            StartCoroutine(this.BeginUpdate());
        }
    }
}
