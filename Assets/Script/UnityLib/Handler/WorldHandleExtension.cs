using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityLib {
    public static class WorldHandleExtension  {
        public static IEnumerator LoadWorldScene(this WorldHandle wh, GameHandle gh, Action onComplete) {
            do {
                Debug.LogFormat("-- -- [+] loading world ... {0}", Time.time);
                yield return null;
                if (WorldHandle.WorldSceneInstance.isLoaded) {
                    Debug.LogFormat("-- -- -- [+] world loaded {0}", Time.time);
                    break;
                }
            } while (true);

            Quadrant worldRoot = Quadrant.InstantiateQuadrantRootGameObject(Vector3.zero, wh.WorldParameters.worldScale, wh.WorldParameters.nodeRadius);
            List<GameObject> goNodes = Quadrant.InstantiateSubQuadrantGameObjects(worldRoot, wh.WorldParameters.nodeCount);

            wh.WorldInstance = new World(worldRoot, goNodes);
            Quadrant.Partition(worldRoot, goNodes, wh.PRNG, wh.WorldParameters.nodeMaxSpawnAttempts);
            SceneManager.MoveGameObjectToScene(worldRoot.gameObject.transform.parent.gameObject, WorldHandle.WorldSceneInstance);

            if (onComplete != null) {
                onComplete();
            }

            foreach (GameObject go in goNodes) {
                Star s = go.AddComponent<Star>();
                s.Construct(gh.GameHUDDetailsCtrl);
                s.AttachListener(() => { gh.Notified(() => s); });
                // s.AttachListener(() => { gh.PopupCtrl.PopupView.Load(() => s); });
                s.AttachListener(() => { gh.GameHUDDetailsCtrl.ShowNodeDetails(() => s); });

                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>(Star.asset_filepath_sprite);

                wh.WorldInstance.Stars.Add(s.AsRedisKey, s);
                Debug.LogWarningFormat("node [{0}]", s.AsRedisKey);
            }

            Debug.LogFormat("[+] finished spawning world node objects... {0}", Time.time);
        }
    }
}
