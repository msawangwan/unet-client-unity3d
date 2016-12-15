using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI.Global {
    public class Globals : SingletonBehaviour<Globals> {
        public ControllerBehaviour titleMenuController;
        public ControllerBehaviour serviceController;
        public ControllerBehaviour GameStateController;

        private IEnumerator Start() {
            do {
                yield return null;
                if (titleMenuController.onInitComplete && serviceController.onInitComplete) {
                    Debug.LogFormat("{0} all controller loaded", gameObject.name);
                    break;
                }
            } while (true);
        }
    }
}
