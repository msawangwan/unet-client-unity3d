﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAdt;
using UnityLib.Net;

namespace UnityLib {
    public static class WorldHandleExtension  {
        public static IEnumerator LoadWorldScene(this WorldHandle wh, Action onComplete) {
            Debug.LogFormat("[+] loading world scene ... {0}", Time.time);
            // WorldHandle.WorldSceneInstance = SceneManager.GetSceneAt(Globals.sceneindex_worldhandler);

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

            Quadrant.Partition(worldRoot, goNodes, wh.PRNG, wh.WorldParameters.nodeMaxSpawnAttempts);

            SceneManager.MoveGameObjectToScene(worldRoot.gameObject.transform.parent.gameObject, WorldHandle.WorldSceneInstance);

            if (onComplete != null) {
                onComplete();
            }

            foreach (var item in goNodes) {
                Debug.LogWarningFormat("node [position: {0} {1}]", item.transform.position.x, item.transform.position.y);
            }

            Debug.LogFormat("[+] finished spawning world node objects... {0}", Time.time);
        }
    }
}
