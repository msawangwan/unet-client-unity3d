using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityLib.Net;

namespace UnityLib {
    public static class SessionHandleExtension {
        private static IEnumerator VerifyName(this SessionHandle sh, string sessionName, Action<bool> onComplete) {
            bool isAvailable = false;

            Handler<LobbyAvailability> handler = new Handler<LobbyAvailability>(
                JsonUtility.ToJson(new JsonString(sessionName))
            );

            handler.POST(SessionHandle.VerifyName.Route);

            do {
                yield return null;
                if (handler.onDone != null) {
                    LobbyAvailability result = handler.onDone();
                    isAvailable = result.isAvailable;
                    break;
                }
            } while (true);

            onComplete(isAvailable);
        }

    }
}
