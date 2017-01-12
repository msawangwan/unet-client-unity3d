using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityLib {
    public class SessionHandle : MonoBehaviour {
        public static readonly Resource VerifyName = new Resource("session/handle/name/verification");

        public int SessionKey {
            get {
                return sessionKey;
            }
            set {
                sessionKey = value;
            }
        }
        
        private int sessionKey;
        
        public void Init(int key) {
            this.SessionKey = key;
            StartCoroutine(Load(1));
        }

        public static SessionHandle New(int key) {
            SessionHandle sh = new GameObject("session_handle").AddComponent<SessionHandle>();
            sh.Init(key);
            return sh;
        }

        private IEnumerator Load(int sceneIndex) {
            Scene scene = SceneManager.GetSceneAt(sceneIndex);
            Debug.LogFormat("-- [*] session handle scene load [index: {1}] [name: {2}] ... [{0}]", Time.time, sceneIndex, scene.name);
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            do {
                yield return new WaitForEndOfFrame();
                Debug.LogFormat("-- -- [*] loading ... [{0}]", Time.time);
            } while (!scene.isLoaded);

            Debug.LogFormat("-- [*] scene finished loading [{0}]", Time.time);

            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneAt(sceneIndex));
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
