using System.Collections;
using UnityEngine;

namespace UnityLib {
    public class Globals : SingletonBehaviour<Globals> {
        public static readonly int sceneindex_clienthandler  = 0;
        public static readonly int sceneindex_sessionhandler = 1;
        public static readonly int sceneindex_lobbyhandler   = 2;
        public static readonly int sceneindex_gamehandler    = 3;
        public static readonly int sceneindex_worldhandler   = 4;

        public static readonly string scenename_lobbyhandle = "lobby_handle";
        public static readonly string scenename_gamehandle = "game_handle";
        public static readonly string scenename_worldhandle = "world_handle";

        public ControllerBehaviour titleMenuController;
        public ControllerBehaviour popupMenuController;

        public ControllerBehaviour RouteHandle;

        public ControllerBehaviour menuLoopController;
        public ControllerBehaviour gameLoopController;

        private ControllerBehaviour[] allControllers;

        private IEnumerator Start() {
            allControllers = new ControllerBehaviour[] {
                titleMenuController,
                popupMenuController,
                RouteHandle,
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
                    Debug.LogFormat("[+][INIT] {0} all controller loaded", gameObject.name);
                    break;
                }
            } while (true);
        }
    }
}
