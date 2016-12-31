using System.Collections;
using UnityEngine;

namespace UnityLib {
    public class Globals : SingletonBehaviour<Globals> {
        public ControllerBehaviour titleMenuController;
        public ControllerBehaviour popupMenuController;

        public ControllerBehaviour serviceController;

        public ControllerBehaviour menuLoopController;
        public ControllerBehaviour gameLoopController;

        private ControllerBehaviour[] allControllers;

        private IEnumerator Start() {
            allControllers = new ControllerBehaviour[] {
                titleMenuController,
                popupMenuController,
                serviceController,
                menuLoopController,
                gameLoopController,
        };

            bool isInitComplete = false;

            do {
                yield return null;

                foreach (var controller in allControllers) {
                    if (controller == null) {
                        continue;
                    }
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
