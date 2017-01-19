﻿using UnityEngine;
using UnityAdt;
using UnityEngine.SceneManagement;

namespace UnityLib {
    public class WorldHandle : MonoBehaviour {
        public static WorldHandle WorldHandleInstance = null;
        public static Scene WorldSceneInstance = default(Scene);

        public World WorldInstance {
            get;
            set;
        }

        public GameHandle.WorldParameters WorldParameters {
            get;
            private set;
        }

        public pRNG PRNG {
            get;
            private set;
        }

        public static WorldHandle New(GameHandle.WorldParameters worldparameters) {
            if (WorldHandleInstance != null) { // destroy the old world and create a new one, TODO: also delete any old scenes
                Destroy(WorldHandleInstance);
            }

            WorldHandle wh = new GameObject("world_handler").AddComponent<WorldHandle>();

            WorldHandleInstance = wh;

            wh.WorldParameters = worldparameters;
            wh.PRNG = new pRNG((ulong)worldparameters.worldSeed);

            WorldSceneInstance = SceneManager.CreateScene(Globals.scenename_worldhandle);
            SceneManager.MoveGameObjectToScene(WorldHandleInstance.gameObject, WorldSceneInstance);

            return wh;
        }
    }
}
