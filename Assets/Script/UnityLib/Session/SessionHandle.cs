using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class SessionHandle : MonoBehaviour {
        public Queue<Action<string[]>> executeOnListFetched = new Queue<Action<string[]>>();
        public Queue<Action<bool>> sessionNameAvailabilityCheck = new Queue<Action<bool>>();

        public Queue<Action> StageOne = new Queue<Action>();
        public Queue<Action> StageOneError = new Queue<Action>();

        public Instance SessionInstance { get; private set; }
        public Key SKey { get; private set; }
        public int SessionKey { get; set; }

        public string OwningPlayerName { get; private set; } // deprecate

        public int ID { get; set; }
        public string Owner { get; private set; } 

        // DEPRECATE THIS
        public IEnumerator Create(string gamename) {
            Handler<Instance> createHandler = new Handler<Instance>(
                JsonUtility.ToJson(new Key(gamename))
            );

            createHandler.POST(RouteHandle.Session_CreateNew);

            do {
                yield return null;
                if (createHandler.onDone != null) {
                    SessionInstance = createHandler.onDone();
                    break;
                }
            } while (true);

            SessionInstance.playerCount++;
            Handler<Key> confirmHandler = new Handler<Key>(
                JsonUtility.ToJson(SessionInstance)
            );

            confirmHandler.POST(RouteHandle.Session_MakeActive);

            do {
                yield return null;
                if (confirmHandler.onDone != null) {
                    SKey = confirmHandler.onDone();
                    Debug.LogFormat("[+] session key: [bare] {0} [db] {1}", SKey.bareFormat, SKey.redisFormat);
                    break;
                }
            } while (true);

            StartCoroutine(this.BeginSession(false));

            Debug.LogFormat("- [+] created a session: {0} {1}", SessionInstance.sessionID, SessionInstance.seed);
        }

        public IEnumerator Join(string gamename) {
            Handler<Instance> joinHandler = new Handler<Instance>(
                JsonUtility.ToJson(new Key(gamename))
            );

            joinHandler.POST(RouteHandle.Session_JoinNew);

            do {
                yield return null;
                if (joinHandler.onDone != null) {
                    SessionInstance = joinHandler.onDone();
                    break;
                }
            } while (true);

            Handler<Key> keyHandler = new Handler<Key>(
                JsonUtility.ToJson(SessionInstance)
            );

            keyHandler.POST(RouteHandle.Session_KeyFromInstance);

            do {
                yield return null;
                if (keyHandler.onDone != null) {
                    SKey = keyHandler.onDone();
                    Debug.LogFormat("[+] session key: [bare] {0} [db] {1}", SKey.bareFormat, SKey.redisFormat);
                    break;
                }
            } while (true);

            StartCoroutine(this.BeginSession(true));

            Debug.LogFormat("- [+] joined a session: {0} {1}", SessionInstance.sessionID, SessionInstance.seed);
        }

        public IEnumerator FetchSessionList() {
            Handler<Lobby> handler = new Handler<Lobby>();

            handler.GET(RouteHandle.Session_ActiveList);

            do {
                yield return null;
                if (handler.onDone != null) {
                    Lobby lobby = handler.onDone();
                    while (executeOnListFetched.Count > 0) {
                        Action<string[]> c = executeOnListFetched.Dequeue();
                        c(lobby.listing);
                    }
                    break;
                }
            } while (true);
        }

        public IEnumerator CheckSessionAvailability(string sessionName) {
            string json = JsonUtility.ToJson(new Key(sessionName));
            Handler<LobbyAvailability> handler = new Handler<LobbyAvailability>(json);
            handler.POST(RouteHandle.Session_Available);

            do {
                yield return null;
                if (handler.onDone != null) {
                    LobbyAvailability result = handler.onDone();
                    while (sessionNameAvailabilityCheck.Count > 0) {
                        Action<bool> c = sessionNameAvailabilityCheck.Dequeue();
                        c(result.isAvailable);
                    }
                    break;
                }
            } while (true);
        }

        public static SessionHandle New(string playerName, bool blah) { // the over load is to use the same name before deleting the old impl
            SessionHandle sh = new GameObject("session_handle").AddComponent<SessionHandle>();
            sh.Owner = playerName;
            return sh;
        }

        public static SessionHandle New(string playerName) {
            SessionHandle sh = new GameObject("session_handle").AddComponent<SessionHandle>();
            sh.OwningPlayerName = playerName;
            return sh;
        }

        private void OnDestroy() {
            Debug.LogFormat("session handle destroyed");
        }
    }
}
