using UnityEngine;
using System;
using System.Collections;

namespace UnityAPI {
    public abstract class ControllerBehaviour : MonoBehaviour {
        private bool isInitialised = false;
        public Func<bool> onInitCompleted = null;

        protected Action onStart = null;

        public bool onInitComplete {
            get {
                return isInitialised;
            }
        }

        protected abstract bool OnInit();

        protected virtual IEnumerator Start() {
            onInitCompleted += OnInit;

            do {
                yield return null;

                if (onInitCompleted != null) {
                    if (onInitCompleted()) {
                        isInitialised = true;
                        Debug.LogFormat("{0} init complete: {1}", gameObject.name, Time.time);
                        break;
                    }
                }
            } while (true);

            onInitCompleted -= OnInit;
        }
    }
}
