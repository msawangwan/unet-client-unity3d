using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityLib.Net;

namespace UnityLib {
    public static class SessionHandleExtension {
        private const int kSceneIndexGameplay = 1;

        // public static IEnumerator KeyFromGameName(this SessionHandle sh) {
        //     Handler<Key> keyHandler = new Handler<Key>(
        //         JsonUtility.ToJson(sh.SessionInstance)
        //     );

        //     do {
        //         yield return null;
        //     } while (true);
        // }

        // public static IEnumerator IncreasePlayerCount(this SessionHandle sh) {
        //     sh.SessionInstance.playerCount++;
        //     yield return null;
        // }

        public static IEnumerator BeginSession(this SessionHandle sh, bool isExistingSession) {
            SceneManager.LoadSceneAsync(kSceneIndexGameplay, LoadSceneMode.Additive);

            Scene scene = SceneManager.GetSceneAt(kSceneIndexGameplay);

            do {
                Debug.Log("----- [*] loading session ...");
                yield return new WaitForEndOfFrame();
            } while (!scene.isLoaded);

            // Handler<Connection> handler = new Handler<Connection>(
            //     JsonUtility.ToJson(sh.SessionInstance)
            // );

            Handler<Connection> connHandler = new Handler<Connection>(
                JsonUtility.ToJson(new SessionOwner(sh.SessionInstance.sessionID, sh.OwningPlayerName))
            );

            connHandler.POST(RouteHandle.Session_EstablishConn);

            Connection conn = null;

            do {
                yield return null;
                if (connHandler.onDone != null) {
                    conn = connHandler.onDone();
                    break;
                }
            } while (true);

            if (conn != null) {
                if (!conn.isConnected) { // TODO: handle this case
                    Debug.LogFormat("----- [*] failed to connect: {0}", sh.SessionInstance.sessionID);
                } else {
                    Debug.LogFormat("----- [*] connected: {0}", sh.SessionInstance.sessionID);
                }
            }

            GameHandle gh = GameHandle.New(sh, isExistingSession);

            SceneManager.MoveGameObjectToScene(sh.gameObject, SceneManager.GetSceneAt(kSceneIndexGameplay));
            SceneManager.MoveGameObjectToScene(gh.gameObject, SceneManager.GetSceneAt(kSceneIndexGameplay));

            gh.Load();
        }
    }
}
