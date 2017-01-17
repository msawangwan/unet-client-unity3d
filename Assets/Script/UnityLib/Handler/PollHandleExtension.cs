using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class PollHandleExtension {
        public static IEnumerator CheckGameStart(this PollHandle ph, int gamekey, Action onComplete) {
            WaitForSeconds ws = new WaitForSeconds(PollHandle.PollInterval);

            Handler<JsonInt> startHandler = new Handler<JsonInt>(
                new JsonInt(gamekey).Marshall()
            );

            startHandler.POST(PollHandle.PollForGameStart.Route);

            do {
                yield return ws;
                Debug.LogFormat("[+] polling for game start ...[{0}]", Time.time);
                if (startHandler.hasLoadedResource) {
                    JsonInt n = startHandler.onDone();
                }
            } while (true);
        }
    }
}
