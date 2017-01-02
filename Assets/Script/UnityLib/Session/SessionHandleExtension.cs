using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityLib {
    public static class SessionHandleExtension {
        private const int kSceneGameLoop = 1;

        public static IEnumerator BeginSession(this SessionHandle sh) {
            Scene scene = SceneManager.GetSceneAt(kSceneGameLoop);

            do {
                Debug.Log("loading session ...");
                yield return new WaitForEndOfFrame();
            } while (!scene.isLoaded);

            GameHandle gh = GameHandle.New(sh);

            SceneManager.MoveGameObjectToScene(sh.gameObject, SceneManager.GetSceneAt(kSceneGameLoop));
            SceneManager.MoveGameObjectToScene(gh.gameObject, SceneManager.GetSceneAt(kSceneGameLoop));
        }
    }
}
