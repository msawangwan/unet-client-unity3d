using UnityEngine;

namespace UnityFramework {
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
        [SerializeField] private bool dontDestroyOnLoad = false;
        [SerializeField] private bool isSafeToOverwrite = false;

        private static SingletonBehaviour<T> s = null;

        public static T S {
            get {
                return s as T;
            }
        }

        protected virtual void Awake() {
            if (s && s != this) {
                if (isSafeToOverwrite) {
                    s = this;
                } else {
                    DestroyImmediate(gameObject);  // destroy cause we're a duplicate
                }
            } else {
                s = this;
                if (dontDestroyOnLoad) {
                    s.transform.parent = null;     // move to root of the hiearchy
                    DontDestroyOnLoad(gameObject); // persist on scene change
                }
            }
        }
    }
}
