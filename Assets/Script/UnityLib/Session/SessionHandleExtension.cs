using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityLib.Net;

namespace UnityLib {
    public static class SessionHandleExtension {
        private const int kSceneIndexGameplay = 1;

        // OK
        public static IEnumerator Register(this SessionHandle sh, string playername) {
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
                sh.SessionPlayerName = playername;
            }

            Handler<JsonEmpty> nameHandler = new Handler<JsonEmpty>(
                JsonUtility.ToJson(new JsonStringWithKey(sh.SessionKey, sh.SessionPlayerName))
            );

            nameHandler.POST(RouteHandle.Session_SetPlayerName);

            do {
                yield return null;
                if (nameHandler.onDone != null) {
                    Debug.LogFormat("-- [+] player name set: {0} ... [{1}]", sh.SessionPlayerName, Time.time);
                    break;
                }
            } while (true);
        }

        // OK
        public static IEnumerator CreateHostSession(this SessionHandle sh, string sessionName){
            bool isHostNameAvailable = false;

            Action<bool> onCheck = (result) => { isHostNameAvailable = result; };

            do { // check for name availability
                yield return sh.CheckGameNameAvailability(sessionName, onCheck);
            } while (!isHostNameAvailable);

            if (isHostNameAvailable) {
                sh.SessionLabelTentative = sessionName;
            } else {
                Debug.Log("-- -- -- [*] error can't use that name");
            }

            float start = Time.time;
            bool wg = false;

            Action onSuccess = () => { wg = true; };

            do { // launch a session as host
                yield return sh.HostGame(onSuccess);

                Debug.LogFormat("-- -- [*] hosting game [{0}] ...", Time.time);

                if (wg) {
                    Debug.LogFormat("-- -- -- [*] success [{0}] ...", Time.time);
                    break;
                }
            } while (true);
            
            Debug.LogFormat("-- -- [*] done (took {1} seconds) [{0}] ...", Time.time, (Time.time - start));
        }

        // OK
        private static IEnumerator CheckGameNameAvailability(this SessionHandle sh, string sessionName, Action<bool> onComplete) {
            bool isAvailable = false;

            Handler<LobbyAvailability> handler = new Handler<LobbyAvailability>(
                JsonUtility.ToJson(new JsonString(sessionName))
            );

            handler.POST(RouteHandle.Session_CheckHostNameAvailable);
            
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

        // OK
        private static IEnumerator HostGame(this SessionHandle sh, Action onSuccess) {
            Debug.LogFormat("-- -- [*] attempting to host new game ... [{0}]", Time.time);

            Handler<Simulation> hostHandler = new Handler<Simulation>(
                JsonUtility.ToJson(new JsonStringWithKey(sh.SessionKey, sh.SessionLabelTentative))
            );

            hostHandler.POST(RouteHandle.Session_HostNewInstance);

            do {
                yield return null;
                Debug.LogFormat("-- -- -- [*] waiting for server response ... [{0}]", Time.time);
                if (hostHandler.isReady) {
                    sh.SimulationInstance = hostHandler.onDone();
                    onSuccess();
                    break;
                }
            } while (true);

            Debug.LogFormat("[+] host session up: [label: {0}] [seed: {1}]", sh.SimulationInstance.label, sh.SimulationInstance.seed);
        }

        // OK
        public static IEnumerator FetchLobbyList(this SessionHandle sh, Action<string[]> onFetch) {
            Handler<Lobby> handler = new Handler<Lobby>();

            handler.GET(RouteHandle.Session_FetchLobbyList);

            do {
                yield return null;
                if (handler.onDone != null) {
                    Debug.LogFormat("-- [+] got lobby list ... [{0}]", Time.time);
                    Lobby lobby = handler.onDone();
                    if (onFetch != null) {
                        onFetch(lobby.listing);
                    }
                    break;
                }
            } while (true);
        }

        // TODO: left off HERE!!
        public static IEnumerator JoinSession(this SessionHandle sh) {
            yield return null;
        }

        // DEPRECATE
        public static IEnumerator BeginSession(this SessionHandle sh, bool isExistingSession) {
            SceneManager.LoadSceneAsync(kSceneIndexGameplay, LoadSceneMode.Additive);

            Scene scene = SceneManager.GetSceneAt(kSceneIndexGameplay);

            do {
                Debug.LogFormat("-- -- [*] loading session ... [{0}]", Time.time);
                yield return new WaitForEndOfFrame();
            } while (!scene.isLoaded);

            // Handler<Connection> connHandler = new Handler<Connection>(
            //     JsonUtility.ToJson(new SessionOwner(sh.SessionInstance.sessionID, sh.OwningPlayerName))
            // );

            // connHandler.POST(RouteHandle.Session_EstablishConn);

            // Connection conn = null;

            // do {
            //     yield return null;
            //     if (connHandler.onDone != null) {
            //         conn = connHandler.onDone();
            //         break;
            //     }
            // } while (true);

            // if (conn != null) {
            //     if (!conn.isConnected) { // TODO: handle this case
            //         Debug.LogFormat("----- [*] failed to connect: {0}", sh.SessionInstance.sessionID);
            //     } else {
            //         Debug.LogFormat("----- [*] connected: {0}", sh.SessionInstance.sessionID);
            //     }
            // }

            GameHandle gh = GameHandle.New(sh, isExistingSession);

            SceneManager.MoveGameObjectToScene(sh.gameObject, SceneManager.GetSceneAt(kSceneIndexGameplay));
            SceneManager.MoveGameObjectToScene(gh.gameObject, SceneManager.GetSceneAt(kSceneIndexGameplay));

            gh.Load();
        }
    }
}
