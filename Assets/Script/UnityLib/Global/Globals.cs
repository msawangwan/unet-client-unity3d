using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class Globals : SingletonBehaviour<Globals> {
        public ControllerBehaviour titleMenuController;
        public ControllerBehaviour serviceController;
        public ControllerBehaviour GameStateController;

        private ControllerBehaviour[] allControllers;

        private IEnumerator Start() {
            allControllers = new ControllerBehaviour[] {
                titleMenuController,
                serviceController,
                GameStateController,
            };

            bool isInitComplete = false;

            do {
                yield return null;

                foreach (var controller in allControllers) {
                    if (controller.onInitComplete) {
                        isInitComplete = true;
                    } else {
                        isInitComplete = false;
                    }
                }

                if (isInitComplete) {
                    Debug.LogFormat("{0} all controller loaded", gameObject.name);
                    break;
                }
            } while (true);
        }
    }
}
