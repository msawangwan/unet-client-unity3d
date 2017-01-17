using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class WorldHandle : MonoBehaviour {
        public static WorldHandle WorldHandleInstance = null;
        
        public static WorldHandle New() {
            if (WorldHandleInstance != null) { // destroy the old world and create a new one
                Destroy(WorldHandleInstance);
            }

            WorldHandle wh = new GameObject("world_handler").AddComponent<WorldHandle>();
            WorldHandleInstance = wh;

            return wh;
        }
    }
}
