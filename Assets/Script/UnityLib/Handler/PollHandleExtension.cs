using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class PollHandleExtension {
        public static IEnumerator WaitForGameStart(this PollHandle ph, int gamekey, Action onComplete) {
            Handler<JsonInt> startHandler = new Handler<JsonInt>(
                new JsonInt(gamekey).Marshall()
            );

            startHandler.POST(PollHandle.PollForGameStart.Route);

            yield return new WaitUntil( // TODO: maybe use WaitForSeconds instead
                () => {
                    Debug.LogFormat("-- -- [+] waiting for game start ... ");

                    if (startHandler.hasLoadedResource) {
                        return true;
                    }

                    return false;
                }
            );

            ph.GameStartKey = startHandler.onDone().value;

            Debug.LogFormat("[+] finished poll start");
        }

        public static IEnumerator WaitForTurnStart(this PollHandle ph, int gamekey, Action onComplete) {
            yield return new WaitUntil(
                () => {
                    Debug.LogFormat("-- -- [+] waiting for next turn ... ");
                    return true;
                }
            );
        }
    }
}
