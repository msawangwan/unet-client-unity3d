using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class PollHandle : MonoBehaviour {
        public static readonly Resource PollForGameStart = new Resource("poll/start");
        public static readonly Resource PollForGameUpdate = new Resource("poll/update");

        public static float PollInterval {
            get {
                return 0.75f;
            }
        }

        public static PollHandle New() {
            PollHandle ph = new GameObject("poll_handle").AddComponent<PollHandle>();
            return ph;
        }

        private void OnEnable() {
            Debug.LogWarningFormat("[+] {0} callback: OnEnable ... [{1}]", gameObject.name, Time.time);
        }

        private void OnDisable() {
            Debug.LogWarningFormat("[+] {0} callback: OnDisable ... [{1}]", gameObject.name, Time.time);
        }

        private void OnDestroy() {
            Debug.LogWarningFormat("[+] {0} callback: OnDestroy ... [{1}]", gameObject.name, Time.time);
        }
        
    }
}
