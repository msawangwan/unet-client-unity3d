using UnityEngine;

namespace UnityLib {
    public static class Wait {
        private static readonly WaitForEndOfFrame forEndOfFrame = new WaitForEndOfFrame();
        private static readonly WaitForFixedUpdate forFixedUpdate = new WaitForFixedUpdate();

        public static WaitForEndOfFrame ForEndOfFrame {
            get {
                return forEndOfFrame;
            }
        }

        public static WaitForFixedUpdate ForFixedUpdate {
            get {
                return forFixedUpdate;
            }
        }
    }
}
