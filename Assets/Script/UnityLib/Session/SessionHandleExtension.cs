using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityLib.Net;

namespace UnityLib {
    public static class SessionHandleExtension {
        private const int kSceneIndexGameplay = 1;

        public static IEnumerator Register(this SessionHandle sh) {
            int skey = -1;

            Handler<JsonInt> registerHandler = new Handler<JsonInt>();
            registerHandler.GET(RouteHandle.Session_RegisterSession);

            do {
                yield return null;
                if (registerHandler.onDone != null) {
                    JsonInt jint = registerHandler.onDone();
                    skey = jint.value;
                    break;
                }
            } while (true);

            Debug.LogFormat("-- [+] new session registered with key: {0}", skey);

            if (skey == -1) {
                Debug.LogErrorFormat("[+] key cannot be negative ({0})", skey);
            }
        }

        public static IEnumerator BeginSession(this SessionHandle sh, bool isExistingSession) {
            SceneManager.LoadSceneAsync(kSceneIndexGameplay, LoadSceneMode.Additive);

            Scene scene = SceneManager.GetSceneAt(kSceneIndexGameplay);

            do {
                Debug.Log("----- [*] loading session ...");
                yield return new WaitForEndOfFrame();
            } while (!scene.isLoaded);

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
