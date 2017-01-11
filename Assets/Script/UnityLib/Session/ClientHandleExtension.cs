using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;
using System;

namespace UnityLib {
    public static class ClientHandleExtension {
        public static IEnumerator Register(this ClientHandle ch, Action onSuccess) {
            JsonInt id = null;

            Debug.LogFormat("-- [+] client [name: {0}]", ch.ClientName);

            Handler<JsonInt> registerHandler = new Handler<JsonInt>(
                JsonUtility.ToJson(new JsonString(ch.ClientName))
            );

            registerHandler.POST(RouteHandle.Client_HandleRegistration);

            do {
                yield return null;
                Debug.LogFormat("-- -- [+] requesting client handle registration [name: {0}] ... [{1}]", ch.ClientName, Time.time);
                if (registerHandler.hasLoadedResource) {
                    id = registerHandler.onDone();
                    break;
                }
            } while (true);

            ch.ClientSessionID = id.value;

            if (onSuccess != null) {
                onSuccess();
            }

            Debug.LogFormat("-- [+] registered client [name: {0}] [id: {1}]", ch.ClientName, ch.ClientSessionID);
        }
    }
}
