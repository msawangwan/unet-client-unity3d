﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;
using UnityAdt;
using UnityEngine.SceneManagement;

namespace UnityLib {
    public class WorldHandle : MonoBehaviour {
        public static WorldHandle WorldHandleInstance = null;
        public static Scene WorldSceneInstance = default(Scene);

        public pRNG PRNG {
            get;
            private set;
        }

        public GameHandle.WorldParameters WorldParameters {
            get;
            private set;
        }

        public static WorldHandle New(GameHandle.WorldParameters worldparameters) {
            if (WorldHandleInstance != null) { // destroy the old world and create a new one, TODO: also delete any old scenes
                Destroy(WorldHandleInstance);
            }

            // SceneManager.LoadSceneAsync(Globals.sceneindex_worldhandler, LoadSceneMode.Additive);

            WorldHandle wh = new GameObject("world_handler").AddComponent<WorldHandle>();

            WorldSceneInstance = SceneManager.CreateScene(Globals.scenename_worldhandle);
            WorldHandleInstance = wh;

            wh.WorldParameters = worldparameters;
            wh.PRNG = new pRNG((ulong)worldparameters.worldSeed);

            return wh;
        }
    }
}