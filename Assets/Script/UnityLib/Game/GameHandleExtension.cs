using System.Collections;
using UnityEngine;
using UnityLib.Framework.Net;

namespace UnityLib {
    public static class GameHandleExtension {
        public static IEnumerator BeginUpdate(this GameHandle gh) {
            do {
                yield return null;
                if (gh.CurrentSession.SessionInstance == null) {
                    continue;
                } else {
                    break;
                }
            } while (true);

            Handler<Frame> handler = new Handler<Frame>(
                JsonUtility.ToJson(gh.CurrentSession.SKey)
            );

            handler.POST(RouteHandle.Game_StartUpdate);

            do {
                yield return null;
                if (handler.onDone != null) {
                    handler.onDone();
                    break;
                }
            } while (true);

            Debug.LogFormat("game update loop is now running!");
        }

        public static void EndGameSession(this GameHandle gb) {
            
        }
    }
}
