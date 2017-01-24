using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class TurnHandleExtension {
        public static IEnumerator SendEndCurrentTurn(this TurnHandle th) {
            while (th.handlers.Count > 0) {
                Func<Handler<JsonEmpty>> current = th.handlers.Dequeue();
                Handler<JsonEmpty> handler = current();
                yield return new WaitUntil( // block until request done
                    ()=> {
                        if (handler.hasLoadedResource) {
                            return true;
                        }
                        return false;
                    }
                );
                handler.onDone();
            };
        }
    }
}
