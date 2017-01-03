using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityLib.Framework.Net;

namespace UnityLib {
    public static class SessionHandleExtension {
        private const int kSceneIndexGameplay = 1;

        public static IEnumerator BeginSession(this SessionHandle sh) {
            SceneManager.LoadSceneAsync(kSceneIndexGameplay, LoadSceneMode.Additive);

            Scene scene = SceneManager.GetSceneAt(kSceneIndexGameplay);

            do {
                Debug.Log("loading session ...");
                yield return new WaitForEndOfFrame();
            } while (!scene.isLoaded);

            Handler<Connection> handler = new Handler<Connection>(
                JsonUtility.ToJson(sh.SessionInstance)
            );

            handler.POST(RouteHandle.Session_EstablishConn);

            Connection conn = null;

            do {
                yield return null;
                if (handler.onDone != null) {
                    conn = handler.onDone();
                    break;
                }
            } while (true);

            if (conn != null) {
                if (!conn.isConnected) { // TODO: handle this case
                    Debug.LogFormat("is not connected");
                } else {
                    Debug.LogFormat("is connected");
                }
            }
            GameHandle gh = GameHandle.New(sh);

            SceneManager.MoveGameObjectToScene(sh.gameObject, SceneManager.GetSceneAt(kSceneIndexGameplay));
            SceneManager.MoveGameObjectToScene(gh.gameObject, SceneManager.GetSceneAt(kSceneIndexGameplay));
        }
    }
}
