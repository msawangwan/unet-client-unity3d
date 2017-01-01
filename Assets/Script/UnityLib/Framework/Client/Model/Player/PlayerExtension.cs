using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.Framework.Client {
    public static class PlayerExtension {
        public static void Init(this Player p, int amount=500) {
            p.Credits = amount;
        }
        public static void SetHQ(this Player p, GameObject hq) {
            p.HQ = hq;
        }
    }
}
