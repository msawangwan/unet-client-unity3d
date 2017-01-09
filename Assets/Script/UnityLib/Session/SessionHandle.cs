using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class SessionHandle : MonoBehaviour {
        // instance variables
        public int SessionKey { get; set; }

        public string SessionPlayerName { get; set; }
        public string SessionLabelTentative { get; set; }
        
        public Simulation SimulationInstance { get; set; }
        public SimulationHandle SimHandle { get; set; }

        // deprecated variables
        public Instance SessionInstance { get; private set; } // DEPRECATE
        public Key SKey { get; private set; }// DEPRECATE
        public string OwningPlayerName { get; private set; } // DEPRECATE


        // DEPRECATE
        public IEnumerator Join(string gamename) {
            Handler<Instance> joinHandler = new Handler<Instance>(
                JsonUtility.ToJson(new Key(gamename))
            );

            // joinHandler.POST(RouteHandle.Session_JoinNew);

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

            // keyHandler.POST(RouteHandle.Session_KeyFromInstance);

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
