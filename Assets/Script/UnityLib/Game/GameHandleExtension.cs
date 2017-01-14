using System;
using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class GameHandleExtension {
        public static IEnumerator LoadAsNew(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] new game is loading ... [{0}]", Time.time);
            WaitForEndOfFrame wf = new WaitForEndOfFrame();

            Handler<JsonInt> gamekeyHandler = new Handler<JsonInt>(
                new JsonString("").Marshall()
            );

            do {
                Debug.LogFormat("-- -- [+] loading ... [{0}]", Time.time);
                yield return wf;
            } while (!gh.isReadyToLoad);

            Debug.LogFormat("-- [+] loaded game, players can now join ... [{0}]", Time.time);
        }
        // public static IEnumerator Init(this GameHandle gh) {
        //     do {
        //         yield return null;
        //         if (gh.isReadyToLoad) {
        //             break;
        //         }
        //     } while (true);

        //     if (gh.isHost) {
        //         // gh.StartCoroutine(gh.LoadAsExisting());
        //     } else {
        //         // gh.StartCoroutine(gh.LoadAsNew());
        //     }

        // }

        // public static IEnumerator GetFrame(this GameHandle gh) {
        //     // do {
        //     //     yield return null;
        //     //     if (gh.Session.SessionInstance == null) {
        //     //         continue;
        //     //     } else {
        //     //         break;
        //     //     }
        //     // } while (true);

        //     Handler<Frame> handler = new Handler<Frame>(
        //         // JsonUtility.ToJson(gh.Session.SessionInstance.sessionID)
        //     );

        //     // handler.POST(RouteHandle.Game_FetchFrameUpdate);

        //     do {
        //         yield return null;
        //         if (handler.onDone != null) {
        //             Frame first = handler.onDone();
        //             break;
        //         }
        //     } while (true);

        //     Debug.LogFormat("--- [+] got frame");
        // }

    }
}
