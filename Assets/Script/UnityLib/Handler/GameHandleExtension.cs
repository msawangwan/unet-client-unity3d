using System;
using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class GameHandleExtension {
        public static IEnumerator LoadWorldAsHost(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] new game world is loading (host) ... [{0}]", Time.time);

            Handler<JsonEmpty> hostWorldHandler = new Handler<JsonEmpty>(
                new JsonStringWithKey(gh.GameKey, gh.GameName).Marshall()
            );

            hostWorldHandler.POST(GameHandle.LoadGameWorld.Route);

            do {
                Debug.LogFormat("-- -- [+] loading ... [{0}]", Time.time);
                yield return null;
            } while (!gh.isReadyToLoad);

            if (onComplete != null) {
                onComplete();
            }

            Debug.LogFormat("-- [+] loaded game world as host... [{0}]", Time.time);
        }

        public static IEnumerator LoadWorldAsClient(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] new game world is loading (client) ... [{0}]", Time.time);

            Handler<JsonEmpty> clientWorldhandler = new Handler<JsonEmpty>(
                new JsonStringWithKey(gh.GameKey, gh.GameName).Marshall()
            );

            do {
                Debug.LogFormat("-- -- [+] loading ... [{0}]", Time.time);
                yield return null;
            } while (!gh.isReadyToLoad);

            if (onComplete != null) {
                onComplete();
            }

            Debug.LogFormat("-- [+] loaded game world as client ... [{0}]", Time.time);
        }

        public static IEnumerator Join(this GameHandle gh, string playername, Action onComplete) {
            Handler<JsonLong> joinHandler = new Handler<JsonLong>(
                new GameHandle.JoinRequest(gh.GameKey, playername, gh.isHost).Marshall()
            );

            joinHandler.POST(GameHandle.JoinGameWorld.Route); // get the world seed

            do {
                yield return null;
                if (joinHandler.hasLoadedResource) {
                    JsonLong seed = joinHandler.onDone();
                    // her e we send event 
                    Debug.LogFormat("GOT THE SEEEED {0}", seed.value);
                    break;
                }
            } while (true);

            if (gh.isHost) {
                Debug.LogFormat("-- -- [+] joined as host");
            } else {
                Debug.LogFormat("-- -- [+] joined as client");
            }

            if (onComplete != null) {
                onComplete();
            }
        }
    }
}
