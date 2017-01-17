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

        public static IEnumerator StartHostSession(this SessionHandle sh, GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] session handle starting host session [gamename: {1}] ... [{0}]", Time.time, gh.GameName);
            int gameKey = -1;

            Handler<JsonInt> hostSessionHandler = new Handler<JsonInt>(
                new JsonString(gh.GameName).Marshall()
            );

            hostSessionHandler.POST(SessionHandle.SpawnGameHandlerAsHost.Route);

            do {
                Debug.LogFormat("-- -- [+] init game handler ... [{0}]", Time.time);
                yield return null;
                if (hostSessionHandler.hasLoadedResource) {
                    gameKey = hostSessionHandler.onDone().value;
                    break;
                }
            } while (true);

            gh.GameKey = gameKey;
            gh.isReadyToLoad = true;

            if (onComplete != null) {
                onComplete(); // join
            }

            Debug.LogFormat("-- [+] session handle host session game handler init [gamhandler key: {1}] ... [{0}]", Time.time, gameKey);
        }

        public static IEnumerator StartClientSession(this SessionHandle sh, GameHandle gh, Action onCompelte) {
            Debug.LogFormat("-- [+] session handle starting client session [gamename: {1}] ... [{0}]", Time.time, gh.GameName);

            int gameKey = -1;

            Handler<JsonInt> clientSessionHandler = new Handler<JsonInt>(
                new JsonString(gh.GameName).Marshall()
            );

            clientSessionHandler.POST(SessionHandle.SpawnGameHandlerAsClient.Route);

            do {
                Debug.LogFormat("-- -- [+] init client game handler ... [{0}]", Time.time);
                yield return null;
                if (clientSessionHandler.hasLoadedResource) {
                    gameKey = clientSessionHandler.onDone().value;
                    break;
                }
            } while (true);

            gh.GameKey = gameKey;
            gh.isReadyToLoad = true;

            if (onCompelte != null) {
                onCompelte(); // join
            }

            Debug.LogFormat("-- [+] session client session loaded [gamename: {1}] [gameid: {2}] ... [{0}]", Time.time, gh.GameName, gh.GameKey);
        }
    }
}
