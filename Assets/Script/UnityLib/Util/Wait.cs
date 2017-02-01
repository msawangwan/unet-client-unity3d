using UnityEngine;
using System.Collections.Generic;

namespace UnityLib {
    public static class Wait {
        private const float defaultIntervalInSeconds = 1.0f;

        private static readonly WaitForEndOfFrame forEndOfFrame = new WaitForEndOfFrame();
        private static readonly WaitForFixedUpdate forFixedUpdate = new WaitForFixedUpdate();

        private static Dictionary<float, WaitForSeconds> intervalTable = new Dictionary<float, WaitForSeconds>();

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

        public static WaitForSeconds ForSeconds(float t = defaultIntervalInSeconds) {
            if (!intervalTable.ContainsKey(t)) {
                intervalTable.Add(t, new WaitForSeconds(t));
            }
            return intervalTable[t];
        }
    }
}
