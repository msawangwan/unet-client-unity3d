using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Framework.Net;
using UnityLib.Framework.Client;

namespace UnityLib {
    public class SessionHandle : MonoBehaviour {
        public Queue<Action<string[]>> executeOnListFetched = new Queue<Action<string[]>>();
        public Queue<Action<bool>> executeOnNameAvailabilityCheck = new Queue<Action<bool>>();

        public Queue<Action> StageOne = new Queue<Action>();
        public Queue<Action> StageOneError = new Queue<Action>();

        public Instance SessionInstance { get; private set; }

        public IEnumerator Create(string gamename) {
            Handler<Instance> createSession = new Handler<Instance>(
                JsonUtility.ToJson(new Key(gamename))
            );

            createSession.POST(RouteHandle.Session_CreateNew);

            do {
                yield return null;
                if (createSession.onDone != null) {
                    SessionInstance = createSession.onDone();
                    break;
                }
            } while (true);

            SessionInstance.playerCount++;
            Handler<Confirmation> confirmGameActive = new Handler<Confirmation>(
                JsonUtility.ToJson(SessionInstance)
            );

            confirmGameActive.POST(RouteHandle.Session_MakeActive);

            do {
                yield return null;
                if (confirmGameActive.onDone != null) {
                    confirmGameActive.onDone();
                    break;
                }
            } while (true);

            StartCoroutine(this.BeginSession());

            Debug.LogFormat("created a session: {0} {1}", SessionInstance.sessionID, SessionInstance.seed);
        }

        public IEnumerator Join(string gamename) {
            Handler<Instance> handler = new Handler<Instance>(
                JsonUtility.ToJson(new Key(gamename))
            );

            handler.POST(RouteHandle.Session_JoinNew);

            do {
                yield return null;
                if (handler.onDone != null) {
                    SessionInstance = handler.onDone();
                    break;
                }
            } while (true);

            StartCoroutine(this.BeginSession());

            Debug.LogFormat("joined a session: {0} {1}", SessionInstance.sessionID, SessionInstance.seed);
        }

        public IEnumerator FetchSessionList() {
            Handler<Lobby> handler = new Handler<Lobby>("poop");
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
                    while (executeOnNameAvailabilityCheck.Count > 0) {
                        Action<bool> c = executeOnNameAvailabilityCheck.Dequeue();
                        c(result.isAvailable);
                    }
                    break;
                }
            } while (true);
        }

        public static SessionHandle New() {
            return new GameObject("session_handle").AddComponent<SessionHandle>();
        }
    }
}
