using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityLib {
    public static class LobbyHandleExtension {
        public static IEnumerator FetchGameList(this LobbyHandle lh) {
            do {
                yield return null;
            } while (true);
        }
    }
}
