using System.Collections;
using UnityEngine;

namespace UnityLib.Framework.Client {
    public class Player : MonoBehaviour {
        public int Credits;
        public GameObject HQ;
        public bool isActive = false;
        public bool hasHQ = false;
        public bool hasAction = false;

        private int tick = -1;

        public static Player New() {
            return new GameObject("player").AddComponent<Player>();
        }

        private IEnumerator Turn() {
            do {
                yield return null;
                if (tick == -1) {
                    break;
                }
            } while (true);
        }
    }
}
