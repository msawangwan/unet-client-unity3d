using System;
using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class PollHandleExtension {
        public static IEnumerator WaitForGameStart(this PollHandle ph, int gamekey, string playername, Action onComplete) { // use ID instead of name??
            Handler<JsonStringWithKey> startHandler = new Handler<JsonStringWithKey>(
                new PollHandle.PlayerReadyNotification(gamekey, playername).Marshall()
            );

            ph.GameHandler.Instance.PlayerName = playername;
            ph.GameHandler.Instance.OpponentName = "<waiting for player to join ...>";;

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

            JsonStringWithKey j = startHandler.onDone();

            int hashedgamekey = j.key;
            string opponent = j.value;

            if (onComplete != null) {
                onComplete();
            }

            Debug.LogFormat("[+] poll handler got start signal and ending wait routine now entering setup");

            ph.GameHandler.Instance.OpponentName = opponent;
        }

        public static IEnumerator WaitForOpponentReady(this PollHandle ph) {
            yield return Wait.ForEndOfFrame;

            Handler<JsonEmpty> onchangehandler = new Handler<JsonEmpty>();
        }
    }
}
