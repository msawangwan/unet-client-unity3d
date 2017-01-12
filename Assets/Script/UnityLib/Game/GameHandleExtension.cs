using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class GameHandleExtension {
        public static IEnumerator Init(this GameHandle gh) {
            do {
                yield return null;
                if (gh.isReadyToLoad) {
                    break;
                }
            } while (true);

            if (gh.isExisting) {
                // gh.StartCoroutine(gh.LoadAsExisting());
            } else {
                // gh.StartCoroutine(gh.LoadAsNew());
            }

        }

        public static IEnumerator GetFrame(this GameHandle gh) {
            // do {
            //     yield return null;
            //     if (gh.Session.SessionInstance == null) {
            //         continue;
            //     } else {
            //         break;
            //     }
            // } while (true);

            Handler<Frame> handler = new Handler<Frame>(
                // JsonUtility.ToJson(gh.Session.SessionInstance.sessionID)
            );

            // handler.POST(RouteHandle.Game_FetchFrameUpdate);

            do {
                yield return null;
                if (handler.onDone != null) {
                    Frame first = handler.onDone();
                    break;
                }
            } while (true);

            Debug.LogFormat("--- [+] got frame");
        }

    }
}
