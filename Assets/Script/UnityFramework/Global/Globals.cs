using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI.Global {
    public class Globals : SingletonBehaviour<Globals> {
        public ControllerBehaviour homeMenuManager = null;

        private IEnumerator Start() {
            do {
                yield return null;
                if (homeMenuManager.onInitComplete) {
                    Debug.LogFormat("{0} all controller loaded", gameObject.name);
                    break;
                }
            } while (true);
        }
    }
}
