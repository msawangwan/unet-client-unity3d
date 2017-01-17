using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class WorldHandleExtension  {
        public static IEnumerator LoadWorldScene(this WorldHandle wh, Action onComplete) {
            do {
                yield return null;
                if (true) {
                    break;
                }
            } while (true);

            if (onComplete != null) {
                onComplete();
            }
        }
    }
}
