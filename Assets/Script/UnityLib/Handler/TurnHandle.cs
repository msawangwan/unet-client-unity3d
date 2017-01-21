using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class TurnHandle : MonoBehaviour {
        public Player PlayerInstance;

        public static TurnHandle New() {
            TurnHandle th = new GameObject("turn_handle").AddComponent<TurnHandle>();
            return th;
        }
    }
}
