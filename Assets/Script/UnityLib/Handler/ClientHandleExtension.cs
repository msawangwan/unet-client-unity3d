using UnityEngine;
using UnityLib.Net;
using System;
using System.Collections;

namespace UnityLib {
    public static class ClientHandleExtension {
        public static IEnumerator Register(this ClientHandle ch, Action onSuccess) {
            Debug.LogFormat("-- [+] new client, register [name: {0}]", ch.ClientName);
            Debug.LogFormat("{0}", ClientHandle.Registration.ToString());

            Handler<JsonInt> registerHandler = new Handler<JsonInt>(ch.json.ClientName);
            JsonInt id = null;

            registerHandler.POST(ClientHandle.Registration.Route);

            do {
                yield return null;
                Debug.LogFormat("-- -- [+] requesting client handle registration [name: {0}] ... [{1}]", ch.ClientName, Time.time);
                if (registerHandler.hasLoadedResource) {
                    id = registerHandler.onDone();
                    break;
                }
            } while (true);

            ch.HandleID = id.value;

            if (onSuccess != null) {
                onSuccess();
            }

            Debug.LogFormat("-- [+] registered client [name: {0}] [id: {1}]", ch.ClientName, ch.HandleID);
        }

        public static IEnumerator RequestHostKey(this ClientHandle ch, Action onSuccess) {
            Debug.LogFormat("-- [+] {0} is requesting a host key ... [{1}]", ch.name, Time.time);
            Debug.LogFormat("{0}", ClientHandle.RequestHostingKey.ToString());

            Handler<JsonInt> keyHandler = new Handler<JsonInt>(ch.json.HandleID);
            JsonInt id = null;

            keyHandler.POST(ClientHandle.RequestHostingKey.Route);

            do {
                yield return null;
                Debug.LogFormat("-- -- [+] fetching host key ... [{0}]", Time.time);
                if (keyHandler.hasLoadedResource) {
                    id = keyHandler.onDone();
                    break;
                }
            } while (true);

            if (id.value == -1) {
                Debug.LogFormat("-- -- [+] client already has a session key [client handle id: {0}] [session key: {1}]", ch.HandleID, ch.SessionKey, Time.time);
            } else {
                ch.SessionKey = id.value;
                ch.Role = ClientHandle.RoleType.Host;
                Debug.LogFormat("-- [+] got host key [client handle id: {0}] [session key: {1}]", ch.HandleID, ch.SessionKey, Time.time);
            }

            if (onSuccess != null) {
                onSuccess();
            }

        }

        public static IEnumerator RequestJoinKey(this ClientHandle ch, Action<string[]> onSuccess) {
            yield return null;
        }
    }

}
