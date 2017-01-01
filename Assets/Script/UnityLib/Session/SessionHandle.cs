using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Framework.Net;

namespace UnityLib {
    public class SessionHandle : MonoBehaviour {
        public Queue<Action<string[]>> executeOnListFetched = new Queue<Action<string[]>>();
        public Queue<Action<bool>> executeOnNameAvailabilityCheck = new Queue<Action<bool>>();

        public bool SessionNameOK { get; set; }

        public IEnumerator Join(string gamename) {
            yield return null;
        }

        public IEnumerator FetchSessionList() {
            Handler<Lobby> handler = new Handler<Lobby>("poop");
            handler.SendGetRequest("GET", ServiceController.DebugAddr_Session_Active_List);

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
            handler.SendJsonRequest("POST", ServiceController.DebugAddr_Session_Available);

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
