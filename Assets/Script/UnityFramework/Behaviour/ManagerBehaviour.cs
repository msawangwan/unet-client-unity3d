using UnityEngine;
using System;
using System.Collections;

namespace UnityFramework {
    public abstract class ManagerBehaviour : MonoBehaviour {
        private bool isInitialised = false;
        private Func<bool> onInitialisationCompleted = null;

        protected Action onStart = null;

        public bool completedInitialisation {
            get {
                return isInitialised;
            }
        }

        protected abstract bool HandleInitialisation();

        private IEnumerator Start() {
            onInitialisationCompleted += HandleInitialisation;

            do {
                yield return null;

                if (onInitialisationCompleted != null) {
                    if (onInitialisationCompleted()) {
                        isInitialised = true;
                        break;
                    }
                }
            } while (true);

            onInitialisationCompleted -= HandleInitialisation;
        }
    }
}
