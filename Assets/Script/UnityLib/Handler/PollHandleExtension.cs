using System;
using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class PollHandleExtension {
        public static IEnumerator WaitForGameStart(this PollHandle ph, int gamekey, string playername, Action<int, string> onComplete) { // use ID instead of name??
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
                onComplete(hashedgamekey, opponent); // don't actually need  this me thinks
            }

            Debug.LogFormat("[+] poll handler got start signal and terminated start routine");

            ph.GameHandler.Instance.OpponentName = opponent;
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
