using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class PlayerHandle : MonoBehaviour {
        public static PlayerHandle New(string name) {
            return new GameObject(name).AddComponent<PlayerHandle>();
        }
    }
}
