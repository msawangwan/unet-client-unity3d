using System;
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
            } else {
                sh.SessionKey = skey;
            }
        }

        public static IEnumerator LoadAsHost(this SessionHandle sh) {
            Debug.LogFormat("-- -- [*] load game as host ... [{0}]", Time.time);

            float start = Time.time;
            bool wg = false;

            Action onSuccess = () => { wg = true; };

            do {
                yield return sh.HostGame(onSuccess);

                Debug.LogFormat("-- -- [*] hosting game [{0}] ...", Time.time);

                if (wg) {
                    Debug.LogFormat("-- -- -- [*] success [{0}] ...", Time.time);
                    break;
                }
            } while (true);
            
            Debug.LogFormat("-- -- [*] loaded game as host (took {1} seconds) [{0}] ...", Time.time, (Time.time - start));
        }

        public static IEnumerator HostGame(this SessionHandle sh, Action onSuccess) {
            Debug.LogFormat("-- -- [*] attempting to host new game ... [{0}]", Time.time);

            Handler<Instance> hostHandler = new Handler<Instance>(
                JsonUtility.ToJson(new JsonInt(sh.SessionKey))
            );

            hostHandler.POST(RouteHandle.Session_HostNewInstance);

            do {
                yield return null;
                Debug.LogFormat("-- -- -- [*] waiting for server response ... [{0}]", Time.time);
                if (hostHandler.isReady) {
                    hostHandler.onDone();
                    onSuccess();
                    break;
                }
            } while (true);
        }

        public static IEnumerator BeginSession(this SessionHandle sh, bool isExistingSession) {
            SceneManager.LoadSceneAsync(kSceneIndexGameplay, LoadSceneMode.Additive);

            Scene scene = SceneManager.GetSceneAt(kSceneIndexGameplay);

            do {
                Debug.LogFormat("-- -- [*] loading session ... [{0}]", Time.time);
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
