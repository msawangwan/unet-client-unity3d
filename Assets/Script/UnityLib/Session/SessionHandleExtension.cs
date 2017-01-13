using System;
using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class SessionHandleExtension {
        public static IEnumerator VerifyName(this SessionHandle sh, string gameName, Action<bool> onComplete) {
            Debug.LogFormat("-- [+] session handle asking server to verify [gamename:[0]] ...[{1}]", gameName, Time.time);

            Handler<JsonBool> handler = new Handler<JsonBool>(
                JsonUtility.ToJson(new JsonString(gameName))
            );

            handler.POST(SessionHandle.VerifyName.Route);
            
            JsonBool isValidName = null;

            do {
                Debug.LogFormat("-- -- [+] waiting for response ... [{0}]", Time.time);
                yield return null;
                if (handler.hasLoadedResource) {
                    isValidName = new JsonBool(handler.onDone().value);
                    break;
                }
            } while (true);

            Debug.LogFormat("-- [+] got response [gamename available: {1}] ... [{0}]", isValidName.value, Time.time);

            if (onComplete != null) {
                onComplete(isValidName.value);
            }
        }

        public static IEnumerator LoadGameHandler(this SessionHandle sh, GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] session handle is loading the game handler ... [{0}]", Time.time);

            // 1. get the seed for this game
            // - call a gamehandlerext coroutine which takes a bool (host or not) and then spawns the world from seed

            do {
                Debug.LogFormat("-- -- [+] loading (WIP) ... [{0}]", Time.time);
                yield return new WaitForSeconds(5.0f); // null
                if (true) {
                    break;
                }
            } while (true);

            if (onComplete != null) {
                onComplete();
            }

            Debug.LogFormat("-- [+] session handle completed loading game handler ... [{0}]", Time.time);
        }
    }

}
