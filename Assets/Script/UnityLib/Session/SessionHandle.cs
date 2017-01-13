using System.Collections;
using UnityLib.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityLib {
    public class SessionHandle : MonoBehaviour {
        public static readonly Resource VerifyName = new Resource("session/handle/name/verification");

        public GameHandle GameHandler {
            get;
            set;
        }

        public int SessionKey {
            get {
                return sessionKey;
            }
            set {
                sessionKey = value;
            }
        }
        
        public int GameID {
            get;
            set;
        }

        private int sessionKey;
        private int gameid;

        public void Init(int key) {
            this.SessionKey = key;
            StartCoroutine(Load(1));
        }

        public static SessionHandle New(int key) {
            SessionHandle sh = new GameObject(string.Format("session_handle_[{0}]", key)).AddComponent<SessionHandle>();
            sh.Init(key);
            return sh;
        }

        private IEnumerator Load(int sceneIndex) {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            Scene scene = SceneManager.GetSceneAt(sceneIndex);

            Debug.LogFormat("-- [*] session handle scene load [index: {1}] [name: {2}] ... [{0}]", Time.time, sceneIndex, scene.name);

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
