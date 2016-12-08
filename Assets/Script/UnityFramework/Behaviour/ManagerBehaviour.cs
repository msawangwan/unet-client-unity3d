using UnityEngine;
using System.Collections;

namespace UnityFramework {
    public abstract class ManagerBehaviour : MonoBehaviour {
        private System.Func<bool> onInitialisationCompleted;
        private bool isInitialised = false;

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
