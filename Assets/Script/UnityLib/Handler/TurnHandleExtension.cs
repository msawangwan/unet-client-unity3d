using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public static class TurnHandleExtension {
        public static IEnumerator SpoolUp(this TurnHandle th) {
            Debug.LogWarningFormat("[+] turn handler spooling up ... [{0}]", Time.time);

            do {
                yield return Wait.ForEndOfFrame;
            } while (true);

            Debug.LogWarningFormat("[+] turn handler spooling up ... [{0}]", Time.time);
        }

        public static IEnumerator ExecuteTurnTick(this TurnHandle th, int turnnumber) {
            do {
                yield return Wait.ForEndOfFrame;
            } while (true);
        }
    }
}
