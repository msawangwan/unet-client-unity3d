using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class LobbyHandleExtension {
        public static IEnumerator FetchGameList(this LobbyHandle lh, Action<string[]> onComplete) {
            Debug.LogFormat("-- [+] client requesting gamelist ...[{0}]", Time.time);
            
            Handler<LobbyHandle.GameList> gameListHandler = new Handler<LobbyHandle.GameList>();
            gameListHandler.GET(LobbyHandle.FetchLobbyList.Route);

            LobbyHandle.GameList gameList = null;

            do {
                Debug.LogFormat("-- -- [+] fetching ...[{0}]", Time.time);
                yield return null;
                if (gameListHandler.hasLoadedResource) {
                    gameList = gameListHandler.onDone();
                    break;
                }
            } while (true);

            if (onComplete != null) {
                onComplete(gameList.listing);
            }

            Debug.LogFormat("-- [+] got lobby list ...[{0}]", Time.time);
        }
    }
}
