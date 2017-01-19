using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public static class GameHUDControllerExtension {
        public static IEnumerator BeginWaitAndPollGameStart(this GameHUDController ghudc, Action onComplete) {
            yield return new WaitForEndOfFrame();

            Globals.S.AppState = Globals.ApplicationState.Game;
            CameraRigController.S.EnableMovement();

            if (onComplete != null) {
                onComplete();
            }
        }
    }
}
