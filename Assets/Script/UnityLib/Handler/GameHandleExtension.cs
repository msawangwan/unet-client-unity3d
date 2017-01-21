using System;
using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class GameHandleExtension {
        public static IEnumerator SendHostGameParameters(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] new game world parameters sent (host) ... [{0}]", Time.time);

            Handler<JsonEmpty> hostWorldHandler = new Handler<JsonEmpty>(
                new JsonStringWithKey(gh.GameKey, gh.GameName).Marshall()
            );

            hostWorldHandler.POST(GameHandle.LoadGameWorld.Route);

            do {
                Debug.LogFormat("-- -- [+] sending parameters ... [{0}]", Time.time);
                yield return null;
            } while (!gh.isReadyToLoad);

            if (onComplete != null) {
                onComplete(); // chained to join
            }

            Debug.LogFormat("-- [+]  game world parameters verifed (host) ... [{0}]", Time.time);
        }

        public static IEnumerator SendClientGameParameters(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] new game world parameters sent (client) ... [{0}]", Time.time);

            Handler<JsonEmpty> clientWorldhandler = new Handler<JsonEmpty>(
                new JsonStringWithKey(gh.GameKey, gh.GameName).Marshall()
            );

            do {
                Debug.LogFormat("-- -- [+] sending parameters ... [{0}]", Time.time);
                yield return null;
            } while (!gh.isReadyToLoad);

            if (onComplete != null) {
                onComplete(); // chained to join
            }

            Debug.LogFormat("-- [+] game world parameters verifed (client) ... [{0}]", Time.time);
        }

        public static IEnumerator Join(this GameHandle gh, string playername, Action onComplete) {
            GameHandle.WorldParameters worldparameters = null;

            Handler<GameHandle.WorldParameters> joinHandler = new Handler<GameHandle.WorldParameters>(
                new GameHandle.JoinRequest(gh.GameKey, playername, gh.isHost).Marshall()
            );

            joinHandler.POST(GameHandle.JoinGameWorld.Route); // requests all game params from the server

            do {
                Debug.LogFormat("-- -- [+] joining ... [{0}]", Time.time);
                yield return null;
                if (joinHandler.hasLoadedResource) {
                    worldparameters = joinHandler.onDone();
                    Debug.LogFormat("-- [+] server sent game world parameters {0}", worldparameters.ToString());
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

            gh.LoadWorldHandle(worldparameters);

            Debug.LogFormat("-- [+] joined game successfully");
        }
    }
}
